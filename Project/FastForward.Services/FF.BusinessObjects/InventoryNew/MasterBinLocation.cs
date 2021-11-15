using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterBinLocation
    {
        //
        // Function             - Inventory Bin Location Referance
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - INR_BIN_LOC
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members
        private Boolean _ibl_act;
        private string _ibl_asl_cd;
        private string _ibl_bin_cd;
        private string _ibl_bin_des;
        private string _ibl_com_cd;
        private string _ibl_cre_by;
        private DateTime _ibl_cre_when;
        private Boolean _ibl_is_bin_featured;
        private string _ibl_loc_cd;
        private string _ibl_lvl_cd;
        private string _ibl_mod_by;
        private DateTime _ibl_mod_when;
        private string _ibl_row_cd;
        private string _ibl_session_id;
        private string _ibl_zone_cd;
        private Boolean _ibl_is_def; //add chamal 25/05/2012


        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties - Referance
        public Boolean Ibl_act { get { return _ibl_act; } set { _ibl_act = value; } }
        public string Ibl_asl_cd { get { return _ibl_asl_cd; } set { _ibl_asl_cd = value; } }
        public string Ibl_bin_cd { get { return _ibl_bin_cd; } set { _ibl_bin_cd = value; } }
        public string Ibl_bin_des { get { return _ibl_bin_des; } set { _ibl_bin_des = value; } }
        public string Ibl_com_cd { get { return _ibl_com_cd; } set { _ibl_com_cd = value; } }
        public string Ibl_cre_by { get { return _ibl_cre_by; } set { _ibl_cre_by = value; } }
        public DateTime Ibl_cre_when { get { return _ibl_cre_when; } set { _ibl_cre_when = value; } }
        public Boolean Ibl_is_bin_featured { get { return _ibl_is_bin_featured; } set { _ibl_is_bin_featured = value; } }
        public string Ibl_loc_cd { get { return _ibl_loc_cd; } set { _ibl_loc_cd = value; } }
        public string Ibl_lvl_cd { get { return _ibl_lvl_cd; } set { _ibl_lvl_cd = value; } }
        public string Ibl_mod_by { get { return _ibl_mod_by; } set { _ibl_mod_by = value; } }
        public DateTime Ibl_mod_when { get { return _ibl_mod_when; } set { _ibl_mod_when = value; } }
        public string Ibl_row_cd { get { return _ibl_row_cd; } set { _ibl_row_cd = value; } }
        public string Ibl_session_id { get { return _ibl_session_id; } set { _ibl_session_id = value; } }
        public string Ibl_zone_cd { get { return _ibl_zone_cd; } set { _ibl_zone_cd = value; } }
        public Boolean Ibl_is_def {get { return _ibl_is_def; } set { _ibl_is_def = value; }}
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Inventory Bin Location Referance</returns>
        #region Converter - Transaction
        public static MasterBinLocation ConvertTotal(DataRow row)
        {
            return new MasterBinLocation
            {
                Ibl_act = row["IBL_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["IBL_ACT"]),
                Ibl_asl_cd = row["IBL_ASL_CD"] == DBNull.Value ? string.Empty : row["IBL_ASL_CD"].ToString(),
                Ibl_bin_cd = row["IBL_BIN_CD"] == DBNull.Value ? string.Empty : row["IBL_BIN_CD"].ToString(),
                Ibl_bin_des = row["IBL_BIN_DES"] == DBNull.Value ? string.Empty : row["IBL_BIN_DES"].ToString(),
                Ibl_com_cd = row["IBL_COM_CD"] == DBNull.Value ? string.Empty : row["IBL_COM_CD"].ToString(),
                Ibl_cre_by = row["IBL_CRE_BY"] == DBNull.Value ? string.Empty : row["IBL_CRE_BY"].ToString(),
                Ibl_cre_when = row["IBL_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBL_CRE_WHEN"]),
                Ibl_is_bin_featured = row["IBL_IS_BIN_FEATURED"] == DBNull.Value ? false : Convert.ToBoolean(row["IBL_IS_BIN_FEATURED"]),
                Ibl_loc_cd = row["IBL_LOC_CD"] == DBNull.Value ? string.Empty : row["IBL_LOC_CD"].ToString(),
                Ibl_lvl_cd = row["IBL_LVL_CD"] == DBNull.Value ? string.Empty : row["IBL_LVL_CD"].ToString(),
                Ibl_mod_by = row["IBL_MOD_BY"] == DBNull.Value ? string.Empty : row["IBL_MOD_BY"].ToString(),
                Ibl_mod_when = row["IBL_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IBL_MOD_WHEN"]),
                Ibl_row_cd = row["IBL_ROW_CD"] == DBNull.Value ? string.Empty : row["IBL_ROW_CD"].ToString(),
                Ibl_session_id = row["IBL_SESSION_ID"] == DBNull.Value ? string.Empty : row["IBL_SESSION_ID"].ToString(),
                Ibl_zone_cd = row["IBL_ZONE_CD"] == DBNull.Value ? string.Empty : row["IBL_ZONE_CD"].ToString(),
                Ibl_is_def = row["IBL_IS_DEF"] == DBNull.Value ? false : Convert.ToBoolean(row["IBL_IS_DEF"])

            };
        }


        #endregion

    }
}
