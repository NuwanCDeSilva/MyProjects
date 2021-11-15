using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.InventoryNew
{
    public class AUD_CHARGES
    {
        public string aud_com_cd { get; set; }
        public string aud_aud_tp { get; set; }
        public string aud_reason { get; set; }
        public string aud_itm_stus { get; set; }
        public string aud_remarks { get; set; }
        public string aud_charge_base_on { get; set; }
        public string aud_value { get; set; }
        public Decimal aud_chargers { get; set; }
        public string aud_price_book { get; set; }
        public string aud_p_level { get; set; }
        public DateTime aud_valid_from { get; set; }
        public DateTime aud_valid_to { get; set; }
        public string aud_effect_to { get; set; }
        public string aud_charge_amend { get; set; }
        public string aud_stock_adjuestment_type { get; set; }
        public string aud_item_cd { get; set; }
        public Int32 aud_edit_charge { get; set; }
        public static AUD_CHARGES Converter(DataRow row)
        {
            return new AUD_CHARGES
            {
                aud_com_cd = row["AUD_COM_CD"] == DBNull.Value ? string.Empty : row["AUD_COM_CD"].ToString(),
                aud_aud_tp = row["AUD_TP"] == DBNull.Value ? string.Empty : row["AUD_TP"].ToString(),
                aud_reason = row["AUD_REASON"] == DBNull.Value ? string.Empty : row["AUD_REASON"].ToString(),
                aud_itm_stus = row["AUD_ITM_STUS"] == DBNull.Value ? string.Empty : row["AUD_ITM_STUS"].ToString(),
                aud_remarks = row["AUD_REMARKS"] == DBNull.Value ? string.Empty : row["AUD_REMARKS"].ToString(),
                aud_charge_base_on = row["AUD_CHARGE_BASE_ON"] == DBNull.Value ? string.Empty : row["AUD_CHARGE_BASE_ON"].ToString(),
                aud_value= row["aud_value"] == DBNull.Value ? string.Empty : row["aud_value"].ToString(),
                aud_chargers = row["AUD_CHARGE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["AUD_CHARGE"]),
                aud_price_book = row["AUD_PRICE_BOOK"] == DBNull.Value ? string.Empty : row["AUD_PRICE_BOOK"].ToString(),
                aud_p_level = row["AUD_P_LEVEL"] == DBNull.Value ? string.Empty : row["AUD_P_LEVEL"].ToString(),
                aud_valid_from = row["AUD_VALID_FROM"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUD_VALID_FROM"]),
                aud_valid_to = row["AUD_VALID_TO"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["AUD_VALID_TO"]),
                aud_effect_to = row["AUD_EFFECT_TO"] == DBNull.Value ? string.Empty : row["AUD_EFFECT_TO"].ToString(),
                aud_charge_amend = row["AUD_CHARGE_AMEND"] == DBNull.Value ? string.Empty : row["AUD_CHARGE_AMEND"].ToString(),
                aud_stock_adjuestment_type = row["AUD_STOCK_ADJUESTMENT_TYPE"] == DBNull.Value ? string.Empty : (row["AUD_STOCK_ADJUESTMENT_TYPE"]).ToString(),
                aud_item_cd = row["AUD_ITEM_CD"] == DBNull.Value ? string.Empty : (row["AUD_ITEM_CD"]).ToString(),
                aud_edit_charge = row["aud_edit_charge"] == DBNull.Value ? 0 : Convert.ToInt32(row["aud_edit_charge"])
            };
        }




 








    }
}
