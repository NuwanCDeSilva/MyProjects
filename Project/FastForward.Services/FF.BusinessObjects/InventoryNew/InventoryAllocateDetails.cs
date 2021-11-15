using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
  public  class InventoryAllocateDetails
    {
        public String isa_itm_cd { get; set; }
        public String isa_chnl { get; set; }
        public String isa_doc_tp { get; set; }
        public DateTime isa_doc_dt { get; set; }
        public DateTime isa_dt { get; set; }

        public Decimal isa_aloc_qty { get; set; }
        public Decimal isa_aloc_bqty { get; set; }
        public String isa_loc { get; set; }
        public String mi_shortdesc { get; set; }
        public String mi_model { get; set; }
       public String mi_cate_1 { get; set; }
        public String mi_cate_2 { get; set; }
        public String mi_cate_3 { get; set; }
        public string isa_com { get; set; }
        public string mi_brand { get; set; }
        public string mi_cd { get; set; }
        public string isa_doc_no { get; set; }

        #region new Propert Data
        public Int32 Isa_seq { get; set; }
        public DateTime Isa_dt
        {
            get { return isa_dt; }
            set { isa_dt = value; }
        }
        public String Isa_com
        {
            get { return isa_com; }
            set { isa_com = value; }
        }
        public String Isa_chnl {
            get { return isa_chnl; }
            set { isa_chnl = value; }
        }
        public String Isa_loc
        {
            get { return isa_loc; }
            set { isa_loc = value; }
        }
        public String Isa_tp { get; set; }
        public String Isa_doc_tp { get; set; }
        public String Isa_doc_no
        {
            get { return isa_doc_no; }
            set { isa_doc_no = value; }
        }
        public DateTime Isa_doc_dt { get; set; }
        public String Isa_itm_cd
        {
            get { return isa_itm_cd; }
            set { isa_itm_cd = value; }
        }
        public String Isa_itm_stus { get; set; }
        public Decimal Isa_aloc_qty
        {
            get { return isa_aloc_qty; }
            set { isa_aloc_qty = value; }
        }
        public Decimal Isa_aloc_bqty
        {
            get { return isa_aloc_bqty; }
            set { isa_aloc_bqty = value; }
        }
        public Int32 Isa_itm_changed { get; set; }
        public String Isa_ref_no { get; set; }
        public Decimal Isa_req_qty { get; set; }
        public Decimal Isa_req_bqty { get; set; }
        public Int32 Isa_act { get; set; }
        public String Isa_cre_by { get; set; }
        public DateTime Isa_cre_dt { get; set; }
        public String Isa_session_id { get; set; }
        public String Isa_cnl_by { get; set; }
        public DateTime Isa_cnl_dt { get; set; }
        public String Isa_cnl_session_id { get; set; }
        #endregion

        public static InventoryAllocateDetails Converter(DataRow row)
        {
            return new InventoryAllocateDetails
            {
                isa_itm_cd = row["isa_itm_cd"] == DBNull.Value ? string.Empty : row["isa_itm_cd"].ToString(),
                isa_chnl = row["isa_chnl"] == DBNull.Value ? string.Empty : row["isa_chnl"].ToString(),
                isa_doc_tp = row["isa_doc_tp"] == DBNull.Value ? string.Empty : row["isa_doc_tp"].ToString(),
                isa_doc_dt = row["isa_doc_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["isa_doc_dt"].ToString()),
                isa_dt = row["isa_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["isa_dt"].ToString()),
                isa_aloc_qty = row["isa_aloc_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["isa_aloc_qty"].ToString()),
                isa_aloc_bqty = row["isa_aloc_bqty"] == DBNull.Value ? 0 : Convert.ToInt32(row["isa_aloc_bqty"].ToString()),
                isa_loc = row["isa_loc"] == DBNull.Value ? string.Empty : row["isa_loc"].ToString(),
                mi_shortdesc = row["mi_shortdesc"] == DBNull.Value ? string.Empty : row["mi_shortdesc"].ToString(),
                mi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString(),
                mi_cate_1 = row["mi_cate_1"] == DBNull.Value ? string.Empty : row["mi_cate_1"].ToString(),
                mi_cate_2 = row["mi_cate_2"] == DBNull.Value ? string.Empty : row["mi_cate_2"].ToString(),
                mi_cate_3 = row["mi_cate_3"] == DBNull.Value ? string.Empty : row["mi_cate_3"].ToString(),
                isa_com = row["isa_com"] == DBNull.Value ? string.Empty : row["isa_com"].ToString(),
                mi_brand = row["mi_brand"] == DBNull.Value ? string.Empty : row["mi_brand"].ToString(),
                mi_cd = row["mi_cd"] == DBNull.Value ? string.Empty : row["mi_cd"].ToString(),
                isa_doc_no = row["isa_doc_no"] == DBNull.Value ? string.Empty : row["isa_doc_no"].ToString(),
            };

        }
        public static InventoryAllocateDetails ConverterNew(DataRow row)
        {
            return new InventoryAllocateDetails
            {
                Isa_seq = row["ISA_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISA_SEQ"].ToString()),
                Isa_dt = row["ISA_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_DT"].ToString()),
                Isa_com = row["ISA_COM"] == DBNull.Value ? string.Empty : row["ISA_COM"].ToString(),
                Isa_chnl = row["ISA_CHNL"] == DBNull.Value ? string.Empty : row["ISA_CHNL"].ToString(),
                Isa_loc = row["ISA_LOC"] == DBNull.Value ? string.Empty : row["ISA_LOC"].ToString(),
                Isa_tp = row["ISA_TP"] == DBNull.Value ? string.Empty : row["ISA_TP"].ToString(),
                Isa_doc_tp = row["ISA_DOC_TP"] == DBNull.Value ? string.Empty : row["ISA_DOC_TP"].ToString(),
                Isa_doc_no = row["ISA_DOC_NO"] == DBNull.Value ? string.Empty : row["ISA_DOC_NO"].ToString(),
                Isa_doc_dt = row["ISA_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_DOC_DT"].ToString()),
                Isa_itm_cd = row["ISA_ITM_CD"] == DBNull.Value ? string.Empty : row["ISA_ITM_CD"].ToString(),
                Isa_itm_stus = row["ISA_ITM_STUS"] == DBNull.Value ? string.Empty : row["ISA_ITM_STUS"].ToString(),
                Isa_aloc_qty = row["ISA_ALOC_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_ALOC_QTY"].ToString()),
                Isa_aloc_bqty = row["ISA_ALOC_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_ALOC_BQTY"].ToString()),
                Isa_itm_changed = row["ISA_ITM_CHANGED"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISA_ITM_CHANGED"].ToString()),
                Isa_ref_no = row["ISA_REF_NO"] == DBNull.Value ? string.Empty : row["ISA_REF_NO"].ToString(),
                Isa_req_qty = row["ISA_REQ_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_REQ_QTY"].ToString()),
                Isa_req_bqty = row["ISA_REQ_BQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ISA_REQ_BQTY"].ToString()),
                Isa_act = row["ISA_ACT"] == DBNull.Value ? 0 : Convert.ToInt32(row["ISA_ACT"].ToString()),
                Isa_cre_by = row["ISA_CRE_BY"] == DBNull.Value ? string.Empty : row["ISA_CRE_BY"].ToString(),
                Isa_cre_dt = row["ISA_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_CRE_DT"].ToString()),
                Isa_session_id = row["ISA_SESSION_ID"] == DBNull.Value ? string.Empty : row["ISA_SESSION_ID"].ToString(),
                Isa_cnl_by = row["ISA_CNL_BY"] == DBNull.Value ? string.Empty : row["ISA_CNL_BY"].ToString(),
                Isa_cnl_dt = row["ISA_CNL_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ISA_CNL_DT"].ToString()),
                Isa_cnl_session_id = row["ISA_CNL_SESSION_ID"] == DBNull.Value ? string.Empty : row["ISA_CNL_SESSION_ID"].ToString()
            };
        }
    }
}
