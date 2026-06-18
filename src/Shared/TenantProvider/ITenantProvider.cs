namespace Shared.TenantProvider
{
    public interface ITenantProvider
    {
        Guid TenantId { get; }
    }
}
