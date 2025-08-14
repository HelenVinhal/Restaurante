namespace Restaurante.Borders.Dtos
{
    public class AtualizarSenhaUsuarioRequest
    {
        public string SenhaAnterior { get; set; }
        public string NovaSenha { get; set; }
        public string ConfirmarNovaSenha { get; set; }
        public string AtualizadoPor { get; set; }

    }

}
