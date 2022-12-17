using Zero.SharedKernel.Types.Result;

namespace ProductApis.Domain.Errors
{
    public class CartError : Error
    {
        public CartError(string message) : base(message)
        {

        }
    }
}
