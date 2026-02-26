using ECommerce.Application.Common.Behaviors;
using ECommerce.Application.Feature.User.Command.CreateUser;
using ECommerce.Application.Interfaces;
using ECommerce.Infrastructure;
using ECommerce.Infrastructure.Persistance.ApiExtern;
using ECommerce.Infrastructure.Persistance.Emails;
using ECommerce.Infrastructure.Persistance.Repositories;
using ECommerce.Middleware;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuestPDF.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.Filters.Add<ValidationExceptionFilter>();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IUserService, UserRepository>();
builder.Services.AddScoped<ICartRepository, CartRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();
builder.Services.AddScoped<IPaymentGateway, StripePaymentGateway>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInvoiceSender, SmtpInvoiceSender>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.Configure<ApiBehaviorOptions>(options =>{options.SuppressModelStateInvalidFilter = true;});
builder.Services.AddMediatR(x => x.RegisterServicesFromAssembly(typeof(CreateUserCommand).Assembly));
builder.Services.AddDbContext<AppDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserValidator>();
builder.Services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
    {
        policy.WithOrigins("http://localhost:4200") // Origen de Angular
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

QuestPDF.Settings.License = LicenseType.Community;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAngular"); // <- habilitar CORS

app.UseMiddleware<MiddlewareExceptions>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
