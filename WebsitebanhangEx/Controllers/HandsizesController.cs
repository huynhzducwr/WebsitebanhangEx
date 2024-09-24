using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Controllers
{
    public class HandsizesController : Controller
    {
        // GET: Handsizes
        ExtendTechEntities database = new ExtendTechEntities();
        public PartialViewResult HandSizePartial()
        {
            var cateList = database.Handsizes.ToList();
            return PartialView(cateList);
        }
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(database.Handsizes.ToList());
            else
            {
                return View(database.Handsizes.Where(s => s.NameSize.Contains(_name)).ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Handsize cate)
        {
            try
            {
                database.Handsizes.Add(cate);
                database.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {
                return Content("Error Creat  New");
            }
        }
        public ActionResult Details(int id)
        {
            return View(database.Handsizes.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Handsizes.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Handsize cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            return View(database.Handsizes.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Handsize cate)
        {
            try
            {
                cate = database.Handsizes.Where(s => s.ID == id).FirstOrDefault();
                database.Handsizes.Remove(cate);
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