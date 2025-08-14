using Microsoft.AspNetCore.Identity;
using Restaurante.Api.Services;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class AtualizarEmailUsuarioUseCase : IAtualizarEmailUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public AtualizarEmailUsuarioUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher<Usuario> passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;

    }

    public async Task Execute(AtualizarEmailUsuarioRequest request)
    {
        if (request.NovoEmail != request.ConfirmarNovoEmail)
            throw new HttpStatusCodeException(400, "Os emails não conferem.");

        var usuarioRegistrado = await _usuarioRepository.GetByEmailAsync(request.NovoEmail);

        if (usuarioRegistrado != null)
            throw new HttpStatusCodeException(400, "Email já cadastrado.");

        var usuario = await _usuarioRepository.GetByEmailAsync(request.EmailAntigo);

        if (usuario == null)
            throw new HttpStatusCodeException(401, "Email ou senha inválidos.");

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, request.Senha);

        if (result == PasswordVerificationResult.Failed)
            throw new HttpStatusCodeException(401, "Email ou senha inválidos.");

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
            usuario.Senha = _passwordHasher.HashPassword(usuario, request.Senha);

        usuario.Email = request.NovoEmail;
        usuario.AtualizadoPor = request.AtualizadoPor;

        await _usuarioRepository.UpdateAsync(usuario);
    }
}