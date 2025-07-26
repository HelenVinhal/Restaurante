namespace Restaurante.Borders.Dtos;

public class AdicionarUsuarioRequest
{
    public string Email { get; set; }
    public string Senha { get; set; }
    public string ConfirmarSenha { get; set; }
    public string CriadoPor { get; set; }
}

