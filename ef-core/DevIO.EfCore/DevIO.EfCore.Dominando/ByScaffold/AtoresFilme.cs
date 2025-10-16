using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class AtoresFilme
{
    public int AtorId { get; set; }

    public int FilmeId { get; set; }

    public DateTime CadastradoEm { get; set; }

    public virtual Ator Ator { get; set; } = null!;

    public virtual Filme Filme { get; set; } = null!;
}
