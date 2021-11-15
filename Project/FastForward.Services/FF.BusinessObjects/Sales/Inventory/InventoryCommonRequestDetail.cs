using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    /// <summary>
    /// Description : Business Object class for Inventory Common Request detail.
    /// Created By : Miginda Geeganage.
    /// Created On : 16/05/2012
    /// </summary>
    public class InventoryCommonRequestDetail
    {

        #region Private Members
        private string _icrd_anal1;
        private string _icrd_anal2;
        private string _icrd_anal3;
        private string _icrd_anal4;
        private string _icrd_anal5;
        private string _icrd_anal6;
        private string _icrd_anal7;
        private DateTime _icrd_dt;
        private Boolean _icrd_israte1;
        private Boolean _icrd_israte2;
        private Boolean _icrd_israte3;
        private Boolean _icrd_israte4;
        private Int32 _icrd_line_no;
        private string _icrd_param_name;
        private Int32 _icrd_seq_no;
        private decimal _icrd_value1;
        private decimal _icrd_value2;
        private decimal _icrd_value3;
        private decimal _icrd_value4;
        private decimal _icrd_value5;
        private decimal _icrd_value6;
        private decimal _icrd_value7;
        #endregion

        #region Property Definition
        public string Icrd_anal1
        {
            get { return _icrd_anal1; }
            set { _icrd_anal1 = value; }
        }
        public string Icrd_anal2
        {
            get { return _icrd_anal2; }
            set { _icrd_anal2 = value; }
        }
        public string Icrd_anal3
        {
            get { return _icrd_anal3; }
            set { _icrd_anal3 = value; }
        }
        public string Icrd_anal4
        {
            get { return _icrd_anal4; }
            set { _icrd_anal4 = value; }
        }
        public string Icrd_anal5
        {
            get { return _icrd_anal5; }
            set { _icrd_anal5 = value; }
        }
        public string Icrd_anal6
        {
            get { return _icrd_anal6; }
            set { _icrd_anal6 = value; }
        }
        public string Icrd_anal7
        {
            get { return _icrd_anal7; }
            set { _icrd_anal7 = value; }
        }
        public DateTime Icrd_dt
        {
            get { return _icrd_dt; }
            set { _icrd_dt = value; }
        }
        public Boolean Icrd_israte1
        {
            get { return _icrd_israte1; }
            set { _icrd_israte1 = value; }
        }
        public Boolean Icrd_israte2
        {
            get { return _icrd_israte2; }
            set { _icrd_israte2 = value; }
        }
        public Boolean Icrd_israte3
        {
            get { return _icrd_israte3; }
            set { _icrd_israte3 = value; }
        }
        public Boolean Icrd_israte4
        {
            get { return _icrd_israte4; }
            set { _icrd_israte4 = value; }
        }
        public Int32 Icrd_line_no
        {
            get { return _icrd_line_no; }
            set { _icrd_line_no = value; }
        }
        public string Icrd_param_name
        {
            get { return _icrd_param_name; }
            set { _icrd_param_name = value; }
        }
        public Int32 Icrd_seq_no
        {
            get { return _icrd_seq_no; }
            set { _icrd_seq_no = value; }
        }
        public decimal Icrd_value1
        {
            get { return _icrd_value1; }
            set { _icrd_value1 = value; }
        }
        public decimal Icrd_value2
        {
            get { return _icrd_value2; }
            set { _icrd_value2 = value; }
        }
        public decimal Icrd_value3
        {
            get { return _icrd_value3; }
            set { _icrd_value3 = value; }
        }
        public decimal Icrd_value4
        {
            get { return _icrd_value4; }
            set { _icrd_value4 = value; }
        }
        public decimal Icrd_value5
        {
            get { return _icrd_value5; }
            set { _icrd_value5 = value; }
        }
        public decimal Icrd_value6
        {
            get { return _icrd_value6; }
            set { _icrd_value6 = value; }
        }
        public decimal Icrd_value7
        {
            get { return _icrd_value7; }
            set { _icrd_value7 = value; }
        }
        #endregion

        public static InventoryCommonRequestDetail TotalConverter(DataRow row)
        {
            return new InventoryCommonRequestDetail
            {
                Icrd_anal1 = row["ICRD_ANAL1"] == DBNull.Value ? string.Empty : row["ICRD_ANAL1"].ToString(),
                Icrd_anal2 = row["ICRD_ANAL2"] == DBNull.Value ? string.Empty : row["ICRD_ANAL2"].ToString(),
                Icrd_anal3 = row["ICRD_ANAL3"] == DBNull.Value ? string.Empty : row["ICRD_ANAL3"].ToString(),
                Icrd_anal4 = row["ICRD_ANAL4"] == DBNull.Value ? string.Empty : row["ICRD_ANAL4"].ToString(),
                Icrd_anal5 = row["ICRD_ANAL5"] == DBNull.Value ? string.Empty : row["ICRD_ANAL5"].ToString(),
                Icrd_anal6 = row["ICRD_ANAL6"] == DBNull.Value ? string.Empty : row["ICRD_ANAL6"].ToString(),
                Icrd_anal7 = row["ICRD_ANAL7"] == DBNull.Value ? string.Empty : row["ICRD_ANAL7"].ToString(),
                Icrd_dt = row["ICRD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ICRD_DT"]),
                Icrd_israte1 = row["ICRD_ISRATE1"] == DBNull.Value ? false : Convert.ToBoolean(row["ICRD_ISRATE1"]),
                Icrd_israte2 = row["ICRD_ISRATE2"] == DBNull.Value ? false : Convert.ToBoolean(row["ICRD_ISRATE2"]),
                Icrd_israte3 = row["ICRD_ISRATE3"] == DBNull.Value ? false : Convert.ToBoolean(row["ICRD_ISRATE3"]),
                Icrd_israte4 = row["ICRD_ISRATE4"] == DBNull.Value ? false : Convert.ToBoolean(row["ICRD_ISRATE4"]),
                Icrd_line_no = row["ICRD_LINE_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICRD_LINE_NO"]),
                Icrd_param_name = row["ICRD_PARAM_NAME"] == DBNull.Value ? string.Empty : row["ICRD_PARAM_NAME"].ToString(),
                Icrd_seq_no = row["ICRD_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ICRD_SEQ_NO"]),
                Icrd_value1 = row["ICRD_VALUE1"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE1"]),
                Icrd_value2 = row["ICRD_VALUE2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE2"]),
                Icrd_value3 = row["ICRD_VALUE3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE3"]),
                Icrd_value4 = row["ICRD_VALUE4"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE4"]),
                Icrd_value5 = row["ICRD_VALUE5"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE5"]),
                Icrd_value6 = row["ICRD_VALUE6"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE6"]),
                Icrd_value7 = row["ICRD_VALUE7"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ICRD_VALUE7"])

            };
        }
    }
}
