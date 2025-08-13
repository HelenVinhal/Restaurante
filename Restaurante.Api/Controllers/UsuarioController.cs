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
    private readonly IExcluirUsuarioUseCase _excluirUsuarioUseCase;
    private readonly IAtualizarEmailUsuarioUseCase _atualizarEmailUsuarioUseCase;


    public UsuarioController(
        IAdicionarUsuarioUseCase adicionarUsuarioUseCase,
        ILoginUsuarioUseCase loginUsuarioUseCase,
        IListarUsuariosUseCase listarUsuarioUseCase,
        IObterUsuarioPorIdUseCase obterUsuarioPorIdUseCase,
        IExcluirUsuarioUseCase excluirUsuarioUseCase,
        IAtualizarEmailUsuarioUseCase atualizarEmailUsuarioUseCase)
    {
        _adicionarUsuarioUseCase = adicionarUsuarioUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
        _listarUsuarioUseCase = listarUsuarioUseCase;
        _obterUsuarioPorIdUseCase = obterUsuarioPorIdUseCase;
        _excluirUsuarioUseCase = excluirUsuarioUseCase;
        _atualizarEmailUsuarioUseCase = atualizarEmailUsuarioUseCase;
    }

    //[Authorize]
    [HttpPost("adicionar")]
    public async Task<ActionResult> Cadastrar(AdicionarUsuarioRequest adicionarUsuarioRequest)
    {
        await _adicionarUsuarioUseCase.Execute(adicionarUsuarioRequest);
        return Created();
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

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id, [FromQuery] string atualizadoPor)
    {
        await _excluirUsuarioUseCase.Execute(new ExcluirUsuarioRequest { Id = id, AtualizadoPor = atualizadoPor });
        return NoContent();
    }

    [HttpPatch("email")]
    public async Task<ActionResult> AtualizarEmail(AtualizarEmailUsuarioRequest atualizarEmailUsuarioRequest)
    {
        await _atualizarEmailUsuarioUseCase.Execute(atualizarEmailUsuarioRequest);
        return NoContent();
    }
}
