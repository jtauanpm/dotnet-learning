using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Ator
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<AtoresFilme> AtoresFilmes { get; set; } = new List<AtoresFilme>();
}
