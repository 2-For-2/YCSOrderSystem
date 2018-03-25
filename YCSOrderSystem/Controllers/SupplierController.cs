using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
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
                    ViewBag.displayMenu = "yes";
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
                    ViewBag.displayMenu = "yes";
                    return View();
                }
            }
            return RedirectToAction("Index", "Users");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SuppName, Address, Contact, Email")]Supplier supplier)
        {
            ViewBag.displayMenu = "yes";
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

        public ActionResult Details(int? id)
        {
            ViewBag.displayMenu = "yes";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Supplier supp = db.Suppliers.Find(id);
            if(supp == null)

            {
                return HttpNotFound();
            }
            return View(supp);
        }

        [HttpGet]
        public ActionResult Delete(int? id, bool? saveChangesError=false)
        {
            ViewBag.displayMenu = "yes";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if(saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Delete Failed, Try Again";
            }
            Supplier supp = db.Suppliers.Find(id);
            if(supp == null)
            {
                return HttpNotFound();
            }
            return View(supp);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            ViewBag.displayMenu = "yes";
            try
            {
                Supplier supp = db.Suppliers.Find(id);
                db.Suppliers.Remove(supp);
                db.SaveChanges();
            }
            catch(DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }


    }
}