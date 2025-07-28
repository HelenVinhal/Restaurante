namespace Restaurante.Api.Services;
public interface IJwtTokenService
{
    Task<string> GenerateToken(int userId);
}
