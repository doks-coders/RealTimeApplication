using RealTimeUpdater.Extensions;
using RealTimeUpdater.SignalR;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
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



app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseCors(u => u.AllowAnyHeader().AllowAnyMethod()
.AllowCredentials()
.WithOrigins("https://localhost:4200"));

app.MapControllers();

app.MapHub<UpdatesHub>("hubs/updates");

app.MapHub<MessageHub>("hubs/messages");

app.Run();
