using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


//darshana on 16-04-2013
namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterItemSubCate
    {
        #region Private Members
        private Boolean _ric2_act;
        private string _ric2_cd;
        private string _ric2_cd1;
        private string _ric2_desc;
       private string _RIC2_CRE_BY;
        private string _RIC2_MOD_BY;
        #endregion

        public Boolean Ric2_act
        {
            get { return _ric2_act; }
            set { _ric2_act = value; }
        }
        public string Ric2_cd
        {
            get { return _ric2_cd; }
            set { _ric2_cd = value; }
        }
        public string Ric2_cd1
        {
            get { return _ric2_cd1; }
            set { _ric2_cd1 = value; }
        }
        public string Ric2_desc
        {
            get { return _ric2_desc; }
            set { _ric2_desc = value; }
        }

        public string RIC2_CRE_BY
        {
            get { return _RIC2_CRE_BY; }
            set { _RIC2_CRE_BY = value; }
        }

        public string RIC2_MOD_BY
        {
            get { return _RIC2_MOD_BY; }
            set { _RIC2_MOD_BY = value; }
        }

        public static MasterItemSubCate Converter(DataRow row)
        {
            return new MasterItemSubCate
            {
                Ric2_act = row["RIC2_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["RIC2_ACT"]),
                Ric2_cd = row["RIC2_CD"] == DBNull.Value ? string.Empty : row["RIC2_CD"].ToString(),
                Ric2_cd1 = row["RIC2_CD1"] == DBNull.Value ? string.Empty : row["RIC2_CD1"].ToString(),
                Ric2_desc = row["RIC2_DESC"] == DBNull.Value ? string.Empty : row["RIC2_DESC"].ToString()

            };
        }

    }
}
