using Microsoft.EntityFrameworkCore;
using YardenProject.Data;
using YardenProject.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register GaiaDbContext
builder.Services.AddDbContext<GaiaDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register OperationService
builder.Services.AddScoped<IOperationService, OperationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseDefaultFiles();
app.UseStaticFiles();


app.UseAuthorization();

app.MapControllers();

app.Run();
