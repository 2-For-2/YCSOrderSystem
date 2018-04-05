﻿using Microsoft.AspNet.Identity;
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

        YCSDatabaseEntities db = new YCSDatabaseEntities();
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

        [HttpGet]
        public ActionResult AddNewStaff(string UserId)
        {
            string[] positions = { "Salesman", "Manager", "Technical Staff","Delivery Driver","Other"};
            ViewBag.UserId = UserId;
            ViewBag.Positions = new SelectList(positions);
            Staff newStaff = new Models.Staff();
            newStaff.AspId = UserId;
            return View(newStaff);
        }

        [HttpPost]
        public ActionResult AddNewStaff([Bind(Include = "StaffNum,StaffName,Position,Address,Contact,Email,AspId")] Staff staff)
        {
            if (ModelState.IsValid)
            {
                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("AddNewStaff", staff.AspId);
            }
        }

        public ActionResult AddNewCustomer()
        {
            return RedirectToAction("Index","Home");
        }
    }
}