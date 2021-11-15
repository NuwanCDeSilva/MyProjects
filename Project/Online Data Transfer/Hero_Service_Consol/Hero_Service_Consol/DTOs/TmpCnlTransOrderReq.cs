using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Hero_Service_Consol.DTOs
{
    class TmpCnlTransOrderReq
    {
        public string ComCode { get; set; }
        public string LocCode { get; set; }
        public string ReqCode { get; set; }
        public string ReqType { get; set; }
        public string ReqStatus { get; set; }
        public DateTime ReqDate { get; set; }
        public string IssueFrm { get; set; }

        public TmpCnlTransOrderReq(DataRow _row)
        {
            ComCode = _row["itr_com"] == DBNull.Value ? string.Empty : _row["itr_com"].ToString();
            LocCode = _row["itr_loc"] == DBNull.Value ? string.Empty : _row["itr_loc"].ToString();
            ReqCode = _row["itr_req_no"] == DBNull.Value ? string.Empty : _row["itr_req_no"].ToString();
            ReqType = _row["itr_tp"] == DBNull.Value ? string.Empty : _row["itr_tp"].ToString();
            ReqStatus = _row["itr_stus"] == DBNull.Value ? string.Empty : _row["itr_stus"].ToString();
            ReqDate = _row["itr_dt"] == DBNull.Value ? DateTime.Today : Convert.ToDateTime(_row["itr_dt"]);
            IssueFrm = _row["itr_issue_from"] == DBNull.Value ? string.Empty : _row["itr_issue_from"].ToString();
        }
    }
}
