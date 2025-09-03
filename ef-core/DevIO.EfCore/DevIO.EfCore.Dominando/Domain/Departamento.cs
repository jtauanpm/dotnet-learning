using Microsoft.EntityFrameworkCore.Infrastructure;

namespace DevIO.EfCore.Dominando.Domain;

public class Departamento
{
    public int Id { get; set; }
    public string Descricao { get; set; }
    public bool Ativo { get; set; }
    public bool Excluido { get; set; }
    public List<Funcionario> Funcionarios { get; set; }

    public override string ToString()
    {
        return $"Id: {Id}, Descricao: {Descricao} \t Excluido: {Excluido}";
    }

    #region Lazy loading configurations

    // public virtual List<Funcionario> Funcionarios { get; set; }
    // public Departamento()
    // {
    // }
    //
    // private ILazyLoader _lazyLoader { get; set; }
    // private Departamento(ILazyLoader lazyLoader)
    // {
    //     _lazyLoader = lazyLoader;
    // }
    //
    // private List<Funcionario>? _funcionarios;
    // public List<Funcionario>? Funcionarios
    // {
    //     get => _lazyLoader.Load(this, ref _funcionarios);
    //     set => _funcionarios = value;
    // }
    
    /*
    private Action<object, string> _lazyLoader { get; set; }
    private Departamento(Action<object, string> lazyLoader)
    {
        _lazyLoader = lazyLoader;
    }

    private List<Funcionario> _funcionarios;
    public virtual List<Funcionario> Funcionarios
    {
        get
        {
            _lazyLoader?.Invoke(this, nameof(Funcionarios));
            return _funcionarios;
        }

        set => _funcionarios = value;
    }
    */

    #endregion
}