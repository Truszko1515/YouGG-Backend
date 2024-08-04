using Business_Logic_Layer.Authentication;
using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Repository;
using Business_Logic_Layer.Services;
using Business_Logic_Layer.Validation;
using Data_Acces_Layer;
using Data_Acces_Layer.Interfaces;
using Data_Acces_Layer.Repository;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using webapi.Middlewares;
using webapi.OptionsSetup;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen( c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "YouGG",
        Version = "v1"
    });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
});

builder.Services.AddMemoryCache();

string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// ------ HTTP Client config for every service that sends requests to RIOT API. -------
builder.Services.AddHttpClient();

builder.Services.AddHttpClient<MatchDetailsService>((Serviceprovider, httpClient) =>
{
    var configuration = Serviceprovider.GetRequiredService<IConfiguration>();

    var path = configuration.GetValue<string>("SingleMatchDetailsURL");
    var apiKey = configuration.GetValue<string>("ApiKey");

    httpClient.BaseAddress = new Uri(path);
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
});

builder.Services.AddHttpClient<ISummonerInfoService, SummonerInfoService>((ServiceProvider, httpClient) =>
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

    var path = configuration.GetValue<string>("SummonerInfoURL");
    var apiKey = configuration.GetValue<string>("ApiKey");

    httpClient.BaseAddress = new Uri(path);
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
});

builder.Services.AddHttpClient<IMatchesService, MatchesService>((ServiceProvider, httpClient) => 
{
    var configuration = ServiceProvider.GetRequiredService<IConfiguration>();

    var path = configuration.GetValue<string>("SummonerMatchesListURL");
    var apiKey = configuration.GetValue<string>("ApiKey");

    httpClient.BaseAddress = new Uri(path);
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
});

builder.Services.AddHttpClient<ISummonerPUUIDService, SummonerPUUIDService>((Serviceprovider, httpClient) =>
{
    var configuration = Serviceprovider.GetRequiredService<IConfiguration>();

    var path = configuration.GetValue<string>("SummonerPuuidBaseUrl");
    var apiKey = configuration.GetValue<string>("ApiKey");

    httpClient.BaseAddress = new Uri(path);
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
});
// ------------------------------------------------------------------------------------


builder.Services.AddTransient<MatchDetailsService>(Serviceprovider =>
{
    var httpClientFactory = Serviceprovider.GetRequiredService<IHttpClientFactory>();
    var httpClient = httpClientFactory.CreateClient(typeof(MatchDetailsService).Name);

    return new MatchDetailsService(httpClient);
});

builder.Services.AddTransient<IMatchDetailsService, CachedMatchesDetailsService>(Serviceprovider =>
{
    var matchDetailsService = Serviceprovider.GetRequiredService<MatchDetailsService>();
    var memoryCache = Serviceprovider.GetRequiredService<IMemoryCache>();
    
    return new CachedMatchesDetailsService(matchDetailsService, memoryCache);
});

builder.Services.AddTransient<DatabaseHealthCheckService>();


builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddTransient<IJwtProvider, JwtProvider>();

builder.Services.AddTransient<ISummonerRepository, SummonerRepository>();

builder.Services.AddTransient<IMemberRepository, MemberRepository>();

builder.Services.AddTransient<GlobalErrorHandlingMiddleware>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer();

builder.Services.ConfigureOptions<JwtOptionsSetup>();
builder.Services.ConfigureOptions<JwtBearerOptionsSetup>();

builder.Services.AddAuthorization();

builder.Services.AddValidatorsFromAssemblyContaining<RegisterCredsValidator>(ServiceLifetime.Transient);

builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalHostPolicy",
        policy =>
        {
            policy
                  .SetIsOriginAllowedToAllowWildcardSubdomains()
                  .WithOrigins("https://localhost:5173", "http://localhost:3000")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowAnyHeader();
        });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();



app.UseMiddleware<GlobalErrorHandlingMiddleware>();

app.UseCors("LocalHostPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
