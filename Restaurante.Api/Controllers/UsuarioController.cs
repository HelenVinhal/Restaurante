using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.UseCases;

namespace API.Controllers;


[Route("api/[controller]")]
[ApiController]
[Authorize]

public class UsuarioController : ControllerBase
{
    private readonly IAdicionarUsuarioUseCase _adicionarUsuarioUseCase;
    private readonly IListarUsuariosUseCase _listarUsuarioUseCase;
    private readonly IObterUsuarioPorIdUseCase _obterUsuarioPorIdUseCase;
    private readonly IExcluirUsuarioUseCase _excluirUsuarioUseCase;
    private readonly IAtualizarEmailUsuarioUseCase _atualizarEmailUsuarioUseCase;
    private readonly IAtualizarSenhaUsuarioUseCase _atualizarSenhaUsuarioUseCase;


    public UsuarioController(
        IAdicionarUsuarioUseCase adicionarUsuarioUseCase,
        IListarUsuariosUseCase listarUsuarioUseCase,
        IObterUsuarioPorIdUseCase obterUsuarioPorIdUseCase,
        IExcluirUsuarioUseCase excluirUsuarioUseCase,
        IAtualizarEmailUsuarioUseCase atualizarEmailUsuarioUseCase,
        IAtualizarSenhaUsuarioUseCase atualizarSenhaUsuarioUseCase)
    {
        _adicionarUsuarioUseCase = adicionarUsuarioUseCase;
        _listarUsuarioUseCase = listarUsuarioUseCase;
        _obterUsuarioPorIdUseCase = obterUsuarioPorIdUseCase;
        _excluirUsuarioUseCase = excluirUsuarioUseCase;
        _atualizarEmailUsuarioUseCase = atualizarEmailUsuarioUseCase;
        _atualizarSenhaUsuarioUseCase = atualizarSenhaUsuarioUseCase;
    }

    [HttpPost("adicionar")]
    public async Task<ActionResult> Cadastrar(AdicionarUsuarioRequest adicionarUsuarioRequest)
    {
        await _adicionarUsuarioUseCase.Execute(adicionarUsuarioRequest);
        return Created();
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
    [Authorize]
    public async Task<ActionResult> AtualizarEmail(AtualizarEmailUsuarioRequest atualizarEmailUsuarioRequest)
    {
        await _atualizarEmailUsuarioUseCase.Execute(atualizarEmailUsuarioRequest);
        return NoContent();
    }

    [HttpPatch("{id:int}/senha")]
    [Authorize]
    public async Task<ActionResult> AtualizarSenha(int id, AtualizarSenhaUsuarioRequest atualizarSenhaUsuarioRequest)
    {
        await _atualizarSenhaUsuarioUseCase.Execute((id, atualizarSenhaUsuarioRequest));
        return NoContent();
    }
}
