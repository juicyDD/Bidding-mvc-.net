using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A.Models
{
    public class ProductOnView
    {
        [DisplayName("Product name")][Required]
        public string ProductName { get; set; }
        public string Description { get; set; }
        [DisplayName("Start price")] [Required]
        public long StartPrice { get; set; }
        [DisplayName("End time")][Required]
        public DateTime EndTime { get; set; }
        [DisplayName("Upload product images")]
        public List<string> imgname { get; set; }
        [DisplayName("Product Images")]
        public List<HttpPostedFileBase> img { get; set; }
        public string OwnerId { get; set; }
    }
}