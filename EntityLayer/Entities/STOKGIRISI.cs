using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class STOKGIRISI
    {
        [Key]
        public int stokgirisID { get; set; }

        [ForeignKey("stokkodu")]
        public int stokkodu { get; set; }
        public virtual STOKKARTI STOKKARTI { get; set; }


        [ForeignKey("projekodu")]
        public int projekodu { get; set; }
        public virtual PROJELER PROJELER { get; set; }

        [ForeignKey("firmaID")]
        public int firmaID { get; set; }
        public virtual FIRMALAR FIRMALAR { get; set; }

        [ForeignKey("kullaniciID")]
        public int kullaniciID { get; set; }
        public virtual KULLANICILAR KULLANICILAR { get; set; }

        [ForeignKey("siparisID")]
        public int siparisID { get; set; }
        public virtual SIPARIS SIPARIS { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string faturano { get; set; }

        public DateTime faturatarihi { get; set; }

        [Column(TypeName = "Varchar(20)")]
        public string irsaliyeno { get; set; }

        public DateTime irsaliyetarihi { get; set; }

        public DateTime tarih { get; set; }

        public decimal kdv { get; set; }
        public decimal birimfiyati { get; set; }
        public decimal kdvdahilbirimfiyati { get; set; }
        public decimal miktar { get; set; }
        public decimal toplamtutar { get; set; }
        public decimal kdvtutari { get; set; }
        public decimal kullanilabilirmiktar { get; set; }

        public ICollection<STOKCIKISI> STOKCIKISI { get; set; }


    }
}
