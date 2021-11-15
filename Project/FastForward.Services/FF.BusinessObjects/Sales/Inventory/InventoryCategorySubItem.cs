using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects.Inventory
{
    class InventoryAdjSubItem 
    {
        #region Private Members
        private string _mmct_mcd;
        private string _mmct_scd;
        private string _mmct_sdesc;
        #endregion

        public string Mmct_mcd { get; set; }
        public string Mmct_scd { get; set; }
        public string Mmct_sdesc { get; set; }

        public static InventoryAdjSubItem Converter(DataRow row)
        {
            return new InventoryAdjSubItem
            {
                Mmct_mcd = row["MMCT_MCD"] == DBNull.Value ? string.Empty : row["MMCT_MCD"].ToString(),
                Mmct_scd = row["MMCT_SCD"] == DBNull.Value ? string.Empty : row["MMCT_SCD"].ToString(),
                Mmct_sdesc = row["MMCT_SDESC"] == DBNull.Value ? string.Empty : row["MMCT_SDESC"].ToString()

            };
        }
    }
}
