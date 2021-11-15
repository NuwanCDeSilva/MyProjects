using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.General
{
    [Serializable]
    public class Transport
    {
        public Int32 Itrn_seq { get; set; }
        public String Itrn_com { get; set; }
        public String Itrn_loc { get; set; }
        public String Itrn_trns_no { get; set; }
        public String Itrn_ref_doc { get; set; }
        public String Itrn_trns_method { get; set; }
        public String Itrn_trns_pty_tp { get; set; }
        public String Itrn_trns_pty_cd { get; set; }
        public String Itrn_ref_no { get; set; }
        public String Itrn_rmk { get; set; }
        public String Itrn_stus { get; set; }
        public String Itrn_cre_by { get; set; }
        public DateTime Itrn_cre_dt { get; set; }
        public String Itrn_cre_session { get; set; }
        public String Itrn_mod_by { get; set; }
        public DateTime Itrn_mod_dt { get; set; }
        public String Itrn_mod_session { get; set; }
        public String Itrn_anal1 { get; set; }
        public String Itrn_anal2 { get; set; }
        public Int32 _grdRowNo { get; set; }
        public bool Slip_no_auto_gen { get; set; }
        public string Mbe_curr_slip_cd { get; set; }
        public string Tmp_slip_cd { get; set; }
        public bool IsRecall { get; set; }//add by akila 2017/07/18

        [DefaultValue(1)]
        public Int32 itrn_isactive { get; set; }//add by akila 2017/07/18
        public MasterAutoNumber MstAuto{get;set;}
          
        public static Transport Converter(DataRow row)
        {
            return new Transport
            {
                Itrn_seq = row["ITRN_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITRN_SEQ"].ToString()),
                Itrn_com = row["ITRN_COM"] == DBNull.Value ? string.Empty : row["ITRN_COM"].ToString(),
                Itrn_loc = row["ITRN_LOC"] == DBNull.Value ? string.Empty : row["ITRN_LOC"].ToString(),
                Itrn_trns_no = row["ITRN_TRNS_NO"] == DBNull.Value ? string.Empty : row["ITRN_TRNS_NO"].ToString(),
                Itrn_ref_doc = row["ITRN_REF_DOC"] == DBNull.Value ? string.Empty : row["ITRN_REF_DOC"].ToString(),
                Itrn_trns_method = row["ITRN_TRNS_METHOD"] == DBNull.Value ? string.Empty : row["ITRN_TRNS_METHOD"].ToString(),
                Itrn_trns_pty_tp = row["ITRN_TRNS_PTY_TP"] == DBNull.Value ? string.Empty : row["ITRN_TRNS_PTY_TP"].ToString(),
                Itrn_trns_pty_cd = row["ITRN_TRNS_PTY_CD"] == DBNull.Value ? string.Empty : row["ITRN_TRNS_PTY_CD"].ToString(),
                Itrn_ref_no = row["ITRN_REF_NO"] == DBNull.Value ? string.Empty : row["ITRN_REF_NO"].ToString(),
                Itrn_rmk = row["ITRN_RMK"] == DBNull.Value ? string.Empty : row["ITRN_RMK"].ToString(),
                Itrn_stus = row["ITRN_STUS"] == DBNull.Value ? string.Empty : row["ITRN_STUS"].ToString(),
                Itrn_cre_by = row["ITRN_CRE_BY"] == DBNull.Value ? string.Empty : row["ITRN_CRE_BY"].ToString(),
                Itrn_cre_dt = row["ITRN_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRN_CRE_DT"].ToString()),
                Itrn_cre_session = row["ITRN_CRE_SESSION"] == DBNull.Value ? string.Empty : row["ITRN_CRE_SESSION"].ToString(),
                Itrn_mod_by = row["ITRN_MOD_BY"] == DBNull.Value ? string.Empty : row["ITRN_MOD_BY"].ToString(),
                Itrn_mod_dt = row["ITRN_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["ITRN_MOD_DT"].ToString()),
                Itrn_mod_session = row["ITRN_MOD_SESSION"] == DBNull.Value ? string.Empty : row["ITRN_MOD_SESSION"].ToString(),
                Itrn_anal1 = row["Itrn_anal1"] == DBNull.Value ? string.Empty : row["Itrn_anal1"].ToString(),
                Itrn_anal2 = row["Itrn_anal2"] == DBNull.Value ? string.Empty : row["Itrn_anal2"].ToString(),
                itrn_isactive = row["itrn_isactive"] == DBNull.Value ? 0 : Convert.ToInt32( row["itrn_isactive"].ToString())
            };
        }
    }
}
