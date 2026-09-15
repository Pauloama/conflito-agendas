namespace ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Models;

public interface iSolicitacaoRepository
{
    Task<Solicitacao> GetByIdAsync(Guid id);
    Task<List<Solicitacao>> GetAllAsync();
    Task<List<Solicitacao>> GetByColaboradorIdAsync(Guid colaboradorId);
    Task<List<Solicitacao>> GetByStatusAsync(StatusSolicitacao status);
    Task AddAsync(Solicitacao solicitacao);
    Task UpdateAsync(Solicitacao solicitacao);
    Task DeleteAsync(Solicitacao solicitacao);
}