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
    private readonly IObterUsuarioPorIdUseCase _obterUsuarioPorIdUseCase;


    public UsuarioController(
        IAdicionarUsuarioUseCase adicionarUsuarioUseCase,
        ILoginUsuarioUseCase loginUsuarioUseCase,
        IListarUsuariosUseCase listarUsuarioUseCase,
        IObterUsuarioPorIdUseCase obterUsuarioPorIdUseCase)
    {
        _adicionarUsuarioUseCase = adicionarUsuarioUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
        _listarUsuarioUseCase = listarUsuarioUseCase;
        _obterUsuarioPorIdUseCase = obterUsuarioPorIdUseCase;
    }

    [Authorize]
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

    [HttpGet("listar")]
    public async Task<ActionResult<IEnumerable<UsuarioResponse>>> GetAll()
    {
        var usuarios = await _listarUsuarioUseCase.Execute();
        return Ok(usuarios);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UsuarioResponse>> GetById(int id)
    {
        var usuario = await _obterUsuarioPorIdUseCase.Execute(id);
        return Ok(usuario);
    }

}
