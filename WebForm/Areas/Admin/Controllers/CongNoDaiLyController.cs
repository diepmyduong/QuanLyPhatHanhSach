using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;
using PagedList;
using System.Globalization;

namespace WebForm.Areas.Admin.Controllers
{
    public class CongNoDaiLyController : BaseController
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
        // GET: CongNoNXB
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult TheoDoi(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<DaiLy> DMDaiLy = DaiLyManager.getAllAlive()
                                    .Where(dl => dl.tinhTongSoLuongNoTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        dl.tongTienNoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        dl.tongTienXuatThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    DMDaiLy = DaiLyManager.filter(search, DMDaiLy);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongTienXuat = DMDaiLy.Sum(nxb => nxb.TongTienXuatTheoThang);
                ViewBag.tongSoLuongNo = DMDaiLy.Sum(nxb => nxb.TongSoLuongNoTheoThang);
                ViewBag.tongTienNo = DMDaiLy.Sum(s => s.TongTienNoThang);
                var models = DMDaiLy.ToPagedList(page, pageSize);
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
            return View();
        }

        public ActionResult Details(int? id, int page = 1, int pageSize = 10, string search = null)
        {
            if(id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("TheoDoi");
            }
            ViewBag.cultureInfo = CultureInfo;
            var dl = DaiLyManager.find((int)id);
            if(dl == null)
            {
                putErrorMessage("Không tìm thấy đại lý");
                return RedirectToAction("TheoDoi");
            }
            ViewBag.currentDaiLy = dl;
            var DMCongNo = dl.CongNo;
            if (!String.IsNullOrEmpty(search))
            {
                DMCongNo = CongNoDaiLyManager.filter(search, DMCongNo);
                ViewBag.SearchKey = search;
            }
            ViewBag.tongSoLuongNo = DMCongNo.Sum(cn => cn.SoLuong);
            ViewBag.tongTienNo = DMCongNo.Sum(cn => cn.ThanhTien);
            var models = DMCongNo.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }
        #endregion



        #region JSON REQUEST
        public JsonResult GetPropertiesTheoDoiNo(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in DaiLy.searchKeysTheoDoiNo())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesChiTietNo(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in CongNoDaiLy.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}