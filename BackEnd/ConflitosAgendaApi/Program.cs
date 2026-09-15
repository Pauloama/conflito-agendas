using Microsoft.EntityFrameworkCore;
using ConflitosAgendaApi.Data;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapControllers();
app.Run();

app.UseHttpsRedirection();


