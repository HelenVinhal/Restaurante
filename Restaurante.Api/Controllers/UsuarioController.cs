using Microsoft.AspNetCore.Mvc;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.UseCases;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]

public class UsuarioController : ControllerBase
{
    private readonly IAdicionarUsuarioUseCase _adicionarUsuarioUseCase;
    private readonly ILoginUsuarioUseCase _loginUsuarioUseCase;


    public UsuarioController(IAdicionarUsuarioUseCase adicionarUsuarioUseCase, ILoginUsuarioUseCase loginUsuarioUseCase)
    {
        _adicionarUsuarioUseCase = adicionarUsuarioUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
    }

    [HttpPost("adicionar")]
    public async Task<ActionResult> Cadastrar(AdicionarUsuarioRequest adicionarUsuarioRequest)
    {
        await _adicionarUsuarioUseCase.Execute(adicionarUsuarioRequest);

        return NoContent();
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUsuarioRequest loginUsuarioRequest)
    {
        var token = await _loginUsuarioUseCase.Execute(loginUsuarioRequest);
        return Ok(token);
    }

}
