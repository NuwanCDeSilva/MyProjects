using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1
    // Computer :- ITPD11  | User :- suneth On 01-Dec-2014 04:39:03
    //===========================================================================================================

    public class Service_Confirm_detail
    {
        public Int32 Jcd_seq { get; set; }
        public String Jcd_no { get; set; }
        public Int32 Jcd_line { get; set; }
        public String Jcd_jobno { get; set; }
        public Int32 Jcd_joblineno { get; set; }
        public String Jcd_itmcd { get; set; }
        public String Jcd_itmstus { get; set; }
        public Decimal Jcd_qty { get; set; }
        public Decimal Jcd_balqty { get; set; }
        public String Jcd_pb { get; set; }
        public String Jcd_pblvl { get; set; }
        public Decimal Jcd_unitprice { get; set; }
        public Decimal Jcd_amt { get; set; }
        public Decimal Jcd_tax_rt { get; set; }
        public Decimal Jcd_tax { get; set; }
        public Decimal Jcd_dis_rt { get; set; }
        public Decimal Jcd_dis { get; set; }
        public Decimal Jcd_net_amt { get; set; }
        public String Jcd_itmtp { get; set; }
        public Int32 Jcd_foc { get; set; }
        public String Jcd_costelement { get; set; }
        public String Jcd_docno { get; set; }
        public String Jcd_rmk { get; set; }
        public Int32 Jcd_costsheetlineno { get; set; }
        public String Jcd_jobitmcd { get; set; }
        public String Jcd_jobitmser { get; set; }
        public String Jcd_jobwarrno { get; set; }
        public Decimal Jcd_pbprice { get; set; }
        public Int32 Jcd_pbseqno { get; set; }
        public Int32 Jcd_pbitmseqno { get; set; }
        public String Jcd_itmdesc { get; set; }
        public String Jcd_itmmodel { get; set; }
        public String Jcd_itmbrand { get; set; }
        public String Jcd_itmuom { get; set; }
        public String Jcd_mov_doc { get; set; }
        public Int32 Jcd_itmline { get; set; }
        public Int32 Jcd_batchline { get; set; }
        public Int32 Jcd_serline { get; set; }
        public Int32 Jcd_ser_id { get; set; }
        public Int32 Jcd_gatepass_raised { get; set; }
        public String Jcd_invtype { get; set; }
        public Int32 Jcd_iswarr { get; set; }
        public String Jcd_movedoctp { get; set; }

        public String Jcd_itmcd_DESC { get; set; }
        public String Jcd_itmstus_DESC { get; set; }
        public string IsNewRecord { get; set; }

        public Int32 WarrantyRepirod { get; set; }
        public String WarrantyRemark { get; set; }
        public String PrintCode { get; set; }
        public Boolean IsPRint { get; set; }
        public Decimal costPrice { get; set; }

        public String Jcd_cuscd { get; set; }
        public String Jcd_cusname { get; set; }
        public String Jcd_cusadd1 { get; set; }
        public String Jcd_cusadd2 { get; set; }
        public Int32 Jcd_isadditm { get; set; }
        public string Jcd_mainitmcd { get; set; }
        public string Jcd_mainitmdesc { get; set; }

        public bool IsPoItem { get; set; }


        public static Service_Confirm_detail Converter(DataRow row)
        {
            return new Service_Confirm_detail
            {
                Jcd_seq = row["JCD_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_SEQ"].ToString()),
                Jcd_no = row["JCD_NO"] == DBNull.Value ? string.Empty : row["JCD_NO"].ToString(),
                Jcd_line = row["JCD_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_LINE"].ToString()),
                Jcd_jobno = row["JCD_JOBNO"] == DBNull.Value ? string.Empty : row["JCD_JOBNO"].ToString(),
                Jcd_joblineno = row["JCD_JOBLINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_JOBLINENO"].ToString()),
                Jcd_itmcd = row["JCD_ITMCD"] == DBNull.Value ? string.Empty : row["JCD_ITMCD"].ToString(),
                Jcd_itmstus = row["JCD_ITMSTUS"] == DBNull.Value ? string.Empty : row["JCD_ITMSTUS"].ToString(),
                Jcd_qty = row["JCD_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_QTY"].ToString()),
                Jcd_balqty = row["JCD_BALQTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_BALQTY"].ToString()),
                Jcd_pb = row["JCD_PB"] == DBNull.Value ? string.Empty : row["JCD_PB"].ToString(),
                Jcd_pblvl = row["JCD_PBLVL"] == DBNull.Value ? string.Empty : row["JCD_PBLVL"].ToString(),
                Jcd_unitprice = row["JCD_UNITPRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_UNITPRICE"].ToString()),
                Jcd_amt = row["JCD_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_AMT"].ToString()),
                Jcd_tax_rt = row["JCD_TAX_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_TAX_RT"].ToString()),
                Jcd_tax = row["JCD_TAX"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_TAX"].ToString()),
                Jcd_dis_rt = row["JCD_DIS_RT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_DIS_RT"].ToString()),
                Jcd_dis = row["JCD_DIS"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_DIS"].ToString()),
                Jcd_net_amt = row["JCD_NET_AMT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_NET_AMT"].ToString()),
                Jcd_itmtp = row["JCD_ITMTP"] == DBNull.Value ? string.Empty : row["JCD_ITMTP"].ToString(),
                Jcd_foc = row["JCD_FOC"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_FOC"].ToString()),
                Jcd_costelement = row["JCD_COSTELEMENT"] == DBNull.Value ? string.Empty : row["JCD_COSTELEMENT"].ToString(),
                Jcd_docno = row["JCD_DOCNO"] == DBNull.Value ? string.Empty : row["JCD_DOCNO"].ToString(),
                Jcd_rmk = row["JCD_RMK"] == DBNull.Value ? string.Empty : row["JCD_RMK"].ToString(),
                Jcd_costsheetlineno = row["JCD_COSTSHEETLINENO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_COSTSHEETLINENO"].ToString()),
                Jcd_jobitmcd = row["JCD_JOBITMCD"] == DBNull.Value ? string.Empty : row["JCD_JOBITMCD"].ToString(),
                Jcd_jobitmser = row["JCD_JOBITMSER"] == DBNull.Value ? string.Empty : row["JCD_JOBITMSER"].ToString(),
                Jcd_jobwarrno = row["JCD_JOBWARRNO"] == DBNull.Value ? string.Empty : row["JCD_JOBWARRNO"].ToString(),
                Jcd_pbprice = row["JCD_PBPRICE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["JCD_PBPRICE"].ToString()),
                Jcd_pbseqno = row["JCD_PBSEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_PBSEQNO"].ToString()),
                Jcd_pbitmseqno = row["JCD_PBITMSEQNO"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_PBITMSEQNO"].ToString()),
                Jcd_itmdesc = row["JCD_ITMDESC"] == DBNull.Value ? string.Empty : row["JCD_ITMDESC"].ToString(),
                Jcd_itmmodel = row["JCD_ITMMODEL"] == DBNull.Value ? string.Empty : row["JCD_ITMMODEL"].ToString(),
                Jcd_itmbrand = row["JCD_ITMBRAND"] == DBNull.Value ? string.Empty : row["JCD_ITMBRAND"].ToString(),
                Jcd_itmuom = row["JCD_ITMUOM"] == DBNull.Value ? string.Empty : row["JCD_ITMUOM"].ToString(),
                Jcd_mov_doc = row["JCD_MOV_DOC"] == DBNull.Value ? string.Empty : row["JCD_MOV_DOC"].ToString(),
                Jcd_itmline = row["JCD_ITMLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_ITMLINE"].ToString()),
                Jcd_batchline = row["JCD_BATCHLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_BATCHLINE"].ToString()),
                Jcd_serline = row["JCD_SERLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_SERLINE"].ToString()),
                Jcd_ser_id = row["JCD_SER_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_SER_ID"].ToString()),
                Jcd_gatepass_raised = row["JCD_GATEPASS_RAISED"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_GATEPASS_RAISED"].ToString()),
                Jcd_invtype = row["JCD_INVTYPE"] == DBNull.Value ? string.Empty : row["JCD_INVTYPE"].ToString(),
                Jcd_iswarr = row["JCD_ISWARR"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_ISWARR"].ToString()),
                Jcd_movedoctp = row["JCD_MOVEDOCTP"] == DBNull.Value ? string.Empty : row["JCD_MOVEDOCTP"].ToString(),

                Jcd_itmcd_DESC = row["JCD_ITMCD_DESC"] == DBNull.Value ? string.Empty : row["JCD_ITMCD_DESC"].ToString(),
                Jcd_itmstus_DESC = row["JCD_ITMSTUS_DESC"] == DBNull.Value ? string.Empty : row["JCD_ITMSTUS_DESC"].ToString(),

                Jcd_cuscd = row["JCD_CUSCD"] == DBNull.Value ? string.Empty : row["JCD_CUSCD"].ToString(),
                Jcd_cusname = row["JCD_CUSNAME"] == DBNull.Value ? string.Empty : row["JCD_CUSNAME"].ToString(),
                Jcd_cusadd1 = row["JCD_CUSADD1"] == DBNull.Value ? string.Empty : row["JCD_CUSADD1"].ToString(),
                Jcd_cusadd2 = row["JCD_CUSADD2"] == DBNull.Value ? string.Empty : row["JCD_CUSADD2"].ToString(),
                Jcd_isadditm = row["JCD_ISADDITM"] == DBNull.Value ? 0 : Convert.ToInt32(row["JCD_ISADDITM"].ToString()),
                Jcd_mainitmcd = row["JCD_MAINITMCD"] == DBNull.Value ? string.Empty : row["JCD_MAINITMCD"].ToString(),
                Jcd_mainitmdesc = row["JCD_MAINITMDESC"] == DBNull.Value ? string.Empty : row["JCD_MAINITMDESC"].ToString()
                
            };
        }
    }
}