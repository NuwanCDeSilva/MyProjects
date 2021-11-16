using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.BusinessObjects.Genaral
{
    public class PortAgentDet
    {
        public Int32 CNT { get; set; }
        public String BL_PORT_LOAD { get; set; }
        public String PA_PRT_NAME { get; set; }
        public String BL_DEL_AGENT_CD { get; set; }
        public String BL_DEL_AGENT_NAME { get; set; }
        public String CONTAINERFCL { get; set; }
        
        public static PortAgentDet Converter(DataRow row)
        {
            return new PortAgentDet
            {
                CNT = row["CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["CNT"].ToString()),
                BL_PORT_LOAD = row["BL_PORT_LOAD"] == DBNull.Value ? string.Empty : row["BL_PORT_LOAD"].ToString(),
                PA_PRT_NAME = row["PA_PRT_NAME"] == DBNull.Value ? string.Empty : row["PA_PRT_NAME"].ToString(),
                BL_DEL_AGENT_CD = row["BL_DEL_AGENT_CD"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_CD"].ToString(),
                BL_DEL_AGENT_NAME = row["BL_DEL_AGENT_NAME"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_NAME"].ToString()
            };
        }

       
    }
    public class BarChartDataPort
    {
        public string BL_PORT_LOAD { get; set; }
        public string PA_PRT_NAME { get; set; }
        public Int32 CNT { get; set; }

        public static BarChartDataPort Converter(DataRow row)
        {
            return new BarChartDataPort
            {
                BL_PORT_LOAD = row["BL_PORT_LOAD"] == DBNull.Value ? string.Empty : row["BL_PORT_LOAD"].ToString(),
                PA_PRT_NAME = row["PA_PRT_NAME"] == DBNull.Value ? string.Empty : row["PA_PRT_NAME"].ToString(),
                CNT = row["CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["CNT"].ToString())
            };
        }
    }
    public class BarChartDataAgent
    {
        public string BL_DEL_AGENT_CD { get; set; }
        public string BL_DEL_AGENT_NAME { get; set; }
        public Int32 CNT { get; set; }

        public static BarChartDataAgent Converter(DataRow row)
        {
            return new BarChartDataAgent
            {
                BL_DEL_AGENT_CD = row["BL_DEL_AGENT_CD"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_CD"].ToString(),
                BL_DEL_AGENT_NAME = row["BL_DEL_AGENT_NAME"] == DBNull.Value ? string.Empty : row["BL_DEL_AGENT_NAME"].ToString(),
                CNT = row["CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["CNT"].ToString())
            };
        }
    }
}
