using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class PROJELER
    {
        [Key]
        public int projeID { get; set; }

        public int projekodu { get; set; }

        public string projeadi { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string proje { get; set; }

        [Column(TypeName = "Varchar(100)")]
        public string firma { get; set; }
        public decimal anlasmatutari { get; set; }

        public ICollection<STOKGIRISI> STOKGIRISI { get; set; }
        public ICollection<STOKCIKISI> STOKCIKISI { get; set; }
        public ICollection<SIPARIS> SIPARIS { get; set; }

    }
}
