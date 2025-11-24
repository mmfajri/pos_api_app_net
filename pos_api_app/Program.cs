// using FluentValidation;
using pos_api_app.Contracts.Repositories.Entities;
using pos_api_app.Contracts.Utilities;
using pos_api_app.Data;
using pos_api_app.Repository.Entities;
using pos_api_app.Services;
// using pos_api_app.Utilities.Handlers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
// using System.Reflection;
using System.Text;
using TokenHandler = pos_api_app.Utilities.Handlers.TokenHandler;
using pos_api_app.Utilities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PosDbContext>(option => option.UseNpgsql(GetConfig.AppSetting["ConnectionStrings:DefaultConnection"] ?? string.Empty));
builder.WebHost.UseUrls(GetConfig.AppSetting["URL_HOST"] ?? string.Empty);


//Add Repository
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<ITransactionRepository, TransactionRepository>();
builder.Services.AddScoped<ITransactionItemRepository, TransactionItemRepository>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IUnitRepository, UnitRepository>();

//Add Services
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<RoleService>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<TransactionService>();
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<UnitService>();

// Add services to the container.
builder.Services.AddControllers();

//Add Fluent Validation Setting

//Add Service for Token
builder.Services.AddScoped<ITokenHandler, TokenHandler>();

//Build Cors Service
builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.AllowAnyHeader();
		policy.AllowAnyOrigin();
		policy.AllowAnyMethod();
	});
});

//JWT Configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
	    options.RequireHttpsMetadata = false; // for development stage
	    options.SaveToken = true;
	    options.TokenValidationParameters = new TokenValidationParameters()
	    {
		    ValidateIssuer = true,
		    ValidIssuer = builder.Configuration["JWTService:Issuer"],
		    ValidateAudience = true,
		    ValidAudience = builder.Configuration["JWTService:Audience"],
		    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWTService:Key"] ?? string.Empty)),
		    ValidateLifetime = true,
		    ClockSkew = TimeSpan.Zero
	    };
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(x =>
{
	x.SwaggerDoc("v1", new OpenApiInfo
	{
		Version = "v1",
		Title = "Pos Minimarket",
		Description = "ASP.NET Core pos_api_app 6.0, Ver.1.0"
	});
	x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.Http,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "JWT Authorization header using the Bearer scheme."
	});
	x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
    {
    new OpenApiSecurityScheme
    {
    Reference = new OpenApiReference
    {
    Type = ReferenceType.SecurityScheme,
    Id = "Bearer"
    }
    },
    Array.Empty<string>()
    }
    });
});

var app = builder.Build();

// Auto Migrate
// using (var scope = app.Services.CreateScope())
// {
// 	var dbContext = scope.ServiceProvider.GetRequiredService<PosDbContext>();
// 	// await dbContext.Database.EnsureCreatedAsync();
// 	await dbContext.Database.MigrateAsync();
// }

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
