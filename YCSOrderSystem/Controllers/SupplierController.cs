using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    public class SupplierController : Controller
    {
        YCSDBEntities db = new YCSDBEntities();
        // GET: Supplier
        public ActionResult Index()
        {
            if(User.Identity.IsAuthenticated)
            {
                String UserRole = SUserRole();
                if(UserRole != "Customer")
                {
                    var supps = db.Suppliers.ToList();
                    return View(supps);
                }                
            }
            return RedirectToAction("Index", "Users");
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

        public ActionResult Create()
        {
            if(User.Identity.IsAuthenticated)
            {
                if(SUserRole() != "Customer")
                {
                    return View();
                }
            }
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuppName, Address, Contact, Email")]Supplier supplier)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    db.Suppliers.Add(supplier);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException)
            {
                ModelState.AddModelError("", "Unable To Save Changes, Try Again");
            }
            return View(supplier);
        }
    }
}