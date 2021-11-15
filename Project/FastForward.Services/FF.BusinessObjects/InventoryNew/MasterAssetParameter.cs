using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class MasterAssetParameter
    {      

        public string map_com { get; set; }
        public string map_cat1 { get; set; }
        public string map_cat2 { get; set; }
        public string map_dep_mth { get; set; }
        public decimal map_dep_val { get; set; }
        public string map_dep_base { get; set; }
        public string map_cre_by { get; set; }
        public DateTime map_cre_dt { get; set; }
        public int map_act { get; set; }
        public string map_mod_by { get; set; }
        public DateTime map_mod_dt { get; set; }

        public string map_stus { get; set; }
        public int map_dis { get; set; }

        public string map_session { get; set; }

        public MasterAssetParameter()
        { }
    }
}
