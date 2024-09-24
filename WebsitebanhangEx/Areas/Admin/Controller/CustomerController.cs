using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Areas.Admin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Admin/Customer
        ExtendTechEntities database = new ExtendTechEntities();
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(database.Customers.ToList());
            else
            {
                return View(database.Customers.Where(s => s.NameCus.Contains(_name)).ToList());
            }
        }
    }
}