using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class DiscountVoucerTypes
    {
        #region Private Members
        private Boolean _spt_act;
        private string _spt_com;
        private string _spt_cre_by;
        private DateTime _spt_cre_when;
        private string _spt_mod_by;
        private DateTime _spt_mod_when;
        private string _spt_vou_cd;
        private string _spt_vou_desc;
        #endregion

        public Boolean Spt_act
        {
            get { return _spt_act; }
            set { _spt_act = value; }
        }
        public string Spt_com
        {
            get { return _spt_com; }
            set { _spt_com = value; }
        }
        public string Spt_cre_by
        {
            get { return _spt_cre_by; }
            set { _spt_cre_by = value; }
        }
        public DateTime Spt_cre_when
        {
            get { return _spt_cre_when; }
            set { _spt_cre_when = value; }
        }
        public string Spt_mod_by
        {
            get { return _spt_mod_by; }
            set { _spt_mod_by = value; }
        }
        public DateTime Spt_mod_when
        {
            get { return _spt_mod_when; }
            set { _spt_mod_when = value; }
        }
        public string Spt_vou_cd
        {
            get { return _spt_vou_cd; }
            set { _spt_vou_cd = value; }
        }
        public string Spt_vou_desc
        {
            get { return _spt_vou_desc; }
            set { _spt_vou_desc = value; }
        }

        public static DiscountVoucerTypes Converter(DataRow row)
        {
            return new DiscountVoucerTypes
            {
                Spt_act = row["SPT_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SPT_ACT"]),
                Spt_com = row["SPT_COM"] == DBNull.Value ? string.Empty : row["SPT_COM"].ToString(),
                Spt_cre_by = row["SPT_CRE_BY"] == DBNull.Value ? string.Empty : row["SPT_CRE_BY"].ToString(),
                Spt_cre_when = row["SPT_CRE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPT_CRE_WHEN"]),
                Spt_mod_by = row["SPT_MOD_BY"] == DBNull.Value ? string.Empty : row["SPT_MOD_BY"].ToString(),
                Spt_mod_when = row["SPT_MOD_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPT_MOD_WHEN"]),
                Spt_vou_cd = row["SPT_VOU_CD"] == DBNull.Value ? string.Empty : row["SPT_VOU_CD"].ToString(),
                Spt_vou_desc = row["SPT_VOU_DESC"] == DBNull.Value ? string.Empty : row["SPT_VOU_DESC"].ToString()

            };
        }

    }
}
