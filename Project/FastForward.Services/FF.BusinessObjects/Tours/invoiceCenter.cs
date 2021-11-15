using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Tours
{

    //Rukshan
    [Serializable]
    public class invoiceCenter
    {
        #region Private Members
        private string mbe_name;
        private string mbe_add1;
        private string mbe_add2;
        private string SIH_MAN_REF;
        private DateTime SIH_DT;
        private string SIH_INV_NO;
        private string MBE_TAX_NO;
        private string sii_alt_itm_desc;
        private Int32 sii_qty;
        private decimal sii_unit_rt;
        private int sii_tot_amt;

        private string mc_desc;
        private string mc_add1;
        private string mc_add2;
        private string mc_tel;
        private string mc_fax;
        private string mc_email;
        private string mc_web;

        #endregion

        public string CustomerName
        {
            get { return mbe_name; }
            set { mbe_name = value; }
        }
        public string CustomerAddress1
        {
            get { return mbe_add1; }
            set { mbe_add1 = value; }
        }
        public string CustomerAddress2
        {
            get { return mbe_add2; }
            set { mbe_add2 = value; }
        }
        public string RefNo
        {
            get { return SIH_MAN_REF; }
            set { SIH_MAN_REF = value; }
        }

        public DateTime Date
        {
            get { return SIH_DT; }
            set { SIH_DT = value; }
        }
        public string InvoiceNo
        {
            get { return SIH_INV_NO; }
            set { SIH_INV_NO = value; }
        }
        public string TAXNo
        {
            get { return MBE_TAX_NO; }
            set { MBE_TAX_NO = value; }
        }
        public string DESC
        {
            get { return sii_alt_itm_desc; }
            set { sii_alt_itm_desc = value; }
        }

        public Int32 QTY
        {
            get { return sii_qty; }
            set { sii_qty = value; }
        }
        public decimal UNITRATE
        {
            get { return sii_unit_rt; }
            set { sii_unit_rt = value; }
        }
        public int TOTAL
        {
            get { return sii_tot_amt; }
            set { sii_tot_amt = value; }
        }
        public string CompanyName
        {
            get { return mc_desc; }
            set { mc_desc = value; }
        }
        public string CompanyAddress1
        {
            get { return mc_add1; }
            set { mc_add1 = value; }
        }
        public string CompanyAddress2
        {
            get { return mc_add2; }
            set { mc_add2 = value; }
        }
        public string CompanyTel
        {
            get { return mc_tel; }
            set { mc_tel = value; }
        }
        public string CompanyFax
        {
            get { return mc_fax; }
            set { mc_fax = value; }
        }
        public string CompanyEmail
        {
            get { return mc_email; }
            set { mc_email = value; }
        }
        public string CompanyWeb
        {
            get { return mc_web; }
            set { mc_web = value; }
        }
        public static invoiceCenter ConvertTotal(DataRow row)
        {
            return new invoiceCenter
            {
                CustomerName = row["mbe_name"] == DBNull.Value ? string.Empty : row["mbe_name"].ToString(),
                CustomerAddress1 = row["mbe_add1"] == DBNull.Value ? string.Empty : row["mbe_add1"].ToString(),
                CustomerAddress2 = row["mbe_add2"] == DBNull.Value ? string.Empty : row["mbe_add2"].ToString(),
                RefNo = row["SIH_MAN_REF"] == DBNull.Value ? string.Empty : row["SIH_MAN_REF"].ToString(),
                Date = row["SIH_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SIH_DT"].ToString()),
                InvoiceNo = row["SIH_INV_NO"] == DBNull.Value ? string.Empty : row["SIH_INV_NO"].ToString(),
                TAXNo = row["MBE_TAX_NO"] == DBNull.Value ? string.Empty : row["MBE_TAX_NO"].ToString(),
                DESC = row["sii_alt_itm_desc"] == DBNull.Value ? string.Empty : row["sii_alt_itm_desc"].ToString(),
                QTY = row["sii_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["sii_qty"].ToString()),
                UNITRATE = row["sii_unit_rt"] == DBNull.Value ? 0 : Convert.ToDecimal(row["sii_unit_rt"].ToString()),
                TOTAL = row["sii_tot_amt"] == DBNull.Value ? 0 : Convert.ToInt32(row["sii_tot_amt"].ToString()),

                CompanyName = row["mc_desc"] == DBNull.Value ? string.Empty : row["mc_desc"].ToString(),
                CompanyAddress1 = row["mc_add1"] == DBNull.Value ? string.Empty : row["mc_add1"].ToString(),
                CompanyAddress2 = row["mc_add2"] == DBNull.Value ? string.Empty : row["mc_add2"].ToString(),
                CompanyTel = row["mc_tel"] == DBNull.Value ? string.Empty : row["mc_tel"].ToString(),
                CompanyFax = row["mc_fax"] == DBNull.Value ? string.Empty : row["mc_fax"].ToString(),
                CompanyEmail = row["mc_email"] == DBNull.Value ? string.Empty : row["mc_email"].ToString(),
                CompanyWeb = row["mc_web"] == DBNull.Value ? string.Empty : row["mc_web"].ToString(),
            };
        }
    }
}
