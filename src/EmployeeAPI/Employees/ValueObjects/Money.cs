namespace EmployeeAPI.Shared.ValueObjects
{
    public record Money
    {
        public int AmountMinor { get; }
        public string CurrencyCode { get; }

        protected Money() { }

        private Money(int amountMinor, string currencyCode)
        {
            AmountMinor = amountMinor;
            CurrencyCode = currencyCode;
        }

        public static Money Of(int amountMinor, string currencyCode)
        {
            if (amountMinor < 0)
                throw new ArgumentException("Amount cannot be negative");

            ArgumentException.ThrowIfNullOrWhiteSpace(currencyCode);

            if (currencyCode.Length != 3)
                throw new ArgumentException("Currency must be 3 letters (e.g. USD, EGP)");

            return new Money(amountMinor, currencyCode.ToUpper());
        }
    }
}