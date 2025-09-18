using DevIO.EfCore.Dominando.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DevIO.EfCore.Dominando.Converters;

public class ConversorCustomizado : ValueConverter<Gender, string>
{
    public ConversorCustomizado() : base(
        gender => ConverterParaBancoDeDados(gender), 
        value => ConverterParaApplicacao(value),
        new ConverterMappingHints(1))
    {
        
    }
    
    static string ConverterParaBancoDeDados(Gender gender)
    {
        return gender.ToString()[..1];
    }
    
    static Gender ConverterParaApplicacao(string value)
    {
        return Enum
            .GetValues<Gender>()
            .FirstOrDefault(g => g.ToString()[..1] == value);
    }
}