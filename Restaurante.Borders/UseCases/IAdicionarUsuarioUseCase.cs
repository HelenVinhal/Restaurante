using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;

public interface IAdicionarUsuarioUseCase
{
    Task Execute(AdicionarUsuarioRequest adicionarUsuarioRequest);
}
