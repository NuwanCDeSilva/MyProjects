using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
  public  class Forcstdata
    {
      public int sfh_calfrm_mnt { get; set; }
      public int sfd_qty { get; set; }
      public string sfh_def_by { get; set; }
      public string sfh_def_cd { get; set; }
      public DateTime sfh_pd_frm { get; set; }

      public DateTime sfh_pd_to { get; set; }
      public string sfd_itm { get; set; }
      public string sfd_model { get; set; }
      public string sfd_brnd { get; set; }
      public string sfd_cat1 { get; set; }
      public string sfd_cat2 { get; set; }
      public static Forcstdata Converter(DataRow row)
      {
          return new Forcstdata
          {
              sfh_calfrm_mnt = row["sfh_calfrm_mnt"] == DBNull.Value ? 0 : Convert.ToInt32(row["sfh_calfrm_mnt"].ToString()),
              sfd_qty = row["sfd_qty"] == DBNull.Value ? 0 : Convert.ToInt32(row["sfd_qty"].ToString()),
              sfh_def_by = row["sfh_def_by"] == DBNull.Value ? string.Empty : row["sfh_def_by"].ToString(),
              sfh_def_cd = row["sfh_def_cd"] == DBNull.Value ? string.Empty : row["sfh_def_cd"].ToString(),
              sfh_pd_frm = row["sfh_pd_frm"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sfh_pd_frm"].ToString()),
              sfh_pd_to = row["sfh_pd_to"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["sfh_pd_to"].ToString()),
              sfd_itm = row["sfd_itm"] == DBNull.Value ? string.Empty : row["sfd_itm"].ToString(),
              sfd_model = row["sfd_model"] == DBNull.Value ? string.Empty : row["sfd_model"].ToString(),
              sfd_brnd = row["sfd_brnd"] == DBNull.Value ? string.Empty : row["sfd_brnd"].ToString(),
              sfd_cat1 = row["sfd_cat1"] == DBNull.Value ? string.Empty : row["sfd_cat1"].ToString(),
              sfd_cat2 = row["sfd_cat2"] == DBNull.Value ? string.Empty : row["sfd_cat2"].ToString(),

          };
      } 

    }
}
