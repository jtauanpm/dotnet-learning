using System.ComponentModel.DataAnnotations.Schema;

namespace DevIO.EfCore.Dominando.Domain;

public class Aeroporto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    [NotMapped]
    public string PropriedadeTeste { get; set; }
    
    [InverseProperty("AeroportoPartida")]
    public ICollection<Voo> VoosDePartida { get; set; }
    
    [InverseProperty("AeroportoChegada")]
    public ICollection<Voo> VoosDeChegada { get; set; }
}

public class Voo
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public Aeroporto AeroportoPartida { get; set; }
    public Aeroporto AeroportoChegada { get; set; }
}