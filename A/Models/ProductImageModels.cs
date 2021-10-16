using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A.Models
{
    public class ProductImage
    {
        [Key] public string ImageURL { get; set; }
        public virtual Product Product { get; set; }
    }
}