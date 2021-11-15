using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.MessagePortal
{
    public class DocumentOutItmModel
    {
        public string ITEMCD{get;set;}
        public string MODEL{get;set;}
        public string ITEMDES{get;set;}
        public string ITEMSTATUS{get;set;}
        public decimal QTY{get;set;}
        public string TOLOC{get;set;}
        public string TOLOCDESC{get;set;}
        public string FROMLOC{get;set;}
        public string FROMLOCDESC{get;set;}
        public string ITMSTUSDESC{get;set;}
        public DateTime DOCDT{get;set;}
        public string DOCNO{get;set;}
        public string OTHDOCNO{get;set;}
        public string REMARKS { get; set; }
        public static DocumentOutItmModel Converter(DataRow row)
        {
            return new DocumentOutItmModel
            {
                ITEMCD = row["ITEMCD"] == DBNull.Value ? string.Empty : row["ITEMCD"].ToString(),
                MODEL = row["MODEL"] == DBNull.Value ? string.Empty : row["MODEL"].ToString(),
                ITEMDES = row["ITEMDES"] == DBNull.Value ? string.Empty : row["ITEMDES"].ToString(),
                ITEMSTATUS = row["ITEMSTATUS"] == DBNull.Value ? string.Empty : row["ITEMSTATUS"].ToString(),
                QTY = row["QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["QTY"].ToString()),
                TOLOC = row["TOLOC"] == DBNull.Value ? string.Empty : row["TOLOC"].ToString(),
                TOLOCDESC = row["TOLOCDESC"] == DBNull.Value ? string.Empty : row["TOLOCDESC"].ToString(),
                FROMLOC = row["FROMLOC"] == DBNull.Value ? string.Empty : row["FROMLOC"].ToString(),
                FROMLOCDESC = row["FROMLOCDESC"] == DBNull.Value ? string.Empty : row["FROMLOCDESC"].ToString(),
                ITMSTUSDESC = row["ITMSTUSDESC"] == DBNull.Value ? string.Empty : row["ITMSTUSDESC"].ToString(),
                DOCDT = row["DOCDT"] == DBNull.Value ? DateTime.MinValue :Convert.ToDateTime(row["DOCDT"].ToString()),
                DOCNO = row["DOCNO"] == DBNull.Value ? string.Empty : row["DOCNO"].ToString(),
                OTHDOCNO = row["OTHDOCNO"] == DBNull.Value ? string.Empty : row["OTHDOCNO"].ToString(),
                REMARKS = row["REMARKS"] == DBNull.Value ? string.Empty : row["REMARKS"].ToString()

            };
        }
    }
}
