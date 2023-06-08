using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class KULLANICILAR
    {
        [Key]
        public int kullaniciID { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string kullaniciadi { get; set; }
        public string sifre { get; set; }
        public bool stokgirisi { get; set; }
        public bool stokgirisisilme { get; set; }
        public bool stokgirisduzenleme { get; set; }
        public bool stokdurumu { get; set; }
        public bool stokcikisi { get; set; }
        public bool stokcikisduzenleme { get; set; }
        public bool siparisiptali { get; set; }
        public bool siparisgirisi { get; set; }
        public bool firmagirisi { get; set; }
        public bool satinalma { get; set; }
        public bool projeler { get; set; }
        public bool raporlar { get; set; }
        public bool rezerveacma { get; set; }
        public bool kullanicipaneli { get; set; }
        public bool siparisduzenleme { get; set; }

        public ICollection<STOKGIRISI> STOKGIRISI { get; set; }
        public ICollection<STOKCIKISI> STOKCIKISI { get; set; }
        public ICollection<SIPARIS> SIPARIS { get; set; }
     
    }
}
