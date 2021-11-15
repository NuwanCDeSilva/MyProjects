using System;
using System.Data;

namespace FF.BusinessObjects
{
    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // Computer :- ITPD11  | User :- suneth On 05-Feb-2015 10:09:37
    //===========================================================================================================

    public class ImageUploadDTO
    {
        public String JobNumber { get; set; }
        public Int32 JobLine { get; set; }
        public String ImagePath { get; set; }
        public String FileName { get; set; }

        public byte[] image { get; set; }

        //kapila 16/2/2016
        public String SerialNo { get; set; }
        public Int32 ImageLine { get; set; }

       
        public static ImageUploadDTO Converter(DataRow row)
        {
            return new ImageUploadDTO
            {
                JobNumber = row["JOBNUMBER"] == DBNull.Value ? string.Empty : row["JOBNUMBER"].ToString(),
                JobLine = row["JOBLINE"] == DBNull.Value ? 0 : Convert.ToInt32(row["JOBLINE"].ToString()),
                ImagePath = row["IMAGEPATH"] == DBNull.Value ? string.Empty : row["IMAGEPATH"].ToString()
            
            };
        }
    }
}
