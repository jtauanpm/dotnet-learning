using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("Products")]
public class Product
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; }
}