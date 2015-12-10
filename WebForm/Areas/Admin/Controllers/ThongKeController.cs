using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
using System.Globalization;

namespace WebForm.Areas.Admin.Controllers
{
    public class ThongKeController : BaseController
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
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
            return View();
        }

        public ActionResult LoNhap(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<Sach> DMSach = SachManager.getAll()
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
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
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
                    setAlertMessage();
                    return View(models);
                }
                else
                {
                    putErrorMessage("Không tìm thấy");
                }
            }
            else
            {
                putErrorMessage("Đường dẫn không đúng");
            }
            return RedirectToAction("LoNhap");
        }

        public ActionResult LoXuat(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<Sach> DMSach = SachManager.getAll()
                                    .Where(s => s.tongSoLuongXuatTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        s.tongTienXuatTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    DMSach = SachManager.filter(search, DMSach);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongSoLuongXuat = DMSach.Sum(s => s.SoLuongXuatTheoThang);
                ViewBag.tongTienXuat = DMSach.Sum(s => s.TongTienXuatTheoThang);
                var models = DMSach.ToPagedList(page, pageSize);
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
            return View();
        }

        public ActionResult LoXuatDetails(int? id, int page = 1, int pageSize = 10)
        {
            if (id != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                var sach = SachManager.find((int)id);
                if (sach != null)
                {
                    ViewBag.currentSach = sach;
                    var models = sach.PhieuXuat.ToPagedList(page, pageSize);
                    setAlertMessage();
                    return View(models);
                }
                else
                {
                    putErrorMessage("Không tìm thấy");
                }
            }
            else
            {
                putErrorMessage("Đường dẫn không đúng");
            }
            return RedirectToAction("LoNhap");
        }

        public ActionResult DoanhThu(DateTime? startDate,
                                    DateTime? endDate,
                                    int dlpage = 1,
                                    int dlpageSize = 10,
                                    int nxbpage = 1,
                                    int nxbpageSize = 10,
                                    string searchDaiLy = null,
                                    string searchNXB = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<DaiLy> DMDaily = DaiLyManager.getAllAlive()
                                    .Where(dl => dl.tinhTongTienThanhToanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                ViewBag.tongTienThu = DMDaily.Sum(s => s.TongTienThanhToanTheoThang);
                if (!String.IsNullOrEmpty(searchDaiLy))
                {
                    DMDaily = DaiLyManager.filter(searchDaiLy, DMDaily);
                    ViewBag.SearchKeyDaiLy = searchDaiLy;
                }
                List<NhaXuatBan> DMNXB = NhaXuatBanManager.getAllAlive()
                                    .Where(nxb => nxb.tinhTongTienThanhToanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                ViewBag.tongTienChi = DMNXB.Sum(s => s.TongTienThanhToanTheoThang);
                if (!String.IsNullOrEmpty(searchNXB))
                {
                    DMNXB = NhaXuatBanManager.filter(searchNXB, DMNXB);
                    ViewBag.SearchKeyNXB = searchNXB;
                }
                ViewBag.tongCongDaiLy = DMDaily.Sum(dl => dl.TongTienThanhToan);
                ViewBag.tongCongDaiLyTheoThang = DMDaily.Sum(dl => dl.TongTienThanhToanTheoThang);
                ViewBag.tongCongNXB = DMNXB.Sum(nxb => nxb.TongTienThanhToan);
                ViewBag.tongCongNXBTheoThang = DMNXB.Sum(nxb => nxb.TongTienThanhToanTheoThang);
                ViewBag.DaiLymodels = DMDaily.ToPagedList(dlpage, dlpageSize);
                ViewBag.NXBmodels = DMNXB.ToPagedList(nxbpage, nxbpageSize);
                setAlertMessage();
                return View();
            }
            setAlertMessage();
            return View();
        }
        public ActionResult SachBan(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<Sach> DMSach = SachManager.getAll()
                                    .Where(s => s.tongSoLuongBanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        s.tongTienBanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                ViewBag.tongSoLuongBan = DMSach.Sum(s => s.TongSoLuongBanTheoThang);
                ViewBag.tongTienBan = DMSach.Sum(s => s.TongTienBanTheoThang);
                if (!String.IsNullOrEmpty(search))
                {
                    DMSach = SachManager.filter(search, DMSach);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongCongSoLuong = DMSach.Sum(s => s.TongSoLuongBanTheoThang);
                ViewBag.tongCongTien = DMSach.Sum(s => s.TongTienBanTheoThang);
                var models = DMSach.ToPagedList(page, pageSize);
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
            return View();
        }
        public ActionResult CongNoNXB(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<Sach> DMSach = SachManager.getAll()
                                    .Where(s => s.tongSoLuongNXBNoTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        s.tongTienNXBNoTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                ViewBag.tongSoLuongNo = DMSach.Sum(s => s.TongSoLuongNXBNoTheoThang);
                ViewBag.tongTienNo = DMSach.Sum(s => s.TongTienNXBNoTheoThang);
                if (!String.IsNullOrEmpty(search))
                {
                    DMSach = SachManager.filter(search, DMSach);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongCongSoLuong = DMSach.Sum(s => s.TongSoLuongNXBNoTheoThang);
                ViewBag.tongCongTien = DMSach.Sum(s => s.TongTienNXBNoTheoThang);
                var models = DMSach.ToPagedList(page, pageSize);
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
            return View();
        }
        public ActionResult CongNoDaiLy(DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, string search = null)
        {
            if (startDate != null && endDate != null)
            {
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.startDate = startDate;
                ViewBag.endDate = endDate;
                List<Sach> DMSach = SachManager.getAll()
                                    .Where(s => s.tongSoLuongDaiLyNoTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        s.tongTienDaiLyNoTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                ViewBag.tongSoLuongNo = DMSach.Sum(s => s.TongSoLuongDaiLyNoTheoThang);
                ViewBag.tongTienNo = DMSach.Sum(s => s.TongTienDaiLyNoTheoTang);
                if (!String.IsNullOrEmpty(search))
                {
                    DMSach = SachManager.filter(search, DMSach);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongCongSoLuong = DMSach.Sum(s => s.TongSoLuongDaiLyNoTheoThang);
                ViewBag.tongCongTien = DMSach.Sum(s => s.TongTienDaiLyNoTheoTang);
                var models = DMSach.ToPagedList(page, pageSize);
                setAlertMessage();
                return View(models);
            }
            setAlertMessage();
            return View();
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

        public JsonResult GetPropertiesDaiLy(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in DaiLy.searchKeysDoanhThu())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesNXB(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in NhaXuatBan.searchKeysDoanhThu())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesSachBan(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in Sach.searchKeysSachBan())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesCongNoNXB(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in Sach.searchKeysCongNoNXB())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesCongNoDaiLy(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in Sach.searchKeysCongNoDaiLy())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion



    }
}