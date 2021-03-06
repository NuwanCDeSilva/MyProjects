using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Master Item Component.(Sub Item for Master Item - KIT Structure)
    /// Created By : Miginda Geeganage.
    /// Created On : 26/04/2012
    /// </summary>
    [Serializable]
    public class MasterItemComponent
    {
        #region Private Members
        private Boolean _micp_act;
        private string _micp_comp_itm_cd; //Component Item Code.
        private string _micp_itm_cd;      //Main Item Code.
        private decimal _micp_cost_percentage;
        private Boolean _micp_isprice;      
        private string _micp_itm_tp;
        private Boolean _micp_must_scan;
        private decimal _micp_qty;

        private MasterItem _componentItem = null;

        #endregion

        public Boolean Micp_act
        {
            get { return _micp_act; }
            set { _micp_act = value; }
        }
        public string Micp_comp_itm_cd
        {
            get { return _micp_comp_itm_cd; }
            set { _micp_comp_itm_cd = value; }
        }
        public decimal Micp_cost_percentage
        {
            get { return _micp_cost_percentage; }
            set { _micp_cost_percentage = value; }
        }
        public Boolean Micp_isprice
        {
            get { return _micp_isprice; }
            set { _micp_isprice = value; }
        }
        public string Micp_itm_cd
        {
            get { return _micp_itm_cd; }
            set { _micp_itm_cd = value; }
        }
        public string Micp_itm_tp
        {
            get { return _micp_itm_tp; }
            set { _micp_itm_tp = value; }
        }
        public Boolean Micp_must_scan
        {
            get { return _micp_must_scan; }
            set { _micp_must_scan = value; }
        }
        public decimal Micp_qty
        {
            get { return _micp_qty; }
            set { _micp_qty = value; }
        }

        public MasterItem ComponentItem
        {
            get { return _componentItem; }
            set { _componentItem = value; }
        }

        public static MasterItemComponent Converter(DataRow row)
        {
            return new MasterItemComponent
            {
                Micp_act = row["MICP_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MICP_ACT"]),
                Micp_comp_itm_cd = row["MICP_COMP_ITM_CD"] == DBNull.Value ? string.Empty : row["MICP_COMP_ITM_CD"].ToString(),
                Micp_cost_percentage = row["MICP_COST_PERCENTAGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICP_COST_PERCENTAGE"]),
                Micp_isprice = row["MICP_ISPRICE"] == DBNull.Value ? false : Convert.ToBoolean(row["MICP_ISPRICE"]),
                Micp_itm_cd = row["MICP_ITM_CD"] == DBNull.Value ? string.Empty : row["MICP_ITM_CD"].ToString(),
                Micp_itm_tp = row["MICP_ITM_TP"] == DBNull.Value ? string.Empty : row["MICP_ITM_TP"].ToString(),
                Micp_must_scan = row["MICP_MUST_SCAN"] == DBNull.Value ? false : Convert.ToBoolean(row["MICP_MUST_SCAN"]),
                Micp_qty = row["MICP_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["MICP_QTY"])

            };
        }


    }
}
