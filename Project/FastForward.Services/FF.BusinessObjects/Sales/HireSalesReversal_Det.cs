using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace FF.BusinessObjects
{
    public class HireSalesReversal_Det
    {
        #region private members
        private string _item_code;
        private string _brand;
        private decimal _grd_val2;
        private string _itm_descr;
        private string _model;
        private string _desc;
        private string _Itm_status;
        private decimal _tot_price;
        
        private decimal _tot_price_out;

        private string _app_status;

       
        private string _in_out;
        private Int32 _grad_lvl;
        private string _grad_anal2;
        private string _cat_desc;
        private string _remark;
        private DateTime _app_date;
        private string _gen_remark;
        private string _usr_name;
        private Int32 _app_lvl;
        private string _mbe_name;
        private string _mbe_addrs;
        private string _mbe_district_cd;
        private string _mbe_tel;
        private DateTime mbe_cre_date;
        private string mbe_loc_desc;
        private string mbe_loc_cd;
        private string grad_remark;

       

       

 #endregion
        public string Itm_descr
        {
            get { return _itm_descr; }
            set { _itm_descr = value; }
        }
        public string App_status
        {
            get { return _app_status; }
            set { _app_status = value; }
        }
        public string Mbe_loc_cd
        {
            get { return mbe_loc_cd; }
            set { mbe_loc_cd = value; }
        }
        public string Mbe_loc_desc
        {
            get { return mbe_loc_desc; }
            set { mbe_loc_desc = value; }
        }
        public DateTime Mbe_cre_date
        {
            get { return mbe_cre_date; }
            set { mbe_cre_date = value; }
        }
        public string Mbe_tel
        {
            get { return _mbe_tel; }
            set { _mbe_tel = value; }
        }
        public decimal Grd_val2
        {
            get { return _grd_val2; }
            set { _grd_val2 = value; }
        }
        public Int32 App_lvl
        {
            get { return _app_lvl; }
            set { _app_lvl = value; }
        }
        public string Usr_name
        {
            get { return _usr_name; }
            set { _usr_name = value; }
        }
        public string Gen_remark
        {
            get { return _gen_remark; }
            set { _gen_remark = value; }
        }
        public DateTime App_date
        {
            get { return _app_date; }
            set { _app_date = value; }
        }
        public string Item_code
        {
            get { return _item_code; }
            set { _item_code = value; }
        }
        public string Brand
        {
            get { return _brand; }
            set { _brand = value; }
        }
        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }
        public string Desc
        {
            get { return _desc; }
            set { _desc = value; }
        }
        public string Itm_status
        {
            get { return _Itm_status; }
            set { _Itm_status = value; }
        }
        public decimal Tot_price
        {
            get { return _tot_price; }
            set { _tot_price = value; }
        }
        public string In_out
        {
            get { return _in_out; }
            set { _in_out = value; }
        }
        public Int32 Grad_lvl
        {
            get { return _grad_lvl; }
            set { _grad_lvl = value; }
        }
        public string Grad_anal2
        {
            get { return _grad_anal2; }
            set { _grad_anal2 = value; }
        }
        public string Cat_desc
        {
            get { return _cat_desc; }
            set { _cat_desc = value; }
        }
        public string Remark
        {
            get { return _remark; }
            set { _remark = value; }
        }
        public string Mbe_name
        {
            get { return _mbe_name; }
            set { _mbe_name = value; }
        }
        public string Mbe_addrs
        {
            get { return _mbe_addrs; }
            set { _mbe_addrs = value; }
        }
        public string Mbe_district_cd
        {
            get { return _mbe_district_cd; }
            set { _mbe_district_cd = value; }
        }
        public string Grad_remark
        {
            get { return grad_remark; }
            set { grad_remark = value; }
        }
        public decimal Tot_price_out
        {
            get { return _tot_price_out; }
            set { _tot_price_out = value; }
        }
        #region Converter
        public static HireSalesReversal_Det Converter(DataRow row)
        {

            return new HireSalesReversal_Det
            {

                Item_code = row["GRAD_REQ_PARAM"] == DBNull.Value ? string.Empty : row["GRAD_REQ_PARAM"].ToString(),
                In_out = row["GRAD_ANAL5"] == DBNull.Value ? string.Empty : row["GRAD_ANAL5"].ToString(),
                Brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Desc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Itm_status = row["SAD_ITM_STUS"] == DBNull.Value ? string.Empty : row["SAD_ITM_STUS"].ToString(),
                Tot_price = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Grad_lvl = row["GRAD_LVL"] == DBNull.Value ? 0 : Convert.ToInt32(row["GRAD_LVL"]),
                Grad_anal2 = row["GRAD_ANAL2"] == DBNull.Value ? string.Empty : row["GRAD_ANAL2"].ToString(),
                Cat_desc = row["MMCT_SDESC"] == DBNull.Value ? string.Empty : row["MMCT_SDESC"].ToString(),
                Remark = row["MMCT_SDESC"] == DBNull.Value ? string.Empty : row["MMCT_SDESC"].ToString(),
                Grd_val2 = row["GRAD_VAL2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["GRAD_VAL2"]),
                Grad_remark = row["GRAH_REMAKS"] == DBNull.Value ? string.Empty : row["GRAH_REMAKS"].ToString(),
                Itm_descr = row["STATUS"] == DBNull.Value ? string.Empty : row["STATUS"].ToString(),
                Tot_price_out = row["OUT_TOT_PRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["OUT_TOT_PRICE"]),
                App_status = row["GRAH_APP_STUS"] == DBNull.Value ? string.Empty : row["GRAH_APP_STUS"].ToString(),
            };
        }
        #endregion
        #region ConverterItemDetails
        public static HireSalesReversal_Det ConverterItemDetails(DataRow row)
        {

            return new HireSalesReversal_Det
            {

                Item_code = row["MI_CD"] == DBNull.Value ? string.Empty : row["MI_CD"].ToString(),
                Brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString(),
                Model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Desc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                //Tot_price = row["SAD_TOT_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["SAD_TOT_AMT"]),
                Tot_price = 1234,

            };
        }
        #endregion
        #region ConverterCustomerDetails
        public static HireSalesReversal_Det ConverterCustomerDetails(DataRow row)
        {

            return new HireSalesReversal_Det
            {

                Mbe_name = row["MBE_NAME"] == DBNull.Value ? string.Empty : row["MBE_NAME"].ToString(),
                Mbe_addrs = row["MBE_ADD1"] == DBNull.Value ? string.Empty : row["MBE_ADD1"].ToString(),
                Mbe_district_cd = row["MBE_DISTRIC_CD"] == DBNull.Value ? string.Empty : row["MBE_DISTRIC_CD"].ToString(),
                Mbe_tel = row["MBE_TEL"] == DBNull.Value ? string.Empty : row["MBE_TEL"].ToString(),
                Mbe_cre_date = row["MBE_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MBE_CRE_DT"]),
                Mbe_loc_desc = row["ML_LOC_DESC"] == DBNull.Value ? string.Empty : row["ML_LOC_DESC"].ToString(),
                Mbe_loc_cd = row["ML_LOC_CD"] == DBNull.Value ? string.Empty : row["ML_LOC_CD"].ToString(),
               
            };
        }
        #endregion

    }

}
