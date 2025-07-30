using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;

public interface IExcluirUsuarioUseCase
{
    Task Execute(ExcluirUsuarioRequest excluirUsuarioRequest);
}
