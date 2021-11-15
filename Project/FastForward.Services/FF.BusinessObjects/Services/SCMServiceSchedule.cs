using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCMServiceSchedule
    {
        #region Private Members
        private Int32 _actual_reading;
        private Boolean _is_free;
        private string _item_code;
        private string _item_status;
        private DateTime _job_date;
        private string _job_no;
        private string _serial_no;
        private int _service_term;
        private string _warr_no;
        private decimal _wr_period_alt_from;
        private decimal _wr_period_alt_to;
        private string _wr_period_alt_uom;
        private decimal _wr_period_from;
        private decimal _wr_period_to;
        private string _wr_period_uom;
        #endregion

        public Int32 Actual_reading
        {
            get { return _actual_reading; }
            set { _actual_reading = value; }
        }
        public Boolean Is_free
        {
            get { return _is_free; }
            set { _is_free = value; }
        }
        public string Item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public string Item_status
        {
            get { return _item_status; }
            set { _item_status = value; }
        }
        public DateTime Job_date
        {
            get { return _job_date; }
            set { _job_date = value; }
        }
        public string Job_no
        {
            get { return _job_no; }
            set { _job_no = value; }
        }
        public string Serial_no
        {
            get { return _serial_no; }
            set { _serial_no = value; }
        }
        public int Service_term
        {
            get { return _service_term; }
            set { _service_term = value; }
        }
        public string Warr_no
        {
            get { return _warr_no; }
            set { _warr_no = value; }
        }
        public decimal Wr_period_alt_from
        {
            get { return _wr_period_alt_from; }
            set { _wr_period_alt_from = value; }
        }
        public decimal Wr_period_alt_to
        {
            get { return _wr_period_alt_to; }
            set { _wr_period_alt_to = value; }
        }
        public string Wr_period_alt_uom
        {
            get { return _wr_period_alt_uom; }
            set { _wr_period_alt_uom = value; }
        }
        public decimal Wr_period_from
        {
            get { return _wr_period_from; }
            set { _wr_period_from = value; }
        }
        public decimal Wr_period_to
        {
            get { return _wr_period_to; }
            set { _wr_period_to = value; }
        }
        public string Wr_period_uom
        {
            get { return _wr_period_uom; }
            set { _wr_period_uom = value; }
        }

        public static SCMServiceSchedule Converter(DataRow row)
        {
            return new SCMServiceSchedule
            {
                Actual_reading = row["ACTUAL_READING"] == DBNull.Value ? 0 : Convert.ToInt32(row["ACTUAL_READING"]),
                Is_free = row["IS_FREE"] == DBNull.Value ? false : Convert.ToBoolean(row["IS_FREE"]),
                Item_code = row["ITEM_CODE"] == DBNull.Value ? string.Empty : row["ITEM_CODE"].ToString(),
                Item_status = row["ITEM_STATUS"] == DBNull.Value ? string.Empty : row["ITEM_STATUS"].ToString(),
                Job_date = row["JOB_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["JOB_DATE"]),
                Job_no = row["JOB_NO"] == DBNull.Value ? string.Empty : row["JOB_NO"].ToString(),
                Serial_no = row["SERIAL_NO"] == DBNull.Value ? string.Empty : row["SERIAL_NO"].ToString(),
                Service_term = row["SERVICE_TERM"] == DBNull.Value ? 0 : Convert.ToInt16(row["SERVICE_TERM"]),
                Warr_no = row["WARR_NO"] == DBNull.Value ? string.Empty : row["WARR_NO"].ToString(),
                Wr_period_alt_from = row["WR_PERIOD_ALT_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WR_PERIOD_ALT_FROM"]),
                Wr_period_alt_to = row["WR_PERIOD_ALT_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WR_PERIOD_ALT_TO"]),
                Wr_period_alt_uom = row["WR_PERIOD_ALT_UOM"] == DBNull.Value ? string.Empty : row["WR_PERIOD_ALT_UOM"].ToString(),
                Wr_period_from = row["WR_PERIOD_FROM"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WR_PERIOD_FROM"]),
                Wr_period_to = row["WR_PERIOD_TO"] == DBNull.Value ? 0 : Convert.ToDecimal(row["WR_PERIOD_TO"]),
                Wr_period_uom = row["WR_PERIOD_UOM"] == DBNull.Value ? string.Empty : row["WR_PERIOD_UOM"].ToString()

            };
        }
    }
}
