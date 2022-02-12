using FinancialChat.Application.Models.Identity;

namespace FinancialChat.Application.Contracts.Identity
{
    public interface IAuthServices
    {
        Task<AuthResponse> Login(AuthRequest authRequest);
        Task<RegistrationResponse> Register(RegistrationRequest registrationRequest);
    }
}
