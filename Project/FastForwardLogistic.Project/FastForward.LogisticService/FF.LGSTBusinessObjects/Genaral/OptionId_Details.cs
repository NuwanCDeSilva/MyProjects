using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class OptionId_Details
    {
        public string OptionId { get; set; }

        public string OptionTitle { get; set; }

        public string OptionDescription { get; set; }

        public string OptionCreatedBy { get; set; }

        public string OptionCreatedDate { get; set; }

        public string ActiveStatus { get; set; }
         

        public static OptionId_Details Converter(DataRow row) {

            return new OptionId_Details
            {
                OptionId = row["SSRM_OPTID"] == DBNull.Value ? String.Empty : row["SSRM_OPTID"].ToString(),
                OptionTitle = row["SSP_TITLE"] == DBNull.Value ? String.Empty : row["SSP_TITLE"].ToString(),
                OptionDescription = row["SSP_DESC"] == DBNull.Value ? String.Empty : row["SSP_DESC"].ToString(),
                OptionCreatedBy = row["SSP_CRE_BY"] == DBNull.Value ? String.Empty : row["SSP_CRE_BY"].ToString(),
                OptionCreatedDate = row["SSP_CRE_DT"] == DBNull.Value ? String.Empty : row["SSP_CRE_DT"].ToString(),
                ActiveStatus = row["SSRM_ACT"] == DBNull.Value ? String.Empty : row["SSRM_ACT"].ToString()
            };
        
        
        }
    }
}
