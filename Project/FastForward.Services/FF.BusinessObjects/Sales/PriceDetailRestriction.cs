using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class PriceDetailRestriction
    {
        #region Private Members
        private string _spr_com;
        private string _spr_msg;
        private Boolean _spr_need_cus;
        private Boolean _spr_need_dl;
        private Boolean _spr_need_mob;
        private Boolean _spr_need_nic;
        private Boolean _spr_need_pp;
        private string _spr_promo;
        private string _spr_usr;
        private DateTime _spr_when;
        #endregion

        public string Spr_com
        {
            get { return _spr_com; }
            set { _spr_com = value; }
        }
        public string Spr_msg
        {
            get { return _spr_msg; }
            set { _spr_msg = value; }
        }
        public Boolean Spr_need_cus
        {
            get { return _spr_need_cus; }
            set { _spr_need_cus = value; }
        }
        public Boolean Spr_need_dl
        {
            get { return _spr_need_dl; }
            set { _spr_need_dl = value; }
        }
        public Boolean Spr_need_mob
        {
            get { return _spr_need_mob; }
            set { _spr_need_mob = value; }
        }
        public Boolean Spr_need_nic
        {
            get { return _spr_need_nic; }
            set { _spr_need_nic = value; }
        }
        public Boolean Spr_need_pp
        {
            get { return _spr_need_pp; }
            set { _spr_need_pp = value; }
        }
        public string Spr_promo
        {
            get { return _spr_promo; }
            set { _spr_promo = value; }
        }
        public string Spr_usr
        {
            get { return _spr_usr; }
            set { _spr_usr = value; }
        }
        public DateTime Spr_when
        {
            get { return _spr_when; }
            set { _spr_when = value; }
        }

        public static PriceDetailRestriction Converter(DataRow row)
        {
            return new PriceDetailRestriction
            {
                Spr_com = row["SPR_COM"] == DBNull.Value ? string.Empty : row["SPR_COM"].ToString(),
                Spr_msg = row["SPR_MSG"] == DBNull.Value ? string.Empty : row["SPR_MSG"].ToString(),
                Spr_need_cus = row["SPR_NEED_CUS"] == DBNull.Value ? false : Convert.ToBoolean(row["SPR_NEED_CUS"]),
                Spr_need_dl = row["SPR_NEED_DL"] == DBNull.Value ? false : Convert.ToBoolean(row["SPR_NEED_DL"]),
                Spr_need_mob = row["SPR_NEED_MOB"] == DBNull.Value ? false : Convert.ToBoolean(row["SPR_NEED_MOB"]),
                Spr_need_nic = row["SPR_NEED_NIC"] == DBNull.Value ? false : Convert.ToBoolean(row["SPR_NEED_NIC"]),
                Spr_need_pp = row["SPR_NEED_PP"] == DBNull.Value ? false : Convert.ToBoolean(row["SPR_NEED_PP"]),
                Spr_promo = row["SPR_PROMO"] == DBNull.Value ? string.Empty : row["SPR_PROMO"].ToString(),
                Spr_usr = row["SPR_USR"] == DBNull.Value ? string.Empty : row["SPR_USR"].ToString(),
                Spr_when = row["SPR_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SPR_WHEN"])

            };
        }

    }
}
