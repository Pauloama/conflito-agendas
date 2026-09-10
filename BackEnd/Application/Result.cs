namespace ConflitosAgenda.Api.Application;

public sealed record Result<T>(T? Valor, IReadOnlyCollection<string> Erros)
{
    public bool Sucesso => Erros.Count == 0;
    public bool NaoEncontrado { get; init; }

    public static Result<T> Ok(T valor) => new(valor, []);
    public static Result<T> Falha(params string[] erros) => new(default, erros);
    public static Result<T> NotFound() => new(default, []) { NaoEncontrado = true };
}
