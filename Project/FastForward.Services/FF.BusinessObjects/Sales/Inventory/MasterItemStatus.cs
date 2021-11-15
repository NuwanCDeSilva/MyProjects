using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterItemStatus
    {
        //
        // Function             - Item Status
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - MST_ITM_STUS
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private Boolean _mis_act;
        private string _mis_cd;
        private string _mis_cre_by;
        private DateTime _mis_cre_dt;
        private string _mis_desc;
        private string _mis_mod_by;
        private DateTime _mis_mod_dt;
        private string _mis_session_id;
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties
        public Boolean Mis_act { get { return _mis_act; } set { _mis_act = value; } }
        public string Mis_cd { get { return _mis_cd; } set { _mis_cd = value; } }
        public string Mis_cre_by { get { return _mis_cre_by; } set { _mis_cre_by = value; } }
        public DateTime Mis_cre_dt { get { return _mis_cre_dt; } set { _mis_cre_dt = value; } }
        public string Mis_desc { get { return _mis_desc; } set { _mis_desc = value; } }
        public string Mis_mod_by { get { return _mis_mod_by; } set { _mis_mod_by = value; } }
        public DateTime Mis_mod_dt { get { return _mis_mod_dt; } set { _mis_mod_dt = value; } }
        public string Mis_session_id { get { return _mis_session_id; } set { _mis_session_id = value; } }
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Item Master</returns>
        #region Converter
        public static MasterItemStatus ConvertTotal(DataRow row)
        {
            return new MasterItemStatus
            {
                Mis_act = row["MIS_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MIS_ACT"]),
                Mis_cd = row["MIS_CD"] == DBNull.Value ? string.Empty : row["MIS_CD"].ToString(),
                Mis_cre_by = row["MIS_CRE_BY"] == DBNull.Value ? string.Empty : row["MIS_CRE_BY"].ToString(),
                Mis_cre_dt = row["MIS_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIS_CRE_DT"]),
                Mis_desc = row["MIS_DESC"] == DBNull.Value ? string.Empty : row["MIS_DESC"].ToString(),
                Mis_mod_by = row["MIS_MOD_BY"] == DBNull.Value ? string.Empty : row["MIS_MOD_BY"].ToString(),
                Mis_mod_dt = row["MIS_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIS_MOD_DT"]),
                Mis_session_id = row["MIS_SESSION_ID"] == DBNull.Value ? string.Empty : row["MIS_SESSION_ID"].ToString()

            };
        }
        #endregion
    }
}

