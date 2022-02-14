using FinancialChat.Application;
using FinancialChat.Application.Features.Bot.Queries.GetBotStockQuote;
using FinancialChat.Infrastructure;
using MediatR;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfraestructureServices(builder.Configuration);
builder.Services.AddAplicationServices();
//builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
//builder.Services.AddMediatR(typeof(GetBotStockQuoteQueryHandler));


builder.Services.AddCors(options => {
    options.AddPolicy("CorsPolicy", builder => builder
                                            //.AllowAnyOrigin()
                                            .AllowCredentials()
                                            .AllowAnyMethod()
                                            .AllowAnyHeader()
                                            .WithOrigins("http://localhost:4200")
                                            );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();
