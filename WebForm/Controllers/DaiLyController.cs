using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;

namespace WebForm.Controllers
{
    public class DaiLyController : Controller
    {
        // GET: DaiLy
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<DaiLy> DMDaiLy = null;
            //if (!String.IsNullOrEmpty(search))
            //{
            //    DMDaiLy = DaiLyManager.filter(search);
            //    ViewBag.SearchKey = search;
            //}
            //else
            //{
            //    DMDaiLy = DaiLyManager.getAll();
            //}
            DMDaiLy = DaiLyManager.getAll();
            var models = DMDaiLy.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: DaiLy/Details/5
        public ActionResult Details(int id)
        {
            var model = DaiLyManager.find(id);
            return View(model);
        }

        // GET: DaiLy/Create
        public ActionResult Create()
        {
            var model = new DaiLy();
            return View(model);
        }

        // POST: DaiLy/Create
        [HttpPost]
        public ActionResult Create(DaiLy model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = DaiLyManager.add(model);
                    if (result != -1)
                    {
                        return RedirectToAction("Details", new { id = result });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: DaiLy/Edit/5
        public ActionResult Edit(int id)
        {
            var model = DaiLyManager.find(id);
            return View(model);
        }

        // POST: DaiLy/Edit/5
        [HttpPost]
        public ActionResult Edit(DaiLy model, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (DaiLyManager.edit(model))
                    {
                        return RedirectToAction("Details", new { id = model.MaSoDaiLy });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: DaiLy/Delete/5
        public ActionResult Delete(int id)
        {
            var model = DaiLyManager.find(id);
            return View(model);

        }

        // POST: DaiLy/Delete/5
        [HttpPost]
        public ActionResult Delete(DaiLy model, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                if (DaiLyManager.delete(model))
                {
                    return RedirectToAction("Index");
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }
    }
}
