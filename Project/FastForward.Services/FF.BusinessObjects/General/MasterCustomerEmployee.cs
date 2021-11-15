using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{

    //Kelum : Customer Assign for Employee : 2016-April-25

    [Serializable]
    public class MasterCustomerEmployee
    {
        public String _mpce_com { get; set; }
        public String _mpce_emp_cd { get; set; }
        public String _mpce_cus_cd { get; set; }
        public Int32 _mpce_stus { get; set; }
        public String _mpce_cre_by { get; set; }
        public DateTime _mpce_cre_dt { get; set; }
        public String _mpce_mod_by { get; set; }
        public DateTime _mpce_mod_dt { get; set; }

        public String Customer { get; set; }
        //
        
        public static MasterCustomerEmployee Converter(DataRow row)
        {
            return new MasterCustomerEmployee
            {
                _mpce_com = row["MCE_COM"] == DBNull.Value ? string.Empty : row["MCE_COM"].ToString(),
                _mpce_emp_cd = row["MCE_EMP_CD"] == DBNull.Value ? string.Empty : row["MCE_EMP_CD"].ToString(),
                _mpce_cus_cd = row["MCE_CUS_CD"] == DBNull.Value ? string.Empty : row["MCE_CUS_CD"].ToString(),
                _mpce_stus = row["MCE_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["MCE_STUS"].ToString()),
                _mpce_cre_by = row["MCE_CRE_BY"] == DBNull.Value ? string.Empty : row["MCE_CRE_BY"].ToString(),
                _mpce_cre_dt = row["MCE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCE_CRE_DT"].ToString()),
                _mpce_mod_by = row["MCE_MOD_BY"] == DBNull.Value ? string.Empty : row["MCE_MOD_BY"].ToString(),
                _mpce_mod_dt = row["MCE_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MCE_MOD_DT"].ToString()),
                Customer = row["Customer"] == DBNull.Value ? string.Empty : row["Customer"].ToString(),
               
           };
        }
    }
}
