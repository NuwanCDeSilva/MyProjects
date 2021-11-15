using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class VoucherDetails
    {
        #region Private Members
        private string _givd_acpt_by;
        private decimal _givd_anal1;
        private string _givd_anal2;
        private string _givd_auth_by;
        private string _givd_expe_cd;
        private string _givd_expe_desc;
        private Boolean _givd_expe_direct;
        private decimal _givd_expe_val;
        private int _givd_line;
        private string _givd_prep_by;
        private Int32 _givd_seq;
        private string _givd_vou_no;
        private DateTime _gvid_dt;
        #endregion

        public string Givd_acpt_by
        {
            get { return _givd_acpt_by; }
            set { _givd_acpt_by = value; }
        }
        public decimal Givd_anal1
        {
            get { return _givd_anal1; }
            set { _givd_anal1 = value; }
        }
        public string Givd_anal2
        {
            get { return _givd_anal2; }
            set { _givd_anal2 = value; }
        }
        public string Givd_auth_by
        {
            get { return _givd_auth_by; }
            set { _givd_auth_by = value; }
        }
        public string Givd_expe_cd
        {
            get { return _givd_expe_cd; }
            set { _givd_expe_cd = value; }
        }
        public string Givd_expe_desc
        {
            get { return _givd_expe_desc; }
            set { _givd_expe_desc = value; }
        }
        public Boolean Givd_expe_direct
        {
            get { return _givd_expe_direct; }
            set { _givd_expe_direct = value; }
        }
        public decimal Givd_expe_val
        {
            get { return _givd_expe_val; }
            set { _givd_expe_val = value; }
        }
        public int Givd_line
        {
            get { return _givd_line; }
            set { _givd_line = value; }
        }
        public string Givd_prep_by
        {
            get { return _givd_prep_by; }
            set { _givd_prep_by = value; }
        }
        public Int32 Givd_seq
        {
            get { return _givd_seq; }
            set { _givd_seq = value; }
        }
        public string Givd_vou_no
        {
            get { return _givd_vou_no; }
            set { _givd_vou_no = value; }
        }
        public DateTime Gvid_dt
        {
            get { return _gvid_dt; }
            set { _gvid_dt = value; }
        }
        public static VoucherDetails Converter(DataRow row)
        {
            return new VoucherDetails
            {
                Givd_acpt_by = row["GIVD_ACPT_BY"] == DBNull.Value ? string.Empty : row["GIVD_ACPT_BY"].ToString(),
                Givd_anal1 = row["GIVD_ANAL1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GIVD_ANAL1"]),
                Givd_anal2 = row["GIVD_ANAL2"] == DBNull.Value ? string.Empty : row["GIVD_ANAL2"].ToString(),
                Givd_auth_by = row["GIVD_AUTH_BY"] == DBNull.Value ? string.Empty : row["GIVD_AUTH_BY"].ToString(),
                Givd_expe_cd = row["GIVD_EXPE_CD"] == DBNull.Value ? string.Empty : row["GIVD_EXPE_CD"].ToString(),
                Givd_expe_desc = row["GIVD_EXPE_DESC"] == DBNull.Value ? string.Empty : row["GIVD_EXPE_DESC"].ToString(),
                Givd_expe_direct = row["GIVD_EXPE_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["GIVD_EXPE_DIRECT"]),
                Givd_expe_val = row["GIVD_EXPE_VAL"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GIVD_EXPE_VAL"]),
                Givd_line = row["GIVD_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GIVD_LINE"]),
                Givd_prep_by = row["GIVD_PREP_BY"] == DBNull.Value ? string.Empty : row["GIVD_PREP_BY"].ToString(),
                Givd_seq = row["GIVD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["GIVD_SEQ"]),
                Givd_vou_no = row["GIVD_VOU_NO"] == DBNull.Value ? string.Empty : row["GIVD_VOU_NO"].ToString(),
                Gvid_dt = row["GVID_DT"] == DBNull.Value ?   DateTime.MinValue : Convert.ToDateTime(row["GVID_DT"])
            };
        }
    }
}
