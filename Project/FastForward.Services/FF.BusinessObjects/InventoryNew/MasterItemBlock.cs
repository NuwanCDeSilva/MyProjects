using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterItemBlock
    {
        //Written By Prabhath on 11/09/2012,  modified by shani on 16-07-2013
        //Table : MST_ITM_BLOCK
        

        //#region Private Members
        //private string _mib_com;
        //private string _mib_cr_by;
        //private DateTime _mib_cr_dt;
        //private string _mib_itm;
        //private string _mib_loc;
        //private string _mib_mod_by;
        //private DateTime _mib_mod_dt;
        //private string _mib_pc;
        //private Boolean _mib_pr_tp;
        //private Boolean _mib_stus;
        //#endregion

        //public string Mib_com { get { return _mib_com; } set { _mib_com = value; } }
        //public string Mib_cr_by { get { return _mib_cr_by; } set { _mib_cr_by = value; } }
        //public DateTime Mib_cr_dt { get { return _mib_cr_dt; } set { _mib_cr_dt = value; } }
        //public string Mib_itm { get { return _mib_itm; } set { _mib_itm = value; } }
        //public string Mib_loc { get { return _mib_loc; } set { _mib_loc = value; } }
        //public string Mib_mod_by { get { return _mib_mod_by; } set { _mib_mod_by = value; } }
        //public DateTime Mib_mod_dt { get { return _mib_mod_dt; } set { _mib_mod_dt = value; } }
        //public string Mib_pc { get { return _mib_pc; } set { _mib_pc = value; } }
        //public Boolean Mib_pr_tp { get { return _mib_pr_tp; } set { _mib_pr_tp = value; } }
        //public Boolean Mib_stus { get { return _mib_stus; } set { _mib_stus = value; } }

        //public static MasterItemBlock Converter(DataRow row)
        //{
        //    return new MasterItemBlock
        //    {
        //        Mib_com = row["MIB_COM"] == DBNull.Value ? string.Empty : row["MIB_COM"].ToString(),
        //        Mib_cr_by = row["MIB_CR_BY"] == DBNull.Value ? string.Empty : row["MIB_CR_BY"].ToString(),
        //        Mib_cr_dt = row["MIB_CR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIB_CR_DT"]),
        //        Mib_itm = row["MIB_ITM"] == DBNull.Value ? string.Empty : row["MIB_ITM"].ToString(),
        //        Mib_loc = row["MIB_LOC"] == DBNull.Value ? string.Empty : row["MIB_LOC"].ToString(),
        //        Mib_mod_by = row["MIB_MOD_BY"] == DBNull.Value ? string.Empty : row["MIB_MOD_BY"].ToString(),
        //        Mib_mod_dt = row["MIB_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIB_MOD_DT"]),
        //        Mib_pc = row["MIB_PC"] == DBNull.Value ? string.Empty : row["MIB_PC"].ToString(),
        //        Mib_pr_tp = row["MIB_PR_TP"] == DBNull.Value ? false : Convert.ToBoolean(row["MIB_PR_TP"]),
        //        Mib_stus = row["MIB_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["MIB_STUS"])

        //    };
        //}

        #region Private Members
        private string _mbi_category_tp;
        private string _mib_category;
        private string _mib_com;
        private string _mib_cr_by;
        private DateTime _mib_cr_dt;
        private string _mib_itm;
        private string _mib_mod_by;
        private DateTime _mib_mod_dt;
        private Int32 _mib_pr_tp;
        private Boolean _mib_stus;
        private string _mib_brnd;
        private string _mib_cat1;
        private string _mib_cat2;
        #endregion

        public string Mbi_category_tp { get { return _mbi_category_tp; } set { _mbi_category_tp = value; } }
        public string Mib_category { get { return _mib_category; } set { _mib_category = value; } }
        public string Mib_com { get { return _mib_com; } set { _mib_com = value; } }
        public string Mib_cr_by { get { return _mib_cr_by; } set { _mib_cr_by = value; } }
        public DateTime Mib_cr_dt { get { return _mib_cr_dt; } set { _mib_cr_dt = value; } }
        public string Mib_itm { get { return _mib_itm; } set { _mib_itm = value; } }
        public string Mib_mod_by { get { return _mib_mod_by; } set { _mib_mod_by = value; } }
        public DateTime Mib_mod_dt { get { return _mib_mod_dt; } set { _mib_mod_dt = value; } }
        public Int32 Mib_pr_tp { get { return _mib_pr_tp; } set { _mib_pr_tp = value; } }
        public Boolean Mib_stus { get { return _mib_stus; } set { _mib_stus = value; } }
        public string Mib_brnd { get { return _mib_brnd; } set { _mib_brnd = value; } }
        public string Mib_cat1 { get { return _mib_cat1; } set { _mib_cat1 = value; } }
        public string Mib_cat2 { get { return _mib_cat2; } set { _mib_cat2 = value; } }

        public static MasterItemBlock Converter(DataRow row)
        {
            return new MasterItemBlock
            {
                Mbi_category_tp = row["MBI_CATEGORY_TP"] == DBNull.Value ? string.Empty : row["MBI_CATEGORY_TP"].ToString(),
                Mib_category = row["MIB_CATEGORY"] == DBNull.Value ? string.Empty : row["MIB_CATEGORY"].ToString(),
                Mib_com = row["MIB_COM"] == DBNull.Value ? string.Empty : row["MIB_COM"].ToString(),
                Mib_cr_by = row["MIB_CR_BY"] == DBNull.Value ? string.Empty : row["MIB_CR_BY"].ToString(),
                Mib_cr_dt = row["MIB_CR_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIB_CR_DT"]),
                Mib_itm = row["MIB_ITM"] == DBNull.Value ? string.Empty : row["MIB_ITM"].ToString(),
                Mib_mod_by = row["MIB_MOD_BY"] == DBNull.Value ? string.Empty : row["MIB_MOD_BY"].ToString(),
                Mib_mod_dt = row["MIB_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MIB_MOD_DT"]),
                Mib_pr_tp = row["MIB_PR_TP"] == DBNull.Value ? -1 : Convert.ToInt32(row["MIB_PR_TP"]),
                Mib_stus = row["MIB_STUS"] == DBNull.Value ? false : Convert.ToBoolean(row["MIB_STUS"]),
                Mib_brnd = row["Mib_brnd"] == DBNull.Value ? string.Empty : row["Mib_brnd"].ToString(),
                Mib_cat1 = row["Mib_cat1"] == DBNull.Value ? string.Empty : row["Mib_cat1"].ToString(),
                Mib_cat2 = row["Mib_cat2"] == DBNull.Value ? string.Empty : row["Mib_cat2"].ToString(),

            };
        }

    }
}

