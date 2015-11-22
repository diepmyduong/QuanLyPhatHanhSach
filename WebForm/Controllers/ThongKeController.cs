using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
using System.Globalization;

namespace WebForm.Controllers
{
    public class ThongKeController : Controller
    {
        private CultureInfo _cultureInfo;
        public CultureInfo CultureInfo
        {
            get
            {
                if (_cultureInfo == null)
                {
                    _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
                }
                return _cultureInfo;
            }
        }
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TonKho(DateTime? date, int page = 1, int pageSize = 10)
        {
            if (date != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentDate = date;
                var DMTheKho = TheKhoManager.getAllByDate((DateTime)date);
                var models = DMTheKho.ToPagedList(page, pageSize);
                return View(models);
            }
            return View();
        }

        public ActionResult LoNhap(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                var DMSach = SachManager.getAll();
                var models = DMSach.ToPagedList(page, pageSize);
                return View(models);
            }
            return View();
        }

        public ActionResult LoNhapDetails(int? id, int page = 1, int pageSize = 10)
        {
            if(id != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                var sach = SachManager.find((int)id);
                if(sach != null)
                {
                    ViewBag.currentSach = sach;
                    var models = sach.PhieuNhap.ToPagedList(page,pageSize);
                    return View(models);
                }
            }
            return RedirectToAction("LoNhap");
        }
    }
}