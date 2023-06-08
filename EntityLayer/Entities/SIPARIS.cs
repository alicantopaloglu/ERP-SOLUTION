using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class SIPARIS
    {
        [Key]
        public int siparisID { get; set; }

        [ForeignKey("stokkodu")]
        public int stokkodu { get; set; }
        public virtual STOKKARTI STOKKARTI { get; set; }


        [ForeignKey("projekodu")]
        public int projekodu { get; set; }
        public virtual PROJELER PROJELER { get; set; }

        [ForeignKey("firmaID")]
        public int firmaID { get; set; }
        public virtual FIRMALAR FIRMALAR { get; set; }

      
        public int siparisFirmaID { get; set; }
        

        [ForeignKey("kullaniciID")]
        public int kullaniciID { get; set; }
        public virtual KULLANICILAR KULLANICILAR { get; set; }


       



        public DateTime? taleptarihi { get; set; }
        public DateTime? termintarihi { get; set; }
        public DateTime? tarih { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string aciklama { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string saciklama { get; set; }

        public bool siparisiptal { get; set; }
        public decimal miktar { get; set; }
        public decimal alinanmiktar { get; set; }
        public decimal kalanmiktar { get; set; }
        public decimal stokeklenenmiktar { get; set; }
        [Column(TypeName = "Varchar(20)")]
        public string durumu { get; set; }

        public ICollection<STOKGIRISI> STOKGIRISI { get; set; }



    }
}
