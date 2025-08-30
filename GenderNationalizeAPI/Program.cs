using Application.Services;
using Infrastructure.ExternalAPIs;
using Domain.Helpers;
var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();


// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register custom services
builder.Services.AddHttpClient<IGenderService, GenderService>();
builder.Services.AddHttpClient<INationalityService, NationalityService>();
CountryLookup.LoadFromFile("./Files/ISO3166-1.alpha2.json");
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