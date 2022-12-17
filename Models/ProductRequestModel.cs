using System.ComponentModel.DataAnnotations;
using Zero.SharedKernel.Constants;

namespace ProductApis.Models
{
    public class ProductRequestModel
    {

        [Required(ErrorMessage = "Name Is Required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Name Is Required.")]
        public string name { get; set; }

        [Required(ErrorMessage = "Price Is Required.")]
        public int price { get; set; }

        [Required(ErrorMessage = "Quantity Is Required.")]
        public int quantity { get; set; }
   

  
    }
}
