using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;
using PagedList;
using PagedList.Mvc;

namespace WebsitebanhangEx.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product

        ExtendTechEntities database = new ExtendTechEntities();


        // GET: Product
        public ActionResult SearchOption(double min = double.MinValue, double max = double.MaxValue)
        {
            var list = database.Products.Where(p => (double)p.Price >= min && (double)p.Price <= max).ToList();
            return View(list);
        }
        public ActionResult Index(string category, string handsize, string nentang, int? page)
        {
            int pageSize = 6;
            int pageNum = page ?? 1;
            IQueryable<Product> productList = database.Products;

            // Kiểm tra nếu user đã chọn category và thêm điều kiện vào truy vấn
            if (!string.IsNullOrEmpty(category))
            {
                productList = productList.Where(x => x.Category == category);
            }

            // Kiểm tra nếu user đã chọn handsize và thêm điều kiện vào truy vấn
            if (!string.IsNullOrEmpty(handsize))
            {
                productList = productList.Where(x => x.Handsize == handsize);
            }

            // Kiểm tra nếu user đã chọn nentang và thêm điều kiện vào truy vấn
            if (!string.IsNullOrEmpty(nentang))
            {
                productList = productList.Where(x => x.Nentang == nentang);
            }

            // Sắp xếp theo tên sản phẩm
            productList = productList.OrderByDescending(x => x.NamePro);

            // Tạo một PagedList từ danh sách đã lọc và sắp xếp
            IPagedList<Product> pagedListProducts = productList.ToPagedList(pageNum, pageSize);

            return View(pagedListProducts);
        }


        //public ActionResult Index(string category, string handsize, string nentang)
        //{
        //    IQueryable<Product> productList = database.Products;

        //    // Kiểm tra nếu user đã chọn category và thêm điều kiện vào truy vấn
        //    if (!string.IsNullOrEmpty(category))
        //    {
        //        productList = productList.Where(x => x.Category == category);
        //    }

        //    // Kiểm tra nếu user đã chọn handsize và thêm điều kiện vào truy vấn
        //    if (!string.IsNullOrEmpty(handsize))
        //    {
        //        productList = productList.Where(x => x.Handsize == handsize);
        //    }

        //    // Kiểm tra nếu user đã chọn nentang và thêm điều kiện vào truy vấn
        //    if (!string.IsNullOrEmpty(nentang))
        //    {
        //        productList = productList.Where(x => x.Nentang == nentang);
        //    }

        //    // Sắp xếp theo tên sản phẩm
        //    productList = productList.OrderByDescending(x => x.NamePro);

        //    return View(productList.ToList());
        //}




        public ActionResult Create()
        {
            {
                List<Category> list = database.Categories.ToList();
                List<Handsize> list1 = database.Handsizes.ToList();
                List<Nentang> list2 = database.Nentangs.ToList();

                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", "|");
                ViewBag.listHandsize = new SelectList(list1, "IDSize", "NameSize", "|");
                ViewBag.listNentang = new SelectList(list2, "IDBase", "NameBase", "|");
                Product pro = new Product();
                return View(pro);
            }

        }

        public ActionResult SelectCate()
        {
            Category se_cate = new Category();
            se_cate.ListCate = database.Categories.ToList();
            return PartialView(se_cate);

        }
        public ActionResult SelectSize()
        {
            Handsize se_cate = new Handsize();
            se_cate.ListSize = database.Handsizes.ToList();
            return PartialView(se_cate);

        }
        public ActionResult SelectBase()
        {
            Nentang se_cate = new Nentang();
            se_cate.ListNentang = database.Nentangs.ToList();
            return PartialView(se_cate);

        }
        [HttpPost]

        public ActionResult Create(Product pro)
        {
            List<Category> list = database.Categories.ToList();
            List<Handsize> list1 = database.Handsizes.ToList();
            List<Nentang> list2 = database.Nentangs.ToList();
            try
            {
                if (pro.UploadImage != null)
                {
                    string filename = Path.GetFileNameWithoutExtension(pro.UploadImage.FileName);
                    string extent = Path.GetExtension(pro.UploadImage.FileName);
                    filename = filename + extent;
                    pro.ImagePro = "~/Content/images/" + filename;
                    pro.UploadImage.SaveAs(Server.MapPath("~/Content/images/" + filename));
                }
                ViewBag.listCategory = new SelectList(list, "IDCate", "NameCate", 1);
                ViewBag.listHandsize = new SelectList(list1, "IDSize", "NameSize", 2);
                ViewBag.listNentang = new SelectList(list2, "IDBase", "NameBase", 3);




                database.Products.Add(pro);
                database.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }

        public ActionResult ProductAdmin(string _name)
        {
            if (_name == null)
                return View(database.Products.ToList());
            else
            {
                return View(database.Products.Where(s => s.NamePro.Contains(_name)).ToList());
            }
        }

        public ActionResult Details(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }

        public ActionResult Edit(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Product cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("index");
        }






        public ActionResult Delete(int id)
        {
            return View(database.Products.Where(s => s.ProductID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Product cate)
        {
            try
            {
                cate = database.Products.Where(s => s.ProductID == id).FirstOrDefault();
                database.Products.Remove(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("This data is using in other table,Error Delete!");
            }
        }
    }
}