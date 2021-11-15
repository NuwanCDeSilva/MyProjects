using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    //create by = shani on 27-07-2013
    //table = hpt_accounts_agreement_product
    [Serializable]
    public class AccountAgreementProduct
    {
        #region Private Members
        private string _itm_account;
        private string _itm_code;
        private string _itm_desc;
        private decimal _itm_qty;
        private decimal _itm_unitprice;
        #endregion

        public string Itm_account { get { return _itm_account; } set { _itm_account = value; } }
        public string Itm_code { get { return _itm_code; } set { _itm_code = value; } }
        public string Itm_desc { get { return _itm_desc; } set { _itm_desc = value; } }
        public decimal Itm_qty { get { return _itm_qty; } set { _itm_qty = value; } }
        public decimal Itm_unitprice { get { return _itm_unitprice; } set { _itm_unitprice = value; } }

        public static AccountAgreementProduct Converter(DataRow row)
        {
            return new AccountAgreementProduct
            {
                Itm_account = row["ITM_ACCOUNT"] == DBNull.Value ? string.Empty : row["ITM_ACCOUNT"].ToString(),
                Itm_code = row["ITM_CODE"] == DBNull.Value ? string.Empty : row["ITM_CODE"].ToString(),
                Itm_desc = row["ITM_DESC"] == DBNull.Value ? string.Empty : row["ITM_DESC"].ToString(),
                Itm_qty = row["ITM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITM_QTY"]),
                Itm_unitprice = row["ITM_UNITPRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITM_UNITPRICE"])

            };
        }
    }
}
