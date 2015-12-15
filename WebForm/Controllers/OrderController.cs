using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;

namespace WebForm.Controllers
{
    public class OrderController : BaseController
    {
        private static PhieuXuat _phieu;
        private static NguoiDung _nguoidung;

        private PhieuXuat currentPhieu
        {
            get
            {
                if(_phieu == null)
                {
                    _phieu = new PhieuXuat();
                    if(Session[Core.Constants.SESSION.SHOPPING_CART] != null)
                    {
                        _phieu.ChiTiet = Session[Core.Constants.SESSION.SHOPPING_CART] as List<ChiTietPhieuXuat>;
                    }
                }
                if(_phieu.MaSoDaiLy == 0)
                {
                    if(Session[Core.Constants.SESSION.USERNAME] != null)
                    {
                        var user = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                        if(user.DaiLy == null)
                        {
                            TempData[Core.Constants.TEMPDATA.ERRORS] = new List<string>(){ "Bạn chưa đăng ký thông tin đại lý"};
                        }
                        else
                        {
                            _phieu.MaSoDaiLy = user.MaSoDaiLy;
                            _phieu.Daily = user.DaiLy;
                            _phieu.NguoiNhan = user.TenDayDu;
                        }
                    }
                }
                return _phieu;
            }
            set
            {
                _phieu = value;
            }
        }
        private NguoiDung currentNguoiDung
        {
            get
            {
                if(_nguoidung == null)
                {
                    _nguoidung = new NguoiDung();
                    if(Session[Core.Constants.SESSION.USERNAME] != null)
                    {
                        _nguoidung = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                    }
                }
                return _nguoidung;
            }
            set
            {
                _nguoidung = value;
                Session[Core.Constants.SESSION.USERNAME] = _nguoidung;
            }
        }
        public ViewResult AddToCart(int id, int? quantity)
        {
            currentPhieu.ChiTiet = Session[Core.Constants.SESSION.SHOPPING_CART] as List<ChiTietPhieuXuat>;
            var sach = SachManager.find(id);
            if(sach == null)
            {
                return null;
            }
            if(quantity != null)
            {
                currentPhieu.addDetail(sach, (int)quantity);
            }
            else
            {
                currentPhieu.addDetail(sach, 1);
            }
            Session[Core.Constants.SESSION.SHOPPING_CART] = currentPhieu.ChiTiet;
            return View("Cart");
        }

        public ActionResult ShoppingCart()
        {
            currentPhieu = null;
            var model = currentPhieu;
            if (TempData[Core.Constants.TEMPDATA.ERRORS] != null)
            {
                ViewBag.Errors = TempData[Core.Constants.TEMPDATA.ERRORS];
                TempData[Core.Constants.TEMPDATA.ERRORS] = null;
            }
            return View(model);
        }

        public ActionResult CheckOut()
        {
            currentPhieu = null;
            if (isUserSessionExisted())
            {
                var user = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                if(user.DaiLy == null)
                {
                    putErrorMessage("Chưa đăng ký thông tin đại lý");
                    return RedirectToAction("Agency", "Manager",null);
                }
                var model = currentPhieu;
                if (TempData[Core.Constants.TEMPDATA.ERRORS] != null)
                {
                    ViewBag.Errors = TempData[Core.Constants.TEMPDATA.ERRORS];
                    TempData[Core.Constants.TEMPDATA.ERRORS] = null;
                }
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
            
        }

        [HttpPost]
        public ActionResult CheckOut(PhieuXuat model)
        {
            if (isUserSessionExisted())
            {

                currentPhieu.Daily = currentNguoiDung.DaiLy;
                currentPhieu.NgayLap = DateTime.Now;
                currentPhieu.NguoiNhan = currentNguoiDung.TenDayDu;
                currentPhieu.MaSoDaiLy = currentNguoiDung.DaiLy.MaSoDaiLy;
                var result = PhieuXuatManager.add(currentPhieu);
                if(result != 0)
                {
                    return RedirectToAction("OrderDetail","Manager",new { id = result });
                }
                else
                {
                    return RedirectToAction("CheckOut");
                }
            }
            else
            {
                return RedirectToAction("Login", "Account", null);
            }
            
        }
        public bool isUserSessionExisted()
        {
            if (Session[Core.Constants.SESSION.USERNAME] != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ViewResult DeleteDetailRow(int masosach)
        {
            currentPhieu.deleteDetail(masosach);
            return null;
        }
        public ViewResult ChangeDetailRow(int masosach, int? soluong)
        {
            foreach (ChiTietPhieuXuat ct in currentPhieu.ChiTiet)
            {
                if (ct.MaSoSach.Equals(masosach))
                {
                    if (soluong != null)
                    {
                        ct.SoLuong = (int)soluong;
                    }
                    break;
                }
            }
            return null;
        }


    }
}