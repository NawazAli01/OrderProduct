using Zero.SharedKernel.Types.Result;

namespace ProductApis.Domain.Errors
{
    public class CustomerError : Error
    {
        public CustomerError(string message) : base(message)
        {

        }
    }
}
