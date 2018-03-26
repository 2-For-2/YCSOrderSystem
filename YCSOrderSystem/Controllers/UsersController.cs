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
                        break;
                    case "Manager":
                        ViewBag.displayMenu = "Staff";
                        break;
                    case "Employee":
                        ViewBag.displayMenu = "Staff";
                        break;
                    default:
                        ViewBag.displayMenu = "Customer";
                        break;
                }
                return View();                    
            }
            else
            {
                ViewBag.Name = "Anonymous";
            }

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

        [HttpGet]
        public ActionResult AddNewStaff(string UserId)
        {
            string[] positions = { "Salesman", "Manager", "Technical Staff","Delivery Driver","Other"};
            ViewBag.UserId = UserId;
            ViewBag.Positions = new SelectList(positions);
            return View();
        }

        public ActionResult AddNewCustomer()
        {
            return View();
        }
    }
}