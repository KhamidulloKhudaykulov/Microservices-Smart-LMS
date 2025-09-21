namespace Identity.Application.Interfaces;

public interface ITokenService
{
    string GenerateToken(string username, List<string> permissions);
}
