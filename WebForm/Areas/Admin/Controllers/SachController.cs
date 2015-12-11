using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.DAL;
using Core.BIZ;
using PagedList;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Data.Linq;

namespace WebForm.Areas.Admin.Controllers
{
    public class SachController : BaseController
    {
        #region Private Properties
        private CultureInfo _cultureInfo; // Thông tin văn hóa
        #endregion

        #region Public Properties
        public CultureInfo CultureInfo
        {
            get
            {
                if (_cultureInfo == null)
                {
                    _cultureInfo = CultureInfo.GetCultureInfo("vi-VN");
                }
                return _cultureInfo;
            }
        }
        #endregion

        #region Actions
        // GET: Sach
        public ActionResult Index(int page = 1, int pageSize = 10, string search = null)
        {
            List<Sach> DMSach = null;
            if (Session[Core.Constants.TEMPDATA.BOOKS] == null)
            {
                DMSach = SachManager.getAllAlive();
                Session[Core.Constants.TEMPDATA.BOOKS] = DMSach;
            }
            else
            {
                DMSach = Session[Core.Constants.TEMPDATA.BOOKS] as List<Sach>;
            }
            
            ViewBag.cultureInfo = CultureInfo;
            if (!String.IsNullOrEmpty(search))
            {
                DMSach = SachManager.filter(search, DMSach);
                ViewBag.SearchKey = search;
            }

            var models = DMSach.ToPagedList(page, pageSize);
            setAlertMessage();
            return View(models);
        }

        // GET: Sach/Details/5
        public ActionResult Details(int? id) // id là mã số sách
        {
            if(id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            ViewBag.cultureInfo = CultureInfo; // Sử dụng cho hiển thị tiền tệ VNĐ
            var model = SachManager.find((int)id);
            if(model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            if (model.HinhAnh == null)
            {
                ViewBag.DefaultImage = "/Resources/DefaultImage.png"; // Load hình ảnh mặc định nếu chưa có hình
            }
            else
            {
                ViewBag.imgSrc = ImagesHelper.ImageToDataBase64String(model.HinhAnhTypeImage);
            }
            setAlertMessage();
            return View(model);
        }

        // GET: Sach/Create
        public ActionResult Create()
        {
            var model = new Sach();
            //Combobox nhà xuất bản
            ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAllAlive(),
                nameof(NhaXuatBanManager.Properties.MaSoNXB),
                nameof(NhaXuatBanManager.Properties.TenNXB), "");
            //Combobox lĩnh vực
            ViewBag.DMLinhVuc = new SelectList(LinhVucManager.getAllALive(),
                nameof(LinhVucManager.Properties.MaSoLinhVuc),
                nameof(LinhVucManager.Properties.TenLinhVuc), "");
            setAlertMessage();
            return View(model);
        }

        // POST: Sach/Create
        [HttpPost]
        public ActionResult Create(Sach model, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.HinhAnhTypeImage = Image.FromStream(file.InputStream);
                    }
                    var result = SachManager.add(model);
                    if (result != 0)
                    {
                        putSuccessMessage("Thêm thành công");
                        return RedirectToAction("Details", new { id = result });
                    }
                    else
                    {
                        putErrorMessage("Thêm không thành công");
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return RedirectToAction("Create");
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Create");
            }
        }

        // GET: Sach/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = SachManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            //Combobox Nhà xuất bản
            ViewBag.DMNXB = new SelectList(NhaXuatBanManager.getAllAlive(),
                nameof(NhaXuatBanManager.Properties.MaSoNXB),
                nameof(NhaXuatBanManager.Properties.TenNXB), "");
            //Combobox lĩnh vực
            ViewBag.DMLinhVuc = new SelectList(LinhVucManager.getAllALive(),
                nameof(LinhVucManager.Properties.MaSoLinhVuc),
                nameof(LinhVucManager.Properties.TenLinhVuc), "");
            if (model.HinhAnh != null)
            {
                ViewBag.imgSrc = ImagesHelper.ImageToDataBase64String(model.HinhAnhTypeImage);
            }
            setAlertMessage();
            return View(model);
        }

        // POST: Sach/Edit/5
        [HttpPost]
        public ActionResult Edit(Sach model, HttpPostedFileBase file)
        {
            try
            {
                // TODO: Add update logic here
                if (ModelState.IsValid)
                {
                    if (file != null)
                    {
                        model.HinhAnhTypeImage = Image.FromStream(file.InputStream);
                    }
                    if (SachManager.edit(model))
                    {
                        putSuccessMessage("Cập nhật thành công");
                        return RedirectToAction("Details", new { id = model.MaSoSach });
                    }
                }
                else
                {
                    putModelStateFailErrors(ModelState);
                }
                return RedirectToAction("Edit",new { id = model.MaSoSach });
            }
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return RedirectToAction("Edit", new { id = model.MaSoSach });
            }
        }

        // GET: Sach/Delete/5
        public ActionResult Delete(int? id)
        {
            if(id == null)
            {
                putErrorMessage("Đường dẫn không chính xác");
                return RedirectToAction("Index");
            }
            var model = SachManager.find((int)id);
            if (model == null || model.TrangThai == 0)
            {
                putErrorMessage("Không tìm thấy");
                return RedirectToAction("Index");
            }
            ViewBag.cultureInfo = CultureInfo; // Sử dụng cho hiển thị tiền tệ VNĐ
            if (model.HinhAnh == null)
            {
                ViewBag.DefaultImage = "/Resources/DefaultImage.png"; // Load hình ảnh mặc định nếu chưa có hình
            }
            else
            {
                ViewBag.imgSrc = ImagesHelper.ImageToDataBase64String(model.HinhAnhTypeImage);
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
                var model = SachManager.find((int)id);
                if(model == null || model.TrangThai == 0)
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
            catch(Exception ex)
            {
                putErrorMessage(ex.Message);
                return View(id);
            }
        }
        #endregion

        #region JSON REQUEST
        public JsonResult GetProperties(string request)
        {
            List<string> results = new List<string>();
            foreach (string pro in Sach.searchKeys())
            {
                results.Add(request + pro);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        #endregion

    }
}
