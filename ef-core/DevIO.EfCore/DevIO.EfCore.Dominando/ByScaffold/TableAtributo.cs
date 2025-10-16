using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

/// <summary>
/// Meu comentário de tabela
/// </summary>
public partial class TableAtributo
{
    public int Id { get; set; }

    public string MinhaDescricao { get; set; } = null!;

    public string Observacao { get; set; } = null!;
}
