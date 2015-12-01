using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using System.Globalization;
using PagedList;

namespace WebForm.Controllers
{
    public class ThanhToanDaiLyController : Controller
    {

        #region Private Properties
        private static HoaDonDaiLy _hoadon;
        private static int? _currentHoaDon;
        private CultureInfo _cultureInfo; // Thông tin văn hóa
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
        // GET: PhieuNhap
        public ActionResult All(int page = 1, int pageSize = 10, string search = null)
        {
            List<HoaDonDaiLy> DMHoaDon = null;
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMHoaDon = HoaDonDaiLyManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMHoaDon = HoaDonDaiLyManager.getAll();
            }
            ViewBag.tongTien = DMHoaDon.Sum(hd => hd.TongTien);
            var models = DMHoaDon.ToPagedList(page, pageSize);
            return View(models);
        }

        // GET: PhieuNhap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = HoaDonDaiLyManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            return View(model);
        }

        // GET: PhieuNhap/ThanhToan
        public ActionResult ThanhToan(int? masodaily)
        {
            if (masodaily != null)
            {
                var dl = DaiLyManager.find((int)masodaily);
                if (dl == null || dl.TrangThai == 0)
                {
                    return new HttpNotFoundResult("Bad Request!");
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentDaiLy = dl;
                ViewBag.DMSach = new SelectList(dl.getSachNo(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                if (_hoadon == null)
                {
                    _hoadon = new HoaDonDaiLy();
                }
                _hoadon.MaSoDaiLy = dl.MaSoDaiLy;
                _hoadon.DaiLy = dl;
                _hoadon.NgayLap = DateTime.Now;
                return View(_hoadon);
            }
            else
            {
                ViewBag.DMDaiLy = new SelectList(DaiLyManager.getAllAlive()
                                                .Where(dl => dl.TongTienNo > 0).ToList(),
                                        nameof(DaiLyManager.Properties.MaSoDaiLy),
                                        nameof(DaiLyManager.Properties.TenDaiLy), "");
                _hoadon = new HoaDonDaiLy();
                return View();
            }
        }

        // POST: PhieuNhap/Create
        [HttpPost]
        public ActionResult ThanhToan(HoaDonDaiLy model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = HoaDonDaiLyManager.add(model);
                    if (result != 0)
                    {
                        _hoadon = null;
                        return RedirectToAction("Details", new { id = result });
                    }
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentDaiLy = _hoadon.DaiLy;
                ViewBag.DMSach = new SelectList(_hoadon.DaiLy.getSachNo(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                _hoadon.NgayLap = DateTime.Now;
                return View(_hoadon);
            }
            catch
            {
                return View();
            }
        }

        // GET: PhieuNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            if (_currentHoaDon == null || _currentHoaDon != id)
            {
                _currentHoaDon = id;
                _hoadon = HoaDonDaiLyManager.find((int)id);
                if (_hoadon == null)
                {
                    return new HttpNotFoundResult("Not Found!");
                }
                if (_hoadon.TrangThai == 1)
                {
                    //Nếu đã duyệt thì không cho sửa, chuyển sang trang chi tiết
                    _currentHoaDon = null;
                    return RedirectToAction("Details", new { id = id });
                }
            }
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.currentDaiLy = _hoadon.DaiLy;
            ViewBag.DMSach = new SelectList(_hoadon.DaiLy.getSachNo(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
            return View(_hoadon);
        }

        // POST: PhieuNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(HoaDonDaiLy model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (HoaDonDaiLyManager.edit(model))
                    {
                        _currentHoaDon = null;
                        return RedirectToAction("Details", new { id = model.MaSoHoaDon });
                    }
                }
                // TODO: Add update logic here
                _hoadon = model;
                ViewBag.currentDaiLy = _hoadon.DaiLy;
                ViewBag.DMSach = new SelectList(_hoadon.DaiLy.getSachNo(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                return View(_hoadon);
            }
            catch
            {
                return View();
            }
        }

        // GET: PhieuNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = HoaDonDaiLyManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            if (model.TrangThai == 1)
            {
                return RedirectToAction("Details", new { id = model.MaSoHoaDon });
            }
            return View(model);
        }

        // POST: PhieuNhap/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                if (HoaDonDaiLyManager.delete((int)id))
                {
                    return RedirectToAction("All");
                }
                _currentHoaDon = null;
                return View(id);
            }
            catch
            {
                return View(id);
            }
        }

        //Duyệt phiếu
        public ActionResult Accept(int? id)
        {

            if (id == null)
            {
                return new HttpNotFoundResult("Bad Request!");
            }
            var model = HoaDonDaiLyManager.find((int)id);
            if (model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            if (model.TrangThai == 1)
            {
                return RedirectToAction("Details", new { id = id });
            }
            if (model.accept())
            {
                return RedirectToAction("Details", new { id = id });
            }
            return View();
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in HoaDonDaiLy.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        

        #region REQUEST
        public ViewResult BlankEditorRow(int masodaily, int masosach = 0)
        {
            var dl = DaiLyManager.find((int)masodaily);
            var chitiet = new ChiTietHoaDonDaiLy();
            if(masosach != 0)
            {
                chitiet.MaSoSach = (int)masosach;
                if (_hoadon.ChiTiet.Contains(chitiet))
                {
                    return null;
                }
            }
            else
            {
                var founded = false;
                foreach (Sach s in dl.getSachNo())
                {
                    chitiet.MaSoSach = s.MaSoSach;
                    chitiet.Sach = s;
                    if (_hoadon.ChiTiet.Contains(chitiet))
                    {
                        continue;
                    }
                    founded = true;
                    break;
                }
                if (!founded)
                {
                    return null;
                }
            }
            ViewBag.currentDaiLy = dl;
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.DMSach = new SelectList(dl.getSachNo(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
            chitiet.SoLuong = 1;
            chitiet.DonGia = chitiet.Sach.GiaNhap;
            _hoadon.addDetail(chitiet);
            ViewData["masodaily"] = dl.MaSoDaiLy;
            return View("ChiTietEditorRow", chitiet);
        }
        public ViewResult DeleteDetailRow(int masosach)
        {
            _hoadon.deleteDetail(masosach);
            return null;
        }
        public ViewResult ChangeDetailRow(int masosach, int? masosach_new, int? soluong)
        {
            foreach(ChiTietHoaDonDaiLy ct in _hoadon.ChiTiet)
            {
                if (ct.MaSoSach.Equals(masosach))
                {
                    if(masosach_new != null)
                    {
                        ct.MaSoSach = (int)masosach_new;
                        ct.Sach = SachManager.find(ct.MaSoSach);
                        ct.DonGia = ct.Sach.GiaNhap;
                        ct.SoLuong = 1;
                    }
                    if(soluong != null)
                    {
                        ct.SoLuong = (int)soluong;
                    }
                    break;
                }
            }
            return null;
        }
        public JsonResult isDetailExisted(string masosach)
        {
            var key = Int32.Parse(masosach);
            var chitiet = new ChiTietHoaDonDaiLy();
            chitiet.MaSoSach = key;
            if (_hoadon.isDetailExisted(chitiet))
            {
                return Json(true,JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}
