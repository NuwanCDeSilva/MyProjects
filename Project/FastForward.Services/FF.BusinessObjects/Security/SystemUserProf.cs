using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class SystemUserProf
    {
        #region private members and public property definition
        private string _sup_com_cd;
        public string Sup_com_cd
        {
            get { return _sup_com_cd; }
            set { _sup_com_cd = value; }
        }

        private Boolean _sup_def_pccd;
        public Boolean Sup_def_pccd
        {
            get { return _sup_def_pccd; }
            set { _sup_def_pccd = value; }
        }

        private string _sup_pc_cd;
        public string Sup_pc_cd
        {
            get { return _sup_pc_cd; }
            set { _sup_pc_cd = value; }
        }

        private string _sup_usr_id;
        public string Sup_usr_id
        {
            get { return _sup_usr_id; }
            set { _sup_usr_id = value; }
        }
        //kapila 18/7/2017
        private string _SUP_CRE_BY;
        public string SUP_CRE_BY
        {
            get { return _SUP_CRE_BY; }
            set { _SUP_CRE_BY = value; }
        }
        private string _SUP_MOD_BY;
        public string SUP_MOD_BY
        {
            get { return _SUP_MOD_BY; }
            set { _SUP_MOD_BY = value; }
        }
        private DateTime _SUP_CRE_DT;
        public DateTime SUP_CRE_DT
        {
            get { return _SUP_CRE_DT; }
            set { _SUP_CRE_DT = value; }
        }
        private DateTime _SUP_MOD_DT;
        public DateTime SUP_MOD_DT
        {
            get { return _SUP_MOD_DT; }
            set { _SUP_MOD_DT = value; }
        }
        MasterProfitCenter _masterPC = null;
        public MasterProfitCenter MasterPC
        {
            get { return _masterPC; }
            set { _masterPC = value; }
        }
        #endregion

        //Chamal 20-03-2012
        public static SystemUserProf Converter(DataRow row)
        {
            return new SystemUserProf
            {
                Sup_com_cd = ((row["SUP_COM_CD"] == DBNull.Value) ? string.Empty : row["SUP_COM_CD"].ToString()),
                Sup_def_pccd = row["Sup_def_pccd"] == DBNull.Value ? false : Convert.ToBoolean(row["Sup_def_pccd"]),
                Sup_pc_cd = ((row["SUP_PC_CD"] == DBNull.Value) ? string.Empty : row["SUP_PC_CD"].ToString()),
                Sup_usr_id = ((row["SUP_USR_ID"] == DBNull.Value) ? string.Empty : row["SUP_USR_ID"].ToString())
            };
        }
    }
}
