using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace A.Models
{
    public class BiddingHistory
    {
        [Key] public int BiddingID { get; set; }
        
        public DateTime BiddingTime { get; set; }
        public long BiddingPrice { get; set; }
        public virtual Product Product {get;set;}
        public virtual ApplicationUser BiddingUser { get; set; }
        
       /* public BiddingHistory(long bidprice, int proid, string userid)
        {
            this.BiddingTime = DateTime.Now;
            this.BiddingPrice = bidprice;
            ApplicationDbContext context = new ApplicationDbContext();
            this.Product = context.Products.Find(proid);
            this.BiddingUser = context.Users.Find(userid);
        }*/
    }
}