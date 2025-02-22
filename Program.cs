using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// 🔹 MySQL kapcsolat beállítása Pomelo csomaggal
var connectionString = "Server=unity-mysql-db-probamysql.h.aivencloud.com;Port=24563;Database=defaultdb;User=avnadmin;Password=AVNS_i2FDdQ6MHsQANalLCmI;";
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// 🔹 Adatbázis modell és DbContext
public class MyDbContext : DbContext {
    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
    public DbSet<PlayerData> Players { get; set; }
}

public class PlayerData {
    public int Id { get; set; }
    public string Name { get; set; }
    public int Score { get; set; }
}
