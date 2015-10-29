using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.BIZ;

namespace Core.DAL
{
    public class LinhVucManager
    {
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
    }
}
