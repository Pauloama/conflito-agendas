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
        this.Projetos = projetos;
        this.IsCoordenador = isCoordenador;
    }
    public int Id { get; private set; }
    public string Nome { get; private set;}
    public string Email { get; private set;}
    public List<Projeto> Projetos { get; private set;} = new();
    public bool IsCoordenador { get; private set;}

    public void TornarCoordenador()
    {
        IsCoordenador = true;
    }

    
}