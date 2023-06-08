using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class FIRMALAR
    {
        [Key]
        public int firmaID { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string firmaadi { get; set; }
        [Column(TypeName = "Varchar(60)")]
        public string vergidairesi { get; set; }
        [Column(TypeName = "Varchar(40)")]
        public string vergino { get; set; }
        [Column(TypeName = "Varchar(100)")]
        public string adresbilgisi { get; set; }
        [Column(TypeName = "Varchar(16)")]
        public string telefon { get; set; }

        public ICollection<STOKGIRISI> STOKGIRISI { get; set; }
        public ICollection<SIPARIS> SIPARIS { get; set; }

    }
}
