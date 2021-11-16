using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


namespace FF.BusinessObjects.Search
{
    public class MST_COM_SEARCH
    {
        public String Mc_cd { get; set; }
        public String Mc_desc { get; set; }
        public string RESULT_COUNT { get; set; }
        public string R__ { get; set; }

        public static MST_COM_SEARCH Converter(DataRow row)
        {
            return new MST_COM_SEARCH
            {
                Mc_cd = row["MC_CD"] == DBNull.Value ? string.Empty : row["MC_CD"].ToString(),
                Mc_desc = row["MC_DESC"] == DBNull.Value ? string.Empty : row["MC_DESC"].ToString(),
                RESULT_COUNT = row["RESULT_COUNT"] == DBNull.Value ? string.Empty : row["RESULT_COUNT"].ToString(),
                R__ = row["R__"] == DBNull.Value ? string.Empty : row["R__"].ToString()

            };
        }
    }
    public class MST_COM_SELECTED
    {
        public string Mc_cd { get; set; }
        public string Mc_desc { get; set; }
    }
}
