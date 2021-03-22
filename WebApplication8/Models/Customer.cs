using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication8.Models
{
    public class Customer
    {

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        [Required]
        [StringLength(400)]
        [Index(IsUnique = true)]
        [Display(Name = "Customer")]
        public string Name { get; set; }
        [StringLength(100)]
        public string Attention { get; set; }


        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        //[RegularExpression(@"^\(?([0-9]{2})[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number in formate 0x xxxxxxx")]
        public string tel { get; set; }

        [StringLength(15)]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})[-. ]?([0-9]{7})$", ErrorMessage = "Not a valid phone number in formate 0xx xxxxxxx")]
        public string mobile { get; set; }

        [StringLength(50)]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string email { get; set; }
        [StringLength(150)]
        public string Address { get; set; }

        public string City { get; set; }

        public string Emirate { get; set; }

        public string Country { get; set; }
    }
}