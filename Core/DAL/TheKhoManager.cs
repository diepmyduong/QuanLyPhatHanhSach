using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class TheKhoManager
    {
        public static partial class Properties
        {
            public const string MaSoSach = "Mã số Sách";
            public const string Sach = "Tên Sách";
            public const string SoLuong = "Số lượng";
            public const string NgayGhi = "Ngày ghi";
        }

        public static List<TheKho> getAll()
        {
            using(EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from tk in db.THEKHOs
                                join s in db.SACHes
                                on tk.masosach equals s.masosach
                                select new TheKho
                                {
                                    MaSoSach = tk.masosach,
                                    Sach = new Sach()
                                    {
                                        MaSoSach = s.masosach,
                                        TenSach = s.tensach,
                                        TenTacGia = s.tacgia,
                                        MaSoLinhVuc = s.masolinhvuc,
                                        MaSoNXB = s.masonxb,
                                        Soluong = s.soluong,
                                        GiaBan = s.giaban,
                                        GiaNhap = s.gianhap,
                                        HinhAnh = s.hinhanh
                                    },
                                    SoLuong = tk.soluong,
                                    NgayGhi = tk.ngayghi
                                };
                return linqQuery.ToList<TheKho>();
            }
        }

        public static List<TheKho> getAllByDate(DateTime date)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = db.THEKHOs
                                .Where(tk => tk.ngayghi <= date) // Duyệt các thẻ kho trước @date
                                .OrderByDescending(tk => tk.ngayghi) //Sắp xếp ngày gần nhất
                                .GroupBy(tk => tk.masosach) // Gọp nhóm các thẻ kho theo mã
                                .Select(group => group.FirstOrDefault()) // Chọn thẻ kho có ngày gần nhất
                                .OrderBy(tk => tk.masosach) // Sắp xếp kết quả theo mã số sách
                                .Join(db.SACHes
                                    , tk => tk.masosach
                                    , s => s.masosach
                                    , (tk, s) => new TheKho
                                    {
                                        MaSoSach = tk.masosach,
                                        Sach = new Sach()
                                        {
                                            MaSoSach = s.masosach,
                                            TenSach = s.tensach,
                                            TenTacGia = s.tacgia,
                                            MaSoLinhVuc = s.masolinhvuc,
                                            MaSoNXB = s.masonxb,
                                            Soluong = s.soluong,
                                            GiaBan = s.giaban,
                                            GiaNhap = s.gianhap,
                                            HinhAnh = s.hinhanh
                                        },
                                        SoLuong = tk.soluong,
                                        NgayGhi = tk.ngayghi
                                    });
                return linqQuery.ToList<TheKho>();
            }
        }
    }
}
