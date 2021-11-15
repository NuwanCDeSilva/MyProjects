using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    //Kelum : Business Entity for Customer Segmentation : 2016-April-29
    public class BusEntityTypes
    {
        #region Private Members
        private string _rbt_cate;
        private string _rbt_tp;
        private string _rbt_desc;
        private int _rbt_act;
        private int _rbt_mad;
        #endregion

        public string RBT_cate
        {
            get { return _rbt_cate; }
            set { _rbt_cate = value; }
        }
        public string RBT_tp
        {
            get { return _rbt_tp; }
            set { _rbt_tp = value; }
        }
        public string RBT_desc
        {
            get { return _rbt_desc; }
            set { _rbt_desc = value; }
        }

        public int RBT_act
        {
            get { return _rbt_act; }
            set { _rbt_act = value; }
        }
        public int RBT_mad
        {
            get { return _rbt_mad; }
            set { _rbt_mad = value; }
        }

        public static BusEntityTypes Converter(DataRow row)
        {
            return new BusEntityTypes
            {
                RBT_cate = ((row["RBT_CATE"] == DBNull.Value) ? string.Empty : row["RBT_CATE"].ToString()),
                RBT_tp = ((row["RBT_TP"] == DBNull.Value) ? string.Empty : row["RBT_TP"].ToString()),
                RBT_desc = ((row["RBT_DESC"] == DBNull.Value) ? string.Empty : row["RBT_DESC"].ToString()),
                RBT_act = ((row["RBT_ACT"] == DBNull.Value) ? 0 : Convert.ToInt32(row["RBT_ACT"].ToString())),
                RBT_mad = ((row["RBT_MAD"] == DBNull.Value) ? 0 : Convert.ToInt32(row["RBT_MAD"].ToString()))
            };
        }
    }
}
