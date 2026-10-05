using ConflitosAgendaApi.Dtos.Solicitacoes;
using ConflitosAgendaApi.Models;
using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Services.Interfaces;

namespace ConflitosAgendaApi.Services;

public class SolicitacaoService : ISolicitacaoService
{
    private readonly ISolicitacaoRepository _solicitacaoRepository;
    private readonly IColaboradorRepository _colaboradorRepository;
    private readonly IPeriodoRepository _periodoRepository;

    public SolicitacaoService(
        ISolicitacaoRepository solicitacaoRepository,
        IColaboradorRepository colaboradorRepository,
        IPeriodoRepository periodoRepository)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _colaboradorRepository = colaboradorRepository;
        _periodoRepository = periodoRepository;
    }

    public async Task<List<Solicitacao>> GetAllAsync()
    {
        return await _solicitacaoRepository.GetAllAsync();
    }

    public async Task<Solicitacao?> GetByIdAsync(int id)
    {
        return await _solicitacaoRepository.GetByIdAsync(id);
    }

    public async Task<List<Solicitacao>> GetByColaboradorIdAsync(int colaboradorId)
    {
        return await _solicitacaoRepository.GetByColaboradorIdAsync(colaboradorId);
    }

    public async Task<List<Solicitacao>> GetByPeriodoIdAsync(int periodoId)
    {
        var solicitacoes = await _solicitacaoRepository.GetAllAsync();

        return solicitacoes
            .Where(s => s.Periodos.Any(p => p.Id == periodoId))
            .ToList();
    }

    public async Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status)
    {
        return await _solicitacaoRepository.GetByStatusAsync(status);
    }

    public async Task<Solicitacao> AddAsync(SolicitacaoInsertionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if (dto.Colaborador is null)
        {
            throw new ArgumentException(
                "A solicitação deve possuir um colaborador.",
                nameof(dto));
        }

        if (dto.Coordenador is null)
        {
            throw new ArgumentException(
                "A solicitação deve possuir um coordenador.",
                nameof(dto));
        }

        if (dto.Periodos.Count == 0)
        {
            throw new ArgumentException(
                "A solicitação deve possuir ao menos um período.",
                nameof(dto));
        }

        var solicitacao = new Solicitacao(
            dto.Colaborador,
            dto.Periodos,
            dto.Coordenador);

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

        return solicitacao is not null &&
               solicitacao.Periodos.Count > 0 &&
               solicitacao.Periodos.All(p => p.DataInicio < p.DataFim);
    }
}