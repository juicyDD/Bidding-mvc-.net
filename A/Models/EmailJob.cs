using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Quartz;
using System.Net;
using System.Net.Mail;
using A.Models;
using System.Threading.Tasks;

namespace A.Models
{
    public class EmailJob : Quartz.IJob
    {
        public void Execute(Quartz.IJobExecutionContext context)
        {
            
            var mycontext = new ApplicationDbContext();
            List<Product> expired = new List<Product>();
            List<BiddingHistory> winner = new List<BiddingHistory>();
            foreach (Product i in mycontext.Products)
            {
                if (i.EndTime.Date == DateTime.Today.AddDays(-1).Date)
                {
                    expired.Add(i);
                }
            }
            
            foreach(Product i in expired)
            {
                foreach (BiddingHistory y in mycontext.BiddingHistories)
                {
                    if (y.Product.ProductID == i.ProductID && y.BiddingPrice == i.CurrentPrice)
                        winner.Add(y);
                }
            }    
            if (winner.Count > 0) foreach (BiddingHistory z in winner)
            {
                
                var senderEmail = new MailAddress("baroisieunac@gmail.com", "Bidding Result");
                var receiverEmail = new MailAddress(z.BiddingUser.Email, "Receiver");
                var sendtoOwner = new MailAddress(z.Product.Owner.Email, "Owner");
                using (var message2 = new MailMessage(senderEmail, sendtoOwner))
                    {

                        message2.Subject = "Result of your auction";
                        message2.Body = z.BiddingUser.Email+ " is the one who has bidded the highest price for your " + z.Product.ProductName + ". Please contact this bidder via " +
                            z.BiddingUser.Email;
                        using (SmtpClient client = new SmtpClient
                        {
                            EnableSsl = true,
                            Host = "smtp.gmail.com",
                            Port = 587,
                            DeliveryMethod = SmtpDeliveryMethod.Network,
                            UseDefaultCredentials = false,
                            Credentials = new NetworkCredential("baroisieunac@gmail.com", "fypgqyflcqkfzpiq") //pw

                        })
                        {
                            client.Send(message2);
                        }
                    }
                    //using (var message = new MailMessage("baroisieunac@gmail.com", z.BiddingUser.Email))
                    using (var message = new MailMessage(senderEmail, receiverEmail))
                {
                    
                    message.Subject = "You're the winner of the auction";
                    message.Body = "You're the one who has submitted the highest bid for buying " + z.Product.ProductName +". Please contact the owner of this product via " +
                        z.Product.Owner.Email + " to purchase. We're really done, thank you : D";
                    using (SmtpClient client = new SmtpClient
                    {
                        EnableSsl = true,
                        Host = "smtp.gmail.com",
                        Port = 587,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential("baroisieunac@gmail.com", "fypgqyflcqkfzpiq") //pw

                    })
                    {
                        client.Send(message);
                    }
                }
                
            }
        }
        

        
    }
}