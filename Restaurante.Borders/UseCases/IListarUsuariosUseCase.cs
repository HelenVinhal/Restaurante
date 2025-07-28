using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;

public interface IListarUsuariosUseCase
{
    Task<IEnumerable<UsuarioResponse>> Execute();
}
