﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class LinhVucManager
    {
        public static partial class Properties
        {
            public const string MaSoLinhVuc = "Mã Số Lĩnh Vực";
            public const string TenLinhVuc = "Tên Lĩnh Vực";
            public const string Sach = "Sách";
        }
        public static List<LinhVuc> getAll()
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from lv in db.LINHVUCs
                                select new LinhVuc
                                {
                                    MaSoLinhVuc = lv.masolinhvuc,
                                    TenLinhVuc = lv.ten
                                };
                return linqQuery.ToList<LinhVuc>();
            };

        }

        public static LinhVuc find(int masolinhvuc)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                var linqQuery = from lv in db.LINHVUCs
                                where lv.masolinhvuc.Equals(masolinhvuc)
                                select new LinhVuc
                                {
                                    MaSoLinhVuc = lv.masolinhvuc,
                                    TenLinhVuc = lv.ten
                                };
                return linqQuery.SingleOrDefault<LinhVuc>();
            };
        }

        public static List<LinhVuc> findBy(Dictionary<string,dynamic> Params)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                dynamic value;

                var linqQuery = (from lv in db.LINHVUCs
                                 select new LinhVuc
                                 {
                                     MaSoLinhVuc = lv.masolinhvuc,
                                     TenLinhVuc = lv.ten
                                 })
                                 .Where(lv => lv.MaSoLinhVuc.Equals(
                                        Params.TryGetValue(Properties.MaSoLinhVuc, out value) ? value as int?
                                        : lv.MaSoLinhVuc
                                 )).Where(lv => lv.MaSoLinhVuc.Equals(
                                        Params.TryGetValue(Properties.TenLinhVuc, out value) ? value as string
                                        : lv.TenLinhVuc
                                 ));
                return linqQuery.ToList<LinhVuc>();
            }
        }

        public static bool add(LinhVuc linhvuc)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                LINHVUC lv;
                lv = (from b in db.LINHVUCs
                     where b.ten.Equals(linhvuc.TenLinhVuc)
                     select b).SingleOrDefault();
                if (lv != null) return false;
                lv = new LINHVUC();
                lv.ten = linhvuc.TenLinhVuc;
                db.LINHVUCs.InsertOnSubmit(lv);
                db.SubmitChanges();
                return true;
            }
        }

        public static bool edit(LinhVuc linhvuc)
        {
            using (EntitiesDataContext db = new EntitiesDataContext())
            {
                //Kiểm tra lĩnh vực có tồn tại không
                LINHVUC lv;
                lv = (from b in db.LINHVUCs
                      where b.masolinhvuc.Equals(linhvuc.MaSoLinhVuc)
                      select b).SingleOrDefault();
                if (lv == null) return false;
                lv.ten = linhvuc.TenLinhVuc;
                db.SubmitChanges();
                return true;
            }
        }
    }
}
