namespace DevIO.NSE.Identidade.API.Configuration;

public class AppSettings
{
    public string Segredo { get; set; }
    public int ExpiracaoHoras { get; set; }
    public string Emissor { get; set; }
    public string Audiencia { get; set; }
}