using Microsoft.EntityFrameworkCore;
using ConflitosAgendaApi.Data;
using ConflitosAgendaApi.Repositories;
using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Services;
using ConflitosAgendaApi.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IColaboradorRepository, ColaboradorRepository>();
builder.Services.AddScoped<IPeriodoRepository, PeriodoRepository>();
builder.Services.AddScoped<IProjetoRepository, ProjetoRepository>();
builder.Services.AddScoped<ISolicitacaoRepository, SolicitacaoRepository>();

builder.Services.AddScoped<IColaboradorService, ColaboradorService>();
builder.Services.AddScoped<IPeriodoService, PeriodoService>();
builder.Services.AddScoped<IProjetoService, ProjetoService>();
builder.Services.AddScoped<ISolicitacaoService, SolicitacaoService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
