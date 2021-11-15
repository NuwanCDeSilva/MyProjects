using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCV_AGR_COVER_ITM
    {
        #region Private Members
        private string _saic_agr_no;
        private string _saic_itm_cd;
        private Int32 _saic_itm_line;
        private Int32 _saic_line;
        private Int32 _saic_seq_no;
        private string _saic_itm_desc;
        #endregion

        #region Public Property Definition
        public string Saic_agr_no
        {
            get { return _saic_agr_no; }
            set { _saic_agr_no = value; }
        }
        public string Saic_itm_cd
        {
            get { return _saic_itm_cd; }
            set { _saic_itm_cd = value; }
        }
        public Int32 Saic_itm_line
        {
            get { return _saic_itm_line; }
            set { _saic_itm_line = value; }
        }
        public Int32 Saic_line
        {
            get { return _saic_line; }
            set { _saic_line = value; }
        }
        public Int32 Saic_seq_no
        {
            get { return _saic_seq_no; }
            set { _saic_seq_no = value; }
        }

        public string Saic_itm_desc
        {
            get { return _saic_itm_desc; }
            set { _saic_itm_desc = value; }
        }

        #endregion

        #region Converters
        public static SCV_AGR_COVER_ITM Converter(DataRow row)
        {
            return new SCV_AGR_COVER_ITM
            {
                Saic_agr_no = row["SAIC_AGR_NO"] == DBNull.Value ? string.Empty : row["SAIC_AGR_NO"].ToString(),
                Saic_itm_cd = row["SAIC_ITM_CD"] == DBNull.Value ? string.Empty : row["SAIC_ITM_CD"].ToString(),
                Saic_itm_line = row["SAIC_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAIC_ITM_LINE"]),
                Saic_line = row["SAIC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAIC_LINE"]),
                Saic_seq_no = row["SAIC_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAIC_SEQ_NO"])


            };
        }

        public static SCV_AGR_COVER_ITM Converter_1(DataRow row)
        {
            return new SCV_AGR_COVER_ITM
            {
                Saic_itm_cd = row["SAIC_ITM_CD"] == DBNull.Value ? string.Empty : row["SAIC_ITM_CD"].ToString(),
                Saic_itm_line = row["SAIC_ITM_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAIC_ITM_LINE"]),
                Saic_line = row["SAIC_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SAIC_LINE"]),
                Saic_itm_desc = row["Saic_itm_desc"] == DBNull.Value ? string.Empty : row["Saic_itm_desc"].ToString()



            };
        }
        #endregion
    }
}

