using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.BIZ
{
    public class LinhVuc
    {
        [System.ComponentModel.DisplayName("Mã Số Lĩnh Vực")]
        public int MaSoLinhVuc { get; set; }
        [System.ComponentModel.DisplayName("Tên Lĩnh Vực")]
        public String TenLinhVuc { get; set; }

        public override String ToString()
        {
            return this.TenLinhVuc;
        }
    }
}
