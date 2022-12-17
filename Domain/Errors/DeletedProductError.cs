namespace ProductApis.Domain.Errors
{
    public class DeletedProductError : ProductError
    {
        public DeletedProductError() : base("Product Is Deleted. ")
        {

        }
        public DeletedProductError(string message) : base(message)
        {

        }
    }
}