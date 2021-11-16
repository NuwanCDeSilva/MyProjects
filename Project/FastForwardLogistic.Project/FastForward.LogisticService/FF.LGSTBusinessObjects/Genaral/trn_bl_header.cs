using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class trn_bl_header
    {
        public String Bl_seq_no { get; set; }
        public String Bl_com_cd { get; set; }
        public String Bl_sbu_cd { get; set; }
        public String Bl_mos_cd { get; set; }
        public String Bl_tos_cd { get; set; }
        public String Bl_doc_no { get; set; }
        public String Bl_d_doc_no { get; set; }
        public String Bl_h_doc_no { get; set; }
        public String Bl_m_doc_no { get; set; }
        public String Bl_job_no { get; set; }
        public String Bl_pouch_no { get; set; }
        public String Bl_cus_cd { get; set; }
        public String Bl_ship_line_cd { get; set; }
        public String Bl_ship_line { get; set; }
        public String Bl_agent_cd { get; set; }
        public DateTime Bl_est_time_arr { get; set; }
        public DateTime Bl_est_time_dep { get; set; }
        public String Bl_load_in_tp { get; set; }
        public String Bl_load_out_tp { get; set; }
        public String Bl_print_cd { get; set; }
        public String Bl_doc_tp { get; set; }
        public DateTime Bl_doc_dt { get; set; }
        public String Bl_ref_no { get; set; }
        public String Bl_bl_tp { get; set; }
        public String Bl_shipper_cd { get; set; }
        public String Bl_shipper_name { get; set; }
        public String Bl_shipper_add1 { get; set; }
        public String Bl_shipper_add2 { get; set; }
        public String Bl_consignee_cd { get; set; }
        public String Bl_consignee_name { get; set; }
        public String Bl_consignee_add1 { get; set; }
        public String Bl_consignee_add2 { get; set; }
        public String Bl_ntfy_party_cd { get; set; }
        public String Bl_ntfy_party_name { get; set; }
        public String Bl_ntfy_party_add1 { get; set; }
        public String Bl_ntfy_party_add2 { get; set; }
        public String Bl_del_agent_cd { get; set; }
        public String Bl_del_agent_name { get; set; }
        public String Bl_del_agent_add1 { get; set; }
        public String Bl_del_agent_add2 { get; set; }
        public String Bl_voage_no { get; set; }
        public DateTime Bl_voage_dt { get; set; }
        public String Bl_vessal_no { get; set; }
        public String Bl_palce_rec { get; set; }
        public String Bl_port_load { get; set; }
        public String Bl_port_discharge { get; set; }
        public String Bl_palce_del { get; set; }
        public Decimal Bl_freight_charg { get; set; }
        public Decimal Bl_freight_payble { get; set; }
        public Int32 Bl_no_of_bol { get; set; }
        public string Bl_recv_ship { get; set; }
        public string Bl_rmk { get; set; }
        public Int32 Bl_act { get; set; }
        public DateTime bl_expires_dt { get; set; }
        public string bl_manual_d_ref { get; set; }
        public string bl_manual_h_ref { get; set; }
        public string bl_manual_m_ref { get; set; }
        public string bl_m_seq_no { get; set; }
        public string bl_pack_uom { get; set; }
        public string bl_terminal { get; set; }
        public Int32 bl_do_print { get; set; }
        public string bl_pay_mode { get; set; }
        public string bl_vsl_oper { get; set; }
        public string bl_cntr_oper { get; set; }
        public string bl_trans_mode { get; set; }
        public string bl_cre_by { get; set; }
        public DateTime bl_cre_dt { get; set; }
        public string bl_mod_by { get; set; }
        public DateTime bl_mod_dt { get; set; }
        public string bl_or_seq { get; set; }



        public static trn_bl_header Converter(DataRow row)
        {
            return new trn_bl_header
            {
                Bl_seq_no = row["BL_SEQ_NO"] == DBNull.Value ? string.Empty : row["BL_SEQ_NO"].ToString(),
                Bl_com_cd = row["BL_COM_CD"] == DBNull.Value ? string.Empty : row["BL_COM_CD"].ToString(),
                Bl_sbu_cd = row["BL_SBU_CD"] == DBNull.Value ? string.Empty : row["BL_SBU_CD"].ToString(),
                Bl_mos_cd = row["BL_MOS_CD"] == DBNull.Value ? string.Empty : row["BL_MOS_CD"].ToString(),
                Bl_tos_cd = row["BL_TOS_CD"] == DBNull.Value ? string.Empty : row["BL_TOS_CD"].ToString(),
                Bl_doc_no = row["BL_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_DOC_NO"].ToString(),
                Bl_d_doc_no = row["BL_D_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_D_DOC_NO"].ToString(),
                Bl_h_doc_no = row["BL_H_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_H_DOC_NO"].ToString(),
                Bl_m_doc_no = row["BL_M_DOC_NO"] == DBNull.Value ? string.Empty : row["BL_M_DOC_NO"].ToString(),
                Bl_job_no = row["BL_JOB_NO"] == DBNull.Value ? string.Empty : row["BL_JOB_NO"].ToString(),
                Bl_pouch_no = row["BL_POUCH_NO"] == DBNull.Value ? string.Empty : row["BL_POUCH_NO"].ToString(),
                Bl_cus_cd = row["BL_CUS_CD"] == DBNull.Value ? string.Empty : row["BL_CUS_CD"].ToString(),
                Bl_ship_line_cd = row["BL_SHIP_LINE_CD"] == DBNull.Value ? string.Empty : row["BL_SHIP_LINE_CD"].ToString(),
                Bl_ship_line = row["BL_SHIP_LINE"] == DBNull.Value ? string.Empty : row["BL_SHIP_LINE"].ToString(),
                Bl_agent_cd = row["BL_AGENT_CD"] == DBNull.Value ? string.Empty : row["BL_AGENT_CD"].ToString(),
                Bl_est_time_arr = row["BL_EST_TIME_ARR"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BL_EST_TIME_ARR"].ToString()),
                Bl_est_time_dep = row["BL_EST_TIME_DEP"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BL_EST_TIME_DEP"].ToString()),
                Bl_load_in_tp = row["BL_LOAD_IN_TP"] == DBNull.Value ? string.Empty : row["BL_LOAD_IN_TP"].ToString(),
                Bl_load_out_tp = row["BL_LOAD_OUT_TP"] == DBNull.Value ? string.Empty : row["BL_LOAD_OUT_TP"].ToString(),
                Bl_print_cd = row["BL_PRINT_CD"] == DBNull.Value ? string.Empty : row["BL_PRINT_CD"].ToString(),
                Bl_doc_tp = row["BL_DOC_TP"] == DBNull.Value ? string.Empty : row["BL_DOC_TP"].ToString(),
                Bl_doc_dt = row["BL_DOC_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BL_DOC_DT"].ToString()),
                Bl_ref_no = row["BL_REF_NO"] == DBNull.Value ? string.Empty : row["BL_REF_NO"].ToString(),
                Bl_bl_tp = row["BL_BL_TP"] == DBNull.Value ? string.Empty : row["BL_BL_TP"].ToString(),
                Bl_shipper_cd = row["BL_SHIPPER_CD"] == DBNull.Value ? string.Empty : row["BL_SHIPPER_CD"].ToString(),
                Bl_shipper_name = row["BL_SHIPPER_NAME"] == DBNull.Value ? string.Empty : row["BL_SHIPPER_NAME"].ToString(),
                Bl_shipper_add1 = row["BL_SHIPPER_ADD1"] == DBNull.Value ? string.Empty : row["BL_SHIPPER_ADD1"].ToString(),
                Bl_shipper_add2 = row["BL_SHIPPER_ADD2"] == DBNull.Value ? string.Empty : row["BL_SHIPPER_ADD2"].ToString(),
                Bl_consignee_cd = row["BL_CONSIGNEE_CD"] == DBNull.Value ? string.Empty : row["BL_CONSIGNEE_CD"].ToString(),
                Bl_consignee_name = row["BL_CONSIGNEE_NAME"] == DBNull.Value ? string.Empty : row["BL_CONSIGNEE_NAME"].ToString(),
                Bl_consignee_add1 = row["BL_CONSIGNEE_ADD1"] == DBNull.Value ? string.Empty : row["BL_CONSIGNEE_ADD1"].ToString(),
                Bl_consignee_add2 = row["BL_CONSIGNEE_ADD2"] == DBNull.Value ? string.Empty : row["BL_CONSIGNEE_ADD2"].ToString(),
                Bl_ntfy_party_cd = row["BL_NTFY_PARTY_CD"] == DBNull.Value ? string.Empty : row["BL_NTFY_PARTY_CD"].ToString(),
                Bl_ntfy_party_name = row["BL_NTFY_PARTY_NAME"] == DBNull.Value ? string.Empty : row["BL_NTFY_PARTY_NAME"].ToString(),
                Bl_ntfy_party_add1 = row["BL_NTFY_PARTY_ADD1"] == DBNull.Value ? string.Empty : row["BL_NTFY_PARTY_ADD1"].ToString(),
                Bl_ntfy_party_add2 = row["BL_NTFY_PARTY_ADD2"] == DBNull.Value ? string.Empty : row["BL_NTFY_PARTY_ADD2"].ToString(),
                Bl_del_agent_cd = row["BL_DEL_AGENT_CD"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_CD"].ToString(),
                Bl_del_agent_name = row["BL_DEL_AGENT_NAME"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_NAME"].ToString(),
                Bl_del_agent_add1 = row["BL_DEL_AGENT_ADD1"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_ADD1"].ToString(),
                Bl_del_agent_add2 = row["BL_DEL_AGENT_ADD2"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_ADD2"].ToString(),
                Bl_voage_no = row["BL_VOAGE_NO"] == DBNull.Value ? string.Empty : row["BL_VOAGE_NO"].ToString(),
                Bl_voage_dt = row["BL_VOAGE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["BL_VOAGE_DT"].ToString()),
                Bl_vessal_no = row["BL_VESSAL_NO"] == DBNull.Value ? string.Empty : row["BL_VESSAL_NO"].ToString(),
                Bl_palce_rec = row["BL_PALCE_REC"] == DBNull.Value ? string.Empty : row["BL_PALCE_REC"].ToString(),
                Bl_port_load = row["BL_PORT_LOAD"] == DBNull.Value ? string.Empty : row["BL_PORT_LOAD"].ToString(),
                Bl_port_discharge = row["BL_PORT_DISCHARGE"] == DBNull.Value ? string.Empty : row["BL_PORT_DISCHARGE"].ToString(),
                Bl_palce_del = row["BL_PALCE_DEL"] == DBNull.Value ? string.Empty : row["BL_PALCE_DEL"].ToString(),
                Bl_freight_charg = row["BL_FREIGHT_CHARG"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BL_FREIGHT_CHARG"].ToString()),
                Bl_freight_payble = row["BL_FREIGHT_PAYBLE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["BL_FREIGHT_PAYBLE"].ToString()),
                Bl_no_of_bol = row["Bl_no_of_bol"] == DBNull.Value ? 0 : Convert.ToInt32(row["Bl_no_of_bol"].ToString()),
                Bl_recv_ship = row["Bl_recv_ship"] == DBNull.Value ? string.Empty : row["Bl_recv_ship"].ToString(),
                Bl_rmk = row["Bl_rmk"] == DBNull.Value ? string.Empty : row["Bl_rmk"].ToString(),
                Bl_act = row["Bl_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["Bl_act"].ToString()),
                bl_expires_dt = row["bl_expires_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["bl_expires_dt"].ToString()),
                bl_manual_d_ref = row["bl_manual_d_ref"] == DBNull.Value ? string.Empty : row["bl_manual_d_ref"].ToString(),
                bl_manual_h_ref = row["bl_manual_h_ref"] == DBNull.Value ? string.Empty : row["bl_manual_h_ref"].ToString(),
                bl_manual_m_ref = row["bl_manual_m_ref"] == DBNull.Value ? string.Empty : row["bl_manual_m_ref"].ToString(),
                bl_m_seq_no = row["bl_m_seq_no"] == DBNull.Value ? string.Empty : row["bl_m_seq_no"].ToString(),
                bl_pack_uom = row["bl_pack_uom"] == DBNull.Value ? string.Empty : row["bl_pack_uom"].ToString(),
                bl_terminal = row["bl_terminal"] == DBNull.Value ? string.Empty : row["bl_terminal"].ToString(),
                bl_do_print = row["bl_do_print"] == DBNull.Value ? 0 : Convert.ToInt32(row["bl_do_print"].ToString()),
                bl_pay_mode = row["bl_pay_mode"] == DBNull.Value ? string.Empty : row["bl_pay_mode"].ToString(),
                bl_vsl_oper = row["bl_vsl_oper"] == DBNull.Value ? string.Empty : row["bl_vsl_oper"].ToString(),
                bl_cntr_oper = row["bl_cntr_oper"] == DBNull.Value ? string.Empty : row["bl_cntr_oper"].ToString(),
                bl_trans_mode = row["bl_trans_mode"] == DBNull.Value ? string.Empty : row["bl_trans_mode"].ToString(),
                bl_cre_by = row["bl_cre_by"] == DBNull.Value ? string.Empty : row["bl_cre_by"].ToString(),
                bl_cre_dt = row["bl_cre_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["bl_cre_dt"].ToString()),
                bl_mod_by = row["bl_mod_by"] == DBNull.Value ? string.Empty : row["bl_mod_by"].ToString(),
                bl_mod_dt = row["bl_mod_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["bl_mod_dt"].ToString()),
                bl_or_seq = row["bl_or_seq"] == DBNull.Value ? string.Empty : row["bl_or_seq"].ToString(),
                
            };
        }
    }
}

