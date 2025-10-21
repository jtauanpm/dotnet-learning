namespace DevIO.EfCore.Dominando.MultiTenancy.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public string TenantId { get; set; }
}