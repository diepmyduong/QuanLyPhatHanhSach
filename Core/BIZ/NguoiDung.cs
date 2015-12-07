using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Core.DAL;
using System.ComponentModel.DataAnnotations;

namespace Core.BIZ
{
    public class NguoiDung
    {
        public NguoiDung() { }
        public NguoiDung(NGUOIDUNG nd)
            :this()
        {
            MaSoNguoiDung = nd.masonguoidung;
            TenNguoiDung = nd.tennguoidung;
            MatKhau = nd.matkhau;
            TenDayDu = nd.tendaydu;
            Email = nd.email;
            PhanQuyen = nd.phanquyen;
            TrangThai = nd.trangthai;
        }
        public NguoiDung(NGUOIDUNG nd, DAILY dl)
            :this(nd)
        {
            MaSoDaiLy = dl.masodaily;
            DaiLy = new DaiLy(dl);
        }

        #region Private Properties
        private DaiLy _daily;
        #endregion

        #region Public Properties
        [Required]
        [DisplayName(NguoiDungManager.Properties.MaSoNguoiDung)]
        public int MaSoNguoiDung { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [DisplayName(NguoiDungManager.Properties.TenNguoiDung)]
        public string TenNguoiDung { get; set; }
        [Required]
        [MaxLength(12)]
        [MinLength(6)]
        [DisplayName(NguoiDungManager.Properties.MatKhau)]
        public string MatKhau { get; set; }
        [Required]
        [DisplayName(NguoiDungManager.Properties.TenDayDu)]
        public string TenDayDu { get; set; }
        [Required]
        [DisplayName(NguoiDungManager.Properties.Email)]
        public string Email { get; set; }
        [Required]
        [DisplayName(NguoiDungManager.Properties.PhanQuyen)]
        public string PhanQuyen { get; set; }
        [DisplayName(NguoiDungManager.Properties.TrangThai)]
        public int TrangThai { get; set; }
        [Required]
        [DisplayName(NguoiDungManager.Properties.MaSoDaiLy)]
        public int MaSoDaiLy { get; set; }
        [DisplayName(NguoiDungManager.Properties.DaiLy)]
        public DaiLy DaiLy {
            get
            {
                if(MaSoDaiLy == 0)
                {
                    var param = new Dictionary<string, dynamic>();
                    param.Add(DaiLyManager.Properties.MaSoNguoiDung, this.MaSoNguoiDung);
                    _daily = DaiLyManager.findBy(param).SingleOrDefault();
                    MaSoDaiLy = _daily.MaSoDaiLy;
                }
                if(_daily == null)
                {
                    _daily = DaiLyManager.find(MaSoDaiLy);
                }
                return _daily;
            }
            set
            {
                _daily = value;
            }
        }
        #endregion

        #region Services
        /// <summary>
        /// đăng nhập người dùng
        /// Trả về 0 nếu xảy ra lỗi
        /// Trả về 1 nếu người dùng tồn tại
        /// Trả về 2 nếu đăng nhập thành cong
        /// Trả về 3 nếu sai mật khẩu
        /// </summary>
        /// <returns></returns>
        public int login()
        {
            try
            {
                var param = new Dictionary<string, dynamic>();
                param.Add(NguoiDungManager.Properties.TenNguoiDung, this.TenNguoiDung);
                var nguoidung = NguoiDungManager.findBy(param).SingleOrDefault();
                if (nguoidung == null)
                {
                    return LoginStatus.NotExisted; //Người dùng không tồn tại
                }
                if (nguoidung.MatKhau.Equals(this.MatKhau))
                {
                    return LoginStatus.Success; //Đăng nhập thành công
                }
                else
                {
                    return LoginStatus.WrongPass; //Sai mật khẩu
                }
            }catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return LoginStatus.Error; //Không thể đăng nhập
            }
            
        }

        public string[] getPhanQuyen()
        {
            return PhanQuyen.Split(',');
        }

        public void setPhanQuyen(string[] roles)
        {
            PhanQuyen = String.Join(",", roles);
        } 
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.TenNguoiDung;
        }
        #endregion

        #region Static Value
        public static class LoginStatus
        {
            public const int NotExisted = 1;
            public const int Success = 2;
            public const int WrongPass = 3;
            public const int Error = 0;
        }
        #endregion
    }
}
