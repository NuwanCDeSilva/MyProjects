using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterAutoNumber 
    {
        #region Private Members
        private string _aut_cate_cd;
        private string _aut_cate_tp;
        private Int32? _aut_direction;
        private DateTime? _aut_modify_dt;
        private string _aut_moduleid;
        private Int32 _aut_number;
        private string _aut_start_char;
        private Int32? _aut_year;
        #endregion

        public string Aut_cate_cd { get { return _aut_cate_cd; } set { _aut_cate_cd = value; } }
        public string Aut_cate_tp { get { return _aut_cate_tp; } set { _aut_cate_tp = value; } }
        public Int32? Aut_direction { get { return _aut_direction; } set { _aut_direction = value; } }
        public DateTime? Aut_modify_dt { get { return _aut_modify_dt; } set { _aut_modify_dt = value; } }
        public string Aut_moduleid { get { return _aut_moduleid; } set { _aut_moduleid = value; } }
        public Int32 Aut_number { get { return _aut_number; } set { _aut_number = value; } }
        public string Aut_start_char { get { return _aut_start_char; } set { _aut_start_char = value; } }
        public Int32? Aut_year { get { return _aut_year; } set { _aut_year = value; } }

        public static MasterAutoNumber ConvertTotal(DataRow row)
        {
            
            return new MasterAutoNumber
            { 
                Aut_cate_cd = row["AUT_CATE_CD"] == DBNull.Value ? string.Empty : row["AUT_CATE_CD"].ToString(),
                Aut_cate_tp = row["AUT_CATE_TP"] == DBNull.Value ? string.Empty : row["AUT_CATE_TP"].ToString(),
                Aut_direction = row["AUT_DIRECTION"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUT_DIRECTION"]),
                Aut_modify_dt = row["AUT_MODIFY_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUT_MODIFY_DT"]),
                Aut_moduleid = row["AUT_MODULEID"] == DBNull.Value ? string.Empty : row["AUT_MODULEID"].ToString(),
                Aut_number = row["AUT_NUMBER"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUT_NUMBER"]),
                Aut_start_char = row["AUT_START_CHAR"] == DBNull.Value ? string.Empty : row["AUT_START_CHAR"].ToString(),
                Aut_year = row["AUT_YEAR"] == DBNull.Value ? 0 : Convert.ToInt32(row["AUT_YEAR"])

            };
        }
    }
}

