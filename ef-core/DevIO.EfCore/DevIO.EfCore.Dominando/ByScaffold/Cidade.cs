using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Cidade
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public int? EstadoId { get; set; }

    public virtual Estado? Estado { get; set; }
}
