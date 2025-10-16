using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Configuraco
{
    public int Id { get; set; }

    public string Chave { get; set; } = null!;

    public string Valor { get; set; } = null!;
}
