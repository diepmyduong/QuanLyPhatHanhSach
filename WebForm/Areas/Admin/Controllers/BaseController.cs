using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using System.Web.Routing;
using Core.DAL;

namespace WebForm.Areas.Admin.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sess = (NguoiDung) Session[Core.Constants.SESSION.USERNAME];
            if(sess == null)
            {

                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login", area = "" }));
            }
            else if(!sess.PhanQuyen.Contains("admin"))
            {
                    filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary(new { controller = "Account", action = "Login" }));
            }
            else
            {
                ViewBag.currentUser = sess;
                var request = String.Format("{{ {0} = chưa duyệt }}", nameof(HoaDonDaiLyManager.Properties.TrangThai));
                ViewBag.HoaDonChuaDuyet = HoaDonDaiLyManager.filter(request).Count;
                request = String.Format("{{ {0} = chưa duyệt }}", nameof(PhieuXuatManager.Properties.TrangThai));
                ViewBag.PhieuXuatChuaDuyet = PhieuXuatManager.filter(request).Count;
                request = String.Format("{{ {0} = đã duyệt }} {{ {1} = {2} }}",
                    nameof(HoaDonDaiLyManager.Properties.TrangThai),
                    nameof(HoaDonDaiLyManager.Properties.NgayLap),
                    String.Format("{0:yyyy/MM}",DateTime.Now));
                ViewBag.ThanhToanTrongThang = HoaDonDaiLyManager.filter(request).Count;
                if(Session[Core.Constants.SESSION.PAGE_SIZE] != null)
                {
                    ViewBag.PageSize = Session[Core.Constants.SESSION.PAGE_SIZE];
                }
            }

            
        }

        protected void setAlertMessage()
        {
            if (TempData[Core.Constants.TEMPDATA.ERRORS] != null)
            {
                ViewBag.Errors = TempData[Core.Constants.TEMPDATA.ERRORS];
                TempData[Core.Constants.TEMPDATA.ERRORS] = null;
            }
            if (TempData[Core.Constants.TEMPDATA.SUCCESS] != null)
            {
                ViewBag.Success = TempData[Core.Constants.TEMPDATA.SUCCESS];
                TempData[Core.Constants.TEMPDATA.SUCCESS] = null;
            }
        }
        protected void putErrorMessage(string message)
        {
            if (TempData[Core.Constants.TEMPDATA.ERRORS] != null)
            {
                (TempData[Core.Constants.TEMPDATA.ERRORS] as List<string>).Add(message);
            }
            else
            {
                TempData[Core.Constants.TEMPDATA.ERRORS] = new List<string> { message };
            }
        }
        protected void putSuccessMessage(string message)
        {
            if (TempData[Core.Constants.TEMPDATA.SUCCESS] != null)
            {
                (TempData[Core.Constants.TEMPDATA.SUCCESS] as List<string>).Add(message);
            }
            else
            {
                TempData[Core.Constants.TEMPDATA.SUCCESS] = new List<string> { message };
            }
        }

        protected void putModelStateFailErrors(ModelStateDictionary modelState)
        {
            var errors = new List<string>();
            foreach (var value in modelState.Values)
            {
                if (value.Errors.Count > 0)
                {
                    foreach (var error in value.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                }
            }
            TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
        }

    }
}