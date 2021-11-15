using System;
using System.Data;

namespace FF.BusinessObjects
{



    public class TBS_IMG_UPLOAD
    {
        public Int32 Jbimg_seq { get; set; }
        public String Jbimg_jobno { get; set; }
        public Int32 Jbimg_jobline { get; set; }
        public String Jbimg_ser { get; set; }
        public Int32 Jbimg_img_line { get; set; }
        public String Jbimg_img_path { get; set; }
        public String Jbimg_img { get; set; }
        public static TBS_IMG_UPLOAD Converter(DataRow row)
        {
            return new TBS_IMG_UPLOAD
            {
                Jbimg_seq = row["JBIMG_SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBIMG_SEQ"].ToString()),
                Jbimg_jobno = row["JBIMG_JOBNO"] == DBNull.Value ? string.Empty : row["JBIMG_JOBNO"].ToString(),
                Jbimg_jobline = row["JBIMG_JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBIMG_JOBLINE"].ToString()),
                Jbimg_ser = row["JBIMG_SER"] == DBNull.Value ? string.Empty : row["JBIMG_SER"].ToString(),
                Jbimg_img_line = row["JBIMG_IMG_LINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JBIMG_IMG_LINE"].ToString()),
                Jbimg_img_path = row["JBIMG_IMG_PATH"] == DBNull.Value ? string.Empty : row["JBIMG_IMG_PATH"].ToString(),
                Jbimg_img = row["JBIMG_IMG"] == DBNull.Value ? string.Empty : row["JBIMG_IMG"].ToString()
            };
        }
    }
}

