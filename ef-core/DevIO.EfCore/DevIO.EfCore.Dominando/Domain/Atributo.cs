using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace DevIO.EfCore.Dominando.Domain;

[Table("TableAtributos")]
[Index(nameof(Descricao), nameof(Id), IsUnique = true)]
[Comment("Meu comentário de tabela")]
public class Atributo
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("MinhaDescricao", TypeName = "VARCHAR(100)")]
    public string Descricao { get; set; }
    
    [Required]
    [MaxLength(255)]
    public string Observacao { get; set; }
}

[Keyless]
public class SemChave
{
    public string Descricao { get; set; }
}