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
    public class CatalogyController : BaseController
    {
        public ActionResult List(int? id,int page = 1, int pageSize = 9, string search = null)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = LinhVucManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            List<Sach> DMSach = model.Sach;
            if (!String.IsNullOrEmpty(search))
            {
                DMSach = SachManager.filter(search, DMSach);
                ViewBag.SearchKey = search;
            }
            ViewBag.currentLV = model;
            var models = DMSach.ToPagedList(page, pageSize);
            return View(models);
        }
    }
}