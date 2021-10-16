using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A.Models;
using Microsoft.AspNet.Identity;

namespace A.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ApplicationDbContext mycontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            string userid = this.User.Identity.GetUserId();
            List<ShoppingCart> myshoppingcart = mycontext.ShoppingCarts.Where(m => m.user.Id == userid).ToList();
            List<Product> myproduct = new List<Product>();
            List<ProductImage> myimg = new List<ProductImage>();
            foreach(ShoppingCart i in myshoppingcart)
            {
                foreach(Product j in mycontext.Products.ToList())
                {
                    if (i.product.ProductID == j.ProductID)
                    {
                        myproduct.Add(j);
                        break;
                    }
                }
                foreach(ProductImage k in mycontext.ProductImages.ToList())
                {
                    if(i.product.ProductID == k.Product.ProductID)
                    {
                        myimg.Add(k);
                        break;
                    }
                }
            }
            Tuple<List<Product>, List<ProductImage>> tuple = new Tuple<List<Product>, List<ProductImage>>(myproduct, myimg);
            return View(tuple);
        }
        public ActionResult DeleteCartItem(int id)
        {
            string userid = this.User.Identity.GetUserId();
            ShoppingCart item = mycontext.ShoppingCarts.Where(m => m.product.ProductID == id && m.user.Id == userid).ToList()[0];
            if (item.user.Id == null) return Redirect(Request.UrlReferrer.ToString());
            else
            {
                mycontext.Entry(item).State = System.Data.Entity.EntityState.Deleted;
                mycontext.SaveChanges();
                return Redirect(Request.UrlReferrer.ToString());
            }
        }
        [Authorize]
        public ActionResult AddToCart(int id)
        {
            string userid = this.User.Identity.GetUserId();
            if (mycontext.ShoppingCarts.Where(m => m.product.ProductID == id && m.user.Id == userid).ToList().Count > 0)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else
            {
                ShoppingCart addingitem = new ShoppingCart();
                addingitem.user = mycontext.Users.Find(userid);
                addingitem.product = mycontext.Products.Find(id);
                mycontext.ShoppingCarts.Add(addingitem);
                mycontext.SaveChanges();

                return Redirect(Request.UrlReferrer.ToString());
            }

        }
    }
}