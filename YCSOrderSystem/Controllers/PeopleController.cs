﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YCSOrderSystem.Controllers
{
    public class PeopleController : Controller
    {
        // GET: People
        
        public ActionResult Index()
        {
            return View();
        }
    }
}