using EvaluacionAcademia.NET.DataAccess;
using EvaluacionAcademia.NET.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

namespace EvaluacionAcademia.NET
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			var AllowEspecificOrigins = "";

			// Add services to the container.

			builder.Services.AddCors(options =>
			{
				options.AddPolicy(name: AllowEspecificOrigins, policy =>
				{
					policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
				});
			});

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "TpIntegradorSofttek", Version = "v1" });

				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				//c.IncludeXmlComments(xmlPath);

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Description = "Autorizacion JWT",
					Type = SecuritySchemeType.Http,
					Scheme = "bearer"
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
					new OpenApiSecurityScheme
					{
						Reference = new OpenApiReference
						{
							Type= ReferenceType.SecurityScheme,
							Id = "Bearer"
						}
					}, new string[]{ }
					}
				});

			});

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
			{
				options.UseSqlServer("name=DefaultConnection");
			});

			builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
			{
				ValidateIssuerSigningKey = true,
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
				ValidateIssuer = false,
				ValidateAudience = false
			});

			builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthentication();

			app.UseAuthorization();

			app.UseAuthorization();

			app.UseCors(AllowEspecificOrigins);

			app.MapControllers();

			app.Run();
		}
	}
}