using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

//darshana on 02-11-2012
namespace FF.BusinessObjects
{
    public class Mst_Sys_Para
    {



        public string msp_pty_com { get; set; }
        public string msp_pty_tp { get; set; }
        public string msp_pty_cd { get; set; }
        public string msp_dir_pty_com { get; set; }
        public string msp_dir_pty_tp { get; set; }

        public string msp_dir_pty_cd { get; set; }
        public string msp_rest_type { get; set; }

        public string msp_rest_desc { get; set; }
        public static Mst_Sys_Para Converter(DataRow row)
        {
            return new Mst_Sys_Para
            {

                msp_pty_com = row["msp_pty_com"] == DBNull.Value ? string.Empty : row["msp_pty_com"].ToString(),
                msp_pty_tp = row["msp_pty_tp"] == DBNull.Value ? string.Empty : row["msp_pty_tp"].ToString(),
                msp_pty_cd = row["msp_pty_cd"] == DBNull.Value ? string.Empty : row["msp_pty_cd"].ToString(),
                msp_dir_pty_com = row["msp_dir_pty_com"] == DBNull.Value ? string.Empty : row["msp_dir_pty_com"].ToString(),
                msp_dir_pty_cd = row["msp_dir_pty_cd"] == DBNull.Value ? string.Empty : row["msp_dir_pty_cd"].ToString(),
                msp_rest_type = row["msp_rest_type"] == DBNull.Value ? string.Empty : row["msp_rest_type"].ToString(),
                msp_rest_desc = row["msp_rest_desc"] == DBNull.Value ? string.Empty : row["msp_rest_desc"].ToString()

            };
        }

    }
}
