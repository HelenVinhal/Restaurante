using Microsoft.AspNet.Identity;
using Restaurante.Api.Services;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.Entites;
using Restaurante.Borders.Repositories;
using Restaurante.Borders.UseCases;

namespace Restaurante.UseCases;

public class AdicionarUsuarioUseCase : IAdicionarUsuarioUseCase
{

    private readonly IUsuarioRepository _usuarioRepository;
    private readonly PasswordHasher _passwordHasher = new();

    public AdicionarUsuarioUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task Execute(AdicionarUsuarioRequest adicionarUsuarioRequest)
    {
        if (adicionarUsuarioRequest.Senha != adicionarUsuarioRequest.ConfirmarSenha)
            throw new HttpStatusCodeException(400, "As senhas não conferem.");

        var usuarioRegistrado = await _usuarioRepository.GetByEmailAsync(adicionarUsuarioRequest.Email);

        if (usuarioRegistrado != null)
            throw new HttpStatusCodeException(400, "Email já Cadastrado.");


        var usuario = new Usuario
        {
            Email = adicionarUsuarioRequest.Email,
            Senha = _passwordHasher.HashPassword(adicionarUsuarioRequest.Senha),
            DataCriacao = DateTime.Now,
            CriadoPor = adicionarUsuarioRequest.CriadoPor
        };

        await _usuarioRepository.AddAsync(usuario);
    }
}
