using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Financial
{
    public class OrderPlanExcelUploader
    {
        public Int32 Line { get; set; }
        public string IO_CRE_BY { get; set; }
        public string IO_MOD_BY { get; set; }
        public string IO_SESSION_ID { get; set; }
        public string IO_COM { get; set; }
        public string IO_SBU { get; set; }
        public string IO_TP { get; set; }
        public string IO_REM { get; set; }
        public string Itm_cd { get; set; }
        public string Itm_tp { get; set; }
        public string Itm_clr { get; set; }
        public string Itm_desc { get; set; }
        public string Model { get; set; }
        public string Model_desc { get; set; }
        public string Brand { get; set; }
        public string Ref_no { get; set; }
        public string Uom { get; set; }
        public string Supplier { get; set; }
        public decimal Unit_price { get; set; }
        public string Price_tp { get; set; }
        public string Proj_name { get; set; }
        public string Tmp_unit_price { get; set; }
        public string Currency { get; set; }
        public decimal CurrencyRate { get; set; }
        public string Traid_term { get; set; }
        public string Mode_of_shipment { get; set; }
        public string Tmp_mode_of_shipment { get; set; }
        public string Port_of_origin { get; set; }
        public string Tmp_port_of_origin { get; set; }
        public string Main_cat { get; set; }
        public string Cat_1 { get; set; }
        public string Cat_2 { get; set; }
        public string Cat_3 { get; set; }
        public decimal Unallocate_Qty { get; set; }
        public string Tmp_unallocate_Qty { get; set; }
        public string Tmp_err { get; set; }
        public string Tmp_err_text { get; set; }
        public string Def_loc { get; set; }
        public bool Is_corr_qty { get; set; }
        public bool Is_new_model { get; set; }
        public bool Is_itm_base { get; set; }
        public bool Is_dec_itm { get; set; }
        //Added By Dulaj 2018/May/15

        public string LcL { get; set; }
        public string Ft20 { get; set; }
        public string Ft40 { get; set; }
        public string Hq40 { get; set; }

        public string Af { get; set; }

        public int TradeAgreement { get; set; }

        public string ContainerType { get; set; }
        public decimal ContainerQty { get; set; }



        public List<OrderPlanExcelMonthData> MonthDataList = new List<OrderPlanExcelMonthData>();
        public List<OrderPlanExcelChnlData> ChnlDataList = new List<OrderPlanExcelChnlData>();
        public List<OrderPlanExcelBuffLvl> BuffLvlDataList = new List<OrderPlanExcelBuffLvl>();
        List<ImportsBLContainer> _contList = new List<ImportsBLContainer>();

        public OrderPlanExcelMonthData MonthData = null;
        public OrderPlanExcelChnlData ChnlData = null;
        public OrderPlanExcelBuffLvl BuffLvl = null;

        public class OrderPlanExcelMonthData 
        {
            public string Mth_cd { get; set; }
            public bool Is_ok { get; set; }
            public Int32 Mth { get; set; }
            public Int32 Year { get; set; }
            public decimal Mth_qty { get; set; }
            public string Tmp_mth_qty { get; set; }
        }
        public class OrderPlanExcelChnlData
        {
            public string All_ch_cd { get; set; }
            public decimal All_ch_qty { get; set; }
            public string Tmp_all_ch_qty { get; set; }
            public bool Is_ok { get; set; }
        }
        public class OrderPlanExcelBuffLvl
        {
            public string Mbc_chnl { get; set; }
            public string Mbc_grade { get; set; }
            public string Mbc_season { get; set; }
            public decimal Buff_qty { get; set; }
            public string Tmp_buff_qty { get; set; }
            public string Tmp_cd { get; set; }
            public bool Is_ok { get; set; }
        }
    }
}
