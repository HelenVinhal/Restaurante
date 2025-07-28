#nullable disable warnings

namespace Restaurante.Borders.Entites;

public class Usuario
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public DateTime DataCriacao { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public string CriadoPor { get; set; }
    public string AtualizadoPor { get; set; }
    public bool Ativo { get; set; }

}
