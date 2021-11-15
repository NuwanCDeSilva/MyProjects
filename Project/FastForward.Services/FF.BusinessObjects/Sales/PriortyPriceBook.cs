using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{


   public class PriortyPriceBook
        {
            #region Private Members
            private string _sppb_com;
            private string _sppb_pb;
            private string _sppb_pb_lvl;
            private string _sppb_pc;
            #endregion

            public string Sppb_com
            {
                get { return _sppb_com; }
                set { _sppb_com = value; }
            }
            public string Sppb_pb
            {
                get { return _sppb_pb; }
                set { _sppb_pb = value; }
            }
            public string Sppb_pb_lvl
            {
                get { return _sppb_pb_lvl; }
                set { _sppb_pb_lvl = value; }
            }
            public string Sppb_pc
            {
                get { return _sppb_pc; }
                set { _sppb_pc = value; }
            }

            public static PriortyPriceBook Converter(DataRow row)
            {
                return new PriortyPriceBook
                {
                    Sppb_com = row["SPPB_COM"] == DBNull.Value ? string.Empty : row["SPPB_COM"].ToString(),
                    Sppb_pb = row["SPPB_PB"] == DBNull.Value ? string.Empty : row["SPPB_PB"].ToString(),
                    Sppb_pb_lvl = row["SPPB_PB_LVL"] == DBNull.Value ? string.Empty : row["SPPB_PB_LVL"].ToString(),
                    Sppb_pc = row["SPPB_PC"] == DBNull.Value ? string.Empty : row["SPPB_PC"].ToString()

                };
            }
        }
    
}
