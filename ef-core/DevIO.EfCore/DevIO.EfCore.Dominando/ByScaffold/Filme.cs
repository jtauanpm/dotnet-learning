using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Filme
{
    public int Id { get; set; }

    public string Descricao { get; set; } = null!;

    public virtual ICollection<AtoresFilme> AtoresFilmes { get; set; } = new List<AtoresFilme>();
}
