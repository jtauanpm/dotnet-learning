using System;
using System.Collections.Generic;

namespace DevIO.EfCore.Dominando.ByScaffold;

public partial class Departamento
{
    public int Id { get; set; }

    public string Descricao { get; set; } = null!;

    public bool Ativo { get; set; }

    public bool Excluido { get; set; }

    public DateTime UltimaAtualizacao { get; set; }

    public virtual ICollection<Funcionario> Funcionarios { get; set; } = new List<Funcionario>();
}
