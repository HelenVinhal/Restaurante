using Restaurante.Api.Services;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class ObterUsuarioPorIdUseCase : IObterUsuarioPorIdUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;

    public ObterUsuarioPorIdUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<UsuarioResponse> Execute(int id)
    {
        var usuario = await Validate(id);

        var usuarioResponse = new UsuarioResponse
        {
            Id = usuario.Id,
            Email = usuario.Email,
            DataCriacao = usuario.DataCriacao,
            CriadoPor = usuario.CriadoPor,
            DataAtualizacao = usuario.DataAtualizacao,
            AtualizadoPor = usuario.AtualizadoPor
        };

        return usuarioResponse;

    }

    private async Task<Usuario> Validate(int id)
    {
        var usuario = await _usuarioRepository.GetByIdAsync(id);

        if (usuario == null)
            throw new HttpStatusCodeException(404, "Usuário não encontrado.");
        return usuario;
    }
}
