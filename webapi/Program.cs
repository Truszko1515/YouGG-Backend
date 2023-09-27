using Business_Logic_Layer.Interfaces;
using Business_Logic_Layer.Services;
using System.IO;
using System.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpClient();
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

builder.Services.AddHttpClient<IMatchesService, MatchesService>((Serviceprovider, httpClient) => 
{
    var configuration = Serviceprovider.GetRequiredService<IConfiguration>();

    var path = configuration.GetValue<string>("SummonerMatchesListURL");
    var apiKey = configuration.GetValue<string>("ApiKey");

    httpClient.BaseAddress = new Uri(path);
    httpClient.DefaultRequestHeaders.Accept.Clear();
    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    httpClient.DefaultRequestHeaders.Add("X-Riot-Token", apiKey);
});


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
