using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class STOKCIKISI
    {

        [Key]
        public int stokcikisID { get; set; }

        [ForeignKey("stokkodu")]
        public int stokkodu { get; set; }
        public virtual STOKKARTI STOKKARTI { get; set; }

        [ForeignKey("projekodu")]
        public int projekodu { get; set; }
        public virtual PROJELER PROJELER { get; set; }

        [ForeignKey("kullaniciID")]
        public int kullaniciID { get; set; }
        public virtual KULLANICILAR KULLANICILAR { get; set; }


        [ForeignKey("stokgirisID")]
        public int stokgirisID { get; set; }
        public virtual STOKGIRISI STOKGIRISI { get; set; }

        public decimal birimfiyati { get; set; }
        public decimal miktar { get; set; }
        public decimal toplamtutar { get; set; }

        public DateTime tarih { get; set; }

    }
}
