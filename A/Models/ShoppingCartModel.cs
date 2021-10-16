using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A.Models
{
    public class ShoppingCart
    {
        [Key]
        public int ID { get; set; }
        public virtual Product product { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}