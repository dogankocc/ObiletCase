using ObiletService.Core.Domain.Seedwork;

namespace ObiletService.Core.Domain.ValueObjects
{
    public class Price : ValueObject
    {
        public decimal Amount { get; }
        public string Currency { get; }

        public Price(decimal amount, string currency)
        {
            Amount = amount;
            Currency = currency;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return Currency;
        }
    }
}
