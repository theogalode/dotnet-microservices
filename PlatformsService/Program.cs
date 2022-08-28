using Microsoft.EntityFrameworkCore;
using PlatformService.SyncDataServices.Http;
using PlatformsService.Data;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager conf = builder.Configuration;
IWebHostEnvironment _env = builder.Environment;

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IPlatformsRepo, PlatformsRepo>();

builder.Services.AddHttpClient<ICommandDataClient, HttpCommandDataClient>();

Console.WriteLine($"--> CommandService endpoint : {conf["CommandsService"]}");

if (_env.IsProduction())
{
    Console.WriteLine("--> Using SqlServer DB");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseSqlServer(conf.GetConnectionString("PlatformsConn")));
}
else
{
    Console.WriteLine("--> Using InMem DB");
    builder.Services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//PrepDb.PrepPopulation(app, _env.IsProduction());

app.MapControllers();

app.Run();
