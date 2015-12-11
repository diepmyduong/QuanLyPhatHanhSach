using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
namespace WebForm.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            var rand = new Random();
            //Giới hạn sách hiển thị
            var limit = 6;
            List<Sach> DMSach = null;
            if (Session[Core.Constants.SESSION.BOOKS] == null)
            {
                DMSach = SachManager.getAllAlive();
                Session[Core.Constants.SESSION.BOOKS] = DMSach;
            }
            else
            {
                DMSach = Session[Core.Constants.SESSION.BOOKS] as List<Sach>;
            }
            //Láy random 6 sách
            var models = DMSach.OrderBy(c => rand.Next()).Take(limit);
            
            //Danh mục lĩnh vực
            ViewBag.DMLinhVuc = LinhVucManager.getAllALive().Take(5).ToList();
            return View(models);
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public ActionResult Search(string request = null , int page = 1, int pageSize = 9)
        {
            if (String.IsNullOrEmpty(request))
            {
                return RedirectToAction("Index");
            }
            List<Sach> DMSach = null;
            if (Session[Core.Constants.SESSION.BOOKS] == null)
            {
                DMSach = SachManager.getAllAlive();
                Session[Core.Constants.SESSION.BOOKS] = DMSach;
            }
            else
            {
                DMSach = Session[Core.Constants.SESSION.BOOKS] as List<Sach>;
            }
            DMSach = SachManager.filter(request, DMSach);
            if(DMSach.Count == 0)
            {
                putErrorMessage("Không có kết quả nào");
            }
            ViewBag.searchKey = request;
            var models = DMSach.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }
    }
}