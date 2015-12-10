using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;
using PagedList;

namespace WebForm.Areas.Admin.Controllers
{
    public class NguoiDungController : BaseController
    {
        #region Private Properties
        
        #endregion

        #region Public Properties
        
        #endregion

        #region Actions
        // GET: Sach
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<NguoiDung> DMND = null;
            if (!String.IsNullOrEmpty(search))
            {
                DMND = NguoiDungManager.filter(search);
                ViewBag.SearchKey = search;
            }
            else
            {
                DMND = NguoiDungManager.getAllAlive();
            }

            var models = DMND.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }

        // GET: Sach/Details/5
        public ActionResult Details(int? id) // id là mã số sách
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = NguoiDungManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            var model = new NguoiDung();
            setAlertMessage();
            return View(model);
        }

        // POST: Sach/Create
        [HttpPost]
        public ActionResult Create(NguoiDung model, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var confirmPass = collection[Core.Constants.ID.CONFIRM_PASSWORD].ToString();
                    if (!confirmPass.Equals(model.MatKhau))
                    {
                        putErrorMessage("Mật khẩu không khớp, vui lòng nhập lại");
                        setAlertMessage();
                        return View(model);
                    }
                    var isAdmin = (collection[Core.Constants.ID.IS_ADMIN] != null);
                    if (isAdmin)
                    {
                        model.PhanQuyen = nameof(NguoiDungManager.Roles.admin);
                    }
                    else
                    {
                        model.PhanQuyen = nameof(NguoiDungManager.Roles.daily);
                    }
                    var result = model.signUp();
                    switch (result)
                    {
                        case NguoiDung.SignUpStatus.Success:
                            putSuccessMessage("Đã tạo thành công");
                            return RedirectToAction("Details", "NguoiDung", new { id = model.MaSoNguoiDung });
                        case NguoiDung.SignUpStatus.UserIStExisted:
                            putErrorMessage("Tên đăng nhập đã tồn tại");
                            break;
                        case NguoiDung.SignUpStatus.EmailIsExisted:
                            putErrorMessage("Email đã tồn tại");
                            break;
                        case NguoiDung.SignUpStatus.Error:
                            putErrorMessage("Tạo không thành công");
                            break;
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                setAlertMessage();
                return View(model);
            }
            catch (Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Create");
            }
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = NguoiDungManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        public ActionResult Edit(NguoiDung model, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    var isAdmin = (collection[Core.Constants.ID.IS_ADMIN] != null) ;
                    if (isAdmin)
                    {
                        model.PhanQuyen = nameof(NguoiDungManager.Roles.admin);
                    }
                    else
                    {
                        model.PhanQuyen = nameof(NguoiDungManager.Roles.daily);
                    }
                    var result = model.update();
                    switch (result)
                    {
                        case NguoiDung.UpdateStatus.Success:
                            putSuccessMessage("Cập nhật thành công");
                            return RedirectToAction("Details", new { id = model.MaSoNguoiDung });
                        case NguoiDung.UpdateStatus.EmailIsExisted:
                            putErrorMessage("Email đã tồn tại");
                            return RedirectToAction("Edit", new { id = model.MaSoNguoiDung });
                        case NguoiDung.UpdateStatus.Error:
                            putErrorMessage("Cập nhật không thành công");
                            return RedirectToAction("Edit", new { id = model.MaSoNguoiDung });
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return RedirectToAction("Edit", new { id = model.MaSoNguoiDung });
            }
            catch (Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoNguoiDung });
            }
        }

        // GET: Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = NguoiDungManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            setAlertMessage();
            return View(model);
        }

        // POST: Sach/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                var model = NguoiDungManager.find((int)id);
                if (model == null || model.TrangThai == 0)
                {
                    putErrorMessage("Không tìm thấy");
                    return RedirectToAction("Index");
                }
                // TODO: Add delete logic here
                if (model.delete())
                {
                    putSuccessMessage("Xóa thành công");
                    return RedirectToAction("Index");
                }
                else
                {
                    putErrorMessage("Xóa không thành công");
                    return RedirectToAction("Delete", new { id });
                }
            }
            catch (Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Delete", new { id });
            }
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in NguoiDung.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion


    }
}