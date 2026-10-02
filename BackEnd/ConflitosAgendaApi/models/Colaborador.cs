namespace ConflitosAgendaApi.Models;

public class Colaborador
{
    public Colaborador(
    )
    {
    }
    public Colaborador(string nome, string email, List<Projeto> projetos, bool isCoordenador)
    {
        this.Nome = nome;
        this.Email = email;
        this.IsCoordenador = isCoordenador;
    }
    public int Id { get; private set; }
    public string Nome { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public bool IsCoordenador { get; private set;}

    public void TornarCoordenador()
    {
        IsCoordenador = true;
    }

    public void AtualizarDados(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}