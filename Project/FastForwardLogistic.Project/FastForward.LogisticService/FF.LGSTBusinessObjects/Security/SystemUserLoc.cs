using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Security
{
    public class SystemUserLoc : MasterLocation
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

        MasterLocation _masterLoc = null;
        public MasterLocation MasterLoc
        {
            get { return _masterLoc; }
            set { _masterLoc = value; }
        }


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
