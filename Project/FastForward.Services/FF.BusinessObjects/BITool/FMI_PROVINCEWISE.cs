using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class FMI_PROVINCEWISE
    {
        public string province_name { get; set; }
        public List<FMI_CHANNELWISE> channel_list { get; set; }
    }
}
