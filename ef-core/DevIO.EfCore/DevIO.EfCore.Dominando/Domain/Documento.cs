namespace DevIO.EfCore.Dominando.Domain;

public class Documento
{
    private string _cpf;

    public void SetCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            throw new ArgumentNullException(nameof(cpf));
        }
        
        _cpf = cpf;
    }
    
    public string Cpf => _cpf;

    public string GetCpf()
    {
        return _cpf;
    }
}