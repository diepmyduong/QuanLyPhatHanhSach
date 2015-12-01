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
        #region Private Properties
        private CultureInfo _cultureInfo;
        #endregion

        #region Public Properties
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
        #endregion

        #region Actions
        // GET: ThongKe
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TonKho(DateTime? date, int page = 1, int pageSize = 10, string search = null)
        {
            if (date != null)
            {
                List<TheKho> DMTheKho = null;
                if (!String.IsNullOrEmpty(search))
                {
                    DMTheKho = TheKhoManager.filter(search, TheKhoManager.getAllByDate((DateTime)date));
                    ViewBag.SearchKey = search;
                }
                else
                {
                    DMTheKho = TheKhoManager.getAllByDate((DateTime)date);
                }
                ViewBag.tongLuongSach = DMTheKho.Sum(tk => tk.SoLuong);
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentDate = date;
                
                var models = DMTheKho.ToPagedList(page, pageSize);
                return View(models);
            }
            return View();
        }

        public ActionResult LoNhap(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<Sach> DMSach = SachManager.getAllAlive()
                                    .Where(s => s.tongSoLuongNhapTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        s.tongTienNhapTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    DMSach = SachManager.filter(search,DMSach);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongSoLuongNhap = DMSach.Sum(s => s.SoLuongNhapTheoThang);
                ViewBag.tongTienNhap = DMSach.Sum(s => s.TongTienNhapTheoThang);
                var models = DMSach.ToPagedList(page, pageSize);
                return View(models);
            }
            return View();
        }

        public ActionResult LoNhapDetails(int? id, int page = 1, int pageSize = 10)
        {
            if (id != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                var sach = SachManager.find((int)id);
                if (sach != null)
                {
                    ViewBag.currentSach = sach;
                    var models = sach.PhieuNhap.ToPagedList(page, pageSize);
                    return View(models);
                }
            }
            return RedirectToAction("LoNhap");
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetPropertiesTonKho(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in TheKho.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesLoNhap(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in Sach.searchKeysLoNhap())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion



    }
}