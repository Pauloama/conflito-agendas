namespace ConflitosAgendaApi.Models;
public sealed class Projeto
{
    public Projeto(
    )
    {
    }
    public Projeto(
        string nome,
        List<Colaborador> pessoasCriticas,
        List<Colaborador> colaboradores
    )
    {
        this.Nome = nome;
        this.PessoasCriticas = pessoasCriticas;
        this.Colaboradores = colaboradores;
    }
    public void Atualizar(
        string nome,
        List<Colaborador> pessoasCriticas,
        List<Colaborador> colaboradores)
    {
        Nome = nome;
        PessoasCriticas = pessoasCriticas;
        Colaboradores = colaboradores;
    }
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public List<Colaborador> PessoasCriticas { get; private set; } = new();
    public List<Colaborador> Colaboradores { get; private set; } = new();

    
}