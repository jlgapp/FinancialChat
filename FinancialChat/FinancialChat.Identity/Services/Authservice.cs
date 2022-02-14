using FinancialChat.Application.Constants;
using FinancialChat.Application.Contracts.Identity;
using FinancialChat.Application.Models.Identity;
using FinancialChat.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FinancialChat.Identity.Services
{
    public class Authservice : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtSettings _jwtSettings;

        public Authservice(UserManager<ApplicationUser> userManager,
                            SignInManager<ApplicationUser> signInManager,
                            IOptions<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<AuthResponse> Login(AuthRequest authRequest)
        {
            var user = await _userManager.FindByEmailAsync(authRequest.Email);
            if (user == null) throw new Exception($"The user with email {authRequest.Email} doesn't exists");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, authRequest.Password, false, lockoutOnFailure: false);

            if (!result.Succeeded)
                throw new Exception($"User or password are incorrect");

            var token = await GenerateToken(user);
            var authResponse = new AuthResponse
            {
                Email = user.Email,
                Id = user.Id,
                UserName = user.UserName,
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };

            return authResponse;
        }

        public async Task<RegistrationResponse> Register(RegistrationRequest registrationRequest)
        {
            var existingUser = await _userManager.FindByNameAsync(registrationRequest.UserName);
            if (existingUser != null) throw new Exception($"User already exists {registrationRequest.UserName}");

            var existingEmail = await _userManager.FindByEmailAsync(registrationRequest.Email);
            if (existingEmail != null) throw new Exception($" Email already exists {registrationRequest.Email}");

            var user = new ApplicationUser
            {
                Email = registrationRequest.Email,
                FirstName = registrationRequest.FirstName,
                LastName = registrationRequest.LastName,
                UserName = registrationRequest.UserName.ToUpper(),
                EmailConfirmed = true,
                
            };

            var result = await _userManager.CreateAsync(user, registrationRequest.Password);
            if (result.Succeeded)
            {
                //await _userManager.AddToRoleAsync(user, "Operator");
                var token = await GenerateToken(user);
                return new RegistrationResponse
                {
                    Email = user.Email,
                    UserName = user.UserName,
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    UserID = user.Id
                };
            }
            throw new Exception($"Error trying add user --> {result.Errors}");
        }

        private async Task<JwtSecurityToken> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(CustomClaimTypes.Uid, user.Id)

            }.Union(userClaims).Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
            var result = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: signingCredentials);

            return result;
        }
    }
}
