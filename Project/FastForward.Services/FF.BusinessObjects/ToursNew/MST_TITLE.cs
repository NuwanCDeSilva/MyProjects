using System; 
using System.Data; 

namespace FF.BusinessObjects 
{
    public class MST_TITLE 
    {
            public String Mstl_cd    { get; set; } 
            public String Mstl_desc    { get; set; }
            public static MST_TITLE Converter(DataRow row)  
            {
                return new MST_TITLE 
                { 
                        Mstl_cd  = row["MSTL_CD"] == DBNull.Value ? string.Empty : row["MSTL_CD"].ToString(), 
                        Mstl_desc  = row["MSTL_DESC"] == DBNull.Value ? string.Empty : row["MSTL_DESC"].ToString()
                }; 
            } 
    } 
} 

