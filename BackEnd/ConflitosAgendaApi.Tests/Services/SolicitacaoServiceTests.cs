using ConflitosAgendaApi.Dtos.Solicitacoes;
using ConflitosAgendaApi.Models;
using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Services;
using ConflitosAgendaApi.Services.Interfaces;

namespace ConflitosAgendaApi.Tests.Services;

public class SolicitacaoServiceTests
{
    [Fact]
    public async Task AddAsync_DeveRejeitarSolicitacaoSemPeriodos()
    {
        var service = CriarService();
        var dto = CriarInsertionDto([]);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.AddAsync(dto));

        Assert.Contains("entre 1 e 3 períodos", exception.Message);
    }

    [Fact]
    public async Task AddAsync_DeveRejeitarAbonoComMaisDeDoisPeriodos()
    {
        var service = CriarService();
        var dto = CriarInsertionDto(
            [CriarPeriodo(), CriarPeriodo(), CriarPeriodo()],
            abono: true);

        var exception = await Assert.ThrowsAsync<ArgumentException>(
            () => service.AddAsync(dto));

        Assert.Contains("critérios de abono", exception.Message);
    }

    [Fact]
    public async Task UpdateAsync_DeveValidarAntesDeAlterarOuPersistir()
    {
        var existente = CriarSolicitacao();
        var repository = new FakeSolicitacaoRepository(existente);
        var service = CriarService(repository);
        var dto = CriarUpdateDto(
            [CriarPeriodo(), CriarPeriodo(), CriarPeriodo(), CriarPeriodo()]);

        await Assert.ThrowsAsync<ArgumentException>(
            () => service.UpdateAsync(existente.Id, dto));

        Assert.Single(existente.Periodos);
        Assert.False(repository.UpdateCalled);
    }

    [Fact]
    public async Task GetByIdAsync_DeveRetornarNullQuandoNaoEncontrar()
    {
        var service = CriarService(new FakeSolicitacaoRepository(null));

        var resultado = await service.GetByIdAsync(99);

        Assert.Null(resultado);
    }

    private static SolicitacaoService CriarService(
        ISolicitacaoRepository? repository = null)
    {
        return new SolicitacaoService(
            repository ?? new FakeSolicitacaoRepository(CriarSolicitacao()),
            new FakeConflitoSolicitacaoService());
    }

    private static SolicitacaoInsertionDto CriarInsertionDto(
        List<Periodo> periodos,
        bool abono = false)
    {
        return new SolicitacaoInsertionDto
        {
            Colaborador = CriarColaborador(),
            Periodos = periodos,
            Coordenador = CriarColaborador(),
            TotalDias = abono ? 10 : 10,
            Abono = abono
        };
    }

    private static SolicitacaoUpdateDto CriarUpdateDto(List<Periodo> periodos)
    {
        return new SolicitacaoUpdateDto
        {
            Colaborador = CriarColaborador(),
            Periodos = periodos,
            Coordenador = CriarColaborador()
        };
    }

    private static Solicitacao CriarSolicitacao()
    {
        return new Solicitacao(
            CriarColaborador(),
            [CriarPeriodo()],
            StatusSolicitacao.Em_Analise,
            DateTimeOffset.UtcNow,
            CriarColaborador());
    }

    private static Colaborador CriarColaborador()
    {
        return new Colaborador("Pessoa", "pessoa@example.com", new(), false);
    }

    private static Periodo CriarPeriodo()
    {
        return new Periodo(
            new DateTime(2026, 10, 1),
            new DateTime(2026, 10, 11));
    }

    private sealed class FakeConflitoSolicitacaoService : IConflitoSolicitacaoService
    {
        public Task<bool> PossuiConflitoAsync(Solicitacao solicitacao)
        {
            return Task.FromResult(false);
        }
    }

    private sealed class FakeSolicitacaoRepository : ISolicitacaoRepository
    {
        private readonly Solicitacao? _solicitacao;
        public bool UpdateCalled { get; private set; }

        public FakeSolicitacaoRepository(Solicitacao? solicitacao)
        {
            _solicitacao = solicitacao;
        }

        public Task<Solicitacao?> GetByIdAsync(int id) =>
            Task.FromResult(_solicitacao);

        public Task<List<Solicitacao>> GetAllAsync() =>
            Task.FromResult(_solicitacao is null
                ? new List<Solicitacao>()
                : new List<Solicitacao> { _solicitacao });

        public Task<List<Solicitacao>> GetByColaboradorIdAsync(int colaboradorId) =>
            GetAllAsync();

        public Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status) =>
            GetAllAsync();

        public Task AddAsync(Solicitacao solicitacao) => Task.CompletedTask;

        public Task UpdateAsync(Solicitacao solicitacao)
        {
            UpdateCalled = true;
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id) => Task.CompletedTask;
    }
}
