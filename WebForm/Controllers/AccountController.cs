using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using WebForm.Models;
using Core.BIZ;
using Core.DAL;
using System.Collections.Generic;

namespace WebForm.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        
        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            if (isUserSessionExisted())
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                ViewBag.ReturnUrl = returnUrl;
                return View();
            }
        }
        [AllowAnonymous]
        public ActionResult Logout()
        {
            if (isUserSessionExisted())
            {
                Session[Core.Constants.SESSION.USERNAME] = null;
                Session[Core.Constants.SESSION.SHOPPING_CART] = null;
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
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
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(FormCollection collection)
        {
            var username = collection[Core.Constants.ID.USERNAME].ToString();
            var password = collection[Core.Constants.ID.PASSWORD].ToString();
            var isRemember = collection[Core.Constants.ID.REMEBER_LOGIN];
            var nd = new NguoiDung();
            nd.TenNguoiDung = username;
            nd.MatKhau = password;
            var result = nd.login();
            List<string> errors = new List<string>();
            switch (result)
            {
                case NguoiDung.LoginStatus.Success:
                    Session.Add(Core.Constants.SESSION.USERNAME, NguoiDungManager.findByName(username));
                    return RedirectToAction("Index", "Home", null);
                case NguoiDung.LoginStatus.Error:
                    errors.Add("Đặng nhập không thành công!");
                    break;
                case NguoiDung.LoginStatus.WrongPass:
                    errors.Add("Mật khẩu không đúng");
                    break;
                case NguoiDung.LoginStatus.NotExisted:
                    errors.Add("Người dùng không tồn tại");
                    break;
            }
            ViewBag.Errors = errors;
            return View();
        }
        
        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            if (isUserSessionExisted())
            {
                return RedirectToAction("Index", "Home", null);
            }
            else
            {
                var model = new NguoiDung();
                model.PhanQuyen = "dayly";
                if (TempData[Core.Constants.TEMPDATA.ERRORS] != null)
                {
                    ViewBag.Errors = TempData[Core.Constants.TEMPDATA.ERRORS];
                    TempData[Core.Constants.TEMPDATA.ERRORS] = null;
                }
                return View(model);
            }
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(NguoiDung model, FormCollection collection)
        {
            var errors = new List<string>();
            if (ModelState.IsValid)
            {
                var confirmPass = collection[Core.Constants.ID.CONFIRM_PASSWORD].ToString();
                model.PhanQuyen = "daily";
                if (!confirmPass.Equals(model.MatKhau))
                {
                    errors.Add("Mật khẩu không khớp, vui lòng nhập lại");
                    ViewBag.Errors = errors;
                    return View(model);
                }
                var result = model.signUp();
                switch (result)
                {
                    case NguoiDung.SignUpStatus.Success:
                        Session.Add(Core.Constants.SESSION.USERNAME, NguoiDungManager.find(model.MaSoNguoiDung));
                        return RedirectToAction("Index", "Home", null);
                    case NguoiDung.SignUpStatus.UserIStExisted:
                        errors.Add("Tên đăng nhập đã tồn tại");
                        break;
                    case NguoiDung.SignUpStatus.EmailIsExisted:
                        errors.Add("Email đã tồn tại");
                        break;
                    case NguoiDung.SignUpStatus.Error:
                        errors.Add("Đăng ký không thành công");
                        break;
                }
                ViewBag.Errors = errors;
                return View(model);

            }
            else
            {
                foreach(var value in ModelState.Values)
                {
                    if(value.Errors.Count > 0)
                    {
                        errors.Add(value.Errors.ToString());
                    }
                }
                ViewBag.Errors = errors;
                return View(model);
            }
            
        }
    }
}