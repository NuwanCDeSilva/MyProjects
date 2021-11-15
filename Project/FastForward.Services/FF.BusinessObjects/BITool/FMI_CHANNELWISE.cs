using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class FMI_CHANNELWISE
    {
        public string channel_name { get; set; }
        public List<FMI_CATWISE> cat_list { get; set; }
    }
}
