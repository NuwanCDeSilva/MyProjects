using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class MasterVehicalInsuranceDefinition
    {
        #region public members

        decimal seq;
        string com;
        string pc;
        string ins_com_cd;
        string plo_cd;
        DateTime from;
        DateTime to;
        string sales_type;
        string item_cd;
        decimal value;
        bool isReq;
        string cre_by;
        DateTime cre_dt;
        int term;

        #endregion

        #region public properties


        public DateTime Cre_dt
        {
            get { return cre_dt; }
            set { cre_dt = value; }
        }

        public string Cre_by
        {
            get { return cre_by; }
            set { cre_by = value; }
        }

        public bool IsReq
        {
            get { return isReq; }
            set { isReq = value; }
        }

        public decimal Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        public string Item_cd
        {
            get { return item_cd; }
            set { item_cd = value; }
        }

        public string Sales_type
        {
            get { return sales_type; }
            set { sales_type = value; }
        }

        public DateTime To
        {
            get { return to; }
            set { to = value; }
        }
        public DateTime From
        {
            get { return from; }
            set { from = value; }
        }

        public string Plo_cd
        {
            get { return plo_cd; }
            set { plo_cd = value; }
        }

        public string Ins_com_cd
        {
            get { return ins_com_cd; }
            set { ins_com_cd = value; }
        }

        public string Pc
        {
            get { return pc; }
            set { pc = value; }
        }

        public string Com
        {
            get { return com; }
            set { com = value; }
        }

        public decimal Seq
        {
            get { return seq; }
            set { seq = value; }
        }

        public int Term
        {
            get { return term; }
            set { term = value; }
        }

        #endregion

        public static MasterVehicalInsuranceDefinition Converter(DataRow row)
        {
            return new MasterVehicalInsuranceDefinition
            {
                Seq = ((row["SVID_SEQ"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVID_SEQ"].ToString())),
                Com = ((row["SVID_COM"] == DBNull.Value) ? string.Empty : row["SVID_COM"].ToString()),
                Pc = ((row["SVID_PC"] == DBNull.Value) ? string.Empty : row["SVID_PC"].ToString()),
                Ins_com_cd = ((row["SVID_INS_COM_CD"] == DBNull.Value) ? string.Empty : row["SVID_INS_COM_CD"].ToString()),
                Plo_cd = ((row["SVID_POLC_CD"] == DBNull.Value) ? string.Empty : row["SVID_POLC_CD"].ToString()),
                From = ((row["SVID_FROM_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVID_FROM_DT"])),
                To = ((row["SVID_TO_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVID_TO_DT"])),
                Sales_type = ((row["SVID_SALE_TP"] == DBNull.Value) ? string.Empty : (row["SVID_SALE_TP"].ToString())),
                Item_cd = ((row["SVID_ITM"] == DBNull.Value) ? string.Empty : row["SVID_ITM"].ToString()),
                Value = ((row["SVID_VAL"] == DBNull.Value) ? 0 : Convert.ToDecimal(row["SVID_VAL"])),
                IsReq = ((row["SVID_IS_REQ"] == DBNull.Value) ? false : Convert.ToBoolean(row["SVID_IS_REQ"])),
                Cre_by = ((row["SVID_CRE_BY"] == DBNull.Value) ? string.Empty : row["SVID_CRE_BY"].ToString()),
                Cre_dt = ((row["SVID_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVID_CRE_DT"])),
                Term = ((row["SVID_TERM"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SVID_TERM"]))
            };
        }


    }
}
