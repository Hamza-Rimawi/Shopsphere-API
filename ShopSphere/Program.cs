using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using ShopSphere.Business;
using ShopSphere.Business.Database;
using ShopSphere.Business.Helpers;
using ShopSphere.Business.Repositories;
using ShopSphere.Business.Services;
using ShopSphere.Data.Repositories;
using ShopSphere.Authorization;
using System.Text;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp", policy =>
    {
        policy.WithOrigins("https://your-react-app.onrender.com",
            "https://localhost:7099", "http://localhost:5227", "http://localhost:5173")      
              .AllowAnyHeader()     
              .AllowAnyMethod();     
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
   
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter: Bearer {your JWT token}"
    });

   
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {

        
        [new OpenApiSecuritySchemeReference("Bearer", document)] = new List<string>()

    });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserOwnerOrAdmin", policy =>
        policy.Requirements.Add(new UserOwnerOrAdminRequirement()));
});
builder.Services.AddSingleton<IAuthorizationHandler, UserOwnerOrAdminHandler>();



builder.Services.AddAutoMapper(cfg =>
{
    // Optional: Add custom configuration here
    cfg.AllowNullCollections = true;
    cfg.AllowNullDestinationValues = true;
}, typeof(MappingProfile).Assembly);
builder.Services.AddSingleton<SqlDataAccess>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderItemsRepository,OrderItemsRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IShippingRepository, ShippingRepository>();
builder.Services.AddScoped<IWalletTransactionsRepository, WalletTransactionsRepository>();
builder.Services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();



builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();


builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();
builder.Services.AddScoped<IReviewService, ReviewService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IOrderItemsService, OrderItemsService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IShippingService, ShippingService>();
builder.Services.AddScoped<IWalletTransactionsService, WalletTransactionsService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
var connectionString = builder.Configuration.GetConnectionString("AzureConnection");
var jwtKey = builder.Configuration["Jwt:Key"];
var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));
builder.Services.AddSingleton(key);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // TokenValidationParameters define how incoming JWTs will be validated.
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // Ensures the token was issued by a trusted issuer.
            ValidateIssuer = true,


            // Ensures the token is intended for this API (audience check).
            ValidateAudience = true,


            // Ensures the token has not expired.
            ValidateLifetime = true,


            // Ensures the token signature is valid and was signed by the API.
            ValidateIssuerSigningKey = true,


            // The expected issuer value (must match the issuer used when creating the JWT).
            ValidIssuer = "ShopSphereApi",


            // The expected audience value (must match the audience used when creating the JWT).
            ValidAudience = "ShopSphereApiUsers",


            // The secret key used to validate the JWT signature.
            // This must be the same key used when generating the token.
            IssuerSigningKey = key

        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReactApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
