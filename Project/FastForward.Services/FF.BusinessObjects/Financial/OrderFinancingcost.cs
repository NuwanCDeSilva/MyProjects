using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class OrderFinancingcost
    {
        #region Private Members
        private Boolean _ifc_act;
        private decimal _ifc_amt;
        private decimal _ifc_amt_deal;
        private string _ifc_anal_1;
        private string _ifc_anal_2;
        private string _ifc_anal_3;
        private string _ifc_anal_4;
        private string _ifc_anal_5;
        private string _ifc_cre_by;
        private DateTime _ifc_cre_dt;
        private string _ifc_doc_no;
        private string _ifc_ele_cat;
        private string _ifc_ele_cd;
        private string _ifc_ele_tp;
        private decimal _ifc_ex_rt;
        private int _ifc_line;
        private Int32 _ifc_seq_no;
        private string _ifc_session_id;
        #endregion
        #region public property definition
        public Boolean Ifc_act
        {
            get { return _ifc_act; }
            set { _ifc_act = value; }
        }
        public decimal Ifc_amt
        {
            get { return _ifc_amt; }
            set { _ifc_amt = value; }
        }
        public decimal Ifc_amt_deal
        {
            get { return _ifc_amt_deal; }
            set { _ifc_amt_deal = value; }
        }
        public string Ifc_anal_1
        {
            get { return _ifc_anal_1; }
            set { _ifc_anal_1 = value; }
        }
        public string Ifc_anal_2
        {
            get { return _ifc_anal_2; }
            set { _ifc_anal_2 = value; }
        }
        public string Ifc_anal_3
        {
            get { return _ifc_anal_3; }
            set { _ifc_anal_3 = value; }
        }
        public string Ifc_anal_4
        {
            get { return _ifc_anal_4; }
            set { _ifc_anal_4 = value; }
        }
        public string Ifc_anal_5
        {
            get { return _ifc_anal_5; }
            set { _ifc_anal_5 = value; }
        }
        public string Ifc_cre_by
        {
            get { return _ifc_cre_by; }
            set { _ifc_cre_by = value; }
        }
        public DateTime Ifc_cre_dt
        {
            get { return _ifc_cre_dt; }
            set { _ifc_cre_dt = value; }
        }
        public string Ifc_doc_no
        {
            get { return _ifc_doc_no; }
            set { _ifc_doc_no = value; }
        }
        public string Ifc_ele_cat
        {
            get { return _ifc_ele_cat; }
            set { _ifc_ele_cat = value; }
        }
        public string Ifc_ele_cd
        {
            get { return _ifc_ele_cd; }
            set { _ifc_ele_cd = value; }
        }
        public string Ifc_ele_tp
        {
            get { return _ifc_ele_tp; }
            set { _ifc_ele_tp = value; }
        }
        public decimal Ifc_ex_rt
        {
            get { return _ifc_ex_rt; }
            set { _ifc_ex_rt = value; }
        }
        public int Ifc_line
        {
            get { return _ifc_line; }
            set { _ifc_line = value; }
        }
        public Int32 Ifc_seq_no
        {
            get { return _ifc_seq_no; }
            set { _ifc_seq_no = value; }
        }
        public string Ifc_session_id
        {
            get { return _ifc_session_id; }
            set { _ifc_session_id = value; }
        }
        #endregion
        public static OrderFinancingcost Converter(DataRow row)
        {
            return new OrderFinancingcost
            {
                Ifc_act = row["IFC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IFC_ACT"]),
                Ifc_amt = row["IFC_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFC_AMT"]),
                Ifc_amt_deal = row["IFC_AMT_DEAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFC_AMT_DEAL"]),
                Ifc_anal_1 = row["IFC_ANAL_1"] == DBNull.Value ? string.Empty : row["IFC_ANAL_1"].ToString(),
                Ifc_anal_2 = row["IFC_ANAL_2"] == DBNull.Value ? string.Empty : row["IFC_ANAL_2"].ToString(),
                Ifc_anal_3 = row["IFC_ANAL_3"] == DBNull.Value ? string.Empty : row["IFC_ANAL_3"].ToString(),
                Ifc_anal_4 = row["IFC_ANAL_4"] == DBNull.Value ? string.Empty : row["IFC_ANAL_4"].ToString(),
                Ifc_anal_5 = row["IFC_ANAL_5"] == DBNull.Value ? string.Empty : row["IFC_ANAL_5"].ToString(),
                Ifc_cre_by = row["IFC_CRE_BY"] == DBNull.Value ? string.Empty : row["IFC_CRE_BY"].ToString(),
                Ifc_cre_dt = row["IFC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IFC_CRE_DT"]),
                Ifc_doc_no = row["IFC_DOC_NO"] == DBNull.Value ? string.Empty : row["IFC_DOC_NO"].ToString(),
                Ifc_ele_cat = row["IFC_ELE_CAT"] == DBNull.Value ? string.Empty : row["IFC_ELE_CAT"].ToString(),
                Ifc_ele_cd = row["IFC_ELE_CD"] == DBNull.Value ? string.Empty : row["IFC_ELE_CD"].ToString(),
                Ifc_ele_tp = row["IFC_ELE_TP"] == DBNull.Value ? string.Empty : row["IFC_ELE_TP"].ToString(),
                Ifc_ex_rt = row["IFC_EX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["IFC_EX_RT"]),
                Ifc_line = row["IFC_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["IFC_LINE"]),
                Ifc_seq_no = row["IFC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["IFC_SEQ_NO"]),
                Ifc_session_id = row["IFC_SESSION_ID"] == DBNull.Value ? string.Empty : row["IFC_SESSION_ID"].ToString()

            };
        }
    }
}
