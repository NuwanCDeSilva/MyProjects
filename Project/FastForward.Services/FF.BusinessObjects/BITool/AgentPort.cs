using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.BITool
{
    public class AgentPort
    {
        public string type { get; set; }
        public bool showInLegend { get; set; }
        public string name { get; set; }
        public List<DataPnt> dataPoints { get; set; }
    }
    public class DataPnt
    {
        public Int32 y { get; set; }
        public string label { get; set; }
    }
    public class highChtdata
    {
        public string name{get;set;}
        public List<decimal> data { get; set; } 
    }
    public class BarChartData
    {
        public string FRM_PORT { get; set; }
        public string PROT_NAME { get; set; }
        public string AGENT_CD { get; set; }
        public string AGENT_NAME { get; set; }
        public string CONTAINERFCL { get; set; }
        public Int32 CNT { get; set; }

        public static BarChartData Converter(DataRow row)
        {
            return new BarChartData
            {
                FRM_PORT = row["FRM_PORT"] == DBNull.Value ? string.Empty : row["FRM_PORT"].ToString(),
                PROT_NAME = row["PROT_NAME"] == DBNull.Value ? string.Empty : row["PROT_NAME"].ToString(),
                AGENT_CD = row["AGENT_CD"] == DBNull.Value ? string.Empty : row["AGENT_CD"].ToString(),
                AGENT_NAME = row["AGENT_NAME"] == DBNull.Value ? string.Empty : row["AGENT_NAME"].ToString(),
                CNT = row["CNT"] == DBNull.Value ? 0 :Convert.ToInt32( row["CNT"].ToString())
            };
        }
    }
    public class BarChartDataPort
    {
        public string FRM_PORT { get; set; }
        public string PROT_NAME { get; set; }
        public Int32 CNT { get; set; }

        public static BarChartDataPort Converter(DataRow row)
        {
            return new BarChartDataPort
            {
                FRM_PORT = row["FRM_PORT"] == DBNull.Value ? string.Empty : row["FRM_PORT"].ToString(),
                PROT_NAME = row["PROT_NAME"] == DBNull.Value ? string.Empty : row["PROT_NAME"].ToString(),
                CNT = row["CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["CNT"].ToString())
            };
        }
    }
    public class BarChartDataAgent
    {
        public string AGENT_CD { get; set; }
        public string AGENT_NAME { get; set; }
        public Int32 CNT { get; set; }

        public static BarChartDataAgent Converter(DataRow row)
        {
            return new BarChartDataAgent
            {
                AGENT_CD = row["AGENT_CD"] == DBNull.Value ? string.Empty : row["AGENT_CD"].ToString(),
                AGENT_NAME = row["AGENT_NAME"] == DBNull.Value ? string.Empty : row["AGENT_NAME"].ToString(),
                CNT = row["CNT"] == DBNull.Value ? 0 : Convert.ToInt32(row["CNT"].ToString())
            };
        }
    }
}
