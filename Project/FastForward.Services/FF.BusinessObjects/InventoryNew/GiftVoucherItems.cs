using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class GiftVoucherItems
    {
        #region Private Members
        private Boolean _gvi_act;
        private Int32 _gvi_bal_qty;
        private Int32 _gvi_book;
        private string _gvi_com;
        private string _gvi_itm;
        private Int32 _gvi_line;
        private Int32 _gvi_page;
        private Int32 _gvi_page_line;
        private string _gvi_pc;
        private string _gvi_pre_fix;
        private Int32 _gvi_qty;
        private string _gvi_ref;
        private string _gvi_tp;//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue 
        private string _gvi_val;//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
        private bool _gvi_verb;//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
        private string _mi_longdesc;
        private string _verbdescription;
        #endregion

        public Boolean Gvi_act
        {
            get { return _gvi_act; }
            set { _gvi_act = value; }
        }
        public Int32 Gvi_bal_qty
        {
            get { return _gvi_bal_qty; }
            set { _gvi_bal_qty = value; }
        }
        public Int32 Gvi_book
        {
            get { return _gvi_book; }
            set { _gvi_book = value; }
        }
        public string Gvi_com
        {
            get { return _gvi_com; }
            set { _gvi_com = value; }
        }
        public string Gvi_itm
        {
            get { return _gvi_itm; }
            set { _gvi_itm = value; }
        }
        public Int32 Gvi_line
        {
            get { return _gvi_line; }
            set { _gvi_line = value; }
        }
        public Int32 Gvi_page
        {
            get { return _gvi_page; }
            set { _gvi_page = value; }
        }
        public Int32 Gvi_page_line
        {
            get { return _gvi_page_line; }
            set { _gvi_page_line = value; }
        }
        public string Gvi_pc
        {
            get { return _gvi_pc; }
            set { _gvi_pc = value; }
        }
        public string Gvi_pre_fix
        {
            get { return _gvi_pre_fix; }
            set { _gvi_pre_fix = value; }
        }
        public Int32 Gvi_qty
        {
            get { return _gvi_qty; }
            set { _gvi_qty = value; }
        }
        public string Gvi_ref
        {
            get { return _gvi_ref; }
            set { _gvi_ref = value; }
        }
        public string Gvi_tp//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
        {
            get { return _gvi_tp; }
            set { _gvi_tp = value; }
        }
        public string Gvi_val//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
        {
            get { return _gvi_val; }
            set { _gvi_val = value; }
        }
        public bool Gvi_verb//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
        {
            get { return _gvi_verb; }
            set { _gvi_verb = value; }
        }
        public string Mi_longdesc
        {
            get { return _mi_longdesc; }
            set { _mi_longdesc = value; }
        }
        public string Verbdescription { get { return _verbdescription; } set { _verbdescription = value; } }
        public static GiftVoucherItems Converter(DataRow row)
        {
            return new GiftVoucherItems
            {
                Gvi_act = row["GVI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["GVI_ACT"]),
                Gvi_bal_qty = row["GVI_BAL_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVI_BAL_QTY"]),
                Gvi_book = row["GVI_BOOK"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVI_BOOK"]),
                Gvi_com = row["GVI_COM"] == DBNull.Value ? string.Empty : row["GVI_COM"].ToString(),
                Gvi_itm = row["GVI_ITM"] == DBNull.Value ? string.Empty : row["GVI_ITM"].ToString(),
                Gvi_line = row["GVI_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVI_LINE"]),
                Gvi_page = row["GVI_PAGE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVI_PAGE"]),
                Gvi_page_line = row["GVI_PAGE_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVI_PAGE_LINE"]),
                Gvi_pc = row["GVI_PC"] == DBNull.Value ? string.Empty : row["GVI_PC"].ToString(),
                Gvi_pre_fix = row["GVI_PRE_FIX"] == DBNull.Value ? string.Empty : row["GVI_PRE_FIX"].ToString(),
                Gvi_qty = row["GVI_QTY"] == DBNull.Value ? 0 : Convert.ToInt32(row["GVI_QTY"]),
                Gvi_ref = row["GVI_REF"] == DBNull.Value ? string.Empty : row["GVI_REF"].ToString(),
                Gvi_tp = row["Gvi_tp"] == DBNull.Value ? string.Empty : row["Gvi_tp"].ToString(),//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
                Gvi_val = row["Gvi_val"] == DBNull.Value ? string.Empty : row["Gvi_val"].ToString(),//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
                Gvi_verb = row["Gvi_verb"] == DBNull.Value ? false : Convert.ToBoolean(row["Gvi_verb"])//Added by Prabhath on 22/11/2013 for the purpose - incorporate AND/OR for the gift voucher print and issue
            };
        }
    }

}
