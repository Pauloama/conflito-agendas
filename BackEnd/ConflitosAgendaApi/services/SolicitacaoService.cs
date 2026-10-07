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

    public async Task<List<SolicitacaoResponseDto>> GetAllAsync()
    {
       var solicitacoes = await _solicitacaoRepository.GetAllAsync();
       
       if (solicitacoes is null)
        {
            throw new InvalidOperationException("Não foi possível recuperar as solicitações.");
        }

        var solicitacoesDto = solicitacoes.Select(s => new SolicitacaoResponseDto(s)).ToList();
        return solicitacoesDto;
    }

    public async Task<SolicitacaoResponseDto?> GetByIdAsync(int id)
    {
        var solicitacao = await _solicitacaoRepository.GetByIdAsync(id);

        if (solicitacao is null)
        {
            throw new InvalidOperationException($"Solicitação com ID {id} não encontrada.");
        }
        
        return new SolicitacaoResponseDto(solicitacao);
    }

    public async Task<List<SolicitacaoResponseDto>> GetByColaboradorIdAsync(int colaboradorId)
    {
        var solicitacoes = await _solicitacaoRepository.GetByColaboradorIdAsync(colaboradorId);
        if (solicitacoes is null)
        {
            throw new InvalidOperationException($"Nenhuma solicitação encontrada para o colaborador com ID {colaboradorId}.");
        }
        return solicitacoes.Select(s => new SolicitacaoResponseDto(s)).ToList();
    }

    public async Task<List<SolicitacaoResponseDto>> GetByPeriodoIdAsync(int periodoId)
    {
        var solicitacoes = await _solicitacaoRepository.GetAllAsync();
        if (solicitacoes is null)
        {
            throw new InvalidOperationException($"Nenhuma solicitação encontrada.");
        }

        var solicitacoesDto = solicitacoes
            .Where(s => s.Periodos.Any(p => p.Id == periodoId))
            .Select(s => new SolicitacaoResponseDto(s)).ToList();
            
        return solicitacoesDto;
    }

    public async Task<List<SolicitacaoResponseDto>> GetByStatusAsync(StatusSolicitacao status)
    {
        var solicitacoes = await _solicitacaoRepository.GetByStatusAsync(status);

        if (solicitacoes is null)
        {
            throw new InvalidOperationException($"Nenhuma solicitação encontrada.");
        }

        var solicitacoesDto = solicitacoes.Select(s => new SolicitacaoResponseDto(s)).ToList();
        return solicitacoesDto;
    }

    public async Task<Solicitacao> AddAsync(SolicitacaoInsertionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        if(dto.Periodos is null || !dto.Periodos.Any())
        {
            throw new ArgumentException("A solicitação deve conter pelo menos um período.");
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

        if (solicitacao is null)
        {
            throw new InvalidOperationException($"Solicitação com ID {solicitacaoId} não encontrada.");
        }

        if ()
    }

    /*



    To do
    Validar se o colaborador é uma pessoa crítica em relação ao projeto
    Validar se os períodos da solicitação entram em conflito com os períodos de outros colaboradores críticos
    
    
    Alterar o estado da solicitação

    */

    /*
    Eu preciso receber 
    */
}