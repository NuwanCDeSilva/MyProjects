using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterLocationPriorityHierarchy
    {
        /// <summary>
        /// Written By Prabhath on 09/04/2013
        /// Table: MST_LOC_INFO (in EMS)
        /// Duplicate BO object (PriorityHierarchy created by Miginda)
        /// </summary>

        #region Private Members
        private Boolean _mli_act;
        private string _mli_cd;
        private string _mli_com_cd;
        private string _mli_loc_cd;
        private string _mli_tp;
        private string _mli_val;
        private string _description;
        #endregion

        public Boolean Mli_act { get { return _mli_act; } set { _mli_act = value; } }
        public string Mli_cd { get { return _mli_cd; } set { _mli_cd = value; } }
        public string Mli_com_cd { get { return _mli_com_cd; } set { _mli_com_cd = value; } }
        public string Mli_loc_cd { get { return _mli_loc_cd; } set { _mli_loc_cd = value; } }
        public string Mli_tp { get { return _mli_tp; } set { _mli_tp = value; } }
        public string Mli_val { get { return _mli_val; } set { _mli_val = value; } }
        public string Description { get { return _description; } set { _description = value; } }

        public static MasterLocationPriorityHierarchy Converter(DataRow row)
        {
            return new MasterLocationPriorityHierarchy
            {
                Mli_act = row["MLI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MLI_ACT"]),
                Mli_cd = row["MLI_CD"] == DBNull.Value ? string.Empty : row["MLI_CD"].ToString(),
                Mli_com_cd = row["MLI_COM_CD"] == DBNull.Value ? string.Empty : row["MLI_COM_CD"].ToString(),
                Mli_loc_cd = row["MLI_LOC_CD"] == DBNull.Value ? string.Empty : row["MLI_LOC_CD"].ToString(),
                Mli_tp = row["MLI_TP"] == DBNull.Value ? string.Empty : row["MLI_TP"].ToString(),
                Mli_val = row["MLI_VAL"] == DBNull.Value ? string.Empty : row["MLI_VAL"].ToString()

            };
        }

        public static MasterLocationPriorityHierarchy ConvertWithDescription(DataRow row)
        {
            return new MasterLocationPriorityHierarchy
            {
                Mli_act = row["MLI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MLI_ACT"]),
                Mli_cd = row["MLI_CD"] == DBNull.Value ? string.Empty : row["MLI_CD"].ToString(),
                Mli_com_cd = row["MLI_COM_CD"] == DBNull.Value ? string.Empty : row["MLI_COM_CD"].ToString(),
                Mli_loc_cd = row["MLI_LOC_CD"] == DBNull.Value ? string.Empty : row["MLI_LOC_CD"].ToString(),
                Mli_tp = row["MLI_TP"] == DBNull.Value ? string.Empty : row["MLI_TP"].ToString(),
                Mli_val = row["MLI_VAL"] == DBNull.Value ? string.Empty : row["MLI_VAL"].ToString(),
                Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString()

            };
        }
    }
}

