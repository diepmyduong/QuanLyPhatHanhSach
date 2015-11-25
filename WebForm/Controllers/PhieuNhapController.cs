using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using System.Globalization;

namespace WebForm.Controllers
{
    public class PhieuNhapController : Controller
    {

        #region Private Properties
        private static PhieuNhap _phieu;
        private static int? currentPhieu;
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
        // GET: PhieuNhap
        public ActionResult Index()
        {
            return View();
        }

        // GET: PhieuNhap/Details/5
        public ActionResult Details(int? id)
        {
            if(id != null)
            {
                var model = PhieuNhapManager.find((int)id);
                if(model == null)
                {
                    return new HttpNotFoundResult("Not Found!");
                }
                return View(model);
            }
            return View();
        }

        // GET: PhieuNhap/Create
        public ActionResult Create(int? masonxb)
        {
            if (masonxb != null)
            {
                var nxb = NhaXuatBanManager.find((int)masonxb);
                if(nxb == null)
                {
                    return new HttpNotFoundResult("nhà xuất bản không tồn tại");
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentNXB = nxb;
                ViewBag.DMSach = new SelectList(nxb.Sach,
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                if(_phieu == null)
                {
                    _phieu = new PhieuNhap();
                }
                _phieu.MaSoNXB = nxb.MaSoNXB;
                _phieu.NXB = nxb;
                _phieu.NgayLap = DateTime.Now;
                return View(_phieu);
            }
            else
            {
                ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAll(),
                                        nameof(NhaXuatBanManager.Properties.MaSoNXB),
                                        nameof(NhaXuatBanManager.Properties.TenNXB), "");
                _phieu = new PhieuNhap();
                return View();
            }
        }

        // POST: PhieuNhap/Create
        [HttpPost]
        public ActionResult Create(PhieuNhap model,FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = PhieuNhapManager.add(model);
                    if (result != 0)
                    {
                        _phieu = null;
                        return RedirectToAction("Details", new { id = result });
                    }
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentNXB = _phieu.NXB;
                ViewBag.DMSach = new SelectList(_phieu.NXB.Sach,
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                _phieu.NgayLap = DateTime.Now;
                return View(_phieu);
            }
            catch
            {
                return View();
            }
        }

        // GET: PhieuNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id != null)
            {
                if(currentPhieu == null || currentPhieu != id)
                {
                    currentPhieu = id;
                    _phieu = PhieuNhapManager.find((int)id);
                    if (_phieu == null)
                    {
                        return new HttpNotFoundResult("Not Found!");
                    }
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentNXB = _phieu.NXB;
                ViewBag.DMSach = new SelectList(_phieu.NXB.Sach,
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                return View(_phieu);
            }
            return View();
        }

        // POST: PhieuNhap/Edit/5
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

        // GET: PhieuNhap/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PhieuNhap/Delete/5
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

        #region REQUEST
        public ViewResult BlankEditorRow(int masonxb, int masosach = 0)
        {
            var nxb = NhaXuatBanManager.find((int)masonxb);
            var chitiet = new ChiTietPhieuNhap();
            if(masosach != 0)
            {
                chitiet.MaSoSach = (int)masosach;
                if (_phieu.ChiTiet.Contains(chitiet))
                {
                    return null;
                }
            }
            else
            {
                var founded = false;
                foreach (Sach s in nxb.Sach)
                {
                    chitiet.MaSoSach = s.MaSoSach;
                    chitiet.Sach = s;
                    if (_phieu.ChiTiet.Contains(chitiet))
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
            ViewBag.currentNXB = nxb;
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.DMSach = new SelectList(nxb.Sach,
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
            chitiet.SoLuong = 1;
            chitiet.DonGia = chitiet.Sach.GiaNhap;
            _phieu.addDetail(chitiet);
            return View("ChiTietEditorRow", chitiet);
        }
        public ViewResult DeleteDetailRow(int masosach)
        {
            _phieu.deleteDetail(masosach);
            return null;
        }
        public ViewResult ChangeDetailRow(int masosach, int? masosach_new, int? soluong)
        {
            foreach(ChiTietPhieuNhap ct in _phieu.ChiTiet)
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
            var chitiet = new ChiTietPhieuNhap();
            chitiet.MaSoSach = key;
            if (_phieu.isDetailExisted(chitiet))
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
