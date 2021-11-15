using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class GroupSaleCustomer
    {
        #region Private Members
        private string _hgc_cre_by;
        private DateTime _hgc_cre_dt;
        private string _hgc_cust_cd;
        private string _hgc_grup_cd;
        private Int32 _hgc_no_itm;
        private Int32 _hgc_no_acc;
        private Decimal _hgc_val;
        private string _hgc_Cust_Name;

        private MasterBusinessCompany _masterBusinessComp = null;  

        #endregion

        public string Hgc_cre_by
        {
            get { return _hgc_cre_by; }
            set { _hgc_cre_by = value; }
        }
        public DateTime Hgc_cre_dt
        {
            get { return _hgc_cre_dt; }
            set { _hgc_cre_dt = value; }
        }
        public string Hgc_cust_cd
        {
            get { return _hgc_cust_cd; }
            set { _hgc_cust_cd = value; }
        }
        public string Hgc_grup_cd
        {
            get { return _hgc_grup_cd; }
            set { _hgc_grup_cd = value; }
        }
        public Int32 Hgc_no_itm
        {
            get { return _hgc_no_itm; }
            set { _hgc_no_itm = value; }
        }
        public Int32 Hgc_no_acc
        {
            get { return _hgc_no_acc; }
            set { _hgc_no_acc = value; }
        }
        public Decimal Hgc_val
        {
            get { return _hgc_val; }
            set { _hgc_val = value; }
        }

        public string Hgc_Cust_Name
        {
            get { return _hgc_Cust_Name; }
            set { _hgc_Cust_Name = value; }
        }

        public MasterBusinessCompany MasterBusinessCompany
        {
            get { return _masterBusinessComp; }
            set { _masterBusinessComp = value; }
        }


        public static GroupSaleCustomer Converter(DataRow row)
        {
            return new GroupSaleCustomer
            {
                Hgc_cre_by = row["HGC_CRE_BY"] == DBNull.Value ? string.Empty : row["HGC_CRE_BY"].ToString(),
                Hgc_cre_dt = row["HGC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["HGC_CRE_DT"]),
                Hgc_cust_cd = row["HGC_CUST_CD"] == DBNull.Value ? string.Empty : row["HGC_CUST_CD"].ToString(),
                Hgc_grup_cd = row["HGC_GRUP_CD"] == DBNull.Value ? string.Empty : row["HGC_GRUP_CD"].ToString(),
                Hgc_no_itm = row["HGC_NO_ITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["HGC_NO_ITM"]),
                Hgc_no_acc = row["HGC_NO_ACC"] == DBNull.Value ? 0 : Convert.ToInt32(row["HGC_NO_ACC"]),
                Hgc_val = row["HGC_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["HGC_VAL"])

            };
        }
    }
}
