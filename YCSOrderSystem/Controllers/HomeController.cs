using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            return View();
        }

        public ActionResult About()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public String SUserRole()
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return "Admin";
                }
                else if (s[0].ToString() == "Manager")
                {
                    return "Manager";
                }
                else if (s[0].ToString() == "Employee")
                {
                    return "Employee";
                }
                else
                {
                    return "Customer";
                }
            }
            return "Customer";
        }
    }
}