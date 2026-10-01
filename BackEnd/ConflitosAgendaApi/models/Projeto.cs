namespace ConflitosAgendaApi.Models;
public sealed class Projeto
{
    public Projeto(
    )
    {
    }
    public Projeto(
        int id,
        string nome,
        List<Colaborador> pessoasCriticas,
        List<Colaborador> colaboradores
    )
    {
        this.Id = id;
        this.Nome = nome;
        this.PessoasCriticas = pessoasCriticas;
        this.Colaboradores = colaboradores;
    }
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public List<Colaborador> PessoasCriticas { get; private set; } = new();
    public List<Colaborador> Colaboradores { get; private set; } = new();

    
}