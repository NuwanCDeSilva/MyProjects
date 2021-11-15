using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class SCMServiceRequestDefect
    {
        #region Private Members
        private string _srd_act_def_remarks;
        private string _srd_act_def_type;
        private string _srd_company;
        private Int32 _srd_def_line;
        private string _srd_job_def_remarks;
        private string _srd_job_def_type;
        private string _srd_req_def_remarks;
        private string _srd_req_def_type;
        private Int32 _srd_req_line;
        private string _srd_req_no;
        #endregion

        public string Srd_act_def_remarks
        {
            get { return _srd_act_def_remarks; }
            set { _srd_act_def_remarks = value; }
        }
        public string Srd_act_def_type
        {
            get { return _srd_act_def_type; }
            set { _srd_act_def_type = value; }
        }
        public string Srd_company
        {
            get { return _srd_company; }
            set { _srd_company = value; }
        }
        public Int32 Srd_def_line
        {
            get { return _srd_def_line; }
            set { _srd_def_line = value; }
        }
        public string Srd_job_def_remarks
        {
            get { return _srd_job_def_remarks; }
            set { _srd_job_def_remarks = value; }
        }
        public string Srd_job_def_type
        {
            get { return _srd_job_def_type; }
            set { _srd_job_def_type = value; }
        }
        public string Srd_req_def_remarks
        {
            get { return _srd_req_def_remarks; }
            set { _srd_req_def_remarks = value; }
        }
        public string Srd_req_def_type
        {
            get { return _srd_req_def_type; }
            set { _srd_req_def_type = value; }
        }
        public Int32 Srd_req_line
        {
            get { return _srd_req_line; }
            set { _srd_req_line = value; }
        }
        public string Srd_req_no
        {
            get { return _srd_req_no; }
            set { _srd_req_no = value; }
        }

        public static SCMServiceRequestDefect Converter(DataRow row)
        {
            return new SCMServiceRequestDefect
            {
                Srd_act_def_remarks = row["SRD_ACT_DEF_REMARKS"] == DBNull.Value ? string.Empty : row["SRD_ACT_DEF_REMARKS"].ToString(),
                Srd_act_def_type = row["SRD_ACT_DEF_TYPE"] == DBNull.Value ? string.Empty : row["SRD_ACT_DEF_TYPE"].ToString(),
                Srd_company = row["SRD_COMPANY"] == DBNull.Value ? string.Empty : row["SRD_COMPANY"].ToString(),
                Srd_def_line = row["SRD_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_DEF_LINE"]),
                Srd_job_def_remarks = row["SRD_JOB_DEF_REMARKS"] == DBNull.Value ? string.Empty : row["SRD_JOB_DEF_REMARKS"].ToString(),
                Srd_job_def_type = row["SRD_JOB_DEF_TYPE"] == DBNull.Value ? string.Empty : row["SRD_JOB_DEF_TYPE"].ToString(),
                Srd_req_def_remarks = row["SRD_REQ_DEF_REMARKS"] == DBNull.Value ? string.Empty : row["SRD_REQ_DEF_REMARKS"].ToString(),
                Srd_req_def_type = row["SRD_REQ_DEF_TYPE"] == DBNull.Value ? string.Empty : row["SRD_REQ_DEF_TYPE"].ToString(),
                Srd_req_line = row["SRD_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_REQ_LINE"]),
                Srd_req_no = row["SRD_REQ_NO"] == DBNull.Value ? string.Empty : row["SRD_REQ_NO"].ToString()

            };
        }

    }
}
