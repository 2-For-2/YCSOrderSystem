using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    public class SalariesController : Controller
    {
        private YCSDatabaseEntities db = new YCSDatabaseEntities();

        // GET: Salaries
        public ActionResult Index()
        {
            
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            var salaries = db.Salaries.Include(s => s.Staff);
            return View(salaries.ToList());
        }

        // GET: Salaries/Details/5
        public ActionResult Details(int? id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // GET: Salaries/Create
        public ActionResult Create()
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            ViewBag.StaffNum = new SelectList(db.Staffs, "StaffNum", "StaffName");
            return View();
        }

        // POST: Salaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalaryNum,Amount,Benefits,Deductions,StaffNum,Description")] Salary salary)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            if (ModelState.IsValid)
            {
                db.Salaries.Add(salary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.StaffNum = new SelectList(db.Staffs, "StaffNum", "StaffName", salary.StaffNum);
            return View(salary);
        }

        // GET: Salaries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            ViewBag.StaffNum = new SelectList(db.Staffs, "StaffNum", "StaffName", salary.StaffNum);
            return View(salary);
        }

        // POST: Salaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalaryNum,Amount,Benefits,Deductions,StaffNum,Description")] Salary salary)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            if (ModelState.IsValid)
            {
                db.Entry(salary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StaffNum = new SelectList(db.Staffs, "StaffNum", "StaffName", salary.StaffNum);
            return View(salary);
        }

        // GET: Salaries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salary salary = db.Salaries.Find(id);
            if (salary == null)
            {
                return HttpNotFound();
            }
            return View(salary);
        }

        // POST: Salaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if(SUserRole() != "Customer" && SUserRole() != null)
            {
                ViewBag.displayMenu = "Yes";
            }

            Salary salary = db.Salaries.Find(id);
            db.Salaries.Remove(salary);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
