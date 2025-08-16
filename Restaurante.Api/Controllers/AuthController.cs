using Microsoft.AspNetCore.Mvc;
using Restaurante.Borders.Dtos;
using Restaurante.Borders.UseCases;

namespace Restaurante.Api.Controllers;

[Route("api/[controller]")]
[ApiController]

public class AuthController : ControllerBase
{
    private readonly ILoginUsuarioUseCase loginUsuarioUseCase;

    public AuthController(ILoginUsuarioUseCase loginUsuarioUseCase)
    {
        this.loginUsuarioUseCase = loginUsuarioUseCase;
    }

    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(LoginUsuarioRequest loginUsuarioRequest)
    {
        var token = await loginUsuarioUseCase.Execute(loginUsuarioRequest);
        return Ok(token);
    }
}
