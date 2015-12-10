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
        public NguoiDung() {
            PhanQuyen = nameof(NguoiDungManager.Roles.daily);
        }
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
        private static List<string> _searchKeys;
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
                    if(_daily != null)
                    {
                        MaSoDaiLy = _daily.MaSoDaiLy;
                    }
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
        /// </summary>
        /// <returns></returns>
        public LoginStatus login()
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



        public SignUpStatus signUp()
        {
            try
            {
                var param = new Dictionary<string, dynamic>();
                param.Add(NguoiDungManager.Properties.TenNguoiDung, this.TenNguoiDung);
                var nguoidung = NguoiDungManager.findBy(param).SingleOrDefault();
                if (nguoidung != null)
                {
                    return SignUpStatus.UserIStExisted; //Người dùng tồn tại
                }
                //Kiểm tra Email
                param = new Dictionary<string, dynamic>();
                param.Add(NguoiDungManager.Properties.Email, this.Email);
                nguoidung = NguoiDungManager.findBy(param).SingleOrDefault();
                if (nguoidung != null)
                {
                    return SignUpStatus.EmailIsExisted; //Email tồn tại
                }
                this.TrangThai = 1;
                var result = NguoiDungManager.add(this);
                if(result == 0)
                {
                    return SignUpStatus.Error;
                }
                else
                {
                    this.MaSoNguoiDung = result;
                    return SignUpStatus.Success;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return SignUpStatus.Error; //Không thể đăng nhập
            }
        }
        public UpdateStatus update()
        {
            try
            {
                //Kiểm tra Email
                var param = new Dictionary<string, dynamic>();
                param.Add(NguoiDungManager.Properties.Email, this.Email);
                var nguoidung = NguoiDungManager.findBy(param).SingleOrDefault();
                if (nguoidung != null && !nguoidung.MaSoNguoiDung.Equals(MaSoNguoiDung))
                {
                    return UpdateStatus.EmailIsExisted; //Email tồn tại
                }
                if (NguoiDungManager.edit(this))
                {
                    return UpdateStatus.Success;
                }
                else
                {
                    return UpdateStatus.Error;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return UpdateStatus.Error; //Không thể đăng nhập
            }
        }
        public bool isAdmin()
        {
            if(PhanQuyen!= null)
            {
                return PhanQuyen.Contains("admin");
            }
            else
            {
                return false;
            }
            
        }
        public bool delete()
        {
            this.TrangThai = 0;
            return NguoiDungManager.edit(this);
        }
        public static List<string> searchKeys()
        {
            if (_searchKeys == null)
            {
                _searchKeys = new List<string>();
                _searchKeys.Add(nameof(NguoiDungManager.Properties.TenNguoiDung));
                _searchKeys.Add(nameof(NguoiDungManager.Properties.TenDayDu));
                _searchKeys.Add(nameof(NguoiDungManager.Properties.Email));
                _searchKeys.Add(nameof(NguoiDungManager.Properties.PhanQuyen));
            }
            return _searchKeys;
        }
        #endregion

        #region Override Methods
        public override string ToString()
        {
            return this.TenNguoiDung;
        }
        #endregion

        #region Static Value
        public enum LoginStatus
        {
            Success,
            NotExisted,
            WrongPass,
            Error
        };
        public enum SignUpStatus
        {
            Success,
            UserIStExisted,
            EmailIsExisted,
            Error
        };

        public enum UpdateStatus
        {
            Success,
            Error,
            EmailIsExisted
        }
        #endregion
    }
}
