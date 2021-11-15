using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{

    public class IncentiveSchPersn
    {
        #region Private Members
        private string _incp_circ;
        private Int32 _incp_dt_line;
        private Int32 _incp_line;
        private string _incp_person;
        private string _incp_ref;
        private string _incp_desc;
        #endregion

        public string Incp_circ
        {
            get { return _incp_circ; }
            set { _incp_circ = value; }
        }
        public Int32 Incp_dt_line
        {
            get { return _incp_dt_line; }
            set { _incp_dt_line = value; }
        }
        public Int32 Incp_line
        {
            get { return _incp_line; }
            set { _incp_line = value; }
        }
        public string Incp_person
        {
            get { return _incp_person; }
            set { _incp_person = value; }
        }
        public string Incp_ref
        {
            get { return _incp_ref; }
            set { _incp_ref = value; }
        }
        public string Incp_desc
        {
            get { return _incp_desc; }
            set { _incp_desc = value; }
        }

        public static IncentiveSchPersn Converter(DataRow row)
        {
            return new IncentiveSchPersn
            {
                Incp_circ = row["INCP_CIRC"] == DBNull.Value ? string.Empty : row["INCP_CIRC"].ToString(),
                Incp_dt_line = row["INCP_DT_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCP_DT_LINE"]),
                Incp_line = row["INCP_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["INCP_LINE"]),
                Incp_person = row["INCP_PERSON"] == DBNull.Value ? string.Empty : row["INCP_PERSON"].ToString(),
                Incp_ref = row["INCP_REF"] == DBNull.Value ? string.Empty : row["INCP_REF"].ToString()
            };
        }

    }
}
