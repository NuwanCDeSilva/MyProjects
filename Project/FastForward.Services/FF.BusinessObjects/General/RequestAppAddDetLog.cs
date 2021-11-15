using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RequestAppAddDetLog
    {
        #region Private Members
        private string _grad_anal1;
        private decimal _grad_anal10;
        private string _grad_anal11;
        private string _grad_anal2;
        private string _grad_anal3;
        private string _grad_anal4;
        private string _grad_anal5;
        private decimal _grad_anal6;
        private decimal _grad_anal7;
        private decimal _grad_anal8;
        private decimal _grad_anal9;
        private Int32 _grad_line;
        private int _grad_lvl;
        private string _grad_ref;
        #endregion

        public string Grad_anal1
        {
            get { return _grad_anal1; }
            set { _grad_anal1 = value; }
        }
        public decimal Grad_anal10
        {
            get { return _grad_anal10; }
            set { _grad_anal10 = value; }
        }
        public string Grad_anal11
        {
            get { return _grad_anal11; }
            set { _grad_anal11 = value; }
        }
        public string Grad_anal2
        {
            get { return _grad_anal2; }
            set { _grad_anal2 = value; }
        }
        public string Grad_anal3
        {
            get { return _grad_anal3; }
            set { _grad_anal3 = value; }
        }
        public string Grad_anal4
        {
            get { return _grad_anal4; }
            set { _grad_anal4 = value; }
        }
        public string Grad_anal5
        {
            get { return _grad_anal5; }
            set { _grad_anal5 = value; }
        }
        public decimal Grad_anal6
        {
            get { return _grad_anal6; }
            set { _grad_anal6 = value; }
        }
        public decimal Grad_anal7
        {
            get { return _grad_anal7; }
            set { _grad_anal7 = value; }
        }
        public decimal Grad_anal8
        {
            get { return _grad_anal8; }
            set { _grad_anal8 = value; }
        }
        public decimal Grad_anal9
        {
            get { return _grad_anal9; }
            set { _grad_anal9 = value; }
        }
        public Int32 Grad_line
        {
            get { return _grad_line; }
            set { _grad_line = value; }
        }
        public int Grad_lvl
        {
            get { return _grad_lvl; }
            set { _grad_lvl = value; }
        }
        public string Grad_ref
        {
            get { return _grad_ref; }
            set { _grad_ref = value; }
        }

        public static RequestAppAddDetLog Converter(DataRow row)
        {
            return new RequestAppAddDetLog
            {
                Grad_anal1 = row["GRAD_ANAL1"] == DBNull.Value ? string.Empty : row["GRAD_ANAL1"].ToString(),
                Grad_anal10 = row["GRAD_ANAL10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL10"]),
                Grad_anal11 = row["GRAD_ANAL11"] == DBNull.Value ? string.Empty : row["GRAD_ANAL11"].ToString(),
                Grad_anal2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                Grad_anal3 = row["GRAD_ANAL3"] == DBNull.Value ? string.Empty : row["GRAD_ANAL3"].ToString(),
                Grad_anal4 = row["GRAD_ANAL4"] == DBNull.Value ? string.Empty : row["GRAD_ANAL4"].ToString(),
                Grad_anal5 = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                Grad_anal6 = row["GRAD_ANAL6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL6"]),
                Grad_anal7 = row["GRAD_ANAL7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL7"]),
                Grad_anal8 = row["GRAD_ANAL8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL8"]),
                Grad_anal9 = row["GRAD_ANAL9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_ANAL9"]),
                Grad_line = row["GRAD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LINE"]),
                Grad_lvl = row["GRAD_LVL"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAD_LVL"]),
                Grad_ref = row["GRAD_REF"] == DBNull.Value ? string.Empty : row["GRAD_REF"].ToString()

            };
        }

    }
}
