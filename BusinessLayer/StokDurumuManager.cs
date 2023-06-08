using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccessLayer;
using EntityLayer.Entities;

namespace BusinessLayer
{
    public class StokDurumuManager
    {
        GenericRepository<STOKDURUMU> gr = new GenericRepository<STOKDURUMU>();
       

        public IQueryable getAll()
        {
            return gr.StokDurumuListCustom();
        }

        public int addStokDurumu(STOKDURUMU sd)
        {
            
           
            return gr.Insert(sd);

        }
        public List<STOKDURUMU> getStokDurumu(int stokkodu)
        {
            return gr.List(x => x.stokkodu == stokkodu);
        }
        public int updateStokDurumu(STOKDURUMU s)
        {
            STOKDURUMU value = gr.Find(x => x.stokkodu == s.stokkodu);
            value.kullanilabilirmiktar = s.kullanilabilirmiktar;
            value.miktar = s.miktar;
            value.ortalamabirimfiyati = s.ortalamabirimfiyati;
            value.rezervealinmismiktar = s.rezervealinmismiktar;
            value.toplamtutar = s.toplamtutar;
            return gr.Update(value);
        }
    }
}
