using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class RCCLocations
    {
        #region Private Members
        private string _ragl_agent;
        private string _ragl_com;
        private string _ragl_loc;
        private string _ragl_mod_by;
        private DateTime _ragl_mod_when;
        #endregion

        public string Ragl_agent
        {
            get { return _ragl_agent; }
            set { _ragl_agent = value; }
        }
        public string Ragl_com
        {
            get { return _ragl_com; }
            set { _ragl_com = value; }
        }
        public string Ragl_loc
        {
            get { return _ragl_loc; }
            set { _ragl_loc = value; }
        }
        public string Ragl_mod_by
        {
            get { return _ragl_mod_by; }
            set { _ragl_mod_by = value; }
        }
        public DateTime Ragl_mod_when
        {
            get { return _ragl_mod_when; }
            set { _ragl_mod_when = value; }
        }

        #region converter

        public static RCCLocations Converter(DataRow row)
        {
            return new RCCLocations
            {
                Ragl_agent = row["RAGL_AGENT"] == DBNull.Value ? string.Empty : row["RAGL_AGENT"].ToString(),
                Ragl_com = row["RAGL_COM"] == DBNull.Value ? string.Empty : row["RAGL_COM"].ToString(),
                Ragl_loc = row["RAGL_LOC"] == DBNull.Value ? string.Empty : row["RAGL_LOC"].ToString(),
                Ragl_mod_by = row["RAGL_MOD_BY"] == DBNull.Value ? string.Empty : row["RAGL_MOD_BY"].ToString(),
                Ragl_mod_when = row["RAGL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["RAGL_MOD_WHEN"])
            };
        }
        #endregion
    }
}
