using Restaurante.Borders.Dtos;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class ExcluirUsuarioUseCase : IExcluirUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ExcluirUsuarioUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task Execute(ExcluirUsuarioRequest excluirUsuarioRequest)
    {
        await _usuarioRepository.DeleteAsync(excluirUsuarioRequest.Id, excluirUsuarioRequest.AtualizadoPor);
    }
}
