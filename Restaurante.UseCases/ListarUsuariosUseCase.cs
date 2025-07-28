using Restaurante.Borders.Dtos;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class ListarUsuariosUseCase : IListarUsuariosUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ListarUsuariosUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<IEnumerable<UsuarioResponse>> Execute()
    {
        var usuarios = await _usuarioRepository.GetAllAsync();

        var response = usuarios.Select(u => new UsuarioResponse
        {
            Id = u.Id,
            Email = u.Email,
            DataCriacao = u.DataCriacao,
            CriadoPor = u.CriadoPor,
            DataAtualizacao = u.DataAtualizacao,
            AtualizadoPor = u.AtualizadoPor
        });

        return response;
    }
}
