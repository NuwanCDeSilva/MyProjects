using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects
{
    public class SaleDisDefinition
    {
        private string _itemcd;
        private decimal _pre;
        private decimal _value;
        private DateTime _frmDate;
        private DateTime _toDate;

        public string itemcd { get { return _itemcd; } set { _itemcd = value; } }
        public decimal pre { get { return _pre; } set { _pre = value; } }
        public decimal value { get { return _value; } set { _value = value; } }
        public DateTime frmDate { get { return _frmDate; } set { _frmDate = value; } }
        public DateTime toDate { get { return _toDate; } set { _toDate = value; } }
         
        public static SaleDisDefinition ConvertTotal(DataRow row)
        {
            return new SaleDisDefinition
            {
                itemcd = row["F1"] == DBNull.Value ? string.Empty : row["F1"].ToString(),
                pre = row["F2"] == DBNull.Value ? 0 : Convert.ToDecimal(row["F2"]),
                value = row["F3"] == DBNull.Value ? 0 : Convert.ToDecimal(row["F3"]),
                frmDate = row["F4"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["F4"]),
                toDate = row["F5"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["F5"]),


            };
        }
    }
}
