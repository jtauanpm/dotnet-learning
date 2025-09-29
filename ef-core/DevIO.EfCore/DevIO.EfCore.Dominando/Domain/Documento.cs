using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Domain;

public class Documento
{
    public int Id { get; set; }
    private string _cpf;

    [BackingField(nameof(_cpf))]
    public string Cpf => _cpf;
    public void SetCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
        {
            throw new ArgumentNullException(nameof(cpf));
        }
        
        _cpf = cpf;
    }

    public string GetCpf()
    {
        return _cpf;
    }
}