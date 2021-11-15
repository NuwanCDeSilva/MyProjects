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
        private bool _tuh_is_take_res;
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
        public bool Tuh_is_take_res { get { return _tuh_is_take_res; } set { _tuh_is_take_res = value; } }

        public static ReptPickHeader ConvertTotal(DataRow row)
        {
            return new ReptPickHeader
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
                Tuh_is_take_res = row["tuh_is_take_res"] == DBNull.Value ? false : Convert.ToBoolean(row["tuh_is_take_res"])
            };
        }

    }
}
