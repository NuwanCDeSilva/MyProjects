using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.TempObject
{
    public class TmpServiceWorkingProcess
    {
        public Int32 Jbd_seq_no { get; set; }
        public string Jbd_jobno { get; set; }
        public Int32 jbd_jobline { get; set; }
        public DateTime sjb_dt { get; set; }
        public string jbd_itm_cd { get; set; }
        public string jbd_ser1 { get; set; }
        public string jbd_warr { get; set; }
        public string jbd_regno { get; set; }
        public string sjb_b_town { get; set; }
        public string sc_direct { get; set; }
        public string sjb_prority { get; set; }
        public DateTime sjb_custexptdt { get; set; }
        public string sc_desc { get; set; }
        public string sc_tp { get; set; }
        public Int32 jbd_act { get; set; }
        public string sjb_b_cust_cd { get; set; }
        public string sjb_cust_name { get; set; }
        public Int32 jbd_isstockupdate { get; set; }
        public string jbd_aodissueloc { get; set; }
        public string jbd_reqno { get; set; }
        public string jbd_aodissueno { get; set; }
        public string mi_model { get; set; }
        public string mi_brand { get; set; }
        public string mi_shortdesc { get; set; }
        public string sjb_cust_cd { get; set; }
        public string jbd_mainjobno { get; set; }
        public string jbd_loc { get; set; }
        public bool _selectLine { get; set; }
        public static TmpServiceWorkingProcess Converter(DataRow row)
        {
            TmpServiceWorkingProcess _obj = new TmpServiceWorkingProcess();
            _obj.Jbd_seq_no = row["Jbd_seq_no"] == DBNull.Value ? 0 : Convert.ToInt32(row["Jbd_seq_no"].ToString());
            _obj.Jbd_jobno = row["Jbd_jobno"] == DBNull.Value ? string.Empty : row["Jbd_jobno"].ToString();
            _obj.jbd_jobline = row["jbd_jobline"] == DBNull.Value ? 0 : Convert.ToInt32(row["jbd_jobline"].ToString());
            _obj.sjb_dt = row["sjb_dt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sjb_dt"].ToString());
            _obj.jbd_itm_cd = row["jbd_itm_cd"] == DBNull.Value ? string.Empty : row["jbd_itm_cd"].ToString();
            _obj.jbd_ser1 = row["jbd_ser1"] == DBNull.Value ? string.Empty : row["jbd_ser1"].ToString();
            _obj.jbd_warr = row["jbd_warr"] == DBNull.Value ? string.Empty : row["jbd_warr"].ToString();
            _obj.jbd_regno = row["jbd_regno"] == DBNull.Value ? string.Empty : row["jbd_regno"].ToString();
            _obj.sjb_b_town = row["sjb_b_town"] == DBNull.Value ? string.Empty : row["jbd_regno"].ToString();
            _obj.sc_direct = row["sc_direct"] == DBNull.Value ? string.Empty : row["sc_direct"].ToString();
            _obj.sjb_prority = row["sjb_prority"] == DBNull.Value ? string.Empty : row["sjb_prority"].ToString();
            _obj.sjb_custexptdt = row["sjb_custexptdt"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sjb_custexptdt"].ToString());
            _obj.sc_desc = row["sc_desc"] == DBNull.Value ? string.Empty : row["sc_desc"].ToString();
            _obj.sc_tp = row["sc_tp"] == DBNull.Value ? string.Empty : row["sc_tp"].ToString();
            _obj.jbd_act = row["Status"] == DBNull.Value ? 0 : Convert.ToInt32(row["Status"].ToString());
            _obj.sjb_b_cust_cd = row["sjb_b_cust_cd"] == DBNull.Value ? string.Empty : row["sjb_b_cust_cd"].ToString();
            _obj.sjb_cust_name = row["sjb_cust_name"] == DBNull.Value ? string.Empty : row["sjb_cust_name"].ToString();
            _obj.jbd_isstockupdate = row["jbd_isstockupdate"] == DBNull.Value ? 0 : Convert.ToInt32(row["jbd_isstockupdate"].ToString());
            _obj.jbd_aodissueloc = row["jbd_aodissueloc"] == DBNull.Value ? string.Empty : row["jbd_aodissueloc"].ToString();
            _obj.jbd_reqno = row["jbd_reqno"] == DBNull.Value ? string.Empty : row["jbd_reqno"].ToString();
            _obj.jbd_aodissueno = row["jbd_aodissueno"] == DBNull.Value ? string.Empty : row["jbd_aodissueno"].ToString();
            _obj.mi_model = row["mi_model"] == DBNull.Value ? string.Empty : row["mi_model"].ToString();
            _obj.mi_brand = row["mi_brand"] == DBNull.Value ? string.Empty : row["mi_brand"].ToString();
            _obj.mi_shortdesc = row["mi_shortdesc"] == DBNull.Value ? string.Empty : row["mi_shortdesc"].ToString();
            //  sjb_cust_cd = row["sjb_cust_cd"] == DBNull.Value ? string.Empty : row["sjb_cust_cd"].ToString(),
            //jbd_mainjobno = row["jbd_mainjobno"] == DBNull.Value ? string.Empty : row["jbd_mainjobno"].ToString(),
            //jbd_loc = row["jbd_loc"] == DBNull.Value ? string.Empty : row["jbd_loc"].ToString()
            return _obj;
        }
    }
}
