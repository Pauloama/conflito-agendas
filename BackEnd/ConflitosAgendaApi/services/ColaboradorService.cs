using ConflitosAgendaApi.Dtos.Colaboradores;
using ConflitosAgendaApi.Services.Interfaces;
using ConflitosAgendaApi.Repositories.Interfaces;
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
    public async Task<ColaboradorResponseDto?> AddAsync(
        ColaboradorInsertionDto dto)
    {
        var colaborador = new Colaborador(
            dto.Nome,
            dto.Email,
            new List<Projeto>(),
            false);

        var novoColaborador = await _colaboradorRepository.AddAsync(colaborador);

        if (novoColaborador is null)
        {
            return null;
        }

        return new ColaboradorResponseDto(novoColaborador);
    }

    public async Task<ColaboradorResponseDto?> UpdateAsync(int id, ColaboradorUpdateDto dto)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador is null)
        {
            return null;
        }

        colaborador.AtualizarDados(dto.Nome, dto.Email);

        var colaboradorAtualizado = await _colaboradorRepository.UpdateAsync(colaborador);

        if (colaboradorAtualizado is null)
        {
            return null;
        }

        return new ColaboradorResponseDto(colaboradorAtualizado);
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
    public async Task<ColaboradorResponseDto?> TornarCoordenadorAsync(int id, ColaboradorUpdateDto dto)
    {
        var colaborador = await _colaboradorRepository.GetByIdAsync(id);

        if (colaborador is null)
        {
            return null;
        }
        colaborador.TornarCoordenador();

        var colaboradorAtualizado = await _colaboradorRepository.UpdateAsync(colaborador);

        if (colaboradorAtualizado is null)
        {
            return null;
        }

        return new ColaboradorResponseDto(colaboradorAtualizado);
    }

}
