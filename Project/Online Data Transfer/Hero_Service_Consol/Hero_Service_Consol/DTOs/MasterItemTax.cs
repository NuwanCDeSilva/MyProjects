using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
  public  class MasterItemTax
    {
        //
        // Function             - Inventory Item
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 01/06/2012
        // Table                - MST_ITM_COMTAX
        //



        #region Private Members
        private Boolean _mict_act;
        private string _mict_com;
        private string _mict_itm_cd;
        private string _mict_stus;
        private string _mict_taxrate_cd;
        private string _mict_tax_cd;
        private decimal _mict_tax_rate;
        private DateTime _mict_effct_dt;
        #endregion

        public Boolean Mict_act { get { return _mict_act; } set { _mict_act = value; } }
        public string Mict_com { get { return _mict_com; } set { _mict_com = value; } }
        public string Mict_itm_cd { get { return _mict_itm_cd; } set { _mict_itm_cd = value; } }
        public string Mict_stus { get { return _mict_stus; } set { _mict_stus = value; } }
        public string Mict_taxrate_cd { get { return _mict_taxrate_cd; } set { _mict_taxrate_cd = value; } }
        public string Mict_tax_cd { get { return _mict_tax_cd; } set { _mict_tax_cd = value; } }
        public decimal Mict_tax_rate { get { return _mict_tax_rate; } set { _mict_tax_rate = value; } }
        public DateTime Mict_effct_dt { get { return _mict_effct_dt; } set { _mict_effct_dt = value; } }

        public static MasterItemTax ConvertTotal(DataRow row)
        {
            return new MasterItemTax
            {
                Mict_act = row["MICT_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MICT_ACT"]),
                Mict_com = row["MICT_COM"] == DBNull.Value ? string.Empty : row["MICT_COM"].ToString(),
                Mict_itm_cd = row["MICT_ITM_CD"] == DBNull.Value ? string.Empty : row["MICT_ITM_CD"].ToString(),
                Mict_stus = row["MICT_STUS"] == DBNull.Value ? string.Empty : row["MICT_STUS"].ToString(),
                Mict_taxrate_cd = row["MICT_TAXRATE_CD"] == DBNull.Value ? string.Empty : row["MICT_TAXRATE_CD"].ToString(),
                Mict_tax_cd = row["MICT_TAX_CD"] == DBNull.Value ? string.Empty : row["MICT_TAX_CD"].ToString(),
                Mict_tax_rate = row["MICT_TAX_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICT_TAX_RATE"]),
                Mict_effct_dt = row["MICT_EFFCT_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MICT_EFFCT_DT"])
            };
        }
    }
}

