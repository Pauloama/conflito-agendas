using ConflitosAgenda.Api.Application;
using ConflitosAgenda.Api.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<IPlanoFocalStore, InMemoryPlanoFocalStore>();
builder.Services.AddSingleton<ISolicitacaoStore, InMemorySolicitacaoStore>();
builder.Services.AddSingleton<SolicitacaoService>();
builder.Services.AddSingleton<PlanoFocalService>();
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.MapGet("/api/solicitacoes", (SolicitacaoService service, CancellationToken cancellationToken) =>
    service.ListarAsync(cancellationToken));

app.MapPost("/api/solicitacoes", async (
    CriarSolicitacaoRequest request,
    SolicitacaoService service,
    CancellationToken cancellationToken) =>
{
    var resultado = await service.CriarAsync(request, cancellationToken);
    return resultado.Sucesso
        ? Results.Created($"/api/solicitacoes/{resultado.Valor!.Id}", resultado.Valor)
        : Results.Conflict(new { errors = resultado.Erros });
});

app.MapPost("/api/solicitacoes/{id:guid}/decisao", async (
    Guid id,
    DecidirSolicitacaoRequest request,
    SolicitacaoService service,
    CancellationToken cancellationToken) =>
{
    var resultado = await service.DecidirAsync(id, request, cancellationToken);
    if (resultado.NaoEncontrado)
    {
        return Results.NotFound();
    }

    return resultado.Sucesso
        ? Results.Ok(resultado.Valor)
        : Results.Conflict(new { errors = resultado.Erros });
});

app.MapPost("/api/plano-focal", async (
    ImportarPlanoFocalRequest request,
    PlanoFocalService service,
    CancellationToken cancellationToken) =>
{
    var resultado = await service.ImportarAsync(request, cancellationToken);
    return resultado.Sucesso
        ? Results.Ok(resultado.Valor)
        : Results.BadRequest(new { errors = resultado.Erros });
});

app.MapGet("/api/plano-focal", (PlanoFocalService service, CancellationToken cancellationToken) =>
    service.ListarAsync(cancellationToken));

app.Run();
