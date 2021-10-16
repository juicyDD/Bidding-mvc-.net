using A.Controllers;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A.Models
{
    public class ProductOnViewController : Controller
    {
        // GET: ProductOnView
        private ApplicationDbContext mycontext = new ApplicationDbContext();
        public ActionResult Upload()
        {
            ProductOnView x = new ProductOnView();
            return View(x);
        }
        [HttpPost]
        public ActionResult Upload(ProductOnView nhine)
        {
            //string UploadPath = ConfigurationManager.AppSettings["UserImagePath"].ToString();
            string UploadPath = Server.MapPath("~/Images/");
            nhine.imgname = new List<string>();
            nhine.OwnerId = this.HttpContext.User.Identity.GetUserId();
            int count = nhine.img.Count();
            
            if (nhine.img.Count>0)
            {
                
                for(int i=0;i<count;i++)
                {
                    string Name = Path.GetFileNameWithoutExtension(nhine.img[i].FileName);
                    //nhine.imgname.Add();
                    string FileExtension = Path.GetExtension(nhine.img[i].FileName);
                    string final = "";
                    final = nhine.OwnerId.ToString() + DateTime.Now.ToString("yyyyMMddHHmmss") + "-" + Name.ToString() + FileExtension.ToString();


                    nhine.imgname.Add(final.ToString());
                    string mypath = UploadPath + nhine.imgname[i];
                    //string mypath = UploadPath + final;
                    nhine.img[i].SaveAs(mypath);
                }
            }
            ProductController ins = new ProductController();
            ins.ConvertToProduct(nhine);
           return Redirect(Request.UrlReferrer.ToString());
        }
    }
}