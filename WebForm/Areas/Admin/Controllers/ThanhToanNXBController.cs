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
    public class ThanhToanNXBController : BaseController
    {

        #region Private Properties
        private static HoaDonNXB _hoadon;
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
            List<HoaDonNXB> DMHoaDon = null;
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMHoaDon = HoaDonNXBManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMHoaDon = HoaDonNXBManager.getAll();
            }
            ViewBag.tongTien = DMHoaDon.Sum(hd => hd.TongTien);
            //ViewBag.tongSoLuong = DMHoaDon.Sum(hd => hd.ChiTiet.Sum(ct => ct.SoLuong));
            var models = DMHoaDon.ToPagedList(page, pageSize);
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
            var model = HoaDonNXBManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            setAlertMessage();
            return View(model);
        }

        // GET: PhieuNhap/ThanhToan
        public ActionResult ThanhToan(int? masonxb)
        {
            if (masonxb != null)
            {
                var nxb = NhaXuatBanManager.find((int)masonxb);
                if (nxb == null || nxb.TrangThai == 0)
                {
                    putErrorMessage("Không tìm thấy nhà xuất bản");
                    return RedirectToAction("ThanhToan");
                }
                ViewBag.cultureInfo = CultureInfo;
                ViewBag.currentNXB = nxb;
                ViewBag.DMSach = new SelectList(nxb.Sach.Where(s => s.CongNoNXB.Count > 0).ToList(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
                if (_hoadon == null)
                {
                    _hoadon = new HoaDonNXB();
                }
                _hoadon.MaSoNXB = nxb.MaSoNXB;
                _hoadon.NXB = nxb;
                _hoadon.NgayLap = DateTime.Now;
                setAlertMessage();
                return View(_hoadon);
            }
            else
            {
                ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAllAlive()
                                                .Where(nxb => nxb.TongTienNo > 0).ToList(),
                                        nameof(NhaXuatBanManager.Properties.MaSoNXB),
                                        nameof(NhaXuatBanManager.Properties.TenNXB), "");
                _hoadon = new HoaDonNXB();
                setAlertMessage();
                return View();
            }
        }

        // POST: PhieuNhap/Create
        [HttpPost]
        public ActionResult ThanhToan(HoaDonNXB model, FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                if (ModelState.IsValid)
                {
                    var result = HoaDonNXBManager.add(model);
                    if (result != 0)
                    {
                        _hoadon = null;
                        putSuccessMessage("Thêm hóa đơn thành công");
                        return RedirectToAction("Details", new { id = result });
                    }
                    else
                    {
                        putErrorMessage("Thánh toán không thành công");
                        return RedirectToAction("ThanhToan", new { masonxb = _hoadon.MaSoNXB });
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                    return RedirectToAction("ThanhToan", new { masonxb = _hoadon.MaSoNXB });
                }
                //ViewBag.cultureInfo = CultureInfo;
                //ViewBag.currentNXB = _hoadon.NXB;
                //ViewBag.DMSach = new SelectList(_hoadon.NXB.Sach.Where(s => s.CongNoNXB.Count > 0).ToList(),
                //                        nameof(SachManager.Properties.MaSoSach),
                //                        nameof(SachManager.Properties.TenSach), "");
                //_hoadon.NgayLap = DateTime.Now;
                //return View(_hoadon);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("ThanhToan", new { masonxb = _hoadon.MaSoNXB });
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
            if (_currentHoaDon == null || _currentHoaDon != id)
            {
                _currentHoaDon = id;
                _hoadon = HoaDonNXBManager.find((int)id);
                if (_hoadon == null)
                {
                    putErrorMessage("Không tìm thấy");
                    return RedirectToAction("All");
                }
                if (_hoadon.TrangThai == 1)
                {
                    //Nếu đã duyệt thì không cho sửa, chuyển sang trang chi tiết
                    _currentHoaDon = null;
                    putErrorMessage("Hóa đơn đã duyệt");
                    return RedirectToAction("Details", new { id = id });
                }
            }
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.currentNXB = _hoadon.NXB;
            ViewBag.DMSach = new SelectList(_hoadon.NXB.Sach.Where(s => s.CongNoNXB.Count > 0).ToList(),
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
            setAlertMessage();
            return View(_hoadon);
        }

        // POST: PhieuNhap/Edit/5
        [HttpPost]
        public ActionResult Edit(HoaDonNXB model, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (HoaDonNXBManager.edit(model))
                    {
                        _currentHoaDon = null;
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoHoaDon });
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
                return RedirectToAction("Edit", new { id = model.MaSoHoaDon });
                // TODO: Add update logic here
                //_hoadon = model;
                //ViewBag.currentNXB = _hoadon.NXB;
                //ViewBag.DMSach = new SelectList(_hoadon.NXB.Sach.Where(s => s.CongNoNXB.Count > 0).ToList(),
                //                        nameof(SachManager.Properties.MaSoSach),
                //                        nameof(SachManager.Properties.TenSach), "");
                //return View(_hoadon);
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoHoaDon });
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
            var model = HoaDonNXBManager.find((int)id);
            if (model == null)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("All");
            }
            if (model.TrangThai == 1)
            {
                putErrorMessage("Hóa đơn đã duyệt");
                return RedirectToAction("Details", new { id = model.MaSoHoaDon });
            }
            setAlertMessage();
            return View(model);
        }

        // POST: PhieuNhap/Delete/5
        [HttpPost]
        public ActionResult Delete(int? id, FormCollection collection)
        {
            try
            {
                if (HoaDonNXBManager.delete((int)id))
                {
                    putSuccessMessage("Đã xóa");
                    _currentHoaDon = null;
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
            var model = HoaDonNXBManager.find((int)id);
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
                putErrorMessage("Duyệt không thành công, vui lòng kiểm tra lại công nợ");
                return RedirectToAction("Edit", new { id });
            }
            
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in HoaDonNXB.searchKeys())
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
            var chitiet = new ChiTietHoaDonNXB();
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
                foreach (Sach s in nxb.Sach.Where(s => s.CongNoNXB.Count > 0).ToList())
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
            ViewBag.currentNXB = nxb;
            ViewBag.cultureInfo = CultureInfo;
            ViewBag.DMSach = new SelectList(nxb.Sach,
                                        nameof(SachManager.Properties.MaSoSach),
                                        nameof(SachManager.Properties.TenSach), "");
            chitiet.SoLuong = 1;
            chitiet.DonGia = chitiet.Sach.GiaNhap;
            _hoadon.addDetail(chitiet);
            return View("ChiTietEditorRow", chitiet);
        }
        public ViewResult DeleteDetailRow(int masosach)
        {
            _hoadon.deleteDetail(masosach);
            return null;
        }
        public ViewResult ChangeDetailRow(int masosach, int? masosach_new, int? soluong)
        {
            foreach(ChiTietHoaDonNXB ct in _hoadon.ChiTiet)
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
            var chitiet = new ChiTietHoaDonNXB();
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
