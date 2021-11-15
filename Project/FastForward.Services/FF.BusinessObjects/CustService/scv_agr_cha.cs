using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class scv_agr_cha
    {
        #region Private Members
        private string _sac_agr_no;
        private decimal _sac_dis_amt;
        private decimal _sac_dis_rt;
        private string _sac_itm_cd;
        private string _sac_itm_desc;
        private Int32 _sac_line;
        private Int32 _sac_qty;
        private Int32 _sac_seq;
        private decimal _sac_tax;
        private decimal _sac_tot_amt;
        private decimal _sac_unit_amt;
        private decimal _sac_unit_rt;
        #endregion

        #region Public Property Definition
        public string Sac_agr_no { get { return _sac_agr_no; } set { _sac_agr_no = value; } }
        public decimal Sac_dis_amt { get { return _sac_dis_amt; } set { _sac_dis_amt = value; } }
        public decimal Sac_dis_rt { get { return _sac_dis_rt; } set { _sac_dis_rt = value; } }
        public string Sac_itm_cd { get { return _sac_itm_cd; } set { _sac_itm_cd = value; } }
        public string Sac_itm_desc { get { return _sac_itm_desc; } set { _sac_itm_desc = value; } }
        public Int32 Sac_line { get { return _sac_line; } set { _sac_line = value; } }
        public Int32 Sac_qty { get { return _sac_qty; } set { _sac_qty = value; } }
        public Int32 Sac_seq { get { return _sac_seq; } set { _sac_seq = value; } }
        public decimal Sac_tax { get { return _sac_tax; } set { _sac_tax = value; } }
        public decimal Sac_tot_amt { get { return _sac_tot_amt; } set { _sac_tot_amt = value; } }
        public decimal Sac_unit_amt { get { return _sac_unit_amt; } set { _sac_unit_amt = value; } }
        public decimal Sac_unit_rt { get { return _sac_unit_rt; } set { _sac_unit_rt = value; } }

        #endregion

        #region Converters
        public static scv_agr_cha Converter(DataRow row)
        {
            return new scv_agr_cha
            {

                Sac_agr_no = row["SAC_AGR_NO"] == DBNull.Value ? string.Empty : row["SAC_AGR_NO"].ToString(),
                Sac_dis_amt = row["SAC_DIS_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_DIS_AMT"]),
                Sac_dis_rt = row["SAC_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_DIS_RT"]),
                Sac_itm_cd = row["SAC_ITM_CD"] == DBNull.Value ? string.Empty : row["SAC_ITM_CD"].ToString(),
                Sac_itm_desc = row["SAC_ITM_DESC"] == DBNull.Value ? string.Empty : row["SAC_ITM_DESC"].ToString(),
                Sac_line = row["SAC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_LINE"]),
                Sac_qty = row["SAC_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_QTY"]),
                Sac_seq = row["SAC_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAC_SEQ"]),
                Sac_tax = row["SAC_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_TAX"]),
                Sac_tot_amt = row["SAC_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_TOT_AMT"]),
                Sac_unit_amt = row["SAC_UNIT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_UNIT_AMT"]),
                Sac_unit_rt = row["SAC_UNIT_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAC_UNIT_RT"])

            };
        }

        #endregion
    }
}

