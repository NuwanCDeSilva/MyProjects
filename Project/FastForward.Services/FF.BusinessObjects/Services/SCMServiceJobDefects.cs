using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SCMServiceJobDefects
    {
        #region Private Members
        private string _srd_company;
        private Int32 _srd_def_line;
        private string _srd_job_def_remarks;
        private string _srd_job_def_type;
        private Int32 _srd_job_line;
        private string _srd_job_no;
        #endregion

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
        public Int32 Srd_job_line
        {
            get { return _srd_job_line; }
            set { _srd_job_line = value; }
        }
        public string Srd_job_no
        {
            get { return _srd_job_no; }
            set { _srd_job_no = value; }
        }

        public static SCMServiceJobDefects Converter(DataRow row)
        {
            return new SCMServiceJobDefects
            {
                Srd_company = row["SRD_COMPANY"] == DBNull.Value ? string.Empty : row["SRD_COMPANY"].ToString(),
                Srd_def_line = row["SRD_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_DEF_LINE"]),
                Srd_job_def_remarks = row["SRD_JOB_DEF_REMARKS"] == DBNull.Value ? string.Empty : row["SRD_JOB_DEF_REMARKS"].ToString(),
                Srd_job_def_type = row["SRD_JOB_DEF_TYPE"] == DBNull.Value ? string.Empty : row["SRD_JOB_DEF_TYPE"].ToString(),
                Srd_job_line = row["SRD_JOB_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRD_JOB_LINE"]),
                Srd_job_no = row["SRD_JOB_NO"] == DBNull.Value ? string.Empty : row["SRD_JOB_NO"].ToString()

            };
        }

    }
}
