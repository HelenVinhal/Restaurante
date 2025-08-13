namespace Restaurante.Borders.Dtos
{
    public class AtualizarEmailUsuarioRequest
    {
        public string EmailAntigo { get; set; }
        public string Senha { get; set; }
        public string NovoEmail { get; set; }
        public string ConfirmarNovoEmail { get; set; }
        public string AtualizadoPor { get; set; }
    }
}
