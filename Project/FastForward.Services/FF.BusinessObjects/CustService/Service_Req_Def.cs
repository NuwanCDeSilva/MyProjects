using System;
using System.Data;

namespace FF.BusinessObjects
{

    public class Service_Req_Def
    {
        private Boolean _srdf_act;
        private string _srdf_cre_by;
        private DateTime _srdf_cre_dt;
        private int _srdf_def_line;
        private string _srdf_def_rmk;
        private string _srdf_def_tp;
        private string _srdf_mod_by;
        private DateTime _srdf_mod_dt;
        private int _srdf_req_line;
        private string _srdf_req_no;
        private Int32 _srdf_seq_no;
        private string _srdf_stage;
        private string _SDT_DESC;

        public String jrd_itm_cd { get; set; }

        public string jrd_ser1 { get; set; }


        public string SDT_DESC
        {
            get { return _SDT_DESC; }
            set { _SDT_DESC = value; }
        }

        public Boolean Srdf_act
        {
            get { return _srdf_act; }
            set { _srdf_act = value; }
        }
        public string Srdf_cre_by
        {
            get { return _srdf_cre_by; }
            set { _srdf_cre_by = value; }
        }
        public DateTime Srdf_cre_dt
        {
            get { return _srdf_cre_dt; }
            set { _srdf_cre_dt = value; }
        }
        public int Srdf_def_line
        {
            get { return _srdf_def_line; }
            set { _srdf_def_line = value; }
        }
        public string Srdf_def_rmk
        {
            get { return _srdf_def_rmk; }
            set { _srdf_def_rmk = value; }
        }
        public string Srdf_def_tp
        {
            get { return _srdf_def_tp; }
            set { _srdf_def_tp = value; }
        }
        public string Srdf_mod_by
        {
            get { return _srdf_mod_by; }
            set { _srdf_mod_by = value; }
        }
        public DateTime Srdf_mod_dt
        {
            get { return _srdf_mod_dt; }
            set { _srdf_mod_dt = value; }
        }
        public int Srdf_req_line
        {
            get { return _srdf_req_line; }
            set { _srdf_req_line = value; }
        }
        public string Srdf_req_no
        {
            get { return _srdf_req_no; }
            set { _srdf_req_no = value; }
        }
        public Int32 Srdf_seq_no
        {
            get { return _srdf_seq_no; }
            set { _srdf_seq_no = value; }
        }
        public string Srdf_stage
        {
            get { return _srdf_stage; }
            set { _srdf_stage = value; }
        }

        public static Service_Req_Def Converter(DataRow row)
        {
            return new Service_Req_Def
            {
                Srdf_act = row["SRDF_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SRDF_ACT"]),
                Srdf_cre_by = row["SRDF_CRE_BY"] == DBNull.Value ? string.Empty : row["SRDF_CRE_BY"].ToString(),
                Srdf_cre_dt = row["SRDF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_CRE_DT"]),
                Srdf_def_line = row["SRDF_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["SRDF_DEF_LINE"]),
                Srdf_def_rmk = row["SRDF_DEF_RMK"] == DBNull.Value ? string.Empty : row["SRDF_DEF_RMK"].ToString(),
                Srdf_def_tp = row["SRDF_DEF_TP"] == DBNull.Value ? string.Empty : row["SRDF_DEF_TP"].ToString(),
                Srdf_mod_by = row["SRDF_MOD_BY"] == DBNull.Value ? string.Empty : row["SRDF_MOD_BY"].ToString(),
                Srdf_mod_dt = row["SRDF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_MOD_DT"]),
                Srdf_req_line = row["SRDF_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["SRDF_REQ_LINE"]),
                Srdf_req_no = row["SRDF_REQ_NO"] == DBNull.Value ? string.Empty : row["SRDF_REQ_NO"].ToString(),
                Srdf_seq_no = row["SRDF_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_SEQ_NO"]),
                Srdf_stage = row["SRDF_STAGE"] == DBNull.Value ? string.Empty : row["SRDF_STAGE"].ToString()
            };
        }

        public static Service_Req_Def Converter_req(DataRow row)
        {
            return new Service_Req_Def
            {
                Srdf_act = row["SRDF_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["SRDF_ACT"]),
                Srdf_cre_by = row["SRDF_CRE_BY"] == DBNull.Value ? string.Empty : row["SRDF_CRE_BY"].ToString(),
                Srdf_cre_dt = row["SRDF_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_CRE_DT"]),
                Srdf_def_line = row["SRDF_DEF_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["SRDF_DEF_LINE"]),
                Srdf_def_rmk = row["SRDF_DEF_RMK"] == DBNull.Value ? string.Empty : row["SRDF_DEF_RMK"].ToString(),
                Srdf_def_tp = row["SRDF_DEF_TP"] == DBNull.Value ? string.Empty : row["SRDF_DEF_TP"].ToString(),
                Srdf_mod_by = row["SRDF_MOD_BY"] == DBNull.Value ? string.Empty : row["SRDF_MOD_BY"].ToString(),
                Srdf_mod_dt = row["SRDF_MOD_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["SRDF_MOD_DT"]),
                Srdf_req_line = row["SRDF_REQ_LINE"] == DBNull.Value ? 0 : Convert.ToInt16(row["SRDF_REQ_LINE"]),
                Srdf_req_no = row["SRDF_REQ_NO"] == DBNull.Value ? string.Empty : row["SRDF_REQ_NO"].ToString(),
                Srdf_seq_no = row["SRDF_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["SRDF_SEQ_NO"]),
                Srdf_stage = row["SRDF_STAGE"] == DBNull.Value ? string.Empty : row["SRDF_STAGE"].ToString(),
                _SDT_DESC = row["SDT_DESC"] == DBNull.Value ? string.Empty : row["SDT_DESC"].ToString(),
                jrd_itm_cd = row["jrd_itm_cd"] == DBNull.Value ? string.Empty : row["jrd_itm_cd"].ToString(),
                jrd_ser1 = row["jrd_ser1"] == DBNull.Value ? string.Empty : row["jrd_ser1"].ToString()
            };
        }
    }
}

