using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data; // Added by Chathura on 18-sep-2017

namespace FF.BusinessObjects.Genaral
{
  public  class HouseBLAll
    {
        public string blno { get; set; }

        // Added by Chathura on 18-sep-2017
        public static HouseBLAll Converter(DataRow row)
        {
            return new HouseBLAll
            {
                blno = row["bl_doc_no"] == DBNull.Value ? string.Empty : row["bl_doc_no"].ToString()
            };
        }
    }
}
