namespace ProductApis.Domain.Errors
{
    public class DeletedCartError : CartError
    {
        public DeletedCartError() : base("Cart products are Deleted. ")
        {

        }
        public DeletedCartError(string message) : base(message)
        {

        }
    }
}
