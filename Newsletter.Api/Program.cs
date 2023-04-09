using FluentValidation;
using FluentValidation.AspNetCore;
using Newsletter.Api.Infrastructure;
using Newsletter.Api.Models.Titel.Validation;
using Newsletter.Application.Titel;
using Newsletter.Infrastructure.Appconfig;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateTitelCommand>());
builder.Services.AddValidatorsFromAssemblyContaining<CreateTitelValidator>()
    .AddFluentValidationAutoValidation();

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddDbContext(configuration.GetConnectionString("Database"));
builder.Services.AddCustomServices();
builder.Services.AddClients();

builder.Services.Configure<MailSettings>(builder.Configuration.GetSection(nameof(MailSettings)));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
