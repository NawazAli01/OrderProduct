namespace ProductApis.Domain.Errors
{
    public class DeletedCustomerError : CustomerError
    {
        public DeletedCustomerError() : base("Customer Is Deleted. ")
        {

        }
        public DeletedCustomerError(string message) : base(message)
        {

        }
    }
}
