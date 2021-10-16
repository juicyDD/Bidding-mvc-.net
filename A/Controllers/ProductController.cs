using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using A.Models;
using Microsoft.AspNet.Identity;

namespace A.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext mycontext = new ApplicationDbContext();
        // GET: Product
        [Authorize]
        public ActionResult MyProduct()
        {
            string userid = this.User.Identity.GetUserId();
            List<Product> myproduct = mycontext.Products.Where(m=>m.Owner.Id==userid).ToList();
            List<ProductImage> myimg = new List<ProductImage>();
            /*foreach(Product bono in myproduct)
            {
                foreach (ProductImage nhi in mycontext.ProductImages.ToList())
                {
                    if (bono.ProductID == nhi.Product.ProductID)
                    {
                        myimg.Add(nhi);
                        break;
                    }
                }
            }*/
            myimg = GetListThumbnail(myproduct);
            Tuple<List<Product>, List<ProductImage>,string> tuple = new Tuple<List<Product>, List<ProductImage>,string>(myproduct, myimg,"");
            return View(tuple);
        }
       
        public ActionResult GetCurrentPrice(int id)
        {
            using(mycontext)
            {
                long price = mycontext.Products.Where(m => m.ProductID == id).FirstOrDefault().CurrentPrice;
                Tuple<long> tuple = new Tuple<long>(price);
                return PartialView("GetCurrentPrice", tuple);
            }
        }
        [HttpPost]
        public ActionResult MyProduct(string name)
        {
            //string name = myform["name"].ToString();
            //List<Product> all = mycontext.Products.ToList();
            string userid = this.User.Identity.GetUserId();
            List<Product> all = mycontext.Products.Where(m => m.Owner.Id == userid).ToList();
            List<Product> result = new List<Product>();
            foreach(Product jennie in all)
            {
                try
                {
                    if (jennie.ProductName!=null && jennie.ProductName.ToString().ToLower().Contains(name.ToLower()))
                    {
                        result.Add(jennie);
                    }
                }
                catch(Exception nhi) { }
            }
            List<ProductImage> resimg = new List<ProductImage>();
            resimg = GetListThumbnail(result);
            Tuple<List<Product>, List<ProductImage>,string> tuple2 = new Tuple<List<Product>, List<ProductImage>,string>(result, resimg,name);
            return View(tuple2);
        }
        public List<ProductImage> GetListThumbnail(List<Product> myproduct)
        {
            List<ProductImage> myimg = new List<ProductImage>();
            foreach (Product bono in myproduct)
            {
                foreach (ProductImage nhi in mycontext.ProductImages.ToList())
                {
                    if (bono.ProductID == nhi.Product.ProductID)
                    {
                        myimg.Add(nhi);
                        break;
                    }
                }
            }
            return myimg;
        }
        [Authorize]
        public ActionResult DeleteProduct(int id)
        {
            List<BiddingHistory> history = mycontext.BiddingHistories.ToList();
            foreach (BiddingHistory i in history)
            {
                if (i.Product.ProductID == id)
                {
                    mycontext.Entry(i).State = System.Data.Entity.EntityState.Deleted;
                    mycontext.SaveChanges();
                }
            }
            List<ShoppingCart> carts = mycontext.ShoppingCarts.ToList();
            foreach (ShoppingCart bo in carts)
            {
                if (bo.product.ProductID == id)
                {
                    mycontext.Entry(bo).State = System.Data.Entity.EntityState.Deleted;
                    mycontext.SaveChanges();
                }
            }
            
            List<ProductImage> imgs = mycontext.ProductImages.ToList();
            foreach (ProductImage no in imgs)
            {
                if (no.Product.ProductID == id)
                {
                    mycontext.Entry(no).State = System.Data.Entity.EntityState.Deleted;
                    mycontext.SaveChanges();
                }
            }
            Product bono = mycontext.Products.Find(id);
            mycontext.Entry(bono).State = System.Data.Entity.EntityState.Deleted;
            mycontext.SaveChanges();
            return Redirect(Request.UrlReferrer.ToString());
            //return Json();
        }
        public ActionResult GetDetails(int id)
        {
            
            List<Product> x = mycontext.Products.Where(b => b.ProductID == id).Select(b => b).ToList<Product>();
            Product mypro = x[0];
            List<string> imgcollection = new List<string>();
            using (mycontext)
            {
                imgcollection = mycontext.ProductImages.Where(b => b.Product.ProductID == id).Select(b => b.ImageURL).ToList<string>();
            }
            var tuple = new Tuple<Product, List<string>>(mypro, imgcollection);
            return View(tuple);
        }
        
        public void ConvertToProduct(ProductOnView nhiprovjp2k1)
        {
            Product myproduct = new Product();
            myproduct.ProductName = nhiprovjp2k1.ProductName;
            myproduct.StartPrice = nhiprovjp2k1.StartPrice;
            myproduct.CurrentPrice = nhiprovjp2k1.StartPrice;
            myproduct.EndTime = nhiprovjp2k1.EndTime;
            myproduct.Owner = mycontext.Users.Find(nhiprovjp2k1.OwnerId);
            myproduct.Description = nhiprovjp2k1.Description;
            mycontext.Products.Add(myproduct);
            mycontext.SaveChanges();
            mycontext.Entry(myproduct).GetDatabaseValues();
            int id = myproduct.ProductID;
            mycontext.Entry(myproduct).State = System.Data.Entity.EntityState.Detached;
            //mycontext.SaveChanges();

            int count = nhiprovjp2k1.imgname.Count;
            //ApplicationDbContext context2 = new ApplicationDbContext();
            for(int b=0;b<count;b++)
            {
                
                ProductImage newimg = new ProductImage();
                newimg.Product = new Product();
                newimg.Product = mycontext.Products.Find(id);
                //newimg.Product.ProductID = id;
                newimg.ImageURL = nhiprovjp2k1.imgname[b].ToString();
                mycontext.ProductImages.Add(newimg);
                mycontext.SaveChanges();
            }
        }
       /* [HttpGet]
        public ActionResult UploadProduct(HttpPostedFileBase uyennhi, FormCollection form)
        {
            try
            {
                if (uyennhi.ContentLength > 0)
                {
                    string _FileName = Path.GetFileName(uyennhi.FileName);
                    string _path = Path.Combine(Server.MapPath("~/Images"), _FileName);
                    uyennhi.SaveAs(_path);
                }
                ViewBag.Message = "File Uploaded Successfully!!";
                return View();
            }
            catch
            {
                ViewBag.Message = "File upload failed!!";
                return View();
            }
        }*/
    }
}