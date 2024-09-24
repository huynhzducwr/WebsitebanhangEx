using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebsitebanhangEx.Models;

namespace WebsitebanhangEx.Controllers
{
    public class NentangController : Controller
    {
        // GET: Nentang
        ExtendTechEntities database = new ExtendTechEntities();
        public PartialViewResult NentangPartial()
        {
            var cateList = database.Nentangs.ToList();
            return PartialView(cateList);
        }
        public ActionResult Index(string _name)
        {
            if (_name == null)
                return View(database.Nentangs.ToList());
            else
            {
                return View(database.Nentangs.Where(s => s.NameBase.Contains(_name)).ToList());
            }
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Nentang cate)
        {
            try
            {
                database.Nentangs.Add(cate);
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
            return View(database.Nentangs.Where(s => s.ID == id).FirstOrDefault());
        }
        public ActionResult Edit(int id)
        {
            return View(database.Nentangs.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Edit(int id, Nentang cate)
        {
            database.Entry(cate).State = System.Data.Entity.EntityState.Modified;
            database.SaveChanges();
            return RedirectToAction("index");
        }

        public ActionResult Delete(int id)
        {
            return View(database.Nentangs.Where(s => s.ID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Delete(int id, Nentang cate)
        {
            try
            {
                cate = database.Nentangs.Where(s => s.ID == id).FirstOrDefault();
                database.Nentangs.Remove(cate);
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