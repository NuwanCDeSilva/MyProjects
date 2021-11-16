using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Sales
{
    [Serializable]
    public class MasterReceiptDivision
    {
        #region Private Members
        private string _msrd_cd;
        private string _msrd_com;
        private string _msrd_cre_by;
        private DateTime _msrd_cre_dt;
        private string _msrd_desc;
        private string _msrd_div_tp;
        private string _msrd_inv_tp;
        private Boolean _msrd_is_def;
        private Boolean _msrd_is_sales;
        private Boolean _msrd_is_ser;
        private string _msrd_mod_by;
        private DateTime _msrd_mod_dt;
        private string _msrd_pc;
        private Boolean _msrd_stus;
        #endregion

        public string Msrd_cd
        {
            get { return _msrd_cd; }
            set { _msrd_cd = value; }
        }
        public string Msrd_com
        {
            get { return _msrd_com; }
            set { _msrd_com = value; }
        }
        public string Msrd_cre_by
        {
            get { return _msrd_cre_by; }
            set { _msrd_cre_by = value; }
        }
        public DateTime Msrd_cre_dt
        {
            get { return _msrd_cre_dt; }
            set { _msrd_cre_dt = value; }
        }
        public string Msrd_desc
        {
            get { return _msrd_desc; }
            set { _msrd_desc = value; }
        }
        public string Msrd_div_tp
        {
            get { return _msrd_div_tp; }
            set { _msrd_div_tp = value; }
        }
        public string Msrd_inv_tp
        {
            get { return _msrd_inv_tp; }
            set { _msrd_inv_tp = value; }
        }
        public Boolean Msrd_is_def
        {
            get { return _msrd_is_def; }
            set { _msrd_is_def = value; }
        }
        public Boolean Msrd_is_sales
        {
            get { return _msrd_is_sales; }
            set { _msrd_is_sales = value; }
        }
        public Boolean Msrd_is_ser
        {
            get { return _msrd_is_ser; }
            set { _msrd_is_ser = value; }
        }
        public string Msrd_mod_by
        {
            get { return _msrd_mod_by; }
            set { _msrd_mod_by = value; }
        }
        public DateTime Msrd_mod_dt
        {
            get { return _msrd_mod_dt; }
            set { _msrd_mod_dt = value; }
        }
        public string Msrd_pc
        {
            get { return _msrd_pc; }
            set { _msrd_pc = value; }
        }
        public Boolean Msrd_stus
        {
            get { return _msrd_stus; }
            set { _msrd_stus = value; }
        }

        public static MasterReceiptDivision Converter(DataRow row)
        {
            return new MasterReceiptDivision
            {
                Msrd_cd = row["MSRD_CD"] == DBNull.Value ? string.Empty : row["MSRD_CD"].ToString(),
                Msrd_com = row["MSRD_COM"] == DBNull.Value ? string.Empty : row["MSRD_COM"].ToString(),
                Msrd_cre_by = row["MSRD_CRE_BY"] == DBNull.Value ? string.Empty : row["MSRD_CRE_BY"].ToString(),
                Msrd_cre_dt = row["MSRD_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSRD_CRE_DT"]),
                Msrd_desc = row["MSRD_DESC"] == DBNull.Value ? string.Empty : row["MSRD_DESC"].ToString(),
                Msrd_div_tp = row["MSRD_DIV_TP"] == DBNull.Value ? string.Empty : row["MSRD_DIV_TP"].ToString(),
                Msrd_inv_tp = row["MSRD_INV_TP"] == DBNull.Value ? string.Empty : row["MSRD_INV_TP"].ToString(),
                Msrd_is_def = row["MSRD_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["MSRD_IS_DEF"]),
                Msrd_is_sales = row["MSRD_IS_SALES"] == DBNull.Value ? false : Convert.ToBoolean(row["MSRD_IS_SALES"]),
                Msrd_is_ser = row["MSRD_IS_SER"] == DBNull.Value ? false : Convert.ToBoolean(row["MSRD_IS_SER"]),
                Msrd_mod_by = row["MSRD_MOD_BY"] == DBNull.Value ? string.Empty : row["MSRD_MOD_BY"].ToString(),
                Msrd_mod_dt = row["MSRD_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MSRD_MOD_DT"]),
                Msrd_pc = row["MSRD_PC"] == DBNull.Value ? string.Empty : row["MSRD_PC"].ToString(),
                Msrd_stus = row["MSRD_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["MSRD_STUS"])

            };
        }

    }
}