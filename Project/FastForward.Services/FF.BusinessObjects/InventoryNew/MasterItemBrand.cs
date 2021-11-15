using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//by darshana
namespace FF.BusinessObjects
{
    [Serializable]
   public class MasterItemBrand
    {

        #region Private Members
        private Boolean _mb_act;
        private string _mb_cd;
        private string _mb_cre_by;
        private DateTime _mb_cre_dt;
        private string _mb_desc;
        private string _mb_mod_by;
        private DateTime _mb_mod_dt;
        #endregion

        public Boolean Mb_act
        {
            get { return _mb_act; }
            set { _mb_act = value; }
        }
        public string Mb_cd
        {
            get { return _mb_cd; }
            set { _mb_cd = value; }
        }
        public string Mb_cre_by
        {
            get { return _mb_cre_by; }
            set { _mb_cre_by = value; }
        }
        public DateTime Mb_cre_dt
        {
            get { return _mb_cre_dt; }
            set { _mb_cre_dt = value; }
        }
        public string Mb_desc
        {
            get { return _mb_desc; }
            set { _mb_desc = value; }
        }
        public string Mb_mod_by
        {
            get { return _mb_mod_by; }
            set { _mb_mod_by = value; }
        }
        public DateTime Mb_mod_dt
        {
            get { return _mb_mod_dt; }
            set { _mb_mod_dt = value; }
        }
        //Dulaj 2018/Nov/27
        public string Mb_contribu { get; set; }

        public Boolean Mb_isupdate { get; set; }
        public static MasterItemBrand Converter(DataRow row)
        {
            return new MasterItemBrand
            {
                Mb_act = row["MB_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MB_ACT"]),
                Mb_cd = row["MB_CD"] == DBNull.Value ? string.Empty : row["MB_CD"].ToString(),
                Mb_cre_by = row["MB_CRE_BY"] == DBNull.Value ? string.Empty : row["MB_CRE_BY"].ToString(),
                Mb_cre_dt = row["MB_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MB_CRE_DT"]),
                Mb_desc = row["MB_DESC"] == DBNull.Value ? string.Empty : row["MB_DESC"].ToString(),
                Mb_mod_by = row["MB_MOD_BY"] == DBNull.Value ? string.Empty : row["MB_MOD_BY"].ToString(),
                Mb_mod_dt = row["MB_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MB_MOD_DT"]),
                Mb_contribu = row["MB_CONTRIBU"] == DBNull.Value ? string.Empty : row["MB_CONTRIBU"].ToString(),
                Mb_isupdate=false
            };
        }

        public static MasterItemBrand Converter2(DataRow row)
        {
            return new MasterItemBrand
            {
                Mb_act = row["MB_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MB_ACT"]),
                Mb_cd = row["MB_CD"] == DBNull.Value ? string.Empty : row["MB_CD"].ToString(),
                Mb_cre_by = row["MB_CRE_BY"] == DBNull.Value ? string.Empty : row["MB_CRE_BY"].ToString(),
                Mb_cre_dt = row["MB_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MB_CRE_DT"]),
                Mb_desc = row["MB_DESC"] == DBNull.Value ? string.Empty : row["MB_DESC"].ToString(),
                Mb_mod_by = row["MB_MOD_BY"] == DBNull.Value ? string.Empty : row["MB_MOD_BY"].ToString(),
                Mb_mod_dt = row["MB_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MB_MOD_DT"]),
                Mb_contribu = row["MB_CONTRIBU"] == DBNull.Value ? string.Empty : row["MB_CONTRIBU"].ToString(),
                Mb_isupdate = false
                };
        }



    }
}
