using ConflitosAgendaApi.Dtos.Solicitacoes;
using ConflitosAgendaApi.Models;
using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Services.Interfaces;

namespace ConflitosAgendaApi.Services;

public class SolicitacaoService : ISolicitacaoService
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly IConflitoSolicitacaoService _conflitoSolicitacaoService;

    public SolicitacaoService(
        ISolicitacaoRepository solicitacaoRepository,
        IConflitoSolicitacaoService conflitoSolicitacaoService)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _conflitoSolicitacaoService = conflitoSolicitacaoService;
    }

    public async Task<List<SolicitacaoResponseDto>> GetAllAsync()
    {
        var solicitacoes = await _solicitacaoRepository.GetAllAsync();

        var solicitacoesDto = solicitacoes.Select(s => new SolicitacaoResponseDto(s)).ToList();
        return solicitacoesDto;
    }

    public async Task<SolicitacaoResponseDto?> GetByIdAsync(int id)
    {
        var solicitacao = await _solicitacaoRepository.GetByIdAsync(id);

        return solicitacao is null ? null : new SolicitacaoResponseDto(solicitacao);
    }

    public async Task<List<SolicitacaoResponseDto>> GetByColaboradorIdAsync(int colaboradorId)
    {
        var solicitacoes = await _solicitacaoRepository.GetByColaboradorIdAsync(colaboradorId);
        return solicitacoes.Select(s => new SolicitacaoResponseDto(s)).ToList();
    }

    public async Task<List<SolicitacaoResponseDto>> GetByPeriodoIdAsync(int periodoId)
    {
        var solicitacoes = await _solicitacaoRepository.GetAllAsync();
        var solicitacoesDto = solicitacoes
            .Where(s => s.Periodos.Any(p => p.Id == periodoId))
            .Select(s => new SolicitacaoResponseDto(s)).ToList();

        return solicitacoesDto;
    }

    public async Task<List<SolicitacaoResponseDto>> GetByStatusAsync(StatusSolicitacao status)
    {
        var solicitacoes = await _solicitacaoRepository.GetByStatusAsync(status);

        var solicitacoesDto = solicitacoes.Select(s => new SolicitacaoResponseDto(s)).ToList();
        return solicitacoesDto;
    }

    public async Task<Solicitacao> AddAsync(SolicitacaoInsertionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.Periodos is null)
        {
            throw new ArgumentException(
                "A solicitação deve conter pelo menos um período.");
        }

        var solicitacao = new Solicitacao(
            dto.Colaborador,
            dto.Periodos,
            dto.Status,
            dto.CriadaEm,
            dto.Coordenador,
            dto.Observacao,
            dto.Abono,
            dto.isPessoaCritica,
            dto.TotalDias);

        ValidarRegras(solicitacao);

        await _solicitacaoRepository.AddAsync(solicitacao);

        return solicitacao;
    }

    public async Task<Solicitacao?> UpdateAsync(
        int id,
        SolicitacaoUpdateDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var solicitacao = await _solicitacaoRepository.GetByIdAsync(id);

        if (solicitacao is null)
        {
            return null;
        }

        if (dto.Periodos is null)
        {
            throw new ArgumentException(
                "A solicitação deve conter pelo menos um período.");
        }

        ValidarRegras(solicitacao, dto.Periodos);

        solicitacao.Atualizar(
            dto.Colaborador,
            dto.Periodos,
            dto.Coordenador,
            dto.Status,
            dto.Observacao);

        await _solicitacaoRepository.UpdateAsync(solicitacao);

        return solicitacao;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var solicitacao = await _solicitacaoRepository.GetByIdAsync(id);

        if (solicitacao is null)
        {
            return false;
        }

        await _solicitacaoRepository.DeleteAsync(id);

        return true;
    }

    public async Task<bool> ValidateSolicitacaoAsync(int solicitacaoId)
    {
        var solicitacao = await _solicitacaoRepository.GetByIdAsync(solicitacaoId);

        if (solicitacao is null)
        {
            throw new InvalidOperationException(
                $"Solicitação com ID {solicitacaoId} não encontrada.");
        }

        return !await _conflitoSolicitacaoService.PossuiConflitoAsync(solicitacao);

    }

    private static void ValidarRegras(
        Solicitacao solicitacao,
        IEnumerable<Periodo>? periodos = null)
    {
        if (!solicitacao.ValidarNumPeriodos(periodos))
        {
            throw new ArgumentException(
                "A solicitação deve conter entre 1 e 3 períodos.");
        }

        if (!solicitacao.ValidarAbono(periodos))
        {
            throw new ArgumentException(
                "A solicitação não atende aos critérios de abono.");
        }
    }
}