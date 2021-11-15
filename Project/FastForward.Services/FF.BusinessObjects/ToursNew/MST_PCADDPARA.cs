using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.ToursNew
{
    public class MST_PCADDPARA
    {
        public Int32 PARA_ID {get;set;}
        public string PARA_COM {get;set;}
        public string PARA_PC {get;set;}
        public string PARA_KEY {get;set;}
        public Int32 PARA_VALUE { get; set; }
        public static MST_PCADDPARA Converter(DataRow row)
        {
            return new MST_PCADDPARA
            {
                PARA_ID = row["PARA_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["PARA_ID"].ToString()),
                PARA_COM = row["PARA_COM"] == DBNull.Value ? string.Empty : row["PARA_COM"].ToString(),
                PARA_PC = row["PARA_PC"] == DBNull.Value ? string.Empty : row["PARA_PC"].ToString(),
                PARA_KEY = row["PARA_KEY"] == DBNull.Value ? string.Empty : row["PARA_KEY"].ToString(),
                PARA_VALUE = row["PARA_VALUE"] == DBNull.Value ? 0 : Convert.ToInt32(row["PARA_VALUE"].ToString())
            };
        }
    }
}
