using ConflitosAgendaApi.Dtos.Colaboradores;
using ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Repositories.Interfaces;
using ConflitosAgendaApi.Repositories;
using ConflitosAgendaApi.Models;
namespace ConflitosAgendaApi.Services;

public class ColaboradorService : IColaboradorService
{
    private readonly IColaboradorRepository _colaboradorRepository;

    public ColaboradorService(IColaboradorRepository colaboradorRepository)
    {
        _colaboradorRepository = colaboradorRepository;
    }

    public async Task<ColaboradorResponseDto?> GetByIdAsync(int id)
    {
        var colaborador =  await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador == null)
        {
            return null;
        }
        return new ColaboradorResponseDto(colaborador);
    }
    public async Task<IEnumerable<ColaboradorResponseDto>> ListAllAsync()
    {
        var colaboradores = await _colaboradorRepository.ListAllAsync();
        return colaboradores.Select(c => new ColaboradorResponseDto(c));
    }
    public async Task<ColaboradorInsertionDto?> AddAsync(Colaborador colaborador)
    {
        var novoColaborador = await _colaboradorRepository.AddAsync(colaborador);
        if (novoColaborador == null)
        {
            return null;
        }
        return new ColaboradorInsertionDto(novoColaborador);
    }
    public async Task<ColaboradorUpdateDto?> UpdateAsync(Colaborador colaborador)
    {
        var colaboradorAtualizado = await _colaboradorRepository.UpdateAsync(colaborador);
        if (colaboradorAtualizado == null)
        {
            return null;
        }
        return new ColaboradorUpdateDto(colaboradorAtualizado);
    }
    public async Task<bool?> DeleteAsync(int id)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);
        if (colaborador == null)
        {
            return null;
        }
        await _colaboradorRepository.DeleteAsync(id);
        return true;
    }
    public async Task<ColaboradorUpdateDto?> TornarCoordenadorAsync(int id)
    {
        var colaborador = await _colaboradorRepository.TornarCoordenadorAsync(id);
        if (colaborador == null)
        {
            return null;
        }
        return new ColaboradorUpdateDto(colaborador);
    }

    }
