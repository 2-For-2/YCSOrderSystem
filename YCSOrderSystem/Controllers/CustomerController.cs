using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YCSOrderSystem.Models;

namespace YCSOrderSystem.Controllers
{
    public class CustomerController : Controller
    {
        YCSDatabaseEntities db = new YCSDatabaseEntities();
        // GET: Customer
        public ActionResult Index()
        {
            var custs = db.Customers.ToList();
            return View(custs);
        }

        // GET: Customer/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            Customer customer = (Customer)TempData["customer"];
            return View(customer);
        }

        // POST: Customer/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                Customer cust = new Models.Customer();
                cust.Address = Request.Form["Address"];
                cust.AspId = Request.Form["AspId"];
                cust.BankDeatils = Request.Form["BankDeatils"];
                cust.Contact = Request.Form["Contact"];
                cust.CustName = Request.Form["CustName"];
                cust.Email = Request.Form["Email"];
                cust.UserName = Request.Form["UserName"];
                db.Customers.Add(cust);
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

        // GET: Customer/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Customer/Edit/5
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

        // GET: Customer/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Customer/Delete/5
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
