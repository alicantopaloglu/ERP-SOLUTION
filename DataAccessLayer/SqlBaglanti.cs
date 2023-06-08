using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;

namespace DataAccessLayer
{
    public class SqlBaglanti
    {
        public SqlConnection baglanti()
        {

            SqlConnection baglan = new SqlConnection(@"server=localhost;database=HedlabErpV3;integrated security=true;");
            //SqlConnection baglan = new SqlConnection(@"Data Source=192.168.10.6; Initial Catalog=SC_Stok_Takip; User ID=sa; Password=likomsa");
            //93.89.67.99  
            //user id=faydamsa;password=0AC0DA0DD0D108F09F065060066067087;
            baglan.Open();
            return baglan;
        }
    }
}
