using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;
using PagedList;

namespace WebForm.Controllers
{
    public class LinhVucController : Controller
    {
        // GET: LinhVuc
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<LinhVuc> DMLinhVuc = null;
            //if (!String.IsNullOrEmpty(search))
            //{
            //    DMLinhVuc = LinhVucManager.filter(search);
            //    ViewBag.SearchKey = search;
            //}
            //else
            //{
            //    DMLinhVuc = LinhVucManager.getAll();
            //}
            DMLinhVuc = LinhVucManager.getAll();
            var models = DMLinhVuc.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: LinhVuc/Details/5
        public ActionResult Details(int id)
        {
            var model = LinhVucManager.find(id);
            return View(model);
        }

        // GET: LinhVuc/Create
        public ActionResult Create()
        {
            var model = new LinhVuc();
            return View(model);
        }

        // POST: LinhVuc/Create
        [HttpPost]
        public ActionResult Create(LinhVuc model)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    if (LinhVucManager.add(model))
                    {
                        RedirectToAction("Index");
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: LinhVuc/Edit/5
        public ActionResult Edit(int id)
        {
            var model = LinhVucManager.find(id);
            return View(model);
        }

        // POST: LinhVuc/Edit/5
        [HttpPost]
        public ActionResult Edit(LinhVuc model, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (LinhVucManager.edit(model))
                    {
                        return RedirectToAction("Details", new { id = model.MaSoLinhVuc });
                    }
                }
                return View(model);
            }
            catch
            {
                return View();
            }
        }

        // GET: LinhVuc/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LinhVuc/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
