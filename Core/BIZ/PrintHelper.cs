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
        private Font _head;
        private Font _normal;
        private Font _underline;
        
        public PrintHelper()
        {
            _doc = new Document(PageSize.A4, 2, 2, 2, 2);
            _tilte = new Paragraph("Page Title");
            _tilte.Alignment = 1; //center
            FileName = Guid.NewGuid() + ".pdf";
            string arialPath = Environment.GetEnvironmentVariable("SystemRoot") + "\\fonts\\arial.ttf";
            BaseFont arial = BaseFont.CreateFont(arialPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            _head = new Font(arial, 12f, Font.BOLD, BaseColor.BLUE);
            _normal = new Font(arial, 10f, Font.NORMAL, BaseColor.BLACK);
            _underline = new Font(arial, 10f, Font.UNDERLINE, BaseColor.BLACK);
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
                _tilte = new Paragraph(value,_head);
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
                pdfTab.AddCell(new Phrase(SachManager.Properties.MaSoSach, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.TenSach, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.TenTacGia, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.LinhVucSach, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.NXB, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.Soluong, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.GiaNhap, _head));
                pdfTab.AddCell(new Phrase(SachManager.Properties.GiaBan, _head));

                foreach (var s in DMSach)
                {
                    pdfTab.AddCell(s.MaSoSach.ToString());
                    pdfTab.AddCell(s.TenSach);
                    pdfTab.AddCell(s.TenTacGia);
                    pdfTab.AddCell(s.LinhVucSach.TenLinhVuc);
                    pdfTab.AddCell(s.NXB.TenNXB);
                    pdfTab.AddCell(s.Soluong.ToString());
                    pdfTab.AddCell(DataDisplayHelper.displayMoney(s.GiaNhap));
                    pdfTab.AddCell(DataDisplayHelper.displayMoney(s.GiaBan));
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

    }
}
