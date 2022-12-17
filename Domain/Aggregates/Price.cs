using System.Text.RegularExpressions;
using Zero.AspNetCoreServiceProjectExample.Domain;
using Zero.SeedWorks;
using Zero.SharedKernel.Constants;
using Zero.SharedKernel.Types.Result;

namespace ProductApis.Domain.Aggregates.ProductAggregate
{
    public class Price : ValueObject
    {
        public int Value { get; }
        private Price(int value)
        {
            Value = value;
        }

        public static Result<Price> Create(int value)
        {   //Check if value of , 
            if (value < 1)
            {
                return Result.Failure<Price>("Value should not be less than One");
            }
            return Result.Success(new Price(value));//if all above conditions are false value passed to the private constructor
        }
        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
        public static implicit operator int(Price price)
        {
            return price.Value;
        }
        public static explicit operator Price(int price)
        {
            return Create(price).Value;
        }
    }
}
