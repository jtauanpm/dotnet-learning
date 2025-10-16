using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Estado
{
    public int Id { get; set; }

    public string Nome { get; set; } = null!;

    public virtual ICollection<Cidade> Cidades { get; set; } = new List<Cidade>();

    public virtual Governador? Governador { get; set; }
}
