using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SystemUserLoc:MasterLocation
    {
        #region private members and  public property definition

        private string _SEL_USR_ID = string.Empty;
        public string SEL_USR_ID
        {
            get { return _SEL_USR_ID; }
            set { _SEL_USR_ID = value; }
        }

        private string _SEL_COM_CD = string.Empty;
        public string SEL_COM_CD
        {
            get { return _SEL_COM_CD; }
            set { _SEL_COM_CD = value; }
        }

        private string _SEL_LOC_CD = string.Empty;
        public string SEL_LOC_CD
        {
            get { return _SEL_LOC_CD; }
            set { _SEL_LOC_CD = value; }
        }

        private int _SEL_DEF_LOCCD = 0;
        public int SEL_DEF_LOCCD
        {
            get { return _SEL_DEF_LOCCD; }
            set { _SEL_DEF_LOCCD = value; }
        }
        //kapila 18/7/2017
        private string _SEL_CRE_BY = string.Empty;
        public string SEL_CRE_BY
        {
            get { return _SEL_CRE_BY; }
            set { _SEL_CRE_BY = value; }
        }
        private string _SEL_MOD_BY = string.Empty;
        public string SEL_MOD_BY
        {
            get { return _SEL_MOD_BY; }
            set { _SEL_MOD_BY = value; }
        }
        private DateTime _SEL_CRE_DT;
        public DateTime SEL_CRE_DT
        {
            get { return _SEL_CRE_DT; }
            set { _SEL_CRE_DT = value; }
        }
        private DateTime _SEL_MOD_DT;
        public DateTime SEL_MOD_DT
        {
            get { return _SEL_MOD_DT; }
            set { _SEL_MOD_DT = value; }
        }
        MasterLocation _masterLoc = null;
        public MasterLocation MasterLoc
        {
            get { return _masterLoc; }
            set { _masterLoc = value; }
        }

        //private string _LOC_DESN = string.Empty;
        //public string LOC_DESN
        //{
        //    get { return _LOC_DESN; }
        //    set { _LOC_DESN = value; }
        //}

        #endregion

        public static SystemUserLoc converter(DataRow row)
        {
            return new SystemUserLoc
            {
                SEL_COM_CD = ((row["SEL_COM_CD"] == DBNull.Value) ? string.Empty : row["SEL_COM_CD"].ToString()),
                SEL_LOC_CD = ((row["SEL_LOC_CD"] == DBNull.Value) ? string.Empty : row["SEL_LOC_CD"].ToString()),
                SEL_USR_ID = ((row["SEL_USR_ID"] == DBNull.Value) ? string.Empty : row["SEL_USR_ID"].ToString()),
                //LOC_DESN = ((row["ml_loc_desc"] == DBNull.Value) ? string.Empty : row["ml_loc_desc"].ToString()),
                SEL_DEF_LOCCD = ((row["SEL_DEF_LOCCD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SEL_DEF_LOCCD"].ToString()))
            };
        }

        public static SystemUserLoc convertWithDescription(DataRow row)
        {
            return new SystemUserLoc
            {
                SEL_COM_CD = ((row["SEL_COM_CD"] == DBNull.Value) ? string.Empty : row["SEL_COM_CD"].ToString()),
                SEL_LOC_CD = ((row["SEL_LOC_CD"] == DBNull.Value) ? string.Empty : row["SEL_LOC_CD"].ToString()),
                SEL_USR_ID = ((row["SEL_USR_ID"] == DBNull.Value) ? string.Empty : row["SEL_USR_ID"].ToString()),
                Ml_loc_desc = ((row["Ml_loc_desc"] == DBNull.Value) ? string.Empty : row["Ml_loc_desc"].ToString()),
                SEL_DEF_LOCCD = ((row["SEL_DEF_LOCCD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["SEL_DEF_LOCCD"].ToString()))
            };
        }
    }
}
