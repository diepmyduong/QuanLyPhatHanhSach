using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using System.Globalization;
using PagedList;

namespace WebForm.Areas.Admin.Controllers
{
    public class PhieuNhapController : BaseController
    {

        #region Private Properties
        private static PhieuNhap _phieu;
        private static int? _currentPhieu;
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
            List<PhieuNhap> DMPhieu = null;
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMPhieu = PhieuNhapManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMPhieu = PhieuNhapManager.getAll();
            }
            ViewBag.tongTien = DMPhieu.Sum(ph => ph.TongTien);
            //ViewBag.tongSoLuong = DMPhieu.Sum(ph => ph.ChiTiet.Sum(ct => ct.SoLuong));
            var models = DMPhieu.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }

        // GET: PhieuNhap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dãn không chính xác");
                return RedirectToAction("All");
            }
            var model = PhieuNhapManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            setAlertMessage();
            return View(model);
        }

        // GET: PhieuNhap/Create
        public ActionResult Create(int? masonxb)
        {
            if (masonxb != null)
            {
                var nxb = NhaXuatBanManager.find((int)masonxb);
                if (nxb == null || nxb.TrangThai == 0)
                {
                    putErrorMessage("Không tìm thấy Nhà xuát bản");
                    return RedirectToAction("Create");
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentNXB = nxb;
                ViewBag.DMSach = new SelectList(nxb.Sach,
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                if (_phieu == null)
                {
                    _phieu = new PhieuNhap();
                }
                _phieu.MaSoNXB = nxb.MaSoNXB;
                _phieu.NXB = nxb;
                _phieu.NgayLap = DateTime.Now;
                setAlertMessage();
                return View(_phieu);
            }
            else
            {
                ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAllAlive(),
                                        nameof(NhaXuatBanManager.Properties.MaSoNXB),
                                        nameof(NhaXuatBanManager.Properties.TenNXB), "");
                _phieu = new PhieuNhap();
                setAlertMessage();
                return View();
            }
        }

        // POST: PhieuNhap/Create
        [HttpPost]
        public ActionResult Create(PhieuNhap model, FormCollection collection)
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
                        putSuccessMessage("Thêm thành công");
                        return RedirectToAction("Details", new { id = result });
                    }
                    else
                    {
                        putErrorMessage("Thêm không thành công");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return RedirectToAction("Create", new { masonxb = _phieu.MaSoNXB });
                //ViewBag.cultureInfo = CultureInfo;
                //ViewBag.currentNXB = _phieu.NXB;
                //ViewBag.DMSach = new SelectList(_phieu.NXB.Sach,
                //                        nameof(SachManager.Properties.MaSoSach),
                //                        nameof(SachManager.Properties.TenSach), "");
                //_phieu.NgayLap = DateTime.Now;
                //setAlertMessage();
                //return View(_phieu);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Create", new { masonxb = _phieu.MaSoNXB });
            }
        }

        // GET: PhieuNhap/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("All");
            }
            if (_currentPhieu == null || _currentPhieu != id)
            {
                _currentPhieu = id;
                _phieu = PhieuNhapManager.find((int)id);
                if (_phieu == null)
                {
                    putErrorMessage("Không tìm thấy");
                    return RedirectToAction("All");
                }
                if (_phieu.TrangThai == 1)
                {
                    //Nếu đã duyệt thì không cho sửa, chuyển sang trang chi tiết
                    putErrorMessage("Phiếu đã duyệt");
                    return RedirectToAction("Details", new { id = id });
                }
            }
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.currentNXB = _phieu.NXB;
            ViewBag.DMSach = new SelectList(_phieu.NXB.Sach,
                                    nameof(SachManager.Properties.MaSoSach),
                                    nameof(SachManager.Properties.TenSach), "");
            setAlertMessage();
            return View(_phieu);
        }

        // POST: PhieuNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(PhieuNhap model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (PhieuNhapManager.edit(model))
                    {
                        _currentPhieu = null;
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoPhieuNhap });
                    }
                    else
                    {
                        putErrorMessage("Cập nhật thất bại");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return RedirectToAction("Edit", new { id = model.MaSoNXB });
                // TODO: Add update logic here
                //_phieu = model;
                //ViewBag.currentNXB = _phieu.NXB;
                //ViewBag.DMSach = new SelectList(_phieu.NXB.Sach,
                //                        nameof(SachManager.Properties.MaSoSach),
                //                        nameof(SachManager.Properties.TenSach), "");
                //return View(_phieu);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoNXB });
            }
        }

        // GET: PhieuNhap/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("All");
            }
            var model = PhieuNhapManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            if (model.TrangThai == 1)
            {
                putErrorMessage("Phiếu đã duyệt");
                return RedirectToAction("Details", new { id = model.MaSoPhieuNhap });
            }
            setAlertMessage();
            return View(model);
        }

        // POST: PhieuNhap/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                if (PhieuNhapManager.delete((int)id))
                {
                    putSuccessMessage("Xóa thành công");
                    return RedirectToAction("All");
                }
                else
                {
                    putErrorMessage("Xóa thất bại");
                }
                return RedirectToAction("Delete", new { id });
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Delete", new { id });
            }
        }

        //Duyệt phiếu
        public ActionResult Accept(int? id)
        {

            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("All");
            }
            var model = PhieuNhapManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            if (model.TrangThai == 1)
            {
                putErrorMessage("Phiếu đã duyệt");
                return RedirectToAction("Details", new { id = id });
            }
            if (model.accept())
            {
                putSuccessMessage("Duyệt thành công");
                return RedirectToAction("Details", new { id = id });
            }
            else
            {
                putErrorMessage("Duyệt thất bại");
                return RedirectToAction("Delete", new { id });
            }
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in PhieuNhap.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        

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
