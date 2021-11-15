using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterCompanyItemStatus
    {
        //
        // Function             - Company Item Status
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - MST_ITM_COMSTUS
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private Boolean _mic_act;
        private string _mic_cd;
        private string _mic_com_cd;
        private string _mic_cre_by;
        private DateTime _mic_cre_dt;
        private Boolean _mic_isrvt;
        private string _mic_mod_by;
        private DateTime _mic_mod_dt;
        private string _mic_session_id;
        //Status Description assign from the item status master table
        private string _mis_description;
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        #region Definition - Properties
        public Boolean Mic_act { get { return _mic_act; } set { _mic_act = value; } }
        public string Mic_cd { get { return _mic_cd; } set { _mic_cd = value; } }
        public string Mic_com_cd { get { return _mic_com_cd; } set { _mic_com_cd = value; } }
        public string Mic_cre_by { get { return _mic_cre_by; } set { _mic_cre_by = value; } }
        public DateTime Mic_cre_dt { get { return _mic_cre_dt; } set { _mic_cre_dt = value; } }
        public Boolean Mic_isrvt { get { return _mic_isrvt; } set { _mic_isrvt = value; } }
        public string Mic_mod_by { get { return _mic_mod_by; } set { _mic_mod_by = value; } }
        public DateTime Mic_mod_dt { get { return _mic_mod_dt; } set { _mic_mod_dt = value; } }
        public string Mic_session_id { get { return _mic_session_id; } set { _mic_session_id = value; } }
        public string Mis_description { get { return _mis_description; } set { _mis_description = value; } }
        #endregion
        
        #region Converter
        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Item Master</returns>
        public static MasterCompanyItemStatus ConvertTotal(DataRow row)
        {
            return new MasterCompanyItemStatus
            {
                Mic_act = row["MIC_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MIC_ACT"]),
                Mic_cd = row["MIC_CD"] == DBNull.Value ? string.Empty : row["MIC_CD"].ToString(),
                Mic_com_cd = row["MIC_COM_CD"] == DBNull.Value ? string.Empty : row["MIC_COM_CD"].ToString(),
                Mic_cre_by = row["MIC_CRE_BY"] == DBNull.Value ? string.Empty : row["MIC_CRE_BY"].ToString(),
                Mic_cre_dt = row["MIC_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIC_CRE_DT"]),
                Mic_isrvt = row["MIC_ISRVT"] == DBNull.Value ? false : Convert.ToBoolean(row["MIC_ISRVT"]),
                Mic_mod_by = row["MIC_MOD_BY"] == DBNull.Value ? string.Empty : row["MIC_MOD_BY"].ToString(),
                Mic_mod_dt = row["MIC_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIC_MOD_DT"]),
                Mic_session_id = row["MIC_SESSION_ID"] == DBNull.Value ? string.Empty : row["MIC_SESSION_ID"].ToString()

            };
        }

        #endregion
    }
}

