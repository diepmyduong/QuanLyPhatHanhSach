using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using System.Web.Routing;
using Core.DAL;

namespace WebForm.Controllers
{
    public class BaseController : Controller
    {
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var sess = (NguoiDung) Session[Core.Constants.SESSION_USERNAME];
            if(sess == null)
            {
                //filterContext.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary(new { controller = "Account", action = "Login"}));
            }
            else
            {
                ViewBag.currentUser = sess;
            }
            ViewBag.Catalory = LinhVucManager.getAllALive();
            var DMSach = SachManager.getAllAlive();
            //Sách còn tồn nhiều nhất
            ViewBag.newProduct = DMSach.OrderByDescending(s => s.Soluong).FirstOrDefault();
            //Sách có giá cao nhất
            ViewBag.hightRateProducts = DMSach.OrderByDescending(s => s.GiaBan).Take(2).ToList();
            base.OnActionExecuted(filterContext);
            
        }
    }
}