namespace ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Dtos.Solicitacoes;
using ConflitosAgendaApi.Models;

public interface ISolicitacaoService
{
    Task<List<Solicitacao>> GetAllAsync();
    Task<Solicitacao?> GetByIdAsync(int id);
    Task<List<Solicitacao>> GetByColaboradorIdAsync(int colaboradorId);
    Task<List<Solicitacao>> GetByPeriodoIdAsync(int periodoId);
    Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status);
    Task<Solicitacao> AddAsync(SolicitacaoInsertionDto solicitacao);
    Task<Solicitacao?> UpdateAsync(int id, SolicitacaoUpdateDto solicitacao);
    Task<bool> DeleteAsync(int id);
    Task<bool> ValidateSolicitacaoAsync(int solicitacaoId);
}