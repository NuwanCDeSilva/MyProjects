using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    //add by akila 2016/12/04 MstRecSubTp class represent the table mst_rec_subtp
    public class MstRecSubTp
    {

         public enum ReceiptTypes
        {
            ADVAN,
             NOR,
             TRAV,
             PRCDF,
             RVT,
             INTR,
             JOB
        };

        public int SequenceNo { get; set; }
        public string CompanyCode { get; set; }
        public string PartyType { get; set; }
        public string PartyCode { get; set; }
        public string ReceiptCode { get; set; }
        public string SubReceiptCode { get; set; }
        public string ItemCategory { get; set; }
        public int PriceValidPeriod { get; set; }
        public int SettelmentPeriod { get; set; }
        public int MinItemCount { get; set; }
        public int IsSerialCompulsory { get; set; }
        public double MinimunItemPrice { get; set; }
        public int RefundablePeriod { get; set; }
        public double MinimumBillValue { get; set; }
        public int IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }


        public static MstRecSubTp Converter(DataRow _row)
        {
            return new MstRecSubTp
            {
                PartyType = _row["msrst_party_tp"] == DBNull.Value ? string.Empty : _row["msrst_party_tp"].ToString(),
                PartyCode = _row["msrst_party_cd"] == DBNull.Value ? string.Empty : _row["msrst_party_cd"].ToString(),
                ReceiptCode = _row["msrst_repcd"] == DBNull.Value ? string.Empty : _row["msrst_repcd"].ToString(),
                SubReceiptCode = _row["msrst_sub_repcd"] == DBNull.Value ? string.Empty : _row["msrst_sub_repcd"].ToString(),
                ItemCategory = _row["msrst_itm_cat"] == DBNull.Value ? string.Empty : _row["msrst_itm_cat"].ToString(),
                PriceValidPeriod = _row["msrst_pri_val_prd"] == DBNull.Value ? 0 : Convert.ToInt32(_row["msrst_pri_val_prd"]),
                SettelmentPeriod = _row["msrst_set_prd"] == DBNull.Value ? 0 :Convert.ToInt32( _row["msrst_set_prd"]),
                MinItemCount = _row["msrst_min_itmcnt"] == DBNull.Value ? 0 : Convert.ToInt32(_row["msrst_min_itmcnt"]),
                IsSerialCompulsory = _row["msrst_is_ser_cmpl"] == DBNull.Value ? 0 :Convert.ToInt32( _row["msrst_is_ser_cmpl"]),
                MinimunItemPrice = _row["msrst_min_itmpr"] == DBNull.Value ? 0 :Convert.ToInt32( _row["msrst_min_itmpr"]),
                RefundablePeriod = _row["msrst_refund_prd"] == DBNull.Value ? 0 :Convert.ToInt32( _row["msrst_refund_prd"]),
                MinimumBillValue = _row["msrst_min_bilpr"] == DBNull.Value ? 0 : Convert.ToInt32(_row["msrst_min_bilpr"]),
                IsActive = _row["msrst_is_act"] == DBNull.Value ? 0 : Convert.ToInt32(_row["msrst_is_act"])
            };
        }
    }
}
