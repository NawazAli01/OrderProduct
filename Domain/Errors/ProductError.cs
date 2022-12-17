using Zero.SharedKernel.Types.Result;

namespace ProductApis.Domain.Errors
{
    public class ProductError : Error
    {
        public ProductError(string message) : base(message)
        {

        }
    }
}