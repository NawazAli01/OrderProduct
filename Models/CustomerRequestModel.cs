using System.ComponentModel.DataAnnotations;
using Zero.SharedKernel.Constants;
using ProductApis.Domain.Aggregates.CustomerAggregate;

namespace ProductApis.Models
{
    public class CustomerRequestModel
    {

        [Required(ErrorMessage = "Name Is Required.")]
        public string? Name { get; set; }

        [RegularExpression(RegexPatterns.MobileNumber, ErrorMessage = "Mobile Number Is Invalid.")]
        public string? MobileNumber { get; set; }

        [RegularExpression(RegexPatterns.EmailAddress, ErrorMessage = "Email Address Is Invalid.")]
        public string? EmailAddress { get; set; }

    }
}
