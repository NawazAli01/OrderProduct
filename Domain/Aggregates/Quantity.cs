//using System.Text.RegularExpressions;
using System.Text.RegularExpressions;
using Zero.AspNetCoreServiceProjectExample.Domain;
using Zero.SeedWorks;
using Zero.SharedKernel.Constants;
using Zero.SharedKernel.Types.Result;

namespace ProductApis.Domain.Aggregates
{
    public class Quantity : ValueObject
    {
        public int Value { get; }
        private Quantity(int value)
        {
            Value = value;
        }

        public static Result<Quantity> Create(int value)
        {   //Check if value is less than one 
            if (value < 1)
            {
                return Result.Failure<Quantity>("Value should not be less than Zero");
            }
            return Result.Success(new Quantity(value));
            
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator int(Quantity quantity)
        {
            return quantity.Value;
        }
        public static explicit operator Quantity(int quantity)
        {
            return Create(quantity).Value;
        }
    }
}
