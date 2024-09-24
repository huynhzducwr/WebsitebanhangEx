using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        ExtendTechEntities database = new ExtendTechEntities();
        // GET: ShoppingCart
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ShowCart()
        {
            if (Session["Cart"] == null)

                return View("EmptyCart");
            Cart _cart = Session["Cart"] as Cart;
            return View(_cart);


        }
        public Cart GetCart()
        {
            Cart cart = Session["Cart"] as Cart;
            if (cart == null || Session["Cart"] == null)
            {
                cart = new Cart();
                Session["Cart"] = cart;
            }
            return cart;
        }
        //Action them product vao gio hang

        public ActionResult AddToCart(int id)
        {
            var _pro = database.Products.SingleOrDefault(s => s.ProductID == id);
            if (_pro != null)
            {
                GetCart().Add_Product_Cart(_pro);
            }
            return RedirectToAction("ShowCart", "ShoppingCart");

        }
        public ActionResult Update_Cart_Quantity(FormCollection form)
        {
            Cart cart = Session["Cart"] as Cart;
            int id_pro = int.Parse(form["idPro"]);

            if (int.TryParse(form["cartQuantity"], out int _quantity))
            {
                // Check if the quantity is non-negative
                if (_quantity >= 0)
                {
                    cart.Update_quantity(id_pro, _quantity);
                }
                else
                {
                    // Quantity is negative, prompt the user to enter a valid quantity
                    TempData["ErrorMessage"] = "Please enter a valid quantity.";
                }
            }
            else
            {
                // cartQuantity is not a valid integer, handle accordingly
                TempData["ErrorMessage"] = "Please enter a valid quantity.";
            }

            return RedirectToAction("ShowCart", "ShoppingCart");
        }

        public ActionResult RemoveCart(int id)
        {
            Cart cart = Session["Cart"] as Cart;
            cart.Remove_CartItem(id);
            return RedirectToAction("ShowCart", "ShoppingCart");
        }
        public ActionResult CheckOut_Success()
        {
            return View();
        }
        public ActionResult CheckOut(FormCollection form)
        {
            try
            {
                Cart cart = Session["Cart"] as Cart;
                OrderPro _order = new OrderPro();
                _order.DateOrder = DateTime.Now;
                _order.AddressDeliverry = form["Địa chỉ nhận hàng"];
                _order.IDCus = int.Parse(form["Mã khách hàng"]);
                database.OrderProes.Add(_order);
                foreach (var item in cart.Items)
                {
                    OrderDetail _order_detail = new OrderDetail();
                    _order_detail.IDOrder = _order.ID;
                    _order_detail.IDProduct = item._product.ProductID;
                    _order_detail.UnitPrice = (double)item._product.Price;
                    _order_detail.Quantity = item._quantity;
                    database.OrderDetails.Add(_order_detail);
                    foreach (var p in database.Products.Where(s => s.ProductID == _order_detail.IDProduct))
                    {
                        var update_quan_pro = p.Quantity - item._quantity;
                        p.Quantity = update_quan_pro;
                    }


                }
                database.SaveChanges();
                cart.ClearCart();
                return RedirectToAction("CheckOut_Success", "ShoppingCart");
            }
            catch (DbUpdateException ex)
            {
                // In thông điệp lỗi chi tiết từ inner exception
                Exception innerException = ex.InnerException;
                while (innerException != null)
                {
                    Console.WriteLine("Inner Exception: " + innerException.Message);
                    innerException = innerException.InnerException;
                }

                return Content("Lỗi khi thanh toán: " + ex.Message);
            }
            catch (Exception ex)
            {
                return Content("Lỗi khi thanh toán: " + ex.Message);
            }




        }
        public PartialViewResult BagCart()
        {
            int toltal_quantity_item = 0;
            Cart cart = Session["Cart"] as Cart;
            if (cart != null)

                toltal_quantity_item = cart.Total_quantity();
            ViewBag.QuantityCart = toltal_quantity_item;
            return PartialView("BagCart");

        }
    }
}