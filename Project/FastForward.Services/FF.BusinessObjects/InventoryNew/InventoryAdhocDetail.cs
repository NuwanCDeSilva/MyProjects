using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
  

namespace FF.BusinessObjects
{
    /// <summary>
    /// Table : INT_ADHOC_DET
    /// Created On : 10/09/2012
    /// </summary>
    [Serializable]
    public class InventoryAdhocDetail
    {
        #region Private Members
        private Decimal _iadd_anal1;
        private string _iadd_anal2;
        private string _iadd_anal3;
        private Int32 _iadd_anal4;
        private string _iadd_anal5;
        private string _iadd_app_itm;
        private string _iadd_app_pb;
        private string _iadd_app_pb_lvl;
        private Decimal _iadd_app_val;
        private string _iadd_claim_itm;
        private string _iadd_claim_pb;
        private string _iadd_claim_pb_lvl;
        private Decimal _iadd_claim_val;
        private string _iadd_coll_itm;
        private string _iadd_coll_pb;
        private string _iadd_coll_pb_lvl;
        private string _iadd_coll_ser1;
        private string _iadd_coll_ser2;
        private string _iadd_coll_ser3;
        private Decimal _iadd_coll_val;
        private Int32 _iadd_line;
        private string _iadd_ref_no;
        private Int32 _iadd_seq;
        private Int32 _iadd_stus;
        #endregion

        public Decimal Iadd_anal1 { get { return _iadd_anal1; } set { _iadd_anal1 = value; } }
        public string Iadd_anal2 { get { return _iadd_anal2; } set { _iadd_anal2 = value; } }
        public string Iadd_anal3 { get { return _iadd_anal3; } set { _iadd_anal3 = value; } }
        public Int32 Iadd_anal4 { get { return _iadd_anal4; } set { _iadd_anal4 = value; } }
        public string Iadd_anal5 { get { return _iadd_anal5; } set { _iadd_anal5 = value; } }
        public string Iadd_app_itm { get { return _iadd_app_itm; } set { _iadd_app_itm = value; } }
        public string Iadd_app_pb { get { return _iadd_app_pb; } set { _iadd_app_pb = value; } }
        public string Iadd_app_pb_lvl { get { return _iadd_app_pb_lvl; } set { _iadd_app_pb_lvl = value; } }
        public Decimal Iadd_app_val { get { return _iadd_app_val; } set { _iadd_app_val = value; } }
        public string Iadd_claim_itm { get { return _iadd_claim_itm; } set { _iadd_claim_itm = value; } }
        public string Iadd_claim_pb { get { return _iadd_claim_pb; } set { _iadd_claim_pb = value; } }
        public string Iadd_claim_pb_lvl { get { return _iadd_claim_pb_lvl; } set { _iadd_claim_pb_lvl = value; } }
        public Decimal Iadd_claim_val { get { return _iadd_claim_val; } set { _iadd_claim_val = value; } }
        public string Iadd_coll_itm { get { return _iadd_coll_itm; } set { _iadd_coll_itm = value; } }
        public string Iadd_coll_pb { get { return _iadd_coll_pb; } set { _iadd_coll_pb = value; } }
        public string Iadd_coll_pb_lvl { get { return _iadd_coll_pb_lvl; } set { _iadd_coll_pb_lvl = value; } }
        public string Iadd_coll_ser1 { get { return _iadd_coll_ser1; } set { _iadd_coll_ser1 = value; } }
        public string Iadd_coll_ser2 { get { return _iadd_coll_ser2; } set { _iadd_coll_ser2 = value; } }
        public string Iadd_coll_ser3 { get { return _iadd_coll_ser3; } set { _iadd_coll_ser3 = value; } }
        public Decimal Iadd_coll_val { get { return _iadd_coll_val; } set { _iadd_coll_val = value; } }
        public Int32 Iadd_line { get { return _iadd_line; } set { _iadd_line = value; } }
        public string Iadd_ref_no { get { return _iadd_ref_no; } set { _iadd_ref_no = value; } }
        public Int32 Iadd_seq { get { return _iadd_seq; } set { _iadd_seq = value; } }
        public Int32 Iadd_stus { get { return _iadd_stus; } set { _iadd_stus = value; } }

        public static InventoryAdhocDetail Converter(DataRow row)
        {
            return new InventoryAdhocDetail
            {
                Iadd_anal1 = row["IADD_ANAL1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IADD_ANAL1"]),
                Iadd_anal2 = row["IADD_ANAL2"] == DBNull.Value ? string.Empty : row["IADD_ANAL2"].ToString(),
                Iadd_anal3 = row["IADD_ANAL3"] == DBNull.Value ? string.Empty : row["IADD_ANAL3"].ToString(),
                Iadd_anal4 = row["IADD_ANAL4"] == DBNull.Value ? 0 : Convert.ToInt32(row["IADD_ANAL4"]),
                Iadd_anal5 = row["IADD_ANAL5"] == DBNull.Value ? string.Empty : row["IADD_ANAL5"].ToString(),
                Iadd_app_itm = row["IADD_APP_ITM"] == DBNull.Value ? string.Empty : row["IADD_APP_ITM"].ToString(),
                Iadd_app_pb = row["IADD_APP_PB"] == DBNull.Value ? string.Empty : row["IADD_APP_PB"].ToString(),
                Iadd_app_pb_lvl = row["IADD_APP_PB_LVL"] == DBNull.Value ? string.Empty : row["IADD_APP_PB_LVL"].ToString(),
                Iadd_app_val = row["IADD_APP_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IADD_APP_VAL"]),
                Iadd_claim_itm = row["IADD_CLAIM_ITM"] == DBNull.Value ? string.Empty : row["IADD_CLAIM_ITM"].ToString(),
                Iadd_claim_pb = row["IADD_CLAIM_PB"] == DBNull.Value ? string.Empty : row["IADD_CLAIM_PB"].ToString(),
                Iadd_claim_pb_lvl = row["IADD_CLAIM_PB_LVL"] == DBNull.Value ? string.Empty : row["IADD_CLAIM_PB_LVL"].ToString(),
                Iadd_claim_val = row["IADD_CLAIM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IADD_CLAIM_VAL"]),
                Iadd_coll_itm = row["IADD_COLL_ITM"] == DBNull.Value ? string.Empty : row["IADD_COLL_ITM"].ToString(),
                Iadd_coll_pb = row["IADD_COLL_PB"] == DBNull.Value ? string.Empty : row["IADD_COLL_PB"].ToString(),
                Iadd_coll_pb_lvl = row["IADD_COLL_PB_LVL"] == DBNull.Value ? string.Empty : row["IADD_COLL_PB_LVL"].ToString(),
                Iadd_coll_ser1 = row["IADD_COLL_SER1"] == DBNull.Value ? string.Empty : row["IADD_COLL_SER1"].ToString(),
                Iadd_coll_ser2 = row["IADD_COLL_SER2"] == DBNull.Value ? string.Empty : row["IADD_COLL_SER2"].ToString(),
                Iadd_coll_ser3 = row["IADD_COLL_SER3"] == DBNull.Value ? string.Empty : row["IADD_COLL_SER3"].ToString(),
                Iadd_coll_val = row["IADD_COLL_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IADD_COLL_VAL"]),
                Iadd_line = row["IADD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["IADD_LINE"]),
                Iadd_ref_no = row["IADD_REF_NO"] == DBNull.Value ? string.Empty : row["IADD_REF_NO"].ToString(),
                Iadd_seq = row["IADD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IADD_SEQ"]),
                Iadd_stus = row["IADD_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IADD_STUS"])

            };
        }


    }
}
