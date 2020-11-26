using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public enum CreditTermList { Cash, Credit };
   

    public class supplier
    {
        [Display(Name = "Supplier")]
        public int supplierId { get; set; }

        [Required]
        [Display(Name = "Supplier")]
        public string Name { get; set; }

        public string Attention { get; set; }
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number in formate 0x xxxxxxx")]
        public string tel { get; set; }

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number in formate 0xx xxxxxxx")]
        public string mobile { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }

        public string Address { get; set; }

        public CreditTermList CreditTerm { get; set; } 

        public bool PickUpOrder;








    }
}