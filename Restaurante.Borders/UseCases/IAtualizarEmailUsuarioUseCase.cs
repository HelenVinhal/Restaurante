using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;

public interface IAtualizarEmailUsuarioUseCase
{
    Task Execute(AtualizarEmailUsuarioRequest atualizarEmailUsuarioRequest);
}
