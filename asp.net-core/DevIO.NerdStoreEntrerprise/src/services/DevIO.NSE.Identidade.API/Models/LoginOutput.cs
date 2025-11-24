namespace DevIO.NSE.Identidade.API.Models;

public class LoginOutput
{
    public string AccessToken { get; set; }
    public double ExpiresIn { get; set; }
    public UserToken UsuarioToken { get; set; }
}