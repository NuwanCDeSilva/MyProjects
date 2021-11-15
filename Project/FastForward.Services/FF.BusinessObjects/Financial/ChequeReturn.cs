using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
   public class ChequeReturn
    {
        #region Private Members
       
        Int32 seq;
        string cheque_no;
        string company;
        string pc;
        string bank;
        string refNo;
        DateTime returndate;
        Decimal sys_value;
        Decimal act_value;
        Decimal intrest;
        bool is_set;
        Decimal settle_val;
        string return_bank;
        int bank_type;
        string create_by;
        DateTime create_Date;
        Decimal srcq_cap_set;
        Decimal srcq_intr_set;
        Decimal srcq_mgr_chg;
        string srcq_chq_branch;

        #endregion

        #region Public Property Definition

        public string Pc
        {
            get { return pc; }
            set { pc = value; }
        }

        public string Bank
        {
            get { return bank; }
            set { bank = value; }
        }

        public string RefNo
        {
            get { return refNo; }
            set { refNo = value; }
        }

        public DateTime Returndate
        {
            get { return returndate; }
            set { returndate = value; }
        }

        public Decimal Sys_value
        {
            get { return sys_value; }
            set { sys_value = value; }
        }

        public string Company
        {
            get { return company; }
            set { company = value; }
        }

        public Decimal Act_value
        {
            get { return act_value; }
            set { act_value = value; }
        }

        public Decimal Intrest
        {
            get { return intrest; }
            set { intrest = value; }
        }

        public bool Is_set
        {
            get { return is_set; }
            set { is_set = value; }
        }

        public Decimal Settle_val
        {
            get { return settle_val; }
            set { settle_val = value; }
        }

        public Int32 Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        public DateTime Create_Date
        {
            get { return create_Date; }
            set { create_Date = value; }
        }

        public string Create_by
        {
            get { return create_by; }
            set { create_by = value; }
        }

        public int Bank_type
        {
            get { return bank_type; }
            set { bank_type = value; }
        }

        public string Cheque_no
        {
            get { return cheque_no; }
            set { cheque_no = value; }
        }

        public string Return_bank
        {
            get { return return_bank; }
            set { return_bank = value; }
        }

        public Decimal Srcq_cap_set
        {
            get { return srcq_cap_set; }
            set { srcq_cap_set = value; }
        }

        public Decimal Srcq_intr_set
        {
            get { return srcq_intr_set; }
            set { srcq_intr_set = value; }
        }

        public Decimal Srcq_mgr_chg
        {
            get { return srcq_mgr_chg; }
            set { srcq_mgr_chg = value; }
        }
        public string Srcq_chq_branch
        {
            get { return srcq_chq_branch; }
            set { srcq_chq_branch = value; }
        }

        #endregion

        #region Converters
        public static ChequeReturn Converter(DataRow row)
        {
            return new ChequeReturn
            {
                Pc = row["SRCQ_PC"] == DBNull.Value ? string.Empty : row["SRCQ_PC"].ToString(),
                Bank = row["SRCQ_BANK"] == DBNull.Value ? string.Empty : row["SRCQ_BANK"].ToString(),
                RefNo = row["SRCQ_REF"] == DBNull.Value ? string.Empty : row["SRCQ_REF"].ToString(),
                Returndate = row["SRCQ_RTN_DT"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime( row["SRCQ_RTN_DT"]),
                Company = row["SRCQ_COM"] == DBNull.Value ? string.Empty : row["SRCQ_COM"].ToString(),
                Seq = row["SRCQ_SEQ"] == DBNull.Value ?0: Convert.ToInt32( row["SRCQ_SEQ"].ToString()),
                Create_by = row["SRCQ_CRE_BY"] == DBNull.Value ? string.Empty : row["SRCQ_CRE_BY"].ToString(),
                Return_bank = row["SRCQ_RTN_BANK"] == DBNull.Value ? string.Empty : row["SRCQ_RTN_BANK"].ToString(),
                Cheque_no = row["SRCQ_CHQ"] == DBNull.Value ? string.Empty : row["SRCQ_CHQ"].ToString(),
                Sys_value = row["SRCQ_SYS_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_SYS_VAL"]),
                Act_value = row["SRCQ_ACT_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_ACT_VAL"]),
                Intrest = row["SRCQ_INTR"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_INTR"]),
                Settle_val = row["SRCQ_SET_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_SET_VAL"]),
                Bank_type = row["SRCQ_BANKED_TP"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRCQ_BANKED_TP"]),
                Is_set = row["SRCQ_IS_SETT"] == DBNull.Value ? false : Convert.ToBoolean(row["SRCQ_IS_SETT"]),
                Create_Date = row["SRCQ_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRCQ_CRE_DT"]),
                Srcq_cap_set = row["SRCQ_CAP_SET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_CAP_SET"]),
                Srcq_intr_set = row["SRCQ_INTR_SET"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_INTR_SET"]),
                Srcq_mgr_chg = row["SRCQ_MGR_CHG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SRCQ_MGR_CHG"]),
                Srcq_chq_branch = row["SRCQ_CHQ_BRANCH"] == DBNull.Value ? string.Empty : row["SRCQ_CHQ_BRANCH"].ToString()
            };
        }
        #endregion

    }
}
