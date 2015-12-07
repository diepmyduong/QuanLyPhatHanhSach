using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;

namespace WebForm.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var rand = new Random();
            //Giới hạn sách hiển thị
            var limit = 6;
            var DMSach = SachManager.getAllAlive();
            //Láy random 6 sách
            var models = DMSach.OrderBy(c => rand.Next()).Take(limit);
            
            //Danh mục lĩnh vực
            ViewBag.DMLinhVuc = LinhVucManager.getAllALive().Take(5).ToList();
            return View(models);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}