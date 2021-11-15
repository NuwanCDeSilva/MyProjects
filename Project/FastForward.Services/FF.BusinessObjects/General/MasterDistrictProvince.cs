using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    public class DistrictProvince
    {
        #region Private Members
        private string _mds_district;
        private string _mds_dist_cd;
        private string _mds_province;
        private string _mds_prov_cd;
        private Boolean _mst_act;
        #endregion

        public string Mds_district { get { return _mds_district; } set { _mds_district = value; } }
        public string Mds_dist_cd { get { return _mds_dist_cd; } set { _mds_dist_cd = value; } }
        public string Mds_province { get { return _mds_province; } set { _mds_province = value; } }
        public string Mds_prov_cd { get { return _mds_prov_cd; } set { _mds_prov_cd = value; } }
        public Boolean Mst_act { get { return _mst_act; } set { _mst_act = value; } }

        public static DistrictProvince Converter(DataRow row)
        {
            return new DistrictProvince
            {
                Mds_district = row["MDS_DISTRICT"] == DBNull.Value ? string.Empty : row["MDS_DISTRICT"].ToString(),
                Mds_dist_cd = row["MDS_DIST_CD"] == DBNull.Value ? string.Empty : row["MDS_DIST_CD"].ToString(),
                Mds_province = row["MDS_PROVINCE"] == DBNull.Value ? string.Empty : row["MDS_PROVINCE"].ToString(),
                Mds_prov_cd = row["MDS_PROV_CD"] == DBNull.Value ? string.Empty : row["MDS_PROV_CD"].ToString(),
                Mst_act = row["MST_ACT"] == DBNull.Value ? false : Convert.ToBoolean(row["MST_ACT"])

            };
        }

    }
}
