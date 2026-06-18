namespace EmployeeAPI.Tenancy
{
    public sealed class TenantService : ITenantService
    {
        public Guid TenantId { get; }

        public TenantService(IHttpContextAccessor httpContextAccessor)
        {
            var httpContext = httpContextAccessor.HttpContext;

            if (httpContext == null)
            {
                // design-time (migrations)
                TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001");
                return;
            }

            var tenantHeader = httpContextAccessor.HttpContext?
                .Request.Headers["X-Tenant-Id"]
                .FirstOrDefault();

            if (string.IsNullOrWhiteSpace(tenantHeader))
                throw new UnauthorizedAccessException(
                    "X-Tenant-Id header is required.");

            if (!Guid.TryParse(tenantHeader, out var tenantId))
                throw new UnauthorizedAccessException(
                    "Invalid X-Tenant-Id.");

            TenantId = tenantId;
        }
    }
}
