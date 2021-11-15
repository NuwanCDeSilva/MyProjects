using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class RequestApprovalSerials
    {

        #region Private Members
        private string _gras_anal1;
        private decimal _gras_anal10;
        private string _gras_anal2;
        private string _gras_anal3;
        private string _gras_anal4;
        private string _gras_anal5;
        private decimal _gras_anal6;
        private decimal _gras_anal7;
        private decimal _gras_anal8;
        private decimal _gras_anal9;
        private int _gras_line;
        private string _gras_ref;
        private string _pc;
        private int _gras_inv_line;
        private int _gras_itm_line;
        private int _gras_batch_line;
        private int _gras_ser_line;

        #endregion

        public string Gras_anal1
        {
            get { return _gras_anal1; }
            set { _gras_anal1 = value; }
        }
        public decimal Gras_anal10
        {
            get { return _gras_anal10; }
            set { _gras_anal10 = value; }
        }
        public string Gras_anal2
        {
            get { return _gras_anal2; }
            set { _gras_anal2 = value; }
        }
        public string Gras_anal3
        {
            get { return _gras_anal3; }
            set { _gras_anal3 = value; }
        }
        public string Gras_anal4
        {
            get { return _gras_anal4; }
            set { _gras_anal4 = value; }
        }
        public string Gras_anal5
        {
            get { return _gras_anal5; }
            set { _gras_anal5 = value; }
        }
        public decimal Gras_anal6
        {
            get { return _gras_anal6; }
            set { _gras_anal6 = value; }
        }
        public decimal Gras_anal7
        {
            get { return _gras_anal7; }
            set { _gras_anal7 = value; }
        }
        public decimal Gras_anal8
        {
            get { return _gras_anal8; }
            set { _gras_anal8 = value; }
        }
        public decimal Gras_anal9
        {
            get { return _gras_anal9; }
            set { _gras_anal9 = value; }
        }
        public int Gras_line
        {
            get { return _gras_line; }
            set { _gras_line = value; }
        }
        
        public string Gras_ref
        {
            get { return _gras_ref; }
            set { _gras_ref = value; }
        }

        public string Pc
        {
            get { return _pc; }
            set { _pc = value; }
        }
        public int Gras_inv_line
        {
            get { return _gras_inv_line; }
            set { _gras_inv_line = value; }
        }
        public int Gras_itm_line
        {
            get { return _gras_itm_line; }
            set { _gras_itm_line = value; }
        }
        public int Gras_batch_line
        {
            get { return _gras_batch_line; }
            set { _gras_batch_line = value; }
        }
        public int Gras_ser_line
        {
            get { return _gras_ser_line; }
            set { _gras_ser_line = value; }
        }

        public static RequestApprovalSerials Converter(DataRow row)
        {
            return new RequestApprovalSerials
            {
                Gras_anal1 = row["GRAS_ANAL1"] == DBNull.Value ? string.Empty : row["GRAS_ANAL1"].ToString(),
                Gras_anal10 = row["GRAS_ANAL10"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL10"]),
                Gras_anal2 = row["GRAS_ANAL2"] == DBNull.Value ? string.Empty : row["GRAS_ANAL2"].ToString(),
                Gras_anal3 = row["GRAS_ANAL3"] == DBNull.Value ? string.Empty : row["GRAS_ANAL3"].ToString(),
                Gras_anal4 = row["GRAS_ANAL4"] == DBNull.Value ? string.Empty : row["GRAS_ANAL4"].ToString(),
                Gras_anal5 = row["GRAS_ANAL5"] == DBNull.Value ? string.Empty : row["GRAS_ANAL5"].ToString(),
                Gras_anal6 = row["GRAS_ANAL6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL6"]),
                Gras_anal7 = row["GRAS_ANAL7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL7"]),
                Gras_anal8 = row["GRAS_ANAL8"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL8"]),
                Gras_anal9 = row["GRAS_ANAL9"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAS_ANAL9"]),
                Gras_line = row["GRAS_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_LINE"]),
                Gras_ref = row["GRAS_REF"] == DBNull.Value ? string.Empty : row["GRAS_REF"].ToString(),
                Gras_inv_line = row["GRAS_INV_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_INV_LINE"]),
                Gras_itm_line = row["GRAS_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_ITM_LINE"]),
                Gras_batch_line = row["GRAS_BATCH_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_BATCH_LINE"]),
                Gras_ser_line = row["GRAS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["GRAS_SER_LINE"])

            };
        }


    }
}
