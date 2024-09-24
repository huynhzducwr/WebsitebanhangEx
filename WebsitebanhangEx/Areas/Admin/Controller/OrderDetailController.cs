using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Areas.Admin.Controllers
{
    public class OrderDetailController : Controller
    {
        // GET: Admin/OrderDetail
        ExtendTechEntities database = new ExtendTechEntities();
        public ActionResult Index(int? _name)
        {
            if (!_name.HasValue)
                return View(database.OrderDetails.ToList());
            else
            {
                return View(database.OrderDetails.Where(s => s.ID.ToString().Contains(_name.ToString())).ToList());
            }
        }

       


    }
}