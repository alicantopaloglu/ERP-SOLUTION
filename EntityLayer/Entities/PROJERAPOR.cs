using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EntityLayer.Entities
{
    public class PROJERAPOR
    {
       

        [ForeignKey("stokkodu")]
        public int stokkodu { get; set; }
      

        public decimal stokcikismiktari { get; set; }
        public decimal stokgirismiktari { get; set; }
        public decimal stokcikistoplam { get; set; }
        public decimal stokgiristoplam { get; set; }

        public string urunadi { get; set; }
        public string uretici { get; set; }
        public string yuzey { get; set; }
        public decimal boy { get; set; }
        public decimal en { get; set; }
        public decimal kalinlik { get; set; }
        public string tanim { get; set; }

    }
}
