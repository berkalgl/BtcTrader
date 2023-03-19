using Instruction.Domain.SeedWork;

namespace Instruction.Domain.AggregatesModel
{
    public class Price : ValueObject
    {
        public decimal Amount { get; private set; }
        // currency could be implemented here
        internal Price()
        {
            
        }
        public Price(decimal amount) => Amount = amount;
    }
}
