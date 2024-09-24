using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Areas.Admin.Controllers
{
    public class OrderProController : Controller
    {
        // GET: Admin/OrderPro
        ExtendTechEntities database = new ExtendTechEntities();
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(database.OrderProes.ToList());
            else
            {
                return View(database.OrderProes.Where(s => s.AddressDeliverry.Contains(_name)).ToList());
            }
        }
    }
}