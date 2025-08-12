namespace DevIO.PrimeiraApi.App.DTOs;

public class JwtSettings
{
    // Chave secreta que irá assinar o token
    public string? Segredo { get; set; }
    
    // Tempo de validade do token
    public int ExpiracaoHoras { get; set; }
    
    // Aplicação que emitiu o token
    public string? Emissor { get; set; }
    
    // Onde o token é válido, pode especificar uma aplicação
    public string? Audiencia { get; set; }
}