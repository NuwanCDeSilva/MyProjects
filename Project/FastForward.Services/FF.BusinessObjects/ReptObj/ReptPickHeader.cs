using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class ReptPickHeader
    {

        #region Private Members
        private DateTime _tuh_cre_dt;
        private Boolean _tuh_direct;
        private string _tuh_doc_no; // Add coulumn TUH_DOC_NO Chamal 02-05-2012
        private string _tuh_doc_tp;
        private Boolean _tuh_ischek_itmstus;
        private Boolean _tuh_ischek_reqqty;
        private Boolean _tuh_ischek_simitm;
        private string _tuh_session_id;
        private Int32 _tuh_usrseq_no;
        //private Int32 _tuh_usr_com;
        private string _tuh_usr_com;
        private string _tuh_usr_id;
        //Edited by Prabhath on 21/08/2013
        private string _tuh_rec_com;
        private string _tuh_rec_loc;
        private bool _tuh_isdirect;
        private string tuh_pro_user;
        private string tuh_usr_loc;
        private string tuh_dir_type;

        //Sahan 10 Nov 2015
        private string tuh_wh_com;
        private string tuh_wh_loc;
        private string tuh_load_bay;

        private bool _tuh_is_take_res;
        private string tuh_base_doc;
        #endregion

        public DateTime Tuh_cre_dt { get { return _tuh_cre_dt; } set { _tuh_cre_dt = value; } }
        public Boolean Tuh_direct { get { return _tuh_direct; } set { _tuh_direct = value; } }
        public string Tuh_doc_no { get { return _tuh_doc_no; } set { _tuh_doc_no = value; } }
        public string Tuh_doc_tp { get { return _tuh_doc_tp; } set { _tuh_doc_tp = value; } }
        public Boolean Tuh_ischek_itmstus { get { return _tuh_ischek_itmstus; } set { _tuh_ischek_itmstus = value; } }
        public Boolean Tuh_ischek_reqqty { get { return _tuh_ischek_reqqty; } set { _tuh_ischek_reqqty = value; } }
        public Boolean Tuh_ischek_simitm { get { return _tuh_ischek_simitm; } set { _tuh_ischek_simitm = value; } }
        public string Tuh_session_id { get { return _tuh_session_id; } set { _tuh_session_id = value; } }
        public Int32 Tuh_usrseq_no { get { return _tuh_usrseq_no; } set { _tuh_usrseq_no = value; } }
        public string Tuh_usr_com { get { return _tuh_usr_com; } set { _tuh_usr_com = value; } }
        public string Tuh_usr_id { get { return _tuh_usr_id; } set { _tuh_usr_id = value; } }
        public string Tuh_rec_com { get { return _tuh_rec_com; } set { _tuh_rec_com = value; } }
        public string Tuh_rec_loc { get { return _tuh_rec_loc; } set { _tuh_rec_loc = value; } }
        public bool Tuh_isdirect { get { return _tuh_isdirect; } set { _tuh_isdirect = value; } }
        public string Tuh_pro_user { get { return tuh_pro_user; } set { tuh_pro_user = value; } }
        public string Tuh_usr_loc { get { return tuh_usr_loc; } set { tuh_usr_loc = value; } }
        public string Tuh_dir_type { get { return tuh_dir_type; } set { tuh_dir_type = value; } }
        public string Tuh_wh_com { get { return tuh_wh_com; } set { tuh_wh_com = value; } }
        public string Tuh_wh_loc { get { return tuh_wh_loc; } set { tuh_wh_loc = value; } }
        public string Tuh_load_bay { get { return tuh_load_bay; } set { tuh_load_bay = value; } }

        public bool Tuh_is_take_res { get { return _tuh_is_take_res; } set { _tuh_is_take_res = value; } }
        public string Tuh_base_doc { get { return tuh_base_doc; } set { tuh_base_doc = value; } }
        public Int32 Tuh_fin_stus { get; set; }
        public DateTime Tuh_fin_time{ get; set; }
        public List<ReptPickSerials> RptSerList = null;
        public bool Is_doc_Partial_save { get; set; }
        public static ReptPickHeader ConvertTotal(DataRow row)
        {
            ReptPickHeader _hdr = new ReptPickHeader();
            _hdr.Tuh_cre_dt = row["TUH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUH_CRE_DT"]);
            _hdr.Tuh_direct = row["TUH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_DIRECT"]);
            _hdr.Tuh_doc_no = row["TUH_DOC_NO"] == DBNull.Value ? string.Empty : row["TUH_DOC_NO"].ToString();
            _hdr.Tuh_doc_tp = row["TUH_DOC_TP"] == DBNull.Value ? string.Empty : row["TUH_DOC_TP"].ToString();
            _hdr.Tuh_ischek_itmstus = row["TUH_ISCHEK_ITMSTUS"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_ITMSTUS"]);
            _hdr.Tuh_ischek_reqqty = row["TUH_ISCHEK_REQQTY"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_REQQTY"]);
            _hdr.Tuh_ischek_simitm = row["TUH_ISCHEK_SIMITM"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_SIMITM"]);
            _hdr.Tuh_session_id = row["TUH_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUH_SESSION_ID"].ToString();
            _hdr.Tuh_usrseq_no = row["TUH_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUH_USRSEQ_NO"]);
            _hdr.Tuh_usr_com = row["TUH_USR_COM"] == DBNull.Value ? string.Empty : row["TUH_USR_COM"].ToString();
            _hdr.Tuh_usr_id = row["TUH_USR_ID"] == DBNull.Value ? string.Empty : row["TUH_USR_ID"].ToString();
            _hdr.Tuh_rec_com = row["Tuh_rec_com"] == DBNull.Value ? string.Empty : row["Tuh_rec_com"].ToString();
            _hdr.Tuh_rec_loc = row["Tuh_rec_loc"] == DBNull.Value ? string.Empty : row["Tuh_rec_loc"].ToString();
            _hdr.Tuh_pro_user = row["Tuh_pro_user"] == DBNull.Value ? string.Empty : row["Tuh_pro_user"].ToString();
            _hdr.Tuh_usr_loc = row["Tuh_usr_loc"] == DBNull.Value ? string.Empty : row["Tuh_usr_loc"].ToString();
            _hdr.Tuh_isdirect = row["Tuh_dir_type"] == DBNull.Value ? false : Convert.ToBoolean(row["Tuh_dir_type"]);
            _hdr.Tuh_wh_com = row["Tuh_wh_com"] == DBNull.Value ? string.Empty : row["Tuh_wh_com"].ToString();
            _hdr.Tuh_wh_loc = row["Tuh_wh_loc"] == DBNull.Value ? string.Empty : row["Tuh_wh_loc"].ToString();
            _hdr.Tuh_load_bay = row["Tuh_load_bay"] == DBNull.Value ? string.Empty : row["Tuh_load_bay"].ToString();
            _hdr.Tuh_is_take_res = row["tuh_is_take_res"] == DBNull.Value ? false : Convert.ToBoolean(row["tuh_is_take_res"]);
            return _hdr;
            /*return new ReptPickHeader
            {
                Tuh_cre_dt = row["TUH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUH_CRE_DT"]),
                Tuh_direct = row["TUH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_DIRECT"]),
                Tuh_doc_no = row["TUH_DOC_NO"] == DBNull.Value ? string.Empty : row["TUH_DOC_NO"].ToString(),
                Tuh_doc_tp = row["TUH_DOC_TP"] == DBNull.Value ? string.Empty : row["TUH_DOC_TP"].ToString(),
                Tuh_ischek_itmstus = row["TUH_ISCHEK_ITMSTUS"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_ITMSTUS"]),
                Tuh_ischek_reqqty = row["TUH_ISCHEK_REQQTY"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_REQQTY"]),
                Tuh_ischek_simitm = row["TUH_ISCHEK_SIMITM"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_SIMITM"]),
                Tuh_session_id = row["TUH_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUH_SESSION_ID"].ToString(),
                Tuh_usrseq_no = row["TUH_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUH_USRSEQ_NO"]),
                Tuh_usr_com = row["TUH_USR_COM"] == DBNull.Value ? string.Empty : row["TUH_USR_COM"].ToString(),
                Tuh_usr_id = row["TUH_USR_ID"] == DBNull.Value ? string.Empty : row["TUH_USR_ID"].ToString(),
                Tuh_rec_com = row["Tuh_rec_com"] == DBNull.Value ? string.Empty : row["Tuh_rec_com"].ToString(),
                Tuh_rec_loc = row["Tuh_rec_loc"] == DBNull.Value ? string.Empty : row["Tuh_rec_loc"].ToString(),
                Tuh_pro_user = row["Tuh_pro_user"] == DBNull.Value ? string.Empty : row["Tuh_pro_user"].ToString(),
                Tuh_usr_loc = row["Tuh_usr_loc"] == DBNull.Value ? string.Empty : row["Tuh_usr_loc"].ToString(),
                Tuh_isdirect = row["Tuh_isdirect"] == DBNull.Value ? false : Convert.ToBoolean(row["Tuh_dir_type"]),
                Tuh_wh_com = row["Tuh_wh_com"] == DBNull.Value ? string.Empty : row["Tuh_wh_com"].ToString(),
                Tuh_wh_loc = row["Tuh_wh_loc"] == DBNull.Value ? string.Empty : row["Tuh_wh_loc"].ToString(),
                Tuh_load_bay = row["Tuh_load_bay"] == DBNull.Value ? string.Empty : row["Tuh_load_bay"].ToString(),
                Tuh_is_take_res = row["tuh_is_take_res"] == DBNull.Value ? false : Convert.ToBoolean(row["tuh_is_take_res"])
            };*/
        }

        //Lakshan 2016 Mar 24
        public static ReptPickHeader ConverterNew(DataRow row)
        {
            return new ReptPickHeader
            {
                Tuh_usrseq_no = row["TUH_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUH_USRSEQ_NO"].ToString()),
                Tuh_usr_id = row["TUH_USR_ID"] == DBNull.Value ? string.Empty : row["TUH_USR_ID"].ToString(),
                Tuh_usr_com = row["TUH_USR_COM"] == DBNull.Value ? string.Empty : row["TUH_USR_COM"].ToString(),
                Tuh_session_id = row["TUH_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUH_SESSION_ID"].ToString(),
                Tuh_cre_dt = row["TUH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUH_CRE_DT"].ToString()),
                Tuh_doc_tp = row["TUH_DOC_TP"] == DBNull.Value ? string.Empty : row["TUH_DOC_TP"].ToString(),
                Tuh_direct = row["TUH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_DIRECT"]),
                Tuh_ischek_itmstus = row["TUH_ISCHEK_ITMSTUS"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_ITMSTUS"]),
                Tuh_ischek_simitm = row["TUH_ISCHEK_SIMITM"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_SIMITM"]),
                Tuh_ischek_reqqty = row["TUH_ISCHEK_REQQTY"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_REQQTY"]),
                Tuh_doc_no = row["TUH_DOC_NO"] == DBNull.Value ? string.Empty : row["TUH_DOC_NO"].ToString(),
                Tuh_rec_com = row["TUH_REC_COM"] == DBNull.Value ? string.Empty : row["TUH_REC_COM"].ToString(),
                Tuh_rec_loc = row["TUH_REC_LOC"] == DBNull.Value ? string.Empty : row["TUH_REC_LOC"].ToString(),
                Tuh_isdirect = row["TUH_ISDIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISDIRECT"]),
                Tuh_pro_user = row["TUH_PRO_USER"] == DBNull.Value ? string.Empty : row["TUH_PRO_USER"].ToString(),
                Tuh_usr_loc = row["TUH_USR_LOC"] == DBNull.Value ? string.Empty : row["TUH_USR_LOC"].ToString(),
                Tuh_dir_type = row["TUH_DIR_TYPE"] == DBNull.Value ? string.Empty : row["TUH_DIR_TYPE"].ToString(),
                Tuh_wh_com = row["TUH_WH_COM"] == DBNull.Value ? string.Empty : row["TUH_WH_COM"].ToString(),
                Tuh_wh_loc = row["TUH_WH_LOC"] == DBNull.Value ? string.Empty : row["TUH_WH_LOC"].ToString(),
                Tuh_load_bay = row["TUH_LOAD_BAY"] == DBNull.Value ? string.Empty : row["TUH_LOAD_BAY"].ToString()
            };
        }

        //Lakshan 2016 Sep 21
        public static ReptPickHeader Converter2(DataRow row)
        {
            return new ReptPickHeader
            {
                Tuh_usrseq_no = row["TUH_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUH_USRSEQ_NO"].ToString()),
                Tuh_usr_id = row["TUH_USR_ID"] == DBNull.Value ? string.Empty : row["TUH_USR_ID"].ToString(),
                Tuh_usr_com = row["TUH_USR_COM"] == DBNull.Value ? string.Empty : row["TUH_USR_COM"].ToString(),
                Tuh_session_id = row["TUH_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUH_SESSION_ID"].ToString(),
                Tuh_cre_dt = row["TUH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUH_CRE_DT"].ToString()),
                Tuh_doc_tp = row["TUH_DOC_TP"] == DBNull.Value ? string.Empty : row["TUH_DOC_TP"].ToString(),
                Tuh_direct = row["TUH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_DIRECT"]),
                Tuh_ischek_itmstus = row["TUH_ISCHEK_ITMSTUS"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_ITMSTUS"]),
                Tuh_ischek_simitm = row["TUH_ISCHEK_SIMITM"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_SIMITM"]),
                Tuh_ischek_reqqty = row["TUH_ISCHEK_REQQTY"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_REQQTY"]),
                Tuh_doc_no = row["TUH_DOC_NO"] == DBNull.Value ? string.Empty : row["TUH_DOC_NO"].ToString(),
                Tuh_rec_com = row["TUH_REC_COM"] == DBNull.Value ? string.Empty : row["TUH_REC_COM"].ToString(),
                Tuh_rec_loc = row["TUH_REC_LOC"] == DBNull.Value ? string.Empty : row["TUH_REC_LOC"].ToString(),
                Tuh_isdirect = row["TUH_ISDIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISDIRECT"]),
                Tuh_pro_user = row["TUH_PRO_USER"] == DBNull.Value ? string.Empty : row["TUH_PRO_USER"].ToString(),
                Tuh_usr_loc = row["TUH_USR_LOC"] == DBNull.Value ? string.Empty : row["TUH_USR_LOC"].ToString(),
                Tuh_dir_type = row["TUH_DIR_TYPE"] == DBNull.Value ? string.Empty : row["TUH_DIR_TYPE"].ToString(),
                Tuh_wh_com = row["TUH_WH_COM"] == DBNull.Value ? string.Empty : row["TUH_WH_COM"].ToString(),
                Tuh_wh_loc = row["TUH_WH_LOC"] == DBNull.Value ? string.Empty : row["TUH_WH_LOC"].ToString(),
                Tuh_load_bay = row["TUH_LOAD_BAY"] == DBNull.Value ? string.Empty : row["TUH_LOAD_BAY"].ToString(),
                Tuh_fin_stus = row["tus_fin_stus"] == DBNull.Value ? 0 : Convert.ToInt32(row["tus_fin_stus"])
            };
        }
        public static ReptPickHeader Converter3(DataRow row)
        {
            return new ReptPickHeader
            {
                Tuh_usrseq_no = row["TUH_USRSEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["TUH_USRSEQ_NO"].ToString()),
                Tuh_usr_id = row["TUH_USR_ID"] == DBNull.Value ? string.Empty : row["TUH_USR_ID"].ToString(),
                Tuh_usr_com = row["TUH_USR_COM"] == DBNull.Value ? string.Empty : row["TUH_USR_COM"].ToString(),
                Tuh_session_id = row["TUH_SESSION_ID"] == DBNull.Value ? string.Empty : row["TUH_SESSION_ID"].ToString(),
                Tuh_cre_dt = row["TUH_CRE_DT"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["TUH_CRE_DT"].ToString()),
                Tuh_doc_tp = row["TUH_DOC_TP"] == DBNull.Value ? string.Empty : row["TUH_DOC_TP"].ToString(),
                Tuh_direct = row["TUH_DIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_DIRECT"]),
                Tuh_ischek_itmstus = row["TUH_ISCHEK_ITMSTUS"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_ITMSTUS"]),
                Tuh_ischek_simitm = row["TUH_ISCHEK_SIMITM"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_SIMITM"]),
                Tuh_ischek_reqqty = row["TUH_ISCHEK_REQQTY"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISCHEK_REQQTY"]),
                Tuh_doc_no = row["TUH_DOC_NO"] == DBNull.Value ? string.Empty : row["TUH_DOC_NO"].ToString(),
                Tuh_rec_com = row["TUH_REC_COM"] == DBNull.Value ? string.Empty : row["TUH_REC_COM"].ToString(),
                Tuh_rec_loc = row["TUH_REC_LOC"] == DBNull.Value ? string.Empty : row["TUH_REC_LOC"].ToString(),
                Tuh_isdirect = row["TUH_ISDIRECT"] == DBNull.Value ? false : Convert.ToBoolean(row["TUH_ISDIRECT"]),
                Tuh_pro_user = row["TUH_PRO_USER"] == DBNull.Value ? string.Empty : row["TUH_PRO_USER"].ToString(),
                Tuh_usr_loc = row["TUH_USR_LOC"] == DBNull.Value ? string.Empty : row["TUH_USR_LOC"].ToString(),
                Tuh_dir_type = row["TUH_DIR_TYPE"] == DBNull.Value ? string.Empty : row["TUH_DIR_TYPE"].ToString(),
                Tuh_wh_com = row["TUH_WH_COM"] == DBNull.Value ? string.Empty : row["TUH_WH_COM"].ToString(),
                Tuh_wh_loc = row["TUH_WH_LOC"] == DBNull.Value ? string.Empty : row["TUH_WH_LOC"].ToString(),
                Tuh_load_bay = row["TUH_LOAD_BAY"] == DBNull.Value ? string.Empty : row["TUH_LOAD_BAY"].ToString(),
                Tuh_fin_stus = row["tus_fin_stus"] == DBNull.Value ? 0 : Convert.ToInt32(row["tus_fin_stus"]),
                Tuh_fin_time = row["tuh_fin_time"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["tuh_fin_time"])
            };
        }
    }
}
