using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.DAL;

namespace Core.BIZ
{
    public class PrintHelper
    {
        private Document _doc;
        private Paragraph _tilte;
        private Font _h1;
        private Font _h2;
        private Font _h3;
        private Font _normal;
        private Font _underline;
        private Font _bold;
        
        public PrintHelper()
        {
            _doc = new Document(PageSize.A4, 10, 10, 10, 2);
            _tilte = new Paragraph("Page Title");
            _tilte.Alignment = 1; //center
            FileName = Guid.NewGuid() + ".pdf";
            FontFactory.RegisterDirectory(Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts");
            _h1 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 28f, Font.BOLD, BaseColor.BLACK);
            _h2 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 20f, Font.NORMAL, BaseColor.BLACK);
            _h3 = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 14f, Font.NORMAL, BaseColor.BLACK);
            _normal = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, Font.NORMAL, BaseColor.BLACK);
            _bold = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, Font.BOLD, BaseColor.BLACK);
            _underline = FontFactory.GetFont("Times New Roman", BaseFont.IDENTITY_H, BaseFont.EMBEDDED, 10f, Font.UNDERLINE, BaseColor.BLACK);
        }

        public string FileName { get; set; }
        public string FolderPath { get; set; }
        public string Title
        {
            get
            {
                return _tilte.ToString();
            }
            set
            {
                _tilte = new Paragraph(value,_h1);
                _tilte.Alignment = 1;
            }
        }



        public void setupPage(Rectangle pageSize)
        {
            _doc = new Document(pageSize, 2, 2, 2, 2);
        }

        public byte[] printDMSach(List<Sach> DMSach)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data
                PdfPTable pdfTab = new PdfPTable(8); // here 4 is no of column
                pdfTab.HorizontalAlignment = 1; // 0- Left, 1- Center, 2- right
                pdfTab.SpacingBefore = 20f;
                pdfTab.SpacingAfter = 20f;

                //Set header
                pdfTab.AddCell(new Phrase(SachManager.Properties.MaSoSach, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.TenSach, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.TenTacGia, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.LinhVucSach, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.NXB, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.Soluong, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.GiaNhap, _h3));
                pdfTab.AddCell(new Phrase(SachManager.Properties.GiaBan, _h3));

                foreach (var s in DMSach)
                {
                    pdfTab.AddCell((new Phrase(s.MaSoSach.ToString(), _normal)));
                    pdfTab.AddCell((new Phrase(s.TenSach, _normal)));
                    pdfTab.AddCell((new Phrase(s.TenTacGia, _normal)));
                    pdfTab.AddCell(new Phrase(s.LinhVucSach.TenLinhVuc, _normal));
                    pdfTab.AddCell((new Phrase(s.NXB.TenNXB, _normal)));
                    pdfTab.AddCell((new Phrase(s.Soluong.ToString(), _normal)));
                    pdfTab.AddCell((new Phrase(DataDisplayHelper.displayMoney(s.GiaNhap), _normal)));
                    pdfTab.AddCell((new Phrase(DataDisplayHelper.displayMoney(s.GiaBan), _normal)));
                }

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(pdfTab);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printTonKho(List<TheKho> DMTheKho, DateTime date)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6});

                HeaderTable.AddCell(createNoBorderCell("Ngày xem",0,1,1));
                HeaderTable.AddCell(createNoBorderCell(": "+DataDisplayHelper.dislayShortDate(date),2,1,1));
                HeaderTable.AddCell(createNoBorderCell("Ngày in", 0, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 2, 1, 1));


                var DetailTable = new PdfPTable(5);
                DetailTable.HorizontalAlignment = 1;
                DetailTable.SpacingAfter = 10;
                DetailTable.DefaultCell.Border = 0;
                DetailTable.SetWidths(new int[] { 3, 4, 4, 2, 4 });
                DetailTable.WidthPercentage = 100;
                DetailTable.DefaultCell.Border = Rectangle.BOX;
                DetailTable.AddCell(createHeaderCell(TheKhoManager.Properties.MaSoSach, 1, 1, 1));
                DetailTable.AddCell(createHeaderCell(SachManager.Properties.TenSach, 1, 1, 1));
                DetailTable.AddCell(createHeaderCell(SachManager.Properties.TenTacGia, 1, 1, 1));
                DetailTable.AddCell(createHeaderCell(TheKhoManager.Properties.SoLuong, 1, 1, 1));
                DetailTable.AddCell(createHeaderCell(TheKhoManager.Properties.NgayGhi, 1, 1, 1));


                foreach (var s in DMTheKho)
                {
                    DetailTable.AddCell(createCell(s.MaSoSach.ToString(), 1, 1, 1));
                    DetailTable.AddCell(createCell(s.Sach.TenSach, 0, 1, 1));
                    DetailTable.AddCell(createCell(s.Sach.TenTacGia, 0, 1, 1));
                    DetailTable.AddCell(createCell(s.SoLuong.ToString(), 0, 1, 1));
                    DetailTable.AddCell(createCell(DataDisplayHelper.dislayShortDate(s.NgayGhi), 1, 1, 1));
                }
                DetailTable.AddCell(createHeaderCell("Tổng cộng", 2, 3, 1));
                DetailTable.AddCell(createCell(DMTheKho.Sum(tk => tk.SoLuong).ToString(), 0, 2, 1));


                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(DetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printDoanhThu(List<DaiLy> DMDaiLy, List<NhaXuatBan> DMNXB, DateTime startDate, DateTime endDate)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var tongTienThu = (decimal)DMDaiLy.Sum(dl => dl.TongTienThanhToanTheoThang);
                var tongTienChi = (decimal)DMNXB.Sum(s => s.TongTienThanhToanTheoThang);
                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6 });

                HeaderTable.AddCell(createNoBorderCell("Ngày in", 2, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 0, 1, 1));

                //Thông tin thống kê
                var InfoTable = new PdfPTable(4);
                InfoTable.HorizontalAlignment = 0;
                InfoTable.SpacingAfter = 10;
                InfoTable.DefaultCell.Border = 0;
                InfoTable.TotalWidth = 15;
                InfoTable.SetWidths(new int[] { 2, 2, 2, 2});
                InfoTable.WidthPercentage = 100;
                InfoTable.DefaultCell.Border = Rectangle.BOX;
                InfoTable.AddCell(createHeaderCell("Xem từ tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(startDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Đến tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(endDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Tổng thu", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienThu), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Tổng chi", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienChi), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Lợi nhuận", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienThu - tongTienChi), 0, 3, 1));

                //Thống kê nguồn thu
                var DLDetailTable = new PdfPTable(4);
                DLDetailTable.HorizontalAlignment = 1;
                DLDetailTable.SpacingAfter = 10;
                DLDetailTable.WidthPercentage = 100;
                DLDetailTable.SetWidths(new int[] { 3, 4, 3, 3 });
                DLDetailTable.AddCell(createTableTitleCell("Nguồn thu", 0, 4, 1));
                DLDetailTable.AddCell(createNoBorderCell(" ", 1, 4, 1));
                DLDetailTable.AddCell(createHeaderCell(DaiLyManager.Properties.MaSoDaiLy, 1, 1, 1));
                DLDetailTable.AddCell(createHeaderCell(DaiLyManager.Properties.TenDaiLy, 1, 1, 1));
                DLDetailTable.AddCell(createHeaderCell(DaiLyManager.Properties.TongTienThanhToan, 1, 1, 1));
                DLDetailTable.AddCell(createHeaderCell(DaiLyManager.Properties.TongTienThanhToanTheoThang, 1, 1, 1));


                foreach (var s in DMDaiLy)
                {
                    DLDetailTable.AddCell(createCell(s.MaSoDaiLy.ToString(), 1, 1, 1));
                    DLDetailTable.AddCell(createCell(s.TenDaiLy, 0, 1, 1));
                    DLDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)s.TongTienThanhToan), 0, 1, 1));
                    DLDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)s.TongTienThanhToanTheoThang), 0, 1, 1));
                }
                DLDetailTable.AddCell(createHeaderCell("Tổng cộng", 2, 2, 1));
                DLDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)DMDaiLy.Sum(dl => dl.TongTienThanhToan)), 1, 1, 1));
                DLDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)DMDaiLy.Sum(dl => dl.TongTienThanhToanTheoThang)), 1, 1, 1));

                //Thông kê nguồn chi
                var NXBDetailTable = new PdfPTable(4);
                NXBDetailTable.HorizontalAlignment = 1;
                NXBDetailTable.SpacingAfter = 10;
                NXBDetailTable.WidthPercentage = 100;
                NXBDetailTable.SetWidths(new int[] { 3, 4, 3, 3 });
                NXBDetailTable.AddCell(createTableTitleCell("Nguồn chi", 0, 4, 1));
                NXBDetailTable.AddCell(createNoBorderCell(" ", 1, 4, 1));
                NXBDetailTable.AddCell(createHeaderCell(NhaXuatBanManager.Properties.MaSoNXB, 1, 1, 1));
                NXBDetailTable.AddCell(createHeaderCell(NhaXuatBanManager.Properties.TenNXB, 1, 1, 1));
                NXBDetailTable.AddCell(createHeaderCell(NhaXuatBanManager.Properties.TongTienThanhToan, 1, 1, 1));
                NXBDetailTable.AddCell(createHeaderCell(NhaXuatBanManager.Properties.TongTienThanhToanTheoThang, 1, 1, 1));


                foreach (var s in DMNXB)
                {
                    NXBDetailTable.AddCell(createCell(s.MaSoNXB.ToString(), 1, 1, 1));
                    NXBDetailTable.AddCell(createCell(s.TenNXB, 0, 1, 1));
                    NXBDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)s.TongTienThanhToan), 0, 1, 1));
                    NXBDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)s.TongTienThanhToanTheoThang), 0, 1, 1));
                }
                NXBDetailTable.AddCell(createHeaderCell("Tổng cộng", 2, 2, 1));
                NXBDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)DMNXB.Sum(dl => dl.TongTienThanhToan)), 1, 1, 1));
                NXBDetailTable.AddCell(createCell(DataDisplayHelper.displayMoney((decimal)DMNXB.Sum(dl => dl.TongTienThanhToanTheoThang)), 1, 1, 1));

                

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(InfoTable);
                _doc.Add(DLDetailTable);
                _doc.Add(NXBDetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printSachBan(List<Sach> DMSach, DateTime startDate, DateTime endDate)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var sachBanDuoc = (decimal)DMSach.Sum(s => s.TongSoLuongBanTheoThang);
                var tongTienBan = (decimal)DMSach.Sum(s => s.TongTienBanTheoThang);
                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6 });

                HeaderTable.AddCell(createNoBorderCell("Ngày in", 2, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 0, 1, 1));

                //Thông tin thống kê
                var InfoTable = new PdfPTable(4);
                InfoTable.HorizontalAlignment = 0;
                InfoTable.SpacingAfter = 10;
                InfoTable.DefaultCell.Border = 0;
                InfoTable.TotalWidth = 15;
                InfoTable.SetWidths(new int[] { 2, 2, 2, 2 });
                InfoTable.WidthPercentage = 100;
                InfoTable.DefaultCell.Border = Rectangle.BOX;
                InfoTable.AddCell(createHeaderCell("Xem từ tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(startDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Đến tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(endDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Sách bán được", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayNumber(sachBanDuoc), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Tổng tiền", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienBan), 0, 3, 1));

                //Thống kê nguồn thu
                var DetailTable = new PdfPTable(8);
                DetailTable.HorizontalAlignment = 1;
                DetailTable.SpacingAfter = 10;
                DetailTable.WidthPercentage = 100;
                DetailTable.SetWidths(new int[] { 1, 6, 4, 3 ,1, 3 , 2, 3});
                DetailTable.AddCell(createTableTitleCell("Danh mục sách bán", 0, 8, 1));
                DetailTable.AddCell(createNoBorderCell(" ", 1, 8, 1));
                foreach (var s in DMSach)
                {
                    DetailTable.AddCell(createBottomBorderCell(s.MaSoSach.ToString(), 1, 1, 1,BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(s.TenSach, 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(" ", 1, 3, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell("Tổng cộng", 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayNumber((decimal)s.TongSoLuongBanTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayMoney((decimal)s.TongTienBanTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    var HoaDon = s.getHoaDonDaiLyTheoThang(startDate.Month, startDate.Year, endDate.Month, endDate.Year);
                    foreach(var hd in HoaDon)
                    {
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Hóa đơn số "+hd.MaSoHoaDon, 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Ngày " + DataDisplayHelper.dislayShortDate(hd.HoaDon.NgayLap), 1, 2, 1));
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(hd.DonGia), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(hd.SoLuong.ToString(), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(hd.ThanhTien), 1, 1, 1));
                    }
                }

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(InfoTable);
                _doc.Add(DetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printLoNhap(List<Sach> DMSach, DateTime startDate, DateTime endDate)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var soLuongNhap = (decimal)DMSach.Sum(s => s.SoLuongNhapTheoThang);
                var tongTienNhap = (decimal)DMSach.Sum(s => s.TongTienNhapTheoThang);
                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6 });

                HeaderTable.AddCell(createNoBorderCell("Ngày in", 2, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 0, 1, 1));

                //Thông tin thống kê
                var InfoTable = new PdfPTable(4);
                InfoTable.HorizontalAlignment = 0;
                InfoTable.SpacingAfter = 10;
                InfoTable.DefaultCell.Border = 0;
                InfoTable.TotalWidth = 15;
                InfoTable.SetWidths(new int[] { 2, 2, 2, 2 });
                InfoTable.WidthPercentage = 100;
                InfoTable.DefaultCell.Border = Rectangle.BOX;
                InfoTable.AddCell(createHeaderCell("Xem từ tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(startDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Đến tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(endDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Số lượng nhập", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayNumber(soLuongNhap), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Tổng tiền", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienNhap), 0, 3, 1));

                //Thống kê nguồn thu
                var DetailTable = new PdfPTable(8);
                DetailTable.HorizontalAlignment = 1;
                DetailTable.SpacingAfter = 10;
                DetailTable.WidthPercentage = 100;
                DetailTable.SetWidths(new int[] { 1, 6, 4, 3, 1, 3, 2, 3 });
                DetailTable.AddCell(createTableTitleCell("Danh mục sách nhập", 0, 8, 1));
                DetailTable.AddCell(createNoBorderCell(" ", 1, 8, 1));
                foreach (var s in DMSach)
                {
                    DetailTable.AddCell(createBottomBorderCell(s.MaSoSach.ToString(), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(s.TenSach, 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(" ", 1, 3, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell("Tổng cộng", 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayNumber((decimal)s.SoLuongNhapTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayMoney((decimal)s.TongTienNhapTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    var PhieuNhap = s.getPhieuNhapTheoThang(startDate.Month, startDate.Year, endDate.Month, endDate.Year);
                    foreach (var p in PhieuNhap)
                    {
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Phiếu nhập số " + p.MaSoPhieuNhap, 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Ngày " + DataDisplayHelper.dislayShortDate(p.PhieuNhap.NgayLap), 1, 2, 1));
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.DonGia), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(p.SoLuong.ToString(), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.ThanhTien), 1, 1, 1));
                    }
                }

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(InfoTable);
                _doc.Add(DetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printLoXuat(List<Sach> DMSach, DateTime startDate, DateTime endDate)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var soLuongXuat = (decimal)DMSach.Sum(s => s.SoLuongXuatTheoThang);
                var tongTienXuat = (decimal)DMSach.Sum(s => s.TongTienXuatTheoThang);
                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6 });

                HeaderTable.AddCell(createNoBorderCell("Ngày in", 2, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 0, 1, 1));

                //Thông tin thống kê
                var InfoTable = new PdfPTable(4);
                InfoTable.HorizontalAlignment = 0;
                InfoTable.SpacingAfter = 10;
                InfoTable.DefaultCell.Border = 0;
                InfoTable.TotalWidth = 15;
                InfoTable.SetWidths(new int[] { 2, 2, 2, 2 });
                InfoTable.WidthPercentage = 100;
                InfoTable.DefaultCell.Border = Rectangle.BOX;
                InfoTable.AddCell(createHeaderCell("Xem từ tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(startDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Đến tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(endDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Số lượng xuất", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayNumber(soLuongXuat), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Tổng tiền", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienXuat), 0, 3, 1));

                //Thống kê nguồn thu
                var DetailTable = new PdfPTable(8);
                DetailTable.HorizontalAlignment = 1;
                DetailTable.SpacingAfter = 10;
                DetailTable.WidthPercentage = 100;
                DetailTable.SetWidths(new int[] { 1, 6, 4, 3, 1, 3, 2, 3 });
                DetailTable.AddCell(createTableTitleCell("Danh mục sách nhập", 0, 8, 1));
                DetailTable.AddCell(createNoBorderCell(" ", 1, 8, 1));
                foreach (var s in DMSach)
                {
                    DetailTable.AddCell(createBottomBorderCell(s.MaSoSach.ToString(), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(s.TenSach, 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(" ", 1, 3, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell("Tổng cộng", 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayNumber((decimal)s.SoLuongXuatTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayMoney((decimal)s.TongTienXuatTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    var PhieuXuat = s.getPhieuXuatTheoThang(startDate.Month, startDate.Year, endDate.Month, endDate.Year);
                    foreach (var p in PhieuXuat)
                    {
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Phiếu xuất số " + p.MaSoPhieuXuat, 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Ngày " + DataDisplayHelper.dislayShortDate(p.PhieuXuat.NgayLap), 1, 2, 1));
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.DonGia), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(p.SoLuong.ToString(), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.ThanhTien), 1, 1, 1));
                    }
                }

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(InfoTable);
                _doc.Add(DetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printCongNoNXB(List<Sach> DMSach, DateTime startDate, DateTime endDate)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var soLuongNo = (decimal)DMSach.Sum(s => s.TongSoLuongNXBNoTheoThang);
                var tongTienNo = (decimal)DMSach.Sum(s => s.TongTienNXBNoTheoThang);
                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6 });

                HeaderTable.AddCell(createNoBorderCell("Ngày in", 2, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 0, 1, 1));

                //Thông tin thống kê
                var InfoTable = new PdfPTable(4);
                InfoTable.HorizontalAlignment = 0;
                InfoTable.SpacingAfter = 10;
                InfoTable.DefaultCell.Border = 0;
                InfoTable.TotalWidth = 15;
                InfoTable.SetWidths(new int[] { 2, 2, 2, 2 });
                InfoTable.WidthPercentage = 100;
                InfoTable.DefaultCell.Border = Rectangle.BOX;
                InfoTable.AddCell(createHeaderCell("Xem từ tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(startDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Đến tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(endDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Số lượng nợ", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayNumber(soLuongNo), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Tổng tiền", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienNo), 0, 3, 1));

                //Thống kê nguồn thu
                var DetailTable = new PdfPTable(8);
                DetailTable.HorizontalAlignment = 1;
                DetailTable.SpacingAfter = 10;
                DetailTable.WidthPercentage = 100;
                DetailTable.SetWidths(new int[] { 1, 6, 4, 3, 1, 3, 2, 3 });
                DetailTable.AddCell(createTableTitleCell("Danh mục sách nợ Nhà xuất bản", 0, 8, 1));
                DetailTable.AddCell(createNoBorderCell(" ", 1, 8, 1));
                foreach (var s in DMSach)
                {
                    DetailTable.AddCell(createBottomBorderCell(s.MaSoSach.ToString(), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(s.TenSach, 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(" ", 1, 3, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell("Tổng cộng", 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayNumber((decimal)s.TongSoLuongNXBNoTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayMoney((decimal)s.TongTienNXBNoTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    var CongNo = s.getCongNoNXBTheoThang(startDate.Month, startDate.Year, endDate.Month, endDate.Year);
                    foreach (var p in CongNo)
                    {
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 2, 1));
                        //DetailTable.AddCell(createNoBorderCell("Phiếu xuất số " + p.Maso, 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Tháng " + DataDisplayHelper.dislayMonth(p.Thang), 1, 2, 1));
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.DonGia), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(p.SoLuong.ToString(), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.ThanhTien), 1, 1, 1));
                    }
                }

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(InfoTable);
                _doc.Add(DetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        public byte[] printCongNoDaiLy(List<Sach> DMSach, DateTime startDate, DateTime endDate)
        {
            try
            {
                var filePath = Path.Combine(FolderPath, FileName);
                PdfWriter writer = PdfWriter.GetInstance(_doc, new FileStream(filePath, FileMode.Create));
                //Create table here for write database data

                var soLuongNo = (decimal)DMSach.Sum(s => s.TongSoLuongDaiLyNoTheoThang);
                var tongTienNo = (decimal)DMSach.Sum(s => s.TongTienDaiLyNoTheoTang);
                var HeaderTable = new PdfPTable(2);
                HeaderTable.HorizontalAlignment = 0;
                HeaderTable.SpacingBefore = 20;
                HeaderTable.SpacingAfter = 10;
                HeaderTable.SetWidths(new int[] { 2, 6 });

                HeaderTable.AddCell(createNoBorderCell("Ngày in", 2, 1, 1));
                HeaderTable.AddCell(createNoBorderCell(": " + DataDisplayHelper.dislayShortDate(DateTime.Now), 0, 1, 1));

                //Thông tin thống kê
                var InfoTable = new PdfPTable(4);
                InfoTable.HorizontalAlignment = 0;
                InfoTable.SpacingAfter = 10;
                InfoTable.DefaultCell.Border = 0;
                InfoTable.TotalWidth = 15;
                InfoTable.SetWidths(new int[] { 2, 2, 2, 2 });
                InfoTable.WidthPercentage = 100;
                InfoTable.DefaultCell.Border = Rectangle.BOX;
                InfoTable.AddCell(createHeaderCell("Xem từ tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(startDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Đến tháng", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.dislayMonth(endDate), 1, 1, 1));
                InfoTable.AddCell(createHeaderCell("Số lượng nợ", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayNumber(soLuongNo), 0, 3, 1));
                InfoTable.AddCell(createHeaderCell("Tổng tiền", 1, 1, 1));
                InfoTable.AddCell(createCell(DataDisplayHelper.displayMoney(tongTienNo), 0, 3, 1));

                //Thống kê nguồn thu
                var DetailTable = new PdfPTable(8);
                DetailTable.HorizontalAlignment = 1;
                DetailTable.SpacingAfter = 10;
                DetailTable.WidthPercentage = 100;
                DetailTable.SetWidths(new int[] { 1, 6, 4, 3, 1, 3, 2, 3 });
                DetailTable.AddCell(createTableTitleCell("Danh mục sách Đại lý nợ", 0, 8, 1));
                DetailTable.AddCell(createNoBorderCell(" ", 1, 8, 1));
                foreach (var s in DMSach)
                {
                    DetailTable.AddCell(createBottomBorderCell(s.MaSoSach.ToString(), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(s.TenSach, 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(" ", 1, 3, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell("Tổng cộng", 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayNumber((decimal)s.TongSoLuongDaiLyNoTheoThang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    DetailTable.AddCell(createBottomBorderCell(DataDisplayHelper.displayMoney((decimal)s.TongTienDaiLyNoTheoTang), 1, 1, 1, BaseColor.LIGHT_GRAY));
                    var CongNo = s.getCongNoDaiLyTheoThang(startDate.Month, startDate.Year, endDate.Month, endDate.Year);
                    foreach (var p in CongNo)
                    {
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 2, 1));
                        //DetailTable.AddCell(createNoBorderCell("Phiếu xuất số " + p.Maso, 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell("Tháng " + DataDisplayHelper.dislayMonth(p.Thang), 1, 2, 1));
                        DetailTable.AddCell(createNoBorderCell(" ", 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.DonGia), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(p.SoLuong.ToString(), 1, 1, 1));
                        DetailTable.AddCell(createNoBorderCell(DataDisplayHelper.displayMoney(p.ThanhTien), 1, 1, 1));
                    }
                }

                _doc.Open();
                _doc.Add(_tilte);
                _doc.Add(HeaderTable);
                _doc.Add(InfoTable);
                _doc.Add(DetailTable);
                _doc.Close();

                return File.ReadAllBytes(filePath);
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                _doc.Close();
            }
        }

        #region Private Method
        private PdfPCell createHeaderCell(string text,int Align, int colspan, int rowspan)
        {
            var cell = new PdfPCell(new Phrase(text, _bold));
            cell.BackgroundColor = BaseColor.LIGHT_GRAY;
            cell.HorizontalAlignment = Align;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Padding = 6;
            return cell;
        }

        private PdfPCell createCell(string text, int Align, int colspan, int rowspan)
        {
            var cell = new PdfPCell(new Phrase(text, _normal));
            cell.HorizontalAlignment = Align;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Padding = 4;
            return cell;
        }

        private PdfPCell createNoBorderCell(string text, int Align, int colspan, int rowspan)
        {
            var cell = new PdfPCell(new Phrase(text, _normal));
            cell.HorizontalAlignment = Align;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Border = Rectangle.NO_BORDER;
            return cell;
        }

        private PdfPCell createBottomBorderCell(string text, int Align, int colspan, int rowspan)
        {
            var cell = new PdfPCell(new Phrase(text, _normal));
            cell.HorizontalAlignment = Align;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.PaddingBottom = 4;
            return cell;
        }
        private PdfPCell createBottomBorderCell(string text, int Align, int colspan, int rowspan, BaseColor bgColor)
        {
            var cell = new PdfPCell(new Phrase(text, _normal));
            cell.HorizontalAlignment = Align;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.PaddingBottom = 4;
            cell.BackgroundColor = bgColor;
            return cell;
        }


        private PdfPCell createTableTitleCell(string text, int Align, int colspan, int rowspan)
        {
            var cell = new PdfPCell(new Phrase(text, _h2));
            cell.HorizontalAlignment = Align;
            cell.Colspan = colspan;
            cell.Rowspan = rowspan;
            cell.Border = Rectangle.NO_BORDER;
            cell.Border = Rectangle.BOTTOM_BORDER;
            cell.PaddingBottom = 8;
            return cell;
        }
        #endregion
    }
}
