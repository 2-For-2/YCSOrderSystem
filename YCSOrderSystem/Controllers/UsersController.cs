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
    public class UsersController : Controller
    {
        [Authorize]
        // GET: Users
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ViewBag.Name = user.Name;

                ViewBag.displayMenu = "Customer";
                
                switch(SUserRole())
                {
                    case "Admin":
                        ViewBag.displayMenu = "Admin";
                        return View("AdminIndex");
                    case "Manager":
                    case "Employee":
                        ViewBag.displayMenu = "Staff";
                        return View("StaffIndex");
                    default:
                        return RedirectToAction("Index","Home");
                }                 
            }
            else
            {
                ViewBag.Name = "Anonymous";
                RedirectToAction("Index", "Home");
            }

            return View();
        }

        public ActionResult AdminIndex()
        {
            return View();
        }

        public ActionResult StaffIndex()
        {
            return View();
        }

        public String SUserRole()
        {
            if(User.Identity.IsAuthenticated)
            {
                var user = User.Identity;
                ApplicationDbContext context = new ApplicationDbContext();
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var s = UserManager.GetRoles(user.GetUserId());
                if (s[0].ToString() == "Admin")
                {
                    return "Admin";
                }
                else if(s[0].ToString() == "Manager")
                {
                    return "Manager";
                }
                else if(s[0].ToString() == "Employee")
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