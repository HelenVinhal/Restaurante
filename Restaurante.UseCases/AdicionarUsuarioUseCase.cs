using Microsoft.AspNetCore.Identity;
using Restaurante.Api.Services;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class AdicionarUsuarioUseCase : IAdicionarUsuarioUseCase
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IPasswordHasher<Usuario> _passwordHasher;

    public AdicionarUsuarioUseCase(IUsuarioRepository usuarioRepository, IPasswordHasher<Usuario> passwordHasher)
    {
        _usuarioRepository = usuarioRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task Execute(AdicionarUsuarioRequest adicionarUsuarioRequest)
    {
        await Validate(adicionarUsuarioRequest);

        var usuario = new Usuario
        {
            Email = adicionarUsuarioRequest.Email,
            DataCriacao = DateTime.Now,
            CriadoPor = adicionarUsuarioRequest.CriadoPor
        };

        usuario.Senha = _passwordHasher.HashPassword(usuario, adicionarUsuarioRequest.Senha);

        await _usuarioRepository.AddAsync(usuario);
    }

    private async Task Validate(AdicionarUsuarioRequest adicionarUsuarioRequest)
    {
        if (adicionarUsuarioRequest.Senha != adicionarUsuarioRequest.ConfirmarSenha)
            throw new HttpStatusCodeException(400, "As senhas não conferem.");

        var usuarioRegistrado = await _usuarioRepository.GetByEmailAsync(adicionarUsuarioRequest.Email);

        if (usuarioRegistrado != null)
            throw new HttpStatusCodeException(400, "Email já Cadastrado.");
    }
}
