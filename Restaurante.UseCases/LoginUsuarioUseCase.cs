using Microsoft.AspNetCore.Identity;
using Restaurante.Api.Services;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class LoginUsuarioUseCase : ILoginUsuarioUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;
    private readonly IJwtTokenService _jwtTokenService;


    public LoginUsuarioUseCase(IUsuarioRepository usuarioRepository, IJwtTokenService jwtTokenService, IPasswordHasher<Usuario> passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _jwtTokenService = jwtTokenService;
        _passwordHasher = passwordHasher;
    }

    public async Task<string> Execute(LoginUsuarioRequest loginUsuarioRequest)
    {
        var usuario = await _usuarioRepository.GetByEmailAsync(loginUsuarioRequest.Email);

        if (usuario == null)
            throw new HttpStatusCodeException(401, "Email ou senha inválidos.");

        var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Senha, loginUsuarioRequest.Senha);

        if (result == PasswordVerificationResult.Failed)
            throw new HttpStatusCodeException(401, "Email ou senha inválidos.");

        if (result == PasswordVerificationResult.SuccessRehashNeeded)
        {
            usuario.Senha = _passwordHasher.HashPassword(usuario, loginUsuarioRequest.Senha);
            await _usuarioRepository.UpdateAsync(usuario);
        }

        return await _jwtTokenService.GenerateToken(usuario.Id);
    }
}
