using Evento.Core.Repositories;
using Evento.Infrastructure.Mappers;
using Evento.Infrastructure.Repositories;
using Evento.Infrastructure.Services;
using Evento.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Evento.Infrastructure.Context;
using AutoMapper;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Sekcja dodana, aby testowaæ dzia³anie frontendu z backendem
// https://learn.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-7.0#attr
// TODO: Zweryfikowaæ czy to jest bezpieczne i ewentualnie to poprawiæ
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                      });
});

builder.Services.AddRazorPages();

builder.Services.AddDbContext<EventoContext>(options =>
  options
    .UseLazyLoadingProxies()
    .UseSqlite(builder.Configuration.GetConnectionString("EventoContext"), b => b.MigrationsAssembly("Evento.Api")));

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection(JwtSettings.SectionName));

JwtSettings jwtSettings = builder.Configuration.GetSection(JwtSettings.SectionName).Get<JwtSettings>();

// Add services to the container.
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = jwtSettings.Issuer,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
    };
});
builder.Services.AddAuthorization(x=>x.AddPolicy("HasAdminRole", p=>p.RequireRole("admin")));

builder.Services.AddControllers();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventService, EventService>();

var passwordHashPepper = builder.Configuration.GetSection("PasswordHash")["Pepper"];

builder.Services.AddScoped<IUserService>(x=>
    new UserService(
        x.GetRequiredService<IUserRepository>(),
        x.GetRequiredService<IJwtHandler>(),
        x.GetRequiredService<IMapper>(),
        passwordHashPepper
        ));
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddSingleton<IJwtHandler, JwtHandler>();
builder.Services.AddSingleton(AutoMapperConfig.Initailize()); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHostedService<PasswordTokenValidityService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<EventoContext>();
    context.Database.EnsureCreated();
    // DbInitializer.Initialize(context);
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }