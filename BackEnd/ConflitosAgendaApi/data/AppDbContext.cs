using Microsoft.EntityFrameworkCore;
using ConflitosAgendaApi.Models;

namespace ConflitosAgendaApi.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Colaborador> Colaboradores { get; set; }
    public DbSet<Projeto> Projetos { get; set; }
    public DbSet<Solicitacao> Solicitacoes { get; set; }
    public DbSet<Periodo> Periodos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Projeto>()
            .HasMany(projeto => projeto.Colaboradores)
            .WithMany(colaborador => colaborador.Projetos)
            .UsingEntity("ProjetoColaborador");

        modelBuilder.Entity<Projeto>()
            .HasMany(projeto => projeto.PessoasCriticas)
            .WithMany()
            .UsingEntity("ProjetoPessoaCritica");
    }
}
