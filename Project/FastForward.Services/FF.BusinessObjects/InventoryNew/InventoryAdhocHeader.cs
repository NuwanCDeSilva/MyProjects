using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

  
namespace FF.BusinessObjects
{
    /// <summary>

    /// Table : INT_ADHOC_HDR
    /// Created On : 10/09/2012
    /// </summary>
      [Serializable]
    public class InventoryAdhocHeader
    {
        #region Private Members
        private string _iadh_adj_no;
        private string _iadh_anal1;
        private string _iadh_anal2;
        private string _iadh_anal3;
        private string _iadh_anal4;
        private string _iadh_anal5;
        private string _iadh_app_by;
        private DateTime _iadh_app_dt;
        private string _iadh_coll_by;
        private DateTime _iadh_coll_dt;
        private string _iadh_com;
        private DateTime _iadh_dt;
        private string _iadh_loc;
        private string _iadh_pc;
        private string _iadh_ref_no;
        private string _iadh_req_by;
        private DateTime _iadh_req_dt;
        private Int32 _iadh_seq;
        private Int32 _iadh_stus;
        private int _iadh_tp;
        #endregion

        public string Iadh_adj_no { get { return _iadh_adj_no; } set { _iadh_adj_no = value; } }
        public string Iadh_anal1 { get { return _iadh_anal1; } set { _iadh_anal1 = value; } }
        public string Iadh_anal2 { get { return _iadh_anal2; } set { _iadh_anal2 = value; } }
        public string Iadh_anal3 { get { return _iadh_anal3; } set { _iadh_anal3 = value; } }
        public string Iadh_anal4 { get { return _iadh_anal4; } set { _iadh_anal4 = value; } }
        public string Iadh_anal5 { get { return _iadh_anal5; } set { _iadh_anal5 = value; } }
        public string Iadh_app_by { get { return _iadh_app_by; } set { _iadh_app_by = value; } }
        public DateTime Iadh_app_dt { get { return _iadh_app_dt; } set { _iadh_app_dt = value; } }
        public string Iadh_coll_by { get { return _iadh_coll_by; } set { _iadh_coll_by = value; } }
        public DateTime Iadh_coll_dt { get { return _iadh_coll_dt; } set { _iadh_coll_dt = value; } }
        public string Iadh_com { get { return _iadh_com; } set { _iadh_com = value; } }
        public DateTime Iadh_dt { get { return _iadh_dt; } set { _iadh_dt = value; } }
        public string Iadh_loc { get { return _iadh_loc; } set { _iadh_loc = value; } }
        public string Iadh_pc { get { return _iadh_pc; } set { _iadh_pc = value; } }
        public string Iadh_ref_no { get { return _iadh_ref_no; } set { _iadh_ref_no = value; } }
        public string Iadh_req_by { get { return _iadh_req_by; } set { _iadh_req_by = value; } }
        public DateTime Iadh_req_dt { get { return _iadh_req_dt; } set { _iadh_req_dt = value; } }
        public Int32 Iadh_seq { get { return _iadh_seq; } set { _iadh_seq = value; } }
        public Int32 Iadh_stus { get { return _iadh_stus; } set { _iadh_stus = value; } }
        public int Iadh_tp { get { return _iadh_tp; } set { _iadh_tp = value; } }

        public static InventoryAdhocHeader Converter(DataRow row)
        {
            return new InventoryAdhocHeader
            {
                Iadh_adj_no = row["IADH_ADJ_NO"] == DBNull.Value ? string.Empty : row["IADH_ADJ_NO"].ToString(),
                Iadh_anal1 = row["IADH_ANAL1"] == DBNull.Value ? string.Empty : row["IADH_ANAL1"].ToString(),
                Iadh_anal2 = row["IADH_ANAL2"] == DBNull.Value ? string.Empty : row["IADH_ANAL2"].ToString(),
                Iadh_anal3 = row["IADH_ANAL3"] == DBNull.Value ? string.Empty : row["IADH_ANAL3"].ToString(),
                Iadh_anal4 = row["IADH_ANAL4"] == DBNull.Value ? string.Empty : row["IADH_ANAL4"].ToString(),
                Iadh_anal5 = row["IADH_ANAL5"] == DBNull.Value ? string.Empty : row["IADH_ANAL5"].ToString(),
                Iadh_app_by = row["IADH_APP_BY"] == DBNull.Value ? string.Empty : row["IADH_APP_BY"].ToString(),
                Iadh_app_dt = row["IADH_APP_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IADH_APP_DT"]),
                Iadh_coll_by = row["IADH_COLL_BY"] == DBNull.Value ? string.Empty : row["IADH_COLL_BY"].ToString(),
                Iadh_coll_dt = row["IADH_COLL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IADH_COLL_DT"]),
                Iadh_com = row["IADH_COM"] == DBNull.Value ? string.Empty : row["IADH_COM"].ToString(),
                Iadh_dt = row["IADH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IADH_DT"]),
                Iadh_loc = row["IADH_LOC"] == DBNull.Value ? string.Empty : row["IADH_LOC"].ToString(),
                Iadh_pc = row["IADH_PC"] == DBNull.Value ? string.Empty : row["IADH_PC"].ToString(),
                Iadh_ref_no = row["IADH_REF_NO"] == DBNull.Value ? string.Empty : row["IADH_REF_NO"].ToString(),
                Iadh_req_by = row["IADH_REQ_BY"] == DBNull.Value ? string.Empty : row["IADH_REQ_BY"].ToString(),
                Iadh_req_dt = row["IADH_REQ_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IADH_REQ_DT"]),
                Iadh_seq = row["IADH_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["IADH_SEQ"]),
                Iadh_stus = row["IADH_STUS"] == DBNull.Value ? 0 : Convert.ToInt32(row["IADH_STUS"]),
                Iadh_tp = row["IADH_TP"] == DBNull.Value ? 0 : Convert.ToInt16(row["IADH_TP"])

            };
        }

    }
}
