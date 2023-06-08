using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class STOKKARTI
    {
        [Key]
        public int stokkodu { get; set; }

        [Column(TypeName = "Varchar(50)")]
        public string urunadi { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string uretici { get; set; }
        [Column(TypeName = "Varchar(50)")]
        public string yuzey { get; set; }
    
        public decimal boy { get; set; }
       
        public decimal en { get; set; }
        
        public decimal kalinlik { get; set; }
        
        public string birim { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string tanim { get; set; }
        [DataType(DataType.Date)]
        public DateTime tarih { get; set; }
        public decimal minmiktar { get; set; }
        public decimal maxmiktar { get; set; }

        public ICollection<STOKGIRISI> STOKGIRISI { get; set; }
      
        public ICollection<SIPARIS> SIPARIS { get; set; }
        public ICollection<STOKDURUMU> STOKDURUMU { get; set; }



    }
}
