using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.BIZ;
using Core.DAL;
using System.IO;

namespace WebForm.Areas.Admin.Controllers
{
    public class PrinterController : Controller
    {
        // GET: Admin/Printer
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public FileStreamResult TonKho(DateTime date)
        {
            var models = TheKhoManager.getAllByDate(date);
            var printer = new PrintHelper();
            printer.FileName = "report.pdf";
            printer.FolderPath = "D://";
            printer.Title = "Thống kê tồn kho";
            var info = new MemoryStream(printer.printTonKho(models, date));
            
            return new FileStreamResult(info, "application/pdf");
        }

        [HttpPost]
        public FileStreamResult DoanhThu(DateTime startDate, DateTime endDate)
        {
            List<DaiLy> DMDaily = DaiLyManager.getAllAlive()
                                    .Where(dl => dl.tinhTongTienThanhToanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
            List<NhaXuatBan> DMNXB = NhaXuatBanManager.getAllAlive()
                                    .Where(nxb => nxb.tinhTongTienThanhToanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
            var printer = new PrintHelper();
            printer.FileName = "report.pdf";
            printer.FolderPath = "D://";
            printer.Title = "Thống kê doanh thu";
            var info = new MemoryStream(printer.printDoanhThu(DMDaily, DMNXB,startDate,endDate));

            return new FileStreamResult(info, "application/pdf");
        }

        [HttpPost]
        public FileStreamResult SachBan(DateTime startDate, DateTime endDate)
        {
            List<Sach> DMSach = SachManager.getAll()
                                    .Where(s => s.tongSoLuongBanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0 &&
                                        s.tongTienBanTheoThang(
                                        ((DateTime)startDate).Month,
                                        ((DateTime)startDate).Year,
                                        ((DateTime)endDate).Month,
                                        ((DateTime)endDate).Year) > 0).ToList();
            var printer = new PrintHelper();
            printer.FileName = "report.pdf";
            printer.FolderPath = "D://";
            printer.Title = "Thống kê sách bán";
            var info = new MemoryStream(printer.printSachBan(DMSach, startDate, endDate));

            return new FileStreamResult(info, "application/pdf");
        }
    }
}