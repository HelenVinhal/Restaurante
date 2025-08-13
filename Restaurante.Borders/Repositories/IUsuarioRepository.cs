using Restaurante.Borders.Entites;

namespace Restaurante.Borders.Repositories;

public interface IUsuarioRepository
{

    Task<Usuario?> GetByIdAsync(int id);
    Task<Usuario?> GetByEmailAsync(string email);
    Task<IEnumerable<Usuario>> GetAllAsync();
    Task AddAsync(Usuario usuario);
    Task UpdateAsync(Usuario usuario);
    Task AtualizarEmailAsync(string EmailAntigo, string senha, string novoEmail, string ConfirmarNovoEmail);
    Task DeleteAsync(int id, string atualizadoPor);

}
