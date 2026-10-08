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

    private readonly IProjetoRepository _projetoRepository;

    public SolicitacaoService(
        ISolicitacaoRepository solicitacaoRepository,
        IColaboradorRepository colaboradorRepository,
        IPeriodoRepository periodoRepository,
        IProjetoRepository projetoRepository)
    {
        _solicitacaoRepository = solicitacaoRepository;
        _colaboradorRepository = colaboradorRepository;
        _periodoRepository = periodoRepository;
        _projetoRepository = projetoRepository;
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

        if (dto.Periodos is null || !dto.Periodos.Any())
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
        var solicitacao =
        await _solicitacaoRepository.GetByIdAsync(solicitacaoId);

        if (solicitacao is null)
        {
            throw new InvalidOperationException(
                $"Solicitação com ID {solicitacaoId} não encontrada.");
        }

        if (solicitacao.Periodos is null ||
            solicitacao.Periodos.Count == 0)
        {
            return false;
        }

        var colaborador = solicitacao.Colaborador;

        if (colaborador is null)
        {
            return false;
        }

        var projetosDoColaborador =
            await _projetoRepository.ListProjetosByColaboradorIdAsync(
                colaborador.Id);

        var projetosOndeEleEcritico =
            await _projetoRepository.ListProjetosByPessoasCriticasIdAsync(
                colaborador.Id);

        var idsProjetosDoColaborador =
            projetosDoColaborador
                .Select(p => p.Id)
                .ToHashSet();

        var projetosRelevantes =
            projetosOndeEleEcritico
                .Where(p => idsProjetosDoColaborador.Contains(p.Id))
                .ToList();

        if (projetosRelevantes.Count == 0)
        {
            return true;
        }

        var idsOutrasPessoasCriticas =
            projetosRelevantes
                .SelectMany(p => p.PessoasCriticas)
                .Where(c => c.Id != colaborador.Id)
                .Select(c => c.Id)
                .Distinct()
                .ToList();

        foreach (var pessoaCriticaId in idsOutrasPessoasCriticas)
        {
            var periodosDaPessoaCritica =
                await _periodoRepository.ListByColaboradorIdAsync(
                    pessoaCriticaId);

            var existeConflito =
                solicitacao.Periodos.Any(periodoSolicitacao =>
                    periodosDaPessoaCritica.Any(periodoExistente =>
                        periodoSolicitacao.DataInicio <
                            periodoExistente.DataFim &&
                        periodoSolicitacao.DataFim >
                            periodoExistente.DataInicio));

            if (existeConflito)
            {
                return false;
            }
        }

        return true;
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