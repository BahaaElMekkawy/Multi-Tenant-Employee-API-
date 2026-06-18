namespace EmployeeAPI.Employees.Dtos
{
    public record MoneyDto
    {
        public int AmountMinor { get; init; }
        public string CurrencyCode { get; init; } = default!;
    }
}
