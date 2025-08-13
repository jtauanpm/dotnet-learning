using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DevIO.PrimeiraApi.App.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace DevIO.PrimeiraApi.App.Controllers;

[ApiController]
[Route("api/conta")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, 
        IOptions<JwtSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }

    [HttpPost("registrar")]
    public async Task<ActionResult> Registrar(RegisterUserViewModel registerUser)
    {
        var user = new IdentityUser
        {
            UserName = registerUser.Email,
            Email = registerUser.Email,
            EmailConfirmed = true
        };
        
        var result = await _userManager.CreateAsync(user, registerUser.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            return Ok(GerarJwt(user.Email));
        }
        
        return Problem("Falha ao registrar usuário");
    }
    
    [HttpPost("login")]
    public async Task<ActionResult> Login(LoginUserViewModel loginUser)
    {
        var result = await _signInManager.PasswordSignInAsync(loginUser.Email, loginUser.Password, false, true);

        return result.Succeeded 
            ? Ok(await GerarJwt(loginUser.Email)) 
            : Problem("Usuário ou senha incorretos");
    }

    private async Task<string> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim> { new(ClaimTypes.Name, user.UserName) };
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });
        
        var encodedToken = tokenHandler.WriteToken(token);
        return encodedToken;
    }
}