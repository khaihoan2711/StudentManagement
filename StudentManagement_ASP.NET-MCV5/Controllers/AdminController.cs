using StudentManagement_ASP.NET_MCV5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentManagement_ASP.NET_MCV5.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        [Authorize]
        public ActionResult Index()
        {
            if (User.IsInRole(ApplicationRole.Administrator.ToString()))
            {
                return View();
            }
            else
            {
                ViewBag.ErrorMessage = "Your account is not qualified to access this page";
                return View("Error");
            }
        }
    }
}