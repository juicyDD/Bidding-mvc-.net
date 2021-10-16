using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A.Models
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public long StartPrice { get; set; }
        public long CurrentPrice { get; set; }
        public DateTime EndTime { get; set; }
        public virtual ApplicationUser Owner { get; set; }
    }
}