using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class STOKDURUMU
    {
        [Key]
        public int stokdurumuID { get; set; }

        [ForeignKey("stokkodu")]
        public int stokkodu { get; set; }
        public virtual STOKKARTI STOKKARTI { get; set; }

        public decimal ortalamabirimfiyati { get; set; }
        public decimal miktar { get; set; }
        public decimal toplamtutar { get; set; }
        public decimal rezervealinmismiktar { get; set; }
        public decimal kullanilabilirmiktar { get; set; }

      



    }
}
