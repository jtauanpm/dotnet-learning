using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Funcionario
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public string Cpf { get; set; } = null!;

    public string Rg { get; set; } = null!;

    public int DepartamentoId { get; set; }

    public string ContractType { get; set; } = null!;

    public string Gender { get; set; } = null!;

    public virtual Departamento Departamento { get; set; } = null!;
}
