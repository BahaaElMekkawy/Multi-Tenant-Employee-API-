namespace EmployeeAPI.Tenancy
{
    public sealed class TenantService : ITenantService
    {
        public Guid TenantId { get; }

        private static readonly HashSet<Guid> ValidTenants = new()
        {
            Guid.Parse("c5547731-4c2b-4a12-8530-72b88f045956"),
            Guid.Parse("090697e6-7112-453f-88fe-740bcaad37bb")
        };

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
            if (!ValidTenants.Contains(tenantId))
                throw new UnauthorizedAccessException($"Tenant '{tenantId}' is not authorized or registered in this system.");

            TenantId = tenantId;
        }
    }
}
