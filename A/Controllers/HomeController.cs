using A.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace A.Controllers
{
    public class HomeController : Controller
    {
        //DataTable dt = new DataTable();
        private ApplicationDbContext mycontext = new ApplicationDbContext();
        public ActionResult Index()
        {
            List<Product> myproduct = mycontext.Products.ToList<Product>();
            List<ProductImage> myimage = mycontext.ProductImages.ToList<ProductImage>();
            List<ProductImage> finalimage = new List<ProductImage>();
            for(int j = 0; j < myproduct.Count; j++)
            {
                int i = 0;
                while ( i < myimage.Count)
                {
                    if (myimage[i].Product.ProductID == myproduct[j].ProductID)
                    {
                        finalimage.Add(myimage[i]);
                        break;
                    }
                    else i++;
                }    
            }
            var tuple = new Tuple<List<Product>, List<ProductImage>,string>(myproduct,finalimage,"");
            //using (var context = new ApplicationDbContext())
            // return View(finalimage);
            return View(tuple);
        }
        [HttpPost]
        public ActionResult Index(string name)
        {
            List<Product> all = mycontext.Products.ToList();
            List<Product> result = new List<Product>();
            foreach (Product jennie in all)
            {
                if (jennie.ProductName!=null && jennie.ProductName.ToString().ToLower().Contains(name.ToLower()))
                {
                    result.Add(jennie);
                }
            }
            List<ProductImage> resimg = new List<ProductImage>();
            using(ProductController x = new ProductController())
            {
                resimg = x.GetListThumbnail(result);
            }
            Tuple<List<Product>, List<ProductImage>,string> tuple2 = new Tuple<List<Product>, List<ProductImage>,string>(result, resimg,name);
            return View(tuple2);
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SortedByName(string name)
        {
            List<Product> all = mycontext.Products.ToList();
            List<Product> result = new List<Product>();
            if (name != null) foreach (Product jennie in all)
                {
                    if (jennie.ProductName!=null&&jennie.ProductName.ToString().ToLower().Contains(name.ToLower()))
                    {
                        result.Add(jennie);
                    }
                }
            else result = all;
            int count = result.Count();
            for(int i=0;i<count-1;i++)
            {
                for(int j=i+1;j<count;j++)
                {
                    if (result[i].ProductName!=null && result[j].ProductName!=null &&result[i].ProductName.ToString().CompareTo(result[j].ProductName.ToString())>0)
                    {
                        Product temp = new Product();
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            List<ProductImage> resimg = new List<ProductImage>();
            using (ProductController x = new ProductController())
            {
                resimg = x.GetListThumbnail(result);
            }
            string type = "name";
            
            Tuple<List<Product>, List<ProductImage>, string,string> tuple2 = new Tuple<List<Product>, List<ProductImage>, string,string>(result, resimg, name,type);
            //Tuple<List<Product>, List<ProductImage>> tuple = new Tuple<List<Product>, List<ProductImage>>(result, resimg);
            return View(tuple2);

        }
        public ActionResult SortedByPrice(string name)
        {
            List<Product> all = mycontext.Products.ToList();
            List<Product> result = new List<Product>();
            if (name != null) foreach (Product jennie in all)
                {
                    if (jennie.ProductName!=null &&jennie.ProductName.ToString().ToLower().Contains(name.ToLower()))
                    {
                        result.Add(jennie);
                    }
                }
            else result = all;
            int count = result.Count();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (result[i].CurrentPrice < result[j].CurrentPrice)
                    {
                        Product temp = new Product();
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            List<ProductImage> resimg = new List<ProductImage>();
            using (ProductController x = new ProductController())
            {
                resimg = x.GetListThumbnail(result);
            }
            string type = "name";

            Tuple<List<Product>, List<ProductImage>, string, string> tuple2 = new Tuple<List<Product>, List<ProductImage>, string, string>(result, resimg, name, type);
            //Tuple<List<Product>, List<ProductImage>> tuple = new Tuple<List<Product>, List<ProductImage>>(result, resimg);
            return View(tuple2);

        }
        public ActionResult SortedByPrice2(string name)
        {
            List<Product> all = mycontext.Products.ToList();
            List<Product> result = new List<Product>();
            if (name != null) foreach (Product jennie in all)
                {
                    if (jennie.ProductName!=null&&jennie.ProductName.ToString().ToLower().Contains(name.ToLower()))
                    {
                        result.Add(jennie);
                    }
                }
            else result = all;
            int count = result.Count();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (result[i].CurrentPrice > result[j].CurrentPrice)
                    {
                        Product temp = new Product();
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            List<ProductImage> resimg = new List<ProductImage>();
            using (ProductController x = new ProductController())
            {
                resimg = x.GetListThumbnail(result);
            }
            string type = "name";

            Tuple<List<Product>, List<ProductImage>, string, string> tuple2 = new Tuple<List<Product>, List<ProductImage>, string, string>(result, resimg, name, type);
            //Tuple<List<Product>, List<ProductImage>> tuple = new Tuple<List<Product>, List<ProductImage>>(result, resimg);
            return View(tuple2);

        }
        public ActionResult SortedByEndTime(string name)
        {
            List<Product> all = mycontext.Products.ToList();
            List<Product> result = new List<Product>();
            if (name != null) foreach (Product jennie in all)
                {
                    if (jennie.ProductName!=null && jennie.ProductName.ToString().ToLower().Contains(name.ToLower()))
                    {
                        result.Add(jennie);
                    }
                }
            else result = all;
            int count = result.Count();
            for (int i = 0; i < count - 1; i++)
            {
                for (int j = i + 1; j < count; j++)
                {
                    if (DateTime.Compare(result[i].EndTime, result[j].EndTime) < 0)
                    {
                        Product temp = new Product();
                        temp = result[i];
                        result[i] = result[j];
                        result[j] = temp;
                    }
                }
            }
            List<ProductImage> resimg = new List<ProductImage>();
            using (ProductController x = new ProductController())
            {
                resimg = x.GetListThumbnail(result);
            }
            string type = "name";

            Tuple<List<Product>, List<ProductImage>, string, string> tuple2 = new Tuple<List<Product>, List<ProductImage>, string, string>(result, resimg, name, type);
            //Tuple<List<Product>, List<ProductImage>> tuple = new Tuple<List<Product>, List<ProductImage>>(result, resimg);
            return View(tuple2);

        }



    }
}