using Application.Services;
using Infrastructure.ExternalAPIs;
using Domain.Helpers;
var builder = WebApplication.CreateBuilder(args);
DotNetEnv.Env.Load();


// Added services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//  custom services
builder.Services.AddHttpClient<IGenderService, GenderService>();
builder.Services.AddHttpClient<INationalityService, NationalityService>();
CountryLookup.LoadFromFile("./Files/ISO3166-1.alpha2.json");

// for frontend use -- start
builder.Services.AddCors(options => {
    var frontendUrl = Environment.GetEnvironmentVariable("FRONTEND_URL") ?? "http://localhost:5173";
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins(frontendUrl)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

// for frontend use -- end

var app = builder.Build();

// for frontend use -- start
app.UseCors("AllowFrontend");
// for frontend use -- end


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();