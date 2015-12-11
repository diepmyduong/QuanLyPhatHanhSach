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
    public class PhieuXuatController : BaseController
    {

        #region Private Properties
        private static PhieuXuat _phieu;
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
            List<PhieuXuat> DMPhieu = null;
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMPhieu = PhieuXuatManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMPhieu = PhieuXuatManager.getAll();
            }
            ViewBag.tongTien = DMPhieu.Sum(ph => ph.TongTien);
            var models = DMPhieu.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }

        // GET: PhieuNhap/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("All");
            }
            var model = PhieuXuatManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            setAlertMessage();
            return View(model);
        }



        // GET: PhieuNhap/Create
        public ActionResult Create()
        {
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.DMSach = new SelectList(SachManager.getAllAlive()
                                                .Where(s => s.Soluong > 0).ToList(),
                                    nameof(SachManager.Properties.MaSoSach),
                                    nameof(SachManager.Properties.TenSach), "");
            ViewBag.DMDaiLy = new SelectList(DaiLyManager.getAllAlive(),
                                    nameof(DaiLyManager.Properties.MaSoDaiLy),
                                    nameof(DaiLyManager.Properties.TenDaiLy), "");
            if (_phieu == null)
            {
                _phieu = new PhieuXuat();
            }
            _phieu.NgayLap = DateTime.Now;
            setAlertMessage();
            return View(_phieu);
        }

        // POST: PhieuNhap/Create
        [HttpPost]
        public ActionResult Create(PhieuXuat model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = PhieuXuatManager.add(model);
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
                //ViewBag.cultureInfo = CultureInfo;
                //ViewBag.DMSach = new SelectList(SachManager.getAllAlive()
                //                                .Where(s => s.Soluong > 0).ToList(),
                //                    nameof(SachManager.Properties.MaSoSach),
                //                    nameof(SachManager.Properties.TenSach), "");
                //ViewBag.DMDaiLy = new SelectList(DaiLyManager.getAllAlive(),
                //                        nameof(DaiLyManager.Properties.MaSoDaiLy),
                //                        nameof(DaiLyManager.Properties.TenDaiLy), "");
                //_phieu.NgayLap = DateTime.Now;

                //return View(_phieu);
                return RedirectToAction("Create");
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return View();
            }
        }

        // GET: PhieuNhap/Edit/5
        public ActionResult Edit(int? id )
        {

            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("All");
            }
            if (_currentPhieu == null || _currentPhieu != id)
            {
                _currentPhieu = id;
                _phieu = PhieuXuatManager.find((int)id);
                if (_phieu == null)
                {
                    putErrorMessage("Không tìm thấy");
                    return RedirectToAction("All");
                }
                if (_phieu.TrangThai == 1)
                {
                    //Nếu đã duyệt thì không cho sửa, chuyển sang trang chi tiết
                    _currentPhieu = null;
                    putErrorMessage("Phiếu đã duyệt");
                    return RedirectToAction("Details", new { id = id });
                }
            }
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.DMSach = new SelectList(SachManager.getAllAlive()
                                                .Where(s => s.Soluong > 0).ToList(),
                                    nameof(SachManager.Properties.MaSoSach),
                                    nameof(SachManager.Properties.TenSach), "");
            ViewBag.DMDaiLy = new SelectList(DaiLyManager.getAllAlive(),
                                    nameof(DaiLyManager.Properties.MaSoDaiLy),
                                    nameof(DaiLyManager.Properties.TenDaiLy), "");
            setAlertMessage();
            return View(_phieu);
        }

        // POST: PhieuNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(PhieuXuat model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (PhieuXuatManager.edit(model))
                    {
                        _currentPhieu = null;
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoPhieuXuat });
                    }
                    else
                    {
                        putErrorMessage("Cập nhật không thành công");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                    return RedirectToAction("Edit", new { id = model.MaSoPhieuXuat});
                }
                //// TODO: Add update logic here
                //_phieu = model;
                //ViewBag.DMSach = new SelectList(SachManager.getAllAlive()
                //                                .Where(s => s.Soluong > 0).ToList(),
                //                    nameof(SachManager.Properties.MaSoSach),
                //                    nameof(SachManager.Properties.TenSach), "");
                //ViewBag.DMDaiLy = new SelectList(DaiLyManager.getAllAlive(),
                //                        nameof(DaiLyManager.Properties.MaSoDaiLy),
                //                        nameof(DaiLyManager.Properties.TenDaiLy), "");
                //return View(_phieu);
                return RedirectToAction("Edit", new { id = model.MaSoPhieuXuat });
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoPhieuXuat });
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
            var model = PhieuXuatManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            if (model.TrangThai == 1)
            {
                putErrorMessage("Phiếu đã duyệt");
                return RedirectToAction("Details", new { id = model.MaSoPhieuXuat });
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
                if (PhieuXuatManager.delete((int)id))
                {
                    putSuccessMessage("Xóa thành công");
                    return RedirectToAction("All");
                }
                else
                {
                    putErrorMessage("Xóa không thành công");
                    return RedirectToAction("Delete", new { id });
                }
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
            var model = PhieuXuatManager.find((int)id);
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
            var result = model.accept();
            switch (result)
            {
                case PhieuXuat.AcceptStatus.Success:
                    putSuccessMessage("Đã duyệt thành công");
                    return RedirectToAction("Details", new { id = id });
                case PhieuXuat.AcceptStatus.Error:
                    putErrorMessage("Sách tồn không đủ để duyệt! Phiếu xuất yêu cầu được hủy!");
                    return RedirectToAction("Edit", new { id });
                case PhieuXuat.AcceptStatus.Limited:
                    putErrorMessage("Tiền nợ đã vượt quá mức cho phép, vui lòng thanh toán trước khi đặt tiếp");
                    return RedirectToAction("Edit", new { id });
                default:
                    putErrorMessage("Duyệt không thành công");
                    return RedirectToAction("Edit", new { id });
            }
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in PhieuXuat.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion
        

        #region REQUEST
        public ViewResult BlankEditorRow()
        {
            var chitiet = new ChiTietPhieuXuat();
            var founded = false;
            foreach (Sach s in SachManager.getAllAlive().Where(s => s.Soluong > 0).ToList())
            {
                chitiet.MaSoSach = s.MaSoSach;
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
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.DMSach = new SelectList(SachManager.getAllAlive()
                                                .Where(s => s.Soluong > 0).ToList(),
                                    nameof(SachManager.Properties.MaSoSach),
                                    nameof(SachManager.Properties.TenSach), "");
            chitiet.SoLuong = 1;
            chitiet.DonGia = chitiet.Sach.GiaBan;
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
            foreach(ChiTietPhieuXuat ct in _phieu.ChiTiet)
            {
                if (ct.MaSoSach.Equals(masosach))
                {
                    if(masosach_new != null)
                    {
                        ct.MaSoSach = (int)masosach_new;
                        ct.Sach = SachManager.find(ct.MaSoSach);
                        ct.DonGia = ct.Sach.GiaBan;
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
            var chitiet = new ChiTietPhieuXuat();
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
