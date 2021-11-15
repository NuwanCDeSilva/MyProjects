using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class LoyaltyType
    {//
        #region Private Members
        private Boolean _salt_alw_multi_cdpnt;
        private string _salt_brand;
        private string _salt_cat_1;
        
       
        
        private string _salt_desc;
        private DateTime _salt_frm_dt;
        private Boolean _salt_is_comp;
        
        private string _salt_loty_tp;
        private decimal _salt_memb_chg;
       
        private string _salt_prt;
        private string _salt_pt_tp;
        private decimal _salt_renew_chg;
        
        private DateTime _salt_to_dt;
        private Int32 _salt_valid;

        //added 2014/03/19
        private bool _salt_allw_promo;
        private bool _salt_defa_pb_only;
        //end

        //==Added by Udesh 30-Oct-2018==
        private bool _salt_is_chg_phy;
        private decimal _salt_phy_cd_pnt;
        private Int32 _salt_pnt_exp;
        //==============================
        #endregion

        public Boolean Salt_alw_multi_cdpnt
        {
            get { return _salt_alw_multi_cdpnt; }
            set { _salt_alw_multi_cdpnt = value; }
        }
        public string Salt_brand
        {
            get { return _salt_brand; }
            set { _salt_brand = value; }
        }
        public string Salt_cat_1
        {
            get { return _salt_cat_1; }
            set { _salt_cat_1 = value; }
        }

       

        public string Salt_desc
        {
            get { return _salt_desc; }
            set { _salt_desc = value; }
        }
        public DateTime Salt_frm_dt
        {
            get { return _salt_frm_dt; }
            set { _salt_frm_dt = value; }
        }
        public Boolean Salt_is_comp
        {
            get { return _salt_is_comp; }
            set { _salt_is_comp = value; }
        }
      
        public string Salt_loty_tp
        {
            get { return _salt_loty_tp; }
            set { _salt_loty_tp = value; }
        }
        public decimal Salt_memb_chg
        {
            get { return _salt_memb_chg; }
            set { _salt_memb_chg = value; }
        }
        
        
        public string Salt_prt
        {
            get { return _salt_prt; }
            set { _salt_prt = value; }
        }
        public string Salt_pt_tp
        {
            get { return _salt_pt_tp; }
            set { _salt_pt_tp = value; }
        }
        public decimal Salt_renew_chg
        {
            get { return _salt_renew_chg; }
            set { _salt_renew_chg = value; }
        }
      
        public DateTime Salt_to_dt
        {
            get { return _salt_to_dt; }
            set { _salt_to_dt = value; }
        }
        public Int32 Salt_valid
        {
            get { return _salt_valid; }
            set { _salt_valid = value; }
        }
        public bool Salt_allw_promo
        {
            get { return _salt_allw_promo; }
            set { _salt_allw_promo = value; }
        }
        public bool Salt_defa_pb_only
        {
            get { return _salt_defa_pb_only; }
            set { _salt_defa_pb_only = value; }
        }

        //==Added by Udesh 30-Oct-2018==
        public bool Salt_is_chg_phy
        {
            get { return _salt_is_chg_phy; }
            set { _salt_is_chg_phy = value; }
        }
        public decimal Salt_phy_cd_pnt
        {
            get { return _salt_phy_cd_pnt; }
            set { _salt_phy_cd_pnt = value; }
        }
        public Int32 Salt_pnt_exp
        {
            get { return _salt_pnt_exp; }
            set { _salt_pnt_exp = value; }
        }
        //==============================


        public static LoyaltyType Converter(DataRow row)
        {
            return new LoyaltyType
            {
                Salt_alw_multi_cdpnt = row["SALT_ALW_MULTI_CDPNT"] == DBNull.Value ? false : Convert.ToBoolean(row["SALT_ALW_MULTI_CDPNT"]),
                Salt_brand = row["SALT_CHK_ONETP"] == DBNull.Value ? string.Empty : row["SALT_CHK_ONETP"].ToString(),
                Salt_cat_1 = row["SALT_CHK_ONCD"] == DBNull.Value ? string.Empty : row["SALT_CHK_ONCD"].ToString(),
               
               
                Salt_desc = row["SALT_DESC"] == DBNull.Value ? string.Empty : row["SALT_DESC"].ToString(),
                Salt_frm_dt = row["SALT_FRM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALT_FRM_DT"]),
                Salt_is_comp = row["SALT_IS_COMP"] == DBNull.Value ? false : Convert.ToBoolean(row["SALT_IS_COMP"]),
                
                Salt_loty_tp = row["SALT_LOTY_TP"] == DBNull.Value ? string.Empty : row["SALT_LOTY_TP"].ToString(),
                Salt_memb_chg = row["SALT_MEMB_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALT_MEMB_CHG"]),
               
                Salt_prt = row["SALT_PRT"] == DBNull.Value ? string.Empty : row["SALT_PRT"].ToString(),
                Salt_pt_tp = row["SALT_PT_TP"] == DBNull.Value ? string.Empty : row["SALT_PT_TP"].ToString(),
                Salt_renew_chg = row["SALT_RENEW_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALT_RENEW_CHG"]),
               
                Salt_to_dt = row["SALT_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SALT_TO_DT"]),
                Salt_valid = row["SALT_VALID"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALT_VALID"]),


                Salt_allw_promo = row["SALT_ALLW_PROMO"] == DBNull.Value ? false : Convert.ToBoolean(row["SALT_ALLW_PROMO"]),
                Salt_defa_pb_only = row["SALT_DEFA_PB_ONLY"] == DBNull.Value ? false : Convert.ToBoolean(row["SALT_DEFA_PB_ONLY"]),

                //==Added by Udesh 01-Nov-2018==
                Salt_is_chg_phy = row["SALT_IS_CHG_PHY"] == DBNull.Value ? false : Convert.ToBoolean(row["SALT_IS_CHG_PHY"]),
                Salt_phy_cd_pnt = row["SALT_PHY_CD_PNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SALT_PHY_CD_PNT"]),
                Salt_pnt_exp = row["SALT_PNT_EXP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SALT_PNT_EXP"])
                //==============================

            };
        }

    }
}

