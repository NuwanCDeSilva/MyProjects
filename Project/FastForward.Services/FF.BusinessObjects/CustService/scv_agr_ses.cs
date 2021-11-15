using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCV_AGR_SES
    {
        #region Private Members
        private string _saga_agr_no;
        private DateTime _saga_from_dt;
        private Int32 _saga_itm_line;
        private Int32 _saga_line;
        private Int32 _saga_seq_no;
        private string _saga_stus;
        private DateTime _saga_to_dt;
        private string _saga_req; //kapila 9/11/2015
        #endregion

        #region Public Property Definition
        public string Saga_req
        {
            get { return _saga_req; }
            set { _saga_req = value; }
        }
        public string Saga_agr_no
        {
            get { return _saga_agr_no; }
            set { _saga_agr_no = value; }
        }
        public DateTime Saga_from_dt
        {
            get { return _saga_from_dt; }
            set { _saga_from_dt = value; }
        }
        public Int32 Saga_itm_line
        {
            get { return _saga_itm_line; }
            set { _saga_itm_line = value; }
        }
        public Int32 Saga_line
        {
            get { return _saga_line; }
            set { _saga_line = value; }
        }
        public Int32 Saga_seq_no
        {
            get { return _saga_seq_no; }
            set { _saga_seq_no = value; }
        }
        public string Saga_stus
        {
            get { return _saga_stus; }
            set { _saga_stus = value; }
        }
        public DateTime Saga_to_dt
        {
            get { return _saga_to_dt; }
            set { _saga_to_dt = value; }
        }


        #endregion

        #region Converters
        public static SCV_AGR_SES Converter(DataRow row)
        {
            return new SCV_AGR_SES
            {
                Saga_agr_no = row["SAGA_AGR_NO"] == DBNull.Value ? string.Empty : row["SAGA_AGR_NO"].ToString(),
                Saga_from_dt = row["SAGA_FROM_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAGA_FROM_DT"]),
                Saga_itm_line = row["SAGA_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAGA_ITM_LINE"]),
                Saga_line = row["SAGA_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAGA_LINE"]),
                Saga_seq_no = row["SAGA_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAGA_SEQ_NO"]),
                Saga_stus = row["SAGA_STUS"] == DBNull.Value ? string.Empty : row["SAGA_STUS"].ToString(),
                Saga_to_dt = row["SAGA_TO_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SAGA_TO_DT"]),
                Saga_req = row["saga_req"] == DBNull.Value ? string.Empty : row["saga_req"].ToString()


            };
        }
        #endregion
    }
}

