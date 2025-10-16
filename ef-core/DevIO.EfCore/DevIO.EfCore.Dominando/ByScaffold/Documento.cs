using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Documento
{
    public int Id { get; set; }

    public string Cpf { get; set; } = null!;
}
