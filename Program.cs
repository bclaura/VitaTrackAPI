using Microsoft.EntityFrameworkCore;
using VitaTrackAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseUrls("http://0.0.0.0:5000");

var connStr = builder.Configuration.GetConnectionString("VitaTrack")
    ?? Environment.GetEnvironmentVariable("ConnectionStrings__VitaTrack");


// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddDbContext<VitaTrackContext>(options =>
    options.UseNpgsql(connStr));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Configure CORS if needed for your MAUI app
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowMauiApp", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
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

// Use CORS
app.UseCors("AllowMauiApp");

app.UseAuthorization();

app.MapControllers();

app.Run();
