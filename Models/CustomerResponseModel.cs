namespace ProductApis.Models
{
    public class CustomerResponseModel
    {
        public int customerId { get; set; }
        public string name { get; set; }
        public string? mobileNumber { get; set; }
        public string? emailAddress { get; set; }
    }
}

