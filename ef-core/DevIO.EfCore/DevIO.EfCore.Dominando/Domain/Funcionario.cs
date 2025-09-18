namespace DevIO.EfCore.Dominando.Domain;

public class Funcionario
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CPF { get; set; }
    public string RG { get; set; }
    public int DepartamentoId { get; set; }
    public ContractType ContractType { get; set; }
    public Gender Gender { get; set; }
    public virtual Departamento Departamento { get; set; }
    
    public override string ToString()
    {
        return $"Nome: {Nome}, CPF: {CPF}, RG: {RG}, DepartamentoId: {DepartamentoId}, TipoContrato: {ContractType}, Sexo: {Gender}";
    }
}

public enum ContractType
{
    CLT = 0,
    PJ = 1, 
    INTERN = 2
}

public enum Gender
{
    Female = 0,
    Male = 0,
}