using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]

    public class QuotationSerial
    {
        #region Private Members
        private string _qs_chassis;
        private string _qs_item;
        private Int32 _qs_main_line;
        private string _qs_no;
        private Int32 _qs_seq_no;
        private string _qs_ser;
        private Int32 _qs_ser_line;
        private Int32 _qs_ser_id;
        private string _qs_ser_loc;
        public string _qs_itm_stus;    //kapila    13/7/2016
        #endregion

        public string Qs_itm_stus
        {
            get { return _qs_itm_stus; }
            set { _qs_itm_stus = value; }
        }
        public string Qs_ser_loc
        {
            get { return _qs_ser_loc; }
            set { _qs_ser_loc = value; }
        }

        public Int32 Qs_ser_id
        {
            get { return _qs_ser_id; }
            set { _qs_ser_id = value; }
        }

        public string Qs_chassis
        {
            get { return _qs_chassis; }
            set { _qs_chassis = value; }
        }
        public string Qs_item
        {
            get { return _qs_item; }
            set { _qs_item = value; }
        }
        public Int32 Qs_main_line
        {
            get { return _qs_main_line; }
            set { _qs_main_line = value; }
        }
        public string Qs_no
        {
            get { return _qs_no; }
            set { _qs_no = value; }
        }
        public Int32 Qs_seq_no
        {
            get { return _qs_seq_no; }
            set { _qs_seq_no = value; }
        }
        public string Qs_ser
        {
            get { return _qs_ser; }
            set { _qs_ser = value; }
        }
        public Int32 Qs_ser_line
        {
            get { return _qs_ser_line; }
            set { _qs_ser_line = value; }
        }

        public static QuotationSerial Converter(DataRow row)
        {
            return new QuotationSerial
            {
                Qs_chassis = row["QS_CHASSIS"] == DBNull.Value ? string.Empty : row["QS_CHASSIS"].ToString(),
                Qs_item = row["QS_ITEM"] == DBNull.Value ? string.Empty : row["QS_ITEM"].ToString(),
                Qs_main_line = row["QS_MAIN_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_MAIN_LINE"]),
                Qs_no = row["QS_NO"] == DBNull.Value ? string.Empty : row["QS_NO"].ToString(),
                Qs_seq_no = row["QS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_SEQ_NO"]),
                Qs_ser = row["QS_SER"] == DBNull.Value ? string.Empty : row["QS_SER"].ToString(),
                Qs_ser_line = row["QS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_SER_LINE"]),
                Qs_ser_id = row["QS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_SER_ID"]),
                Qs_ser_loc = row["QS_SER_LOC"] == DBNull.Value ? string.Empty : row["QS_SER_LOC"].ToString()
            };
        }

        public static QuotationSerial Converter1(DataRow row)
        {
            return new QuotationSerial
            {
                Qs_chassis = row["QS_CHASSIS"] == DBNull.Value ? string.Empty : row["QS_CHASSIS"].ToString(),
                Qs_item = row["QS_ITEM"] == DBNull.Value ? string.Empty : row["QS_ITEM"].ToString(),
                Qs_main_line = row["QS_MAIN_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_MAIN_LINE"]),
                Qs_no = row["QS_NO"] == DBNull.Value ? string.Empty : row["QS_NO"].ToString(),
                Qs_seq_no = row["QS_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_SEQ_NO"]),
                Qs_ser = row["QS_SER"] == DBNull.Value ? string.Empty : row["QS_SER"].ToString(),
                Qs_ser_line = row["QS_SER_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_SER_LINE"]),
                Qs_ser_id = row["QS_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["QS_SER_ID"]),
                Qs_ser_loc = row["QS_SER_LOC"] == DBNull.Value ? string.Empty : row["QS_SER_LOC"].ToString(),
                Qs_itm_stus = row["Qs_itm_stus"] == DBNull.Value ? string.Empty : row["Qs_itm_stus"].ToString()
            };
        }

    }
}
