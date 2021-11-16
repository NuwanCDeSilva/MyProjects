using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FF.BusinessObjects.Genaral;

namespace FF.BusinessObjects.Search
{
    public class CustomerSearchObject
    
    {
        public String Mbe_com { get; set; }  
        public String Mbe_cd    { get; set; }
        public String Mbe_tp { get; set; }
        public String Mbe_name    { get; set; } 
        public String Mbe_add1    { get; set; } 
        public String Mbe_add2    { get; set; } 
        public String Mbe_country_cd    { get; set; } 
        public String Mbe_mob    { get; set; }
        public String Mbe_email    { get; set; }
        public String Mbe_wr_com_name { get; set; }
        public String Mbe_cre_by { get; set; }
        public DateTime Mbe_cre_dt { get; set; }

        public static CustomerSearchObject Converter(DataRow row)
        {
            return new CustomerSearchObject
            {

                Mbe_cd = row["Mbe_cd"] == DBNull.Value ? string.Empty : row["Mbe_cd"].ToString(),
                Mbe_name = row["Mbe_name"] == DBNull.Value ? string.Empty : row["Mbe_name"].ToString(),
                Mbe_add1 = row["Mbe_add1"] == DBNull.Value ? string.Empty : row["Mbe_add1"].ToString(),
                Mbe_add2 = row["Mbe_add2"] == DBNull.Value ? string.Empty : row["Mbe_add2"].ToString(),
                Mbe_country_cd = row["Mbe_country_cd"] == DBNull.Value ? string.Empty : row["Mbe_country_cd"].ToString(),
                Mbe_mob = row["Mbe_mob"] == DBNull.Value ? string.Empty : row["Mbe_mob"].ToString(),
                Mbe_email = row["Mbe_email"] == DBNull.Value ? string.Empty : row["Mbe_email"].ToString()
            };
        }
    }
}
