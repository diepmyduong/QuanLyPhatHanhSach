using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;

namespace WebForm.Controllers
{
    public class ProductController : BaseController
    {
        public ActionResult Details(int? id)
        {
            if(id == null)
            {
                return new HttpNotFoundResult("Bad Request");
            }
            var model = SachManager.find((int)id);
            if(model == null)
            {
                return new HttpNotFoundResult("Not Found!");
            }
            

            return View(model);
        }
    }
}