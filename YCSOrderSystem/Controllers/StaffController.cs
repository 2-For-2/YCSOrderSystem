using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    public class StaffController : Controller
    {
        YCSDatabaseEntities db = new YCSDatabaseEntities();
        // GET: Staff
        public ActionResult Index()
        {
            var staffList = db.Staffs.ToList();
            return View(staffList);
        }

        // GET: Staff/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Staff/Create
        public ActionResult Create()
        {
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

                return RedirectToAction("Index");
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
            return View();
        }

        // POST: Staff/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
