using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class VehicalRegistrationDefnition
    {
        #region private members

        //decimal svrd_seq;
        //string svrd_com;
        //string svrd_pc;
        //DateTime svrd_from_dt;
        //DateTime svrd_to_dt;
        //string svrd_sale_tp;
        //string svrd_itm;
        //decimal svrd_val;
        //decimal svrd_claim_val;
        //bool svrd_is_req;
        //string svrd_cre_by;
        //DateTime svrd_cre_dt;
        private Boolean _svrd_alw_mult_pay;
        private Boolean _svrd_check_on;
        private string _svrd_circular;
        private decimal _svrd_claim_val;
        private string _svrd_com;
        private string _svrd_cre_by;
        private DateTime _svrd_cre_dt;
        private string _svrd_cust_tp;
        private DateTime _svrd_from_dt;
        private Int32 _svrd_from_qty;
        private decimal _svrd_from_val;
        private Boolean _svrd_is_req;
        private string _svrd_itm;
        private string _svrd_pay_tp;
        private string _svrd_pb;
        private string _svrd_pb_lvl;
        private string _svrd_pc;
        private string _svrd_sale_tp;
        private string _svrd_scheme;
        private Int32 _svrd_seq;
        private Int32 _svrd_term;
        private DateTime _svrd_to_dt;
        private Int32 _svrd_to_qty;
        private decimal _svrd_to_val;
        private decimal _svrd_val;
       //kapila 25/1/2016
        private Int32 _SVRD_NO_OF_TIMES;
        private Int32 _SVRD_NO_OF_USED;
        private string _Svrd_ser_1;



        #endregion

        #region property definitions


        //public Decimal Svrd_seq
        //{
        //    get { return svrd_seq; }
        //    set { svrd_seq = value; }
        //}


        //public string Svrd_Com
        //{
        //    get { return svrd_com; }
        //    set { svrd_com = value; }
        //}

        //public string Svrd_PC
        //{
        //    get { return svrd_pc; }
        //    set { svrd_pc = value; }
        //}

        //public DateTime Svrd_from_dt
        //{
        //    get { return svrd_from_dt; }
        //    set { svrd_from_dt = value; }
        //}

        //public DateTime Svrd_to_dt
        //{
        //    get { return svrd_to_dt; }
        //    set { svrd_to_dt = value; }
        //}

        //public string Svrd_sale_tp
        //{
        //    get { return svrd_sale_tp; }
        //    set { svrd_sale_tp = value; }
        //}

        //public string Svrd_itm
        //{
        //    get { return svrd_itm; }
        //    set { svrd_itm = value; }
        //}

        //public decimal Svrd_val
        //{
        //    get { return svrd_val; }
        //    set { svrd_val = value; }
        //}

        //public decimal Svrd_claim_val
        //{
        //    get { return svrd_claim_val; }
        //    set { svrd_claim_val = value; }
        //}

        //public bool Svrd_is_req
        //{
        //    get { return svrd_is_req; }
        //    set { svrd_is_req = value; }
        //}

        //public string Svrd_cre_by
        //{
        //    get { return svrd_cre_by; }
        //    set { svrd_cre_by = value; }
        //}

        //public DateTime Svrd_cre_dt
        //{
        //    get { return svrd_cre_dt; }
        //    set { svrd_cre_dt = value; }
        //}
        public Int32 SVRD_NO_OF_TIMES { get { return _SVRD_NO_OF_TIMES; } set { _SVRD_NO_OF_TIMES = value; } }
        public Int32 SVRD_NO_OF_USED { get { return _SVRD_NO_OF_USED; } set { _SVRD_NO_OF_USED = value; } }
        public Boolean Svrd_alw_mult_pay { get { return _svrd_alw_mult_pay; } set { _svrd_alw_mult_pay = value; } }
        public Boolean Svrd_check_on { get { return _svrd_check_on; } set { _svrd_check_on = value; } }
        public string Svrd_circular { get { return _svrd_circular; } set { _svrd_circular = value; } }
        public decimal Svrd_claim_val { get { return _svrd_claim_val; } set { _svrd_claim_val = value; } }
        public string Svrd_com { get { return _svrd_com; } set { _svrd_com = value; } }
        public string Svrd_cre_by { get { return _svrd_cre_by; } set { _svrd_cre_by = value; } }
        public DateTime Svrd_cre_dt { get { return _svrd_cre_dt; } set { _svrd_cre_dt = value; } }
        public string Svrd_cust_tp { get { return _svrd_cust_tp; } set { _svrd_cust_tp = value; } }
        public DateTime Svrd_from_dt { get { return _svrd_from_dt; } set { _svrd_from_dt = value; } }
        public Int32 Svrd_from_qty { get { return _svrd_from_qty; } set { _svrd_from_qty = value; } }
        public decimal Svrd_from_val { get { return _svrd_from_val; } set { _svrd_from_val = value; } }
        public Boolean Svrd_is_req { get { return _svrd_is_req; } set { _svrd_is_req = value; } }
        public string Svrd_itm { get { return _svrd_itm; } set { _svrd_itm = value; } }
        public string Svrd_pay_tp { get { return _svrd_pay_tp; } set { _svrd_pay_tp = value; } }
        public string Svrd_pb { get { return _svrd_pb; } set { _svrd_pb = value; } }
        public string Svrd_pb_lvl { get { return _svrd_pb_lvl; } set { _svrd_pb_lvl = value; } }
        public string Svrd_pc { get { return _svrd_pc; } set { _svrd_pc = value; } }
        public string Svrd_sale_tp { get { return _svrd_sale_tp; } set { _svrd_sale_tp = value; } }
        public string Svrd_scheme { get { return _svrd_scheme; } set { _svrd_scheme = value; } }
        public Int32 Svrd_seq { get { return _svrd_seq; } set { _svrd_seq = value; } }
        public Int32 Svrd_term { get { return _svrd_term; } set { _svrd_term = value; } }
        public DateTime Svrd_to_dt { get { return _svrd_to_dt; } set { _svrd_to_dt = value; } }
        public Int32 Svrd_to_qty { get { return _svrd_to_qty; } set { _svrd_to_qty = value; } }
        public decimal Svrd_to_val { get { return _svrd_to_val; } set { _svrd_to_val = value; } }
        public decimal Svrd_val { get { return _svrd_val; } set { _svrd_val = value; } }
        public string Svrd_ser_1 { get { return _Svrd_ser_1; } set { _Svrd_ser_1 = value; } } 
       


        #endregion

        public static VehicalRegistrationDefnition Converter(DataRow row)
        {
            return new VehicalRegistrationDefnition
            {
                //Svrd_seq = ((row["SVRD_SEQ"] == DBNull.Value) ? 0 : Convert.ToDecimal( row["SVRD_SEQ"].ToString())),
                //Svrd_Com = ((row["SVRD_COM"] == DBNull.Value) ? string.Empty : row["SVRD_COM"].ToString()),
                //Svrd_PC = ((row["SVRD_PC"] == DBNull.Value) ? string.Empty : row["SVRD_PC"].ToString()),
                //Svrd_from_dt = ((row["SVRD_FROM_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRD_FROM_DT"])),
                //Svrd_to_dt = ((row["SVRD_TO_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRD_TO_DT"])),
                //Svrd_sale_tp = ((row["SVRD_SALE_TP"] == DBNull.Value) ? string.Empty : row["SVRD_SALE_TP"].ToString()),
                //Svrd_itm = ((row["SVRD_ITM"] == DBNull.Value) ? string.Empty : row["SVRD_ITM"].ToString()),
                //Svrd_val = row["SVRD_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVRD_VAL"]),
                //Svrd_claim_val = row["SVRD_CLAIM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVRD_CLAIM_VAL"]),
                //Svrd_is_req = row["SVRD_IS_REQ"] == DBNull.Value ? false : Convert.ToBoolean(row["SVRD_IS_REQ"]),
                //Svrd_cre_by = ((row["SVRD_CRE_BY"] == DBNull.Value) ? string.Empty : row["SVRD_CRE_BY"].ToString()),
                //Svrd_cre_dt = ((row["SVRD_CRE_DT"] == DBNull.Value) ? DateTime.MinValue : Convert.ToDateTime(row["SVRD_CRE_DT"]))

                Svrd_alw_mult_pay = row["SVRD_ALW_MULT_PAY"] == DBNull.Value ? false : Convert.ToBoolean(row["SVRD_ALW_MULT_PAY"]),
                Svrd_check_on = row["SVRD_CHECK_ON"] == DBNull.Value ? false : Convert.ToBoolean(row["SVRD_CHECK_ON"]),
                Svrd_circular = row["SVRD_CIRCULAR"] == DBNull.Value ? string.Empty : row["SVRD_CIRCULAR"].ToString(),
                Svrd_claim_val = row["SVRD_CLAIM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVRD_CLAIM_VAL"]),
                Svrd_com = row["SVRD_COM"] == DBNull.Value ? string.Empty : row["SVRD_COM"].ToString(),
                Svrd_cre_by = row["SVRD_CRE_BY"] == DBNull.Value ? string.Empty : row["SVRD_CRE_BY"].ToString(),
                Svrd_cre_dt = row["SVRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVRD_CRE_DT"]),
                Svrd_cust_tp = row["SVRD_CUST_TP"] == DBNull.Value ? string.Empty : row["SVRD_CUST_TP"].ToString(),
                Svrd_from_dt = row["SVRD_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVRD_FROM_DT"]),
                Svrd_from_qty = row["SVRD_FROM_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVRD_FROM_QTY"]),
                Svrd_from_val = row["SVRD_FROM_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVRD_FROM_VAL"]),
                Svrd_is_req = row["SVRD_IS_REQ"] == DBNull.Value ? false : Convert.ToBoolean(row["SVRD_IS_REQ"]),
                Svrd_itm = row["SVRD_ITM"] == DBNull.Value ? string.Empty : row["SVRD_ITM"].ToString(),
                Svrd_pay_tp = row["SVRD_PAY_TP"] == DBNull.Value ? string.Empty : row["SVRD_PAY_TP"].ToString(),
                Svrd_pb = row["SVRD_PB"] == DBNull.Value ? string.Empty : row["SVRD_PB"].ToString(),
                Svrd_pb_lvl = row["SVRD_PB_LVL"] == DBNull.Value ? string.Empty : row["SVRD_PB_LVL"].ToString(),
                Svrd_pc = row["SVRD_PC"] == DBNull.Value ? string.Empty : row["SVRD_PC"].ToString(),
                Svrd_sale_tp = row["SVRD_SALE_TP"] == DBNull.Value ? string.Empty : row["SVRD_SALE_TP"].ToString(),
                Svrd_scheme = row["SVRD_SCHEME"] == DBNull.Value ? string.Empty : row["SVRD_SCHEME"].ToString(),
                Svrd_seq = row["SVRD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVRD_SEQ"]),
                Svrd_term = row["SVRD_TERM"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVRD_TERM"]),
                Svrd_to_dt = row["SVRD_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SVRD_TO_DT"]),
                Svrd_to_qty = row["SVRD_TO_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVRD_TO_QTY"]),
                Svrd_to_val = row["SVRD_TO_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVRD_TO_VAL"]),
                Svrd_val = row["SVRD_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SVRD_VAL"]),
                SVRD_NO_OF_TIMES = row["SVRD_NO_OF_TIMES"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVRD_NO_OF_TIMES"]),
                SVRD_NO_OF_USED = row["SVRD_NO_OF_USED"] == DBNull.Value ? 0 : Convert.ToInt32(row["SVRD_NO_OF_USED"]),
                Svrd_ser_1 = row["Svrd_ser_1"] == DBNull.Value ? string.Empty : row["Svrd_ser_1"].ToString(),


            };
        }
    }
}
