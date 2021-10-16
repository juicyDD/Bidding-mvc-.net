using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A.Models;
using Microsoft.AspNet.Identity;

namespace A.Controllers
{
    public class BiddingHistoryController : Controller
    {
        private ApplicationDbContext mycontext = new ApplicationDbContext();
        // GET: BiddingHistory

        public ActionResult Bidding(int id)
        {

            long price = mycontext.Products.Find(id).CurrentPrice;
            BiddingHistory his = new BiddingHistory();
            var tuple = new Tuple<long, int, BiddingHistory>(price, id, his);
            return View(tuple);
        }
        
        public ActionResult GetBidHistory(int id)
        {
            using (mycontext)
            {
                List<BiddingHistory> biddingList = mycontext.BiddingHistories.Where(b => b.Product.ProductID == id).ToList<BiddingHistory>();
                //Response.Write("xxx"+biddingList.Count.ToString());
                //return Json(new { data = biddingList }, JsonRequestBehavior.AllowGet);
                var tuple = new Tuple<List<BiddingHistory>, int>(biddingList, id);
                return PartialView("GetBidHistory", tuple);

            }
        }
        [HttpPost]
        [Authorize]
        public ActionResult Bidding(FormCollection form)
        {


            int proid = Convert.ToInt32(form["productid"]);
            BiddingHistory myhis = new BiddingHistory();
            string userid = this.User.Identity.GetUserId();
            myhis.BiddingPrice = Convert.ToInt64(form["myprice"]);
            myhis.BiddingTime = DateTime.Now;
            myhis.BiddingUser = mycontext.Users.Find(this.User.Identity.GetUserId());
            myhis.Product = mycontext.Products.Find(proid);

            /*using (mycontext)
            {
                mycontext.BiddingHistories.Add(myhis);
                mycontext.SaveChangesAsync();
            }*/
            mycontext.Products.Find(myhis.Product.ProductID).CurrentPrice = myhis.BiddingPrice;
            mycontext.BiddingHistories.Add(myhis);
            mycontext.SaveChanges();

            //BiddingHistory his = new BiddingHistory(tuple.Item2, tuple.Item3, userid );
            //this.User.Identity.GetUserId();
            //return RedirectToAction("GetDetails","Product",his.Product.ProductID);
            return Redirect(Request.UrlReferrer.ToString());

        }





    }
}