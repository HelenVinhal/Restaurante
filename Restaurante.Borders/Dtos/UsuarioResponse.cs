#nullable disable warnings

namespace Restaurante.Borders.Dtos;

public class UsuarioResponse
{
    public int Id { get; set; }
    public string Email { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public string CriadoPor { get; set; }
    public string AtualizadoPor { get; set; }
}
