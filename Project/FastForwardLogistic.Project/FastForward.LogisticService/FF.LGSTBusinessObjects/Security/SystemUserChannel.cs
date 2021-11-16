using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

// Class added by Chathura on 14-Sep-2017
namespace FF.BusinessObjects.Security
{
    public class SystemUserChannel
    {
        #region private members and public property definition
        private string _msc_cd;
        public string Msc_cd
        {
            get { return _msc_cd; }
            set { _msc_cd = value; }
        }

        private string _msc_desc;
        public string Msc_desc
        {
            get { return _msc_desc; }
            set { _msc_desc = value; }
        }

        #endregion

        public static SystemUserChannel Converter(DataRow row)
        {
            return new SystemUserChannel
            {
                Msc_cd = ((row["MSC_CD"] == DBNull.Value) ? string.Empty : row["MSC_CD"].ToString()),
                Msc_desc = (row["MSC_DESC"] == DBNull.Value ? string.Empty : row["MSC_DESC"].ToString())
            };
        }
    }
}
