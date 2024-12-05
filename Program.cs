using GestaoResiduosApi.Data.Repository;
using GestaoResiduosApi.Data;
using GestaoResiduosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text.Json.Serialization;
using System.Text;
using GestaoResiduosApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

var builder = WebApplication.CreateBuilder(args);

#region Configuração de Autenticação e Autorização
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"])),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"]
    };
});

builder.Services.AddAuthorization(options =>
{
    // Política para Admin e User (GET)
    options.AddPolicy("UserOrAdminPolicy", policy =>
    {
        policy.RequireRole("ADMIN", "USER");
    });

    // Política para Admin apenas (POST, PUT, DELETE)
    options.AddPolicy("AdminPolicy", policy =>
    {
        policy.RequireRole("ADMIN");
    });
});
#endregion

#region Configuração de Controladores e JSON
builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        options.InvalidModelStateResponseFactory = context =>
        {
            return new BadRequestObjectResult(context.ModelState);
        };
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });
#endregion

#region Configuração de Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#endregion

#region Configuração do Banco de Dados
var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

builder.Services.AddDbContext<DatabaseContext>(opt =>
    opt.UseOracle(connectionString)
       .EnableSensitiveDataLogging(true)); // Desative em produção
#endregion

#region Configuração de Repositórios e Serviços
builder.Services.AddScoped<IRecipienteRepository, RecipienteRepository>();
builder.Services.AddScoped<IRecipienteService, RecipienteService>();
builder.Services.AddScoped<ICaminhaoRepository, CaminhaoRepository>();
builder.Services.AddScoped<ICaminhaoService, CaminhaoService>();
builder.Services.AddScoped<IRotaRepository, RotaRepository>();
builder.Services.AddScoped<IRotaService, RotaService>();
builder.Services.AddScoped<IEmergenciaRepository, EmergenciaRepository>();
builder.Services.AddScoped<IEmergenciaService, EmergenciaService>();
builder.Services.AddScoped<IAgendamentoRepository, AgendamentoRepository>();
builder.Services.AddScoped<IAgendamentoService, AgendamentoService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPasswordHasher<UsuarioModel>, PasswordHasher<UsuarioModel>>();


#endregion

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


var app = builder.Build();

// Middleware
app.UseAuthentication();
app.UseAuthorization();

// Configuração de rotas e políticas
app.MapPost("/auth/register", [AllowAnonymous] () => Results.Ok("Register endpoint"));
app.MapPost("/auth/login", [AllowAnonymous] () => Results.Ok("Login endpoint"));

app.MapGet("/some-get-endpoint", [Authorize(Policy = "UserOrAdminPolicy")] () => Results.Ok("Get endpoint for Admin or User"));

app.MapPost("/some-post-endpoint", [Authorize(Policy = "AdminPolicy")] () => Results.Ok("Post endpoint for Admin only"));
app.MapPut("/some-put-endpoint", [Authorize(Policy = "AdminPolicy")] () => Results.Ok("Put endpoint for Admin only"));
app.MapDelete("/some-delete-endpoint", [Authorize(Policy = "AdminPolicy")] () => Results.Ok("Delete endpoint for Admin only"));


#region Configuração do Pipeline de Middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
