using Microsoft.AspNetCore.Identity;
using Restaurante.Api.Services;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases
{


    public class AtualizarSenhaUsuarioUseCase : IAtualizarSenhaUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IPasswordHasher<Usuario> _passwordHasher;

        public AtualizarSenhaUsuarioUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher<Usuario> passwordHasher)
        {
            _usuarioRepository = usuarioRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task Execute((int, AtualizarSenhaUsuarioRequest) request)
        {
            var (id, atualizarSenhaUsuarioRequest) = request;

            var usuario = await Validate(id, atualizarSenhaUsuarioRequest);

            usuario.Senha = _passwordHasher.HashPassword(usuario, atualizarSenhaUsuarioRequest.NovaSenha);
            usuario.AtualizadoPor = atualizarSenhaUsuarioRequest.AtualizadoPor;

            await _usuarioRepository.UpdateAsync(usuario);
        }

        private async Task<Usuario> Validate(int id, AtualizarSenhaUsuarioRequest atualizarSenhaUsuarioRequest)
        {
            if (atualizarSenhaUsuarioRequest.NovaSenha != atualizarSenhaUsuarioRequest.ConfirmarNovaSenha)
                throw new HttpStatusCodeException(400, "As senhas não conferem.");

            var usuario = await _usuarioRepository.GetByIdAsync(id);

            if (usuario == null)
                throw new HttpStatusCodeException(404, "Usuário não encontrado.");

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, atualizarSenhaUsuarioRequest.SenhaAnterior);

            if (result == PasswordVerificationResult.Failed)
                throw new HttpStatusCodeException(401, "Senha incorreta.");
            return usuario;
        }
    }

}
