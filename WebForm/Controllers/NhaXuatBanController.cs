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
    public class NhaXuatBanController : Controller
    {
        // GET: NhaXuatBan
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<NhaXuatBan> DMNXB = null;
            //if (!String.IsNullOrEmpty(search))
            //{
            //    DMNXB = NhaXuatBanManager.filter(search);
            //    ViewBag.SearchKey = search;
            //}
            //else
            //{
            //    DMNXB = NhaXuatBanManager.getAll();
            //}
            DMNXB = NhaXuatBanManager.getAll();
            var models = DMNXB.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: NhaXuatBan/Details/5
        public ActionResult Details(int id)
        {
            var model = NhaXuatBanManager.find(id);
            return View(model);
        }

        // GET: NhaXuatBan/Create
        public ActionResult Create()
        {
            var model = new NhaXuatBan();
            return View(model);
        }

        // POST: NhaXuatBan/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhaXuatBan/Edit/5
        public ActionResult Edit(int id)
        {
            var model = NhaXuatBanManager.find(id);
            return View(model);
        }

        // POST: NhaXuatBan/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: NhaXuatBan/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: NhaXuatBan/Delete/5
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
