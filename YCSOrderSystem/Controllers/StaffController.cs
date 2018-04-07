using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    public class StaffController : Controller
    {
        YCSDatabaseEntities db = new YCSDatabaseEntities();
        // GET: Staff
        public ActionResult Index()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            var staffList = db.Staffs.ToList();
            return View(staffList);
        }

        // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            return View();
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }

            Staff newstaff = (Staff)TempData["staff"];
                List<string> staffPositions = new List<string>();
                staffPositions.Add("Manager");
                staffPositions.Add("Floor Salesman");
                staffPositions.Add("Technical Staff");
                staffPositions.Add("Delivery Driver");
                staffPositions.Add("Other");
                ViewBag.Positions = staffPositions.AsEnumerable();
                return View(newstaff);
        }


        // POST: Staff/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            try
            {
                // TODO: Add insert logic here
                Staff newStaff = new Models.Staff();
                newStaff.Address = Request.Form["Address"];
                newStaff.AspId = Request.Form["AspId"];
                newStaff.Contact = Request.Form["Contact"];
                newStaff.Email = Request.Form["Email"];
                newStaff.Position = Request.Form["ddlPosition"];
                newStaff.StaffName = Request.Form["StaffName"];
                newStaff.UserName = Request.Form["UserName"];
                db.Staffs.Add(newStaff);
                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            catch(Exception e)
            {
                string error = e.InnerException.Message;
                string error2 = e.InnerException.InnerException.Message;
                return View();
            }
            
        }

        // GET: Staff/Edit/5
        public ActionResult Edit(int id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            return View();
        }

        // POST: Staff/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Staff/Delete/5
        public ActionResult Delete(int id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            Staff delStaff = db.Staffs.Find(id);
            return View(delStaff);
        }

        // POST: Staff/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            try
            {
                // TODO: Add delete logic here
                Staff delStaff = db.Staffs.Find(id);
                db.Staffs.Remove(delStaff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
