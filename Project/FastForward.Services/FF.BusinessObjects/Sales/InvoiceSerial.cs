using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
     [Serializable]
   public class InvoiceSerial
    {
        /// <summary>
        /// Written By Prabhathh on 26/04/2012
        /// </summary>

        #region Private Members
        private string _sap_del_loc;
        private string _sap_inv_no;
        private string _sap_itm_cd;
        private Int32 _sap_itm_line;
        private string _sap_remarks;
        private Int32 _sap_seq_no;
        private string _sap_ser_1;
        private string _sap_ser_2;
        private Int32 _sap_ser_line;
        private string _sap_sev_loc;
        #endregion

        public string Sap_del_loc { get { return _sap_del_loc; } set { _sap_del_loc = value; } }
        public string Sap_inv_no { get { return _sap_inv_no; } set { _sap_inv_no = value; } }
        public string Sap_itm_cd { get { return _sap_itm_cd; } set { _sap_itm_cd = value; } }
        public string Sap_itm_sts { get { return _sap_itm_cd; } set { _sap_itm_cd = value; } }
        public Int32 Sap_itm_line { get { return _sap_itm_line; } set { _sap_itm_line = value; } }
        public string Sap_remarks { get { return _sap_remarks; } set { _sap_remarks = value; } }
        public Int32 Sap_seq_no { get { return _sap_seq_no; } set { _sap_seq_no = value; } }
        public string Sap_ser_1 { get { return _sap_ser_1; } set { _sap_ser_1 = value; } }
        public string Sap_ser_2 { get { return _sap_ser_2; } set { _sap_ser_2 = value; } }
        public Int32 Sap_ser_line { get { return _sap_ser_line; } set { _sap_ser_line = value; } }
        public string Sap_sev_loc { get { return _sap_sev_loc; } set { _sap_sev_loc = value; } }

        public static InvoiceSerial ConvertTotal(DataRow row)
        {
            return new InvoiceSerial
            {
                Sap_del_loc = row["SAP_DEL_LOC"] == DBNull.Value ? string.Empty : row["SAP_DEL_LOC"].ToString(),
                Sap_inv_no = row["SAP_INV_NO"] == DBNull.Value ? string.Empty : row["SAP_INV_NO"].ToString(),
                Sap_itm_cd = row["SAP_ITM_CD"] == DBNull.Value ? string.Empty : row["SAP_ITM_CD"].ToString(),
                Sap_itm_line = row["SAP_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAP_ITM_LINE"]),
                Sap_remarks = row["SAP_REMARKS"] == DBNull.Value ? string.Empty : row["SAP_REMARKS"].ToString(),
                Sap_seq_no = row["SAP_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAP_SEQ_NO"]),
                Sap_ser_1 = row["SAP_SER_1"] == DBNull.Value ? string.Empty : row["SAP_SER_1"].ToString(),
                Sap_ser_2 = row["SAP_SER_2"] == DBNull.Value ? string.Empty : row["SAP_SER_2"].ToString(),
                Sap_ser_line = row["SAP_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAP_SER_LINE"]),
                Sap_sev_loc = row["SAP_SEV_LOC"] == DBNull.Value ? string.Empty : row["SAP_SEV_LOC"].ToString()

            };
        }
    }
}

