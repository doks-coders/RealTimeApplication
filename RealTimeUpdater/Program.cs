using Microsoft.AspNetCore.Builder;
using RealTimeUpdater.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddSingleton<PresenceTracker>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
	options.AddPolicy("AllowSpecificOrigin",
		builder => builder
			.WithOrigins("https://localhost:4200")
			.AllowAnyMethod()
			.AllowAnyHeader()
			.AllowCredentials());
});

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseCors(u => u.AllowAnyHeader().AllowAnyMethod()
.AllowCredentials()
.WithOrigins("https://localhost:4200"));

app.MapHub<UpdatesHub>("hubs/updates");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
