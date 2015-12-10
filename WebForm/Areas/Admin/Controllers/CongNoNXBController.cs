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
    public class CongNoNXBController : BaseController
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
                List<NhaXuatBan> DMNXB = NhaXuatBanManager.getAllAlive()
                                    .Where(nxb => nxb.tinhTongSoLuongNoTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        nxb.tongTienNoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        nxb.tongTienNhapThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
                if (!String.IsNullOrEmpty(search))
                {
                    DMNXB = NhaXuatBanManager.filter(search, DMNXB);
                    ViewBag.SearchKey = search;
                }
                ViewBag.tongTienNhap = DMNXB.Sum(nxb => nxb.TongTienNhapTheoThang);
                ViewBag.tongSoLuongNo = DMNXB.Sum(nxb => nxb.TongSoLuongNoTheoThang);
                ViewBag.tongTienNo = DMNXB.Sum(s => s.TongTienNoThang);
                var models = DMNXB.ToPagedList(page, pageSize);
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
            var nxb = NhaXuatBanManager.find((int)id);
            if(nxb == null)
            {
                putErrorMessage("Không tìm thấy Nhà xuất bản");
                return RedirectToAction("TheoDoi");
            }
            ViewBag.currentNXB = nxb;
            var DMCongNo = nxb.CongNo;
            if (!String.IsNullOrEmpty(search))
            {
                DMCongNo = CongNoNXBManager.filter(search, DMCongNo);
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
            foreach (string pro in NhaXuatBan.searchKeysTheoDoiNo())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetPropertiesChiTietNo(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in CongNoNXB.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}