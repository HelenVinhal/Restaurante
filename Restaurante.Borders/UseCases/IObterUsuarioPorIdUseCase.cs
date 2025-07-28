using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;

public interface IObterUsuarioPorIdUseCase
{
    Task<UsuarioResponse> Execute(int id);
}
