using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Endereco
{
    public long ClienteId { get; set; }

    public string Logradouro { get; set; } = null!;

    public string Bairro { get; set; } = null!;

    public string Cidade { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public virtual Cliente Cliente { get; set; } = null!;
}
