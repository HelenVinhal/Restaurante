using Restaurante.Borders.Dtos;

namespace Restaurante.Borders.UseCases;


public interface IAtualizarSenhaUsuarioUseCase
{
    Task Execute((int, AtualizarSenhaUsuarioRequest) request);


}
