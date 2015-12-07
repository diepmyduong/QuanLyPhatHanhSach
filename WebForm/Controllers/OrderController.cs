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

        public PhieuXuat currentPhieu
        {
            get
            {
                if(_phieu == null)
                {
                    _phieu = new PhieuXuat();
                    if(Session[Core.Constants.SESSION_SHOPPING_CART] != null)
                    {
                        _phieu.ChiTiet = Session[Core.Constants.SESSION_SHOPPING_CART] as List<ChiTietPhieuXuat>;
                    }
                }
                return _phieu;
            }
            set
            {
                _phieu = value;
            }
        }

        public ViewResult AddToCart(int id)
        {
            currentPhieu.ChiTiet = Session[Core.Constants.SESSION_SHOPPING_CART] as List<ChiTietPhieuXuat>;
            var sach = SachManager.find(id);
            if(sach == null)
            {
                return null;
            }
            currentPhieu.addDetail(sach, 1);
            Session[Core.Constants.SESSION_SHOPPING_CART] = currentPhieu.ChiTiet;
            return View("Cart");
        }

        public ActionResult ShoppingCart()
        {
            return View(currentPhieu);
        }

        public ActionResult CheckOut()
        {
            return View(currentPhieu);
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