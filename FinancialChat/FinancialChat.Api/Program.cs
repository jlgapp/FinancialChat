using FinancialChat.Api.Middleware;
using FinancialChat.Application;
using FinancialChat.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAplicationServices();
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => builder
                                            //.AllowAnyOrigin()
                                            .AllowCredentials()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .WithOrigins("http://localhost:4200")
                                            );
});

builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ExceptionMiddleware>();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapHub<MessageHub>("/MessageHub");

app.MapControllers();



app.Run();
