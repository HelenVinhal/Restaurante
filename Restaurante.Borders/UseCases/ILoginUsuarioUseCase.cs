using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;

public interface ILoginUsuarioUseCase
{
    Task<string> Execute(LoginUsuarioRequest loginUsuarioRequest);
}
