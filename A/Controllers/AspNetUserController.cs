using A.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace A.Controllers
{
    public class AspNetUserController : Controller
    {
        // GET: AspNetUser
        private ApplicationDbContext mycontext = new Models.ApplicationDbContext();
        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            List<ApplicationUser> myuser = mycontext.Users.ToList();
            return View(myuser);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult Index(string name)
        {
            List<ApplicationUser> myuser = mycontext.Users.ToList();
            List<ApplicationUser> result = new List<ApplicationUser>();
            string str = name.ToLower();
            foreach(ApplicationUser i in myuser)
            {
                if(i.UserName.ToString().ToLower().Contains(str))
                {
                    result.Add(i);
                }
            }
            return View(result);
        }
        [Authorize(Roles ="Admin")]
        public ActionResult EditUser(string id)
        {
            ApplicationUser user = new ApplicationUser();
            user = mycontext.Users.Where(m => m.Id == id).FirstOrDefault();
            string email = "";
            if(user.Email!=null) email = user.Email;
            String location = "";
            if (user.Location != null) location = user.Location.ToString();
            String phoneNumber = "";
            if (user.PhoneNumber != null) phoneNumber = user.PhoneNumber;
            
            Tuple<string, string, string> tuplecuanhi = new Tuple<string, string, string>(email, location, phoneNumber);
            return View(tuplecuanhi);
            
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult EditUser(FormCollection mydata)
        {
            string uname = mydata["userName"].ToString();
            ApplicationUser myuser = mycontext.Users.Where(m => m.Email == uname).FirstOrDefault();
            myuser.Location = mydata["location"].ToString();
            myuser.PhoneNumber = mydata["phoneNum"].ToString();
            mycontext.SaveChanges();
            return View(new Tuple<string,string,string>(uname,myuser.Location,myuser.PhoneNumber));
            //return View("EditUser", "AspNetUser", myuser.Id);


        }
    }
}