using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;

namespace WebForm.Controllers
{
    public class ManagerController : BaseController
    {

        
        // GET: Manager
        public ActionResult Index()
        {
            if (isUserSessionExisted())
            {
                var model = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                setAlertMessage();
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }

        public ActionResult Agency()
        {
            if (isUserSessionExisted())
            {
                var model = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                setAlertMessage();
                return View(model.DaiLy);
            }
            else
            {
                return RedirectToAction("Index", "Home", null);
            }
        }
        [HttpPost]
        public ActionResult UpdateUser(NguoiDung model, FormCollection collection)
        {
            var errors = new List<string>();
            if (ModelState.IsValid)
            {
                var currentUser = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                var tempEmail = currentUser.Email;
                currentUser.TenDayDu = model.TenDayDu;
                currentUser.Email = model.Email;
                var result = currentUser.update();
                switch (result)
                {
                    case NguoiDung.UpdateStatus.Success:
                        Session[Core.Constants.SESSION.USERNAME] = currentUser;
                        TempData[Core.Constants.TEMPDATA.SUCCESS] = new List<string> { "Cập nhật thành công" };
                        break;
                    case NguoiDung.UpdateStatus.EmailIsExisted:
                        errors.Add("Email đã tồn tại");
                        currentUser.Email = tempEmail;
                        break;
                    case NguoiDung.UpdateStatus.Error:
                        errors.Add("Cập nhật không thành công");
                        break;
                }
                if(errors.Count > 0)
                {
                    TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                }
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var value in ModelState.Values)
                {
                    if (value.Errors.Count > 0)
                    {
                        foreach(var error in value.Errors)
                        {
                            errors.Add(error.ErrorMessage);
                        }
                    }
                }
                TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public ActionResult UpdateAgency(DaiLy model, FormCollection collection)
        {
            var errors = new List<string>();
            if (ModelState.IsValid)
            {
                var currentUser = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                model.MaSoNguoiDung = currentUser.MaSoNguoiDung;
                if (currentUser.DaiLy == null)
                {
                    var result = DaiLyManager.add(model);
                    if(result != 0)
                    {
                        model.MaSoDaiLy = result;
                        currentUser.DaiLy = model;
                        currentUser.MaSoDaiLy = result;
                        Session[Core.Constants.SESSION.USERNAME] = currentUser;
                        TempData[Core.Constants.TEMPDATA.SUCCESS] = new List<string> { "Đăng ký đại lý thành công" };
                    }
                    else
                    {
                        errors.Add("Đăng ký không thành công");
                        TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                    }
                }
                else
                {
                    if (DaiLyManager.edit(model))
                    {
                        currentUser.DaiLy = model;
                        Session[Core.Constants.SESSION.USERNAME] = currentUser;
                        TempData[Core.Constants.TEMPDATA.SUCCESS] = new List<string> { "Cập nhật thành công" };
                    }
                    else
                    {
                        
                        errors.Add("Cập nhật không thành công");
                        TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                    }
                }
                if (errors.Count > 0)
                {
                    TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                }
                return RedirectToAction("Agency");

            }
            else
            {
                foreach (var value in ModelState.Values)
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
                return RedirectToAction("Agency");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(FormCollection collection)
        {
            var errors = new List<string>();
            if (ModelState.IsValid)
            {
                var oldPassword = collection[Core.Constants.ID.OLD_PASSWORD].ToString();
                var confirmPassword = collection[Core.Constants.ID.CONFIRM_PASSWORD].ToString();
                var newPassword = collection[Core.Constants.ID.NEW_PASSWORD].ToString();
                var currentUser = Session[Core.Constants.SESSION.USERNAME] as NguoiDung;
                if(oldPassword.Length < 6)
                {
                    errors.Add("Mật khẩu ít nhất phải 6 ký tự");
                }
                if (!confirmPassword.Equals(newPassword))
                {
                    errors.Add("Mật khẩu không khớp");
                }
                if (!currentUser.MatKhau.Equals(oldPassword))
                {
                    errors.Add("Sai mật khẩu");
                }
                if(errors.Count > 0)
                {
                    TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                    return RedirectToAction("Index");
                }
                currentUser.MatKhau = newPassword;
                var result = currentUser.update();
                switch (result)
                {
                    case NguoiDung.UpdateStatus.Success:
                        Session[Core.Constants.SESSION.USERNAME] = currentUser;
                        TempData[Core.Constants.TEMPDATA.SUCCESS] = new List<string> { "Cập nhật thành công" };
                        break;
                    case NguoiDung.UpdateStatus.EmailIsExisted:
                        errors.Add("Email đã tồn tại");
                        break;
                    case NguoiDung.UpdateStatus.Error:
                        errors.Add("Cập nhật không thành công");
                        break;
                }
                if (errors.Count > 0)
                {
                    TempData[Core.Constants.TEMPDATA.ERRORS] = errors;
                }
                return RedirectToAction("Index");

            }
            else
            {
                foreach (var value in ModelState.Values)
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
                return RedirectToAction("Index");
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
    }
}