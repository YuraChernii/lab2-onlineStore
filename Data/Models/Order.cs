using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Tymchak_shop.Data.Models
{
    public class Order
    {
        [BindNever]
        public int id { get; set; }
        [Display(Name = "Name")]

        [StringLength(20,MinimumLength = 3, ErrorMessage = "The length of the tape should be more than 3 characters")]
        
        public string name { get; set; }
        [Display(Name = "Surname")]
        [StringLength(20, MinimumLength = 3, ErrorMessage = "The length of the tape should be more than 3 characters")]
        public string surname { get; set; }
        [Display(Name = "Adress")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "The length of the tape should be more than 3 characters")]
        public string adress { get; set; }
        [Display(Name = "Phone")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "The length of the tape should be more than 9 characters")]
        public string phone { get; set; }
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(30, MinimumLength = 3, ErrorMessage = "The address is entered incorrectly")]
        public string email { get; set; }
       [BindNever]
       [ScaffoldColumn(false)]
        public DateTime orderTime { get; set; }
        public List<OrderDetail> orederDetails { get; set; }
    }
}
