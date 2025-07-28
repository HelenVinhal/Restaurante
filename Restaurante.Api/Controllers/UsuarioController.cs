using Microsoft.AspNetCore.Authorization;
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
    private readonly IListarUsuariosUseCase _listarUsuarioUseCase;


    public UsuarioController(IAdicionarUsuarioUseCase adicionarUsuarioUseCase, ILoginUsuarioUseCase loginUsuarioUseCase, IListarUsuariosUseCase listarUsuarioUseCase)
    {
        _adicionarUsuarioUseCase = adicionarUsuarioUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
        _listarUsuarioUseCase = listarUsuarioUseCase;
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

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        var usuarios = await _listarUsuarioUseCase.Execute();
        return Ok(usuarios);
    }

}
