using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class MasterItemSimilar : MasterItem
    {
        //
        // Function             - Similar Item Master
        // Function Written By  - Chamal De Silva
        // Date                 - 28/03/2013
        // Table                - MST_ITM_SIMILAR
        //

        #region Private Members
        private Boolean _misi_act;
        private string _misi_com;
        private string _misi_doc_no;
        private DateTime _misi_from;
        private string _misi_itm_cd;
        private string _misi_loc;
        private string _misi_pc;
        private string _misi_promo;
        private Int32 _misi_seq_no;
        private string _misi_sim_itm_cd;
        private DateTime _misi_to;
        private string _misi_tp;
        private string _misi_cre_by;
        private DateTime _misi_cre_dt;
        private string _misi_mod_by;
        private DateTime _misi_mod_dt;
        private string _misi_pty_tp;

        #endregion

        public Boolean Misi_act { get { return _misi_act; } set { _misi_act = value; } }
        public string Misi_com { get { return _misi_com; } set { _misi_com = value; } }
        public string Misi_doc_no { get { return _misi_doc_no; } set { _misi_doc_no = value; } }
        public DateTime Misi_from { get { return _misi_from; } set { _misi_from = value; } }
        public string Misi_itm_cd { get { return _misi_itm_cd; } set { _misi_itm_cd = value; } }
        public string Misi_loc { get { return _misi_loc; } set { _misi_loc = value; } }
        public string Misi_pc { get { return _misi_pc; } set { _misi_pc = value; } }
        public string Misi_promo { get { return _misi_promo; } set { _misi_promo = value; } }
        public Int32 Misi_seq_no { get { return _misi_seq_no; } set { _misi_seq_no = value; } }
        public string Misi_sim_itm_cd { get { return _misi_sim_itm_cd; } set { _misi_sim_itm_cd = value; } }
        public DateTime Misi_to { get { return _misi_to; } set { _misi_to = value; } }
        public string Misi_tp { get { return _misi_tp; } set { _misi_tp = value; } }
        public string Misi_cre_by { get { return _misi_cre_by; } set { _misi_cre_by = value; } }
        public DateTime Misi_cre_dt { get { return _misi_cre_dt; } set { _misi_cre_dt = value; } }
        public string Misi_mod_by { get { return _misi_mod_by; } set { _misi_mod_by = value; } }
        public DateTime Misi_mod_dt { get { return _misi_mod_dt; } set { _misi_mod_dt = value; } }
        public string Misi_pty_tp { get { return _misi_pty_tp; } set { _misi_pty_tp = value; } }
        public string Tmp_req_com { get; set; }
        public string Tmp_req_loc { get; set; }
        public string Tmp_iss_com { get; set; }
        public string Tmp_iss_loc { get; set; }
        public decimal Tmp_req_loc_bal { get; set; }
        public decimal Tmp_Iss_loc_bal { get; set; }
        public string Tmp_itm_desc { get; set; }

        //--Updated by akila ------------------------
        public string Misi_Itm_Tp { get; set; }
        public string Misi_Vou_Tp { get; set; }
        public string Misi_Vou_Disc_Tp { get; set; }
        public decimal Misi_Vou_Value { get; set; }
        public Int32 Misi_Vou_Val_Period { get; set; }
        //-------------------------------------------
        //add by tharanga 
        public string Misi_price_book { get; set; }
        public string Misi_price_leval { get; set; }
        //

        public static MasterItemSimilar Converter(DataRow row)
        {
            return new MasterItemSimilar
            {
                Misi_act = row["MISI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MISI_ACT"]),
                Misi_com = row["MISI_COM"] == DBNull.Value ? string.Empty : row["MISI_COM"].ToString(),
                Misi_doc_no = row["MISI_DOC_NO"] == DBNull.Value ? string.Empty : row["MISI_DOC_NO"].ToString(),
                Misi_from = row["MISI_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_FROM"]),
                Misi_itm_cd = row["MISI_ITM_CD"] == DBNull.Value ? string.Empty : row["MISI_ITM_CD"].ToString(),
                Misi_loc = row["MISI_LOC"] == DBNull.Value ? string.Empty : row["MISI_LOC"].ToString(),
                Misi_pc = row["MISI_PC"] == DBNull.Value ? string.Empty : row["MISI_PC"].ToString(),
                Misi_promo = row["MISI_PROMO"] == DBNull.Value ? string.Empty : row["MISI_PROMO"].ToString(),
                Misi_seq_no = row["MISI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["MISI_SEQ_NO"]),
                Misi_sim_itm_cd = row["MISI_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["MISI_SIM_ITM_CD"].ToString(),
                Misi_to = row["MISI_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_TO"]),
                Misi_tp = row["MISI_TP"] == DBNull.Value ? string.Empty : row["MISI_TP"].ToString(),
                Misi_cre_by = row["MISI_CRE_BY"] == DBNull.Value ? string.Empty : row["MISI_CRE_BY"].ToString(),
                Misi_cre_dt = row["MISI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_CRE_DT"]),
                Misi_mod_by = row["MISI_MOD_BY"] == DBNull.Value ? string.Empty : row["MISI_MOD_BY"].ToString(),
                Misi_mod_dt = row["MISI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_MOD_DT"]),

                Mi_longdesc = row["MI_LONGDESC"] == DBNull.Value ? string.Empty : row["MI_LONGDESC"].ToString(),
                Mi_model = row["MI_MODEL"] == DBNull.Value ? string.Empty : row["MI_MODEL"].ToString(),
                Mi_brand = row["MI_BRAND"] == DBNull.Value ? string.Empty : row["MI_BRAND"].ToString()

            };
        }

        public static MasterItemSimilar ConverterSimilar(DataRow row)
        {
            return new MasterItemSimilar
            {
                Misi_act = row["MISI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MISI_ACT"]),
                Misi_com = row["MISI_COM"] == DBNull.Value ? string.Empty : row["MISI_COM"].ToString(),
                Misi_doc_no = row["MISI_DOC_NO"] == DBNull.Value ? string.Empty : row["MISI_DOC_NO"].ToString(),
                Misi_from = row["MISI_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_FROM"]),
                Misi_itm_cd = row["MISI_ITM_CD"] == DBNull.Value ? string.Empty : row["MISI_ITM_CD"].ToString(),
                Misi_loc = row["MISI_LOC"] == DBNull.Value ? string.Empty : row["MISI_LOC"].ToString(),
                Misi_pc = row["MISI_PC"] == DBNull.Value ? string.Empty : row["MISI_PC"].ToString(),
                Misi_promo = row["MISI_PROMO"] == DBNull.Value ? string.Empty : row["MISI_PROMO"].ToString(),
                Misi_seq_no = row["MISI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["MISI_SEQ_NO"]),
                Misi_sim_itm_cd = row["MISI_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["MISI_SIM_ITM_CD"].ToString(),
                Misi_to = row["MISI_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_TO"]),
                Misi_tp = row["MISI_TP"] == DBNull.Value ? string.Empty : row["MISI_TP"].ToString(),
                Misi_cre_by = row["MISI_CRE_BY"] == DBNull.Value ? string.Empty : row["MISI_CRE_BY"].ToString(),
                Misi_cre_dt = row["MISI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_CRE_DT"]),
                Misi_mod_by = row["MISI_MOD_BY"] == DBNull.Value ? string.Empty : row["MISI_MOD_BY"].ToString(),
                Misi_mod_dt = row["MISI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_MOD_DT"]),
                Misi_pty_tp = row["Misi_pty_tp"] == DBNull.Value ? string.Empty : row["Misi_pty_tp"].ToString()

            };
        }

        public static MasterItemSimilar ConverterSimilarNew(DataRow row)
        {
            return new MasterItemSimilar
            {
                Misi_act = row["MISI_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MISI_ACT"]),
                Misi_com = row["MISI_COM"] == DBNull.Value ? string.Empty : row["MISI_COM"].ToString(),
                Misi_doc_no = row["MISI_DOC_NO"] == DBNull.Value ? string.Empty : row["MISI_DOC_NO"].ToString(),
                Misi_from = row["MISI_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_FROM"]),
                Misi_itm_cd = row["MISI_ITM_CD"] == DBNull.Value ? string.Empty : row["MISI_ITM_CD"].ToString(),
                Misi_loc = row["MISI_LOC"] == DBNull.Value ? string.Empty : row["MISI_LOC"].ToString(),
                Misi_pc = row["MISI_PC"] == DBNull.Value ? string.Empty : row["MISI_PC"].ToString(),
                Misi_promo = row["MISI_PROMO"] == DBNull.Value ? string.Empty : row["MISI_PROMO"].ToString(),
                Misi_seq_no = row["MISI_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["MISI_SEQ_NO"]),
                Misi_sim_itm_cd = row["MISI_SIM_ITM_CD"] == DBNull.Value ? string.Empty : row["MISI_SIM_ITM_CD"].ToString(),
                Misi_to = row["MISI_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_TO"]),
                Misi_tp = row["MISI_TP"] == DBNull.Value ? string.Empty : row["MISI_TP"].ToString(),
                Misi_cre_by = row["MISI_CRE_BY"] == DBNull.Value ? string.Empty : row["MISI_CRE_BY"].ToString(),
                Misi_cre_dt = row["MISI_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_CRE_DT"]),
                Misi_mod_by = row["MISI_MOD_BY"] == DBNull.Value ? string.Empty : row["MISI_MOD_BY"].ToString(),
                Misi_mod_dt = row["MISI_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["MISI_MOD_DT"]),
                Misi_pty_tp = row["Misi_pty_tp"] == DBNull.Value ? string.Empty : row["Misi_pty_tp"].ToString(),
                Misi_Itm_Tp = row["MISI_ITM_TP"] == DBNull.Value ? string.Empty : row["MISI_ITM_TP"].ToString(),
                Misi_Vou_Tp = row["MISI_VOU_TP"] == DBNull.Value ? string.Empty : row["MISI_VOU_TP"].ToString(),
                Misi_Vou_Disc_Tp = row["MISI_VOU_DISC_TP"] == DBNull.Value ? string.Empty : row["MISI_VOU_DISC_TP"].ToString(),
                Misi_Vou_Value = row["MISI_VOU_VAL"] == DBNull.Value ? 0: Convert.ToDecimal( row["MISI_VOU_VAL"].ToString()),
                Misi_Vou_Val_Period = row["MISI_VOU_PERIOD"] == DBNull.Value ? 0 : Convert.ToInt32(row["MISI_VOU_PERIOD"].ToString()),
                Misi_price_book = row["misi_vou_p_book"] == DBNull.Value ? string.Empty : row["misi_vou_p_book"].ToString(),
                Misi_price_leval = row["misi_vou_p_leval"] == DBNull.Value ? string.Empty : row["misi_vou_p_leval"].ToString()
                

            };
        }
    }
}
