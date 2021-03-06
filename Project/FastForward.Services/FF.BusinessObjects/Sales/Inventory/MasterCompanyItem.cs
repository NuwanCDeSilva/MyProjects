using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class MasterCompanyItem
    {
        #region Private Members
        private Boolean _mci_act;
        private string _mci_com;
        private Boolean _mci_hpqty_chk;
        private Boolean _mci_isfoc;
        private string _mci_itm_cd;
        #endregion

        public Boolean Mci_act
        {
            get { return _mci_act; }
            set { _mci_act = value; }
        }
        public string Mci_com
        {
            get { return _mci_com; }
            set { _mci_com = value; }
        }
        public Boolean Mci_hpqty_chk
        {
            get { return _mci_hpqty_chk; }
            set { _mci_hpqty_chk = value; }
        }
        public Boolean Mci_isfoc
        {
            get { return _mci_isfoc; }
            set { _mci_isfoc = value; }
        }
        public string Mci_itm_cd
        {
            get { return _mci_itm_cd; }
            set { _mci_itm_cd = value; }
        }

        public static MasterCompanyItem Converter(DataRow row)
        {
            return new MasterCompanyItem
            {
                Mci_act = row["MCI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MCI_ACT"]),
                Mci_com = row["MCI_COM"] == DBNull.Value ? string.Empty : row["MCI_COM"].ToString(),
                Mci_hpqty_chk = row["MCI_HPQTY_CHK"] == DBNull.Value ? false : Convert.ToBoolean(row["MCI_HPQTY_CHK"]),
                Mci_isfoc = row["MCI_ISFOC"] == DBNull.Value ? false : Convert.ToBoolean(row["MCI_ISFOC"]),
                Mci_itm_cd = row["MCI_ITM_CD"] == DBNull.Value ? string.Empty : row["MCI_ITM_CD"].ToString()

            };

        }

    }
}
