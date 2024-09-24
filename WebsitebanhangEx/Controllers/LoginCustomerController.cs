using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Controllers
{
    public class LoginCustomerController : Controller
    {
        // GET: LoginCustomer
        ExtendTechEntities database = new ExtendTechEntities();
        public ActionResult Index()
        {
            return View();

        }
        [HttpPost]

        public ActionResult LoginAcount(Customer _user)
        {
            var check = database.Customers.
                Where(s => s.Email == _user.Email && s.PasswordCus == _user.PasswordCus).FirstOrDefault();
            if (check == null)
            {
                ViewBag.ErrorInfo = "SaiInfo";
                return View("Index");
            }
            else
            {
                database.Configuration.ValidateOnSaveEnabled = false;
                Session["Email"] = _user.Email;
                Session["IDCus"] = check.IDCus;

                return RedirectToAction("Index", "Product");
            }
        }
        public ActionResult Profilee()
        {
            // Kiểm tra nếu người dùng đã nhập chưa
            if (Session["Email"] == null || Session["IDCus"] == null)
            {
                // Nếu chưa đăng nhập thì chuyển hướng đến index home
                return RedirectToAction("Index", "Home");
            }

            // Người dùng đã đăng nhập rồi
            string userEmail = Session["Email"].ToString();
            int userId = Convert.ToInt32(Session["IDCus"]);

            var user = database.Customers.FirstOrDefault(s => s.Email == userEmail && s.IDCus == userId);

            // kiểm tra xem user có tồn tại không
            if (user == null)
            {
                // Nếu không có thì trả về index home
                return RedirectToAction("Index", "Home");
            }

            //trả về thông tin khách hàng
            return View(user);
        }

        public ActionResult LogOutCustomer()
        {
            Session.Abandon();
            return RedirectToAction("Index", "LoginCustomer");
        }
        public ActionResult RegisterCustomer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCustomer(Customer _user)
        {
            if (ModelState.IsValid)
            {
                var check_ID = database.Customers.Where(s => s.IDCus == _user.IDCus).FirstOrDefault();
                if (check_ID == null)
                {
                    database.Configuration.ValidateOnSaveEnabled = false;
                    database.Customers.Add(_user);
                    database.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.ErrorRegister = "This ID is exixst";
                    return View();
                }
            }
            return View();
        }
    }
}