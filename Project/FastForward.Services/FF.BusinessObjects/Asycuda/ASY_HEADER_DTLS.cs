using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Asycuda
{
    public class ASY_HEADER_DTLS
    {
        private string _declarent_1;
        private string _declarent_2;
        private string _declarent_3 ;

        private string _declarent_tin;
        private string _declarent_name;
        private string _declarent_address;

        private string _consignee;
        private string _exporter;
        private decimal _itm_qty;
        private string _total_pakage ;
        private string _city_of_last_consignee;
        private string _description_expr1;
        private string _country_of_destination;
        private string _description_expr2 ;
        private string _vessel;
        private string _fcl;
        private string _container_fcl;
        private string _delivery_terms;
        private DateTime _voyage_date;
        private string _currency;
        private decimal _total_amount;
        private decimal _exchange_rate;
        private string _nature_of_trance;
        private string _bank_code;
        private string _terms_of_payment;
        private string _bank_name;
        private string _bank_branch;
        private string _related_document;
        private string _account_no;
        private string _ware_house_and_piriod;
        private string _office_of_entry;
        private string _location_of_goods;
        private string _totlal_pakage_unit;
        private string _print_1_b37_1;
        private string _print_2_b37_2 ;
        private string _lision_no;
        private string _company_name;
        private string _address_line1;
        private string _address_line2;
        private string _tin_no;
        private string _supplier_name;
        private string _address_line_1_expr3 ;
        private string _address_line_2_expr4 ;
        private string _trading_country ;
        private string _financial_document_no;
        private string _voyage;
        private string _main_hs ;
        private string _country_of_origin;
        private decimal _total_num_of_forms;
        private string _office_entry_description;
        private string _type_of_declaration;
        private string _exporter_name;
        private string _consignee_code;
        private string _financial_code;
        private string _financial_name;
        private string _location_of_gods_desc;
        private decimal _freight_amount;
        private decimal _cost_amount;
        private decimal _other_amount;
        private decimal _insuarance_amount;
        private decimal _total_fob_amount;
        private decimal _total_gross_mass;
        private string _entry_desc;
        private string _SUN_BOND_NUM;
        public string DECLRENT_1 { get { return _declarent_1; } set { _declarent_1 = value; } }
        public string DECLRENT_2 { get { return _declarent_2; } set { _declarent_2 = value; } }
        public string DECLRENT_3 { get { return _declarent_3; } set { _declarent_3 = value; } }
        public string DECLRENT_TIN { get { return _declarent_tin; } set { _declarent_tin = value; } }
        public string DECLRENT_NAME { get { return _declarent_name  ; } set { _declarent_name = value; } }
        public string DECLRENT_ADDRESS { get { return _declarent_address; } set { _declarent_address = value; } }

        public string  CONSIGNEE { get { return _consignee; } set { _consignee = value; } }
        public string  EXPORTER  { get { return _exporter; } set { _exporter = value; } }
        public decimal  ITM_QTY { get { return _itm_qty; } set { _itm_qty = value; } }
        public string TOTAL_PAKAGE { get { return _total_pakage; } set { _total_pakage = value; } }
        public string CITY_OF_LAST_CONSIGNEE { get { return _city_of_last_consignee; } set { _city_of_last_consignee = value; } }
        public string DESCRIPTION_EXPR1 { get { return _description_expr1; } set { _description_expr1 = value; } }
        public string COUNTRY_OF_DESTINATION { get { return _country_of_destination; } set { _country_of_destination = value; } }
        public string DESCRIPTION_EXPR2 { get { return _description_expr2; } set { _description_expr2 = value; } }
        public string VESSEL { get { return _vessel; } set { _vessel = value; } }
        public string FCL { get { return _fcl; } set { _fcl = value; } }
        public string CONTAINER_FCL { get { return _container_fcl; } set { _container_fcl = value; } }
        public string DELIVERY_TERMS { get { return _delivery_terms; } set { _delivery_terms = value; } }
        public DateTime VOYAGE_DATE { get { return _voyage_date; } set { _voyage_date = value; } }
        public string CURRENCY { get { return _currency; } set { _currency = value; } }
        public decimal TOTAL_AMOUNT { get { return _total_amount; } set { _total_amount = value; } }
        public decimal EXCHANGE_RATE { get { return _exchange_rate; } set { _exchange_rate = value; } }
        public string NATURE_OF_TRANCE { get { return _nature_of_trance; } set { _nature_of_trance = value; } }
        public string BANK_CODE { get { return _bank_code; } set { _bank_code = value; } }
        public string TERMS_OF_PAYMENT { get { return _terms_of_payment; } set { _terms_of_payment = value; } }
        public string BANK_NAME { get { return _bank_name; } set { _bank_name = value; } }
        public string BANK_BRANCH { get { return _bank_branch; } set { _bank_branch = value; } }
        public string RELATED_DOCUMENT { get { return _related_document; } set { _related_document = value; } }
        public string ACCOUNT_NO { get { return _account_no; } set { _account_no = value; } }
        public string WARE_HOUSE_AND_PIRIOD { get { return _ware_house_and_piriod; } set { _ware_house_and_piriod = value; } }
        public string OFFICE_OF_ENTRY { get { return _office_of_entry; } set { _office_of_entry = value; } }
        public string LOCATION_OF_GOODS { get { return _location_of_goods; } set { _location_of_goods = value; } }
        public string TOTAL_PAKAGE_UNIT { get { return _totlal_pakage_unit; } set { _totlal_pakage_unit = value; } }
        public string PRINT_1_B37_1 { get { return _print_1_b37_1; } set { _print_1_b37_1 = value; } }
        public string PRINT_2_B37_2 { get { return _print_2_b37_2; } set { _print_2_b37_2 = value; } }
        public string LISION_NO { get { return _lision_no; } set { _lision_no = value; } }
        public string COMPANY_NAME { get { return _company_name; } set { _company_name = value; } }
        public string ADDRESS_LINE1 { get { return _address_line1; } set { _address_line1 = value; } }
        public string ADDRESS_LINE2 { get { return _address_line2; } set { _address_line2 = value; } }
        public string TIN_NO { get { return _tin_no; } set { _tin_no = value; } }
        public string SUPPLIER_NAME { get { return _supplier_name; } set { _supplier_name = value; } }
        public string ADDRESS_LINE_1_EXPR3 { get { return _address_line_1_expr3; } set { _address_line_1_expr3 = value; } }
        public string ADDRESS_LINE_2_EXPR4 { get { return _address_line_2_expr4; } set { _address_line_2_expr4 = value; } }
        public string TRADING_COUNTRY { get { return _trading_country; } set { _trading_country = value; } }
        public string FINANCIAL_DOCUMENT_NO { get { return _financial_document_no; } set { _financial_document_no = value; } }
        public string VOYAGE { get { return _voyage; } set { _voyage = value; } }
        public string MAIN_HS { get { return _main_hs; } set { _main_hs = value; } }
        public string COUNTRY_OF_ORIGIN { get { return _country_of_origin; } set { _country_of_origin = value; } }
        public decimal TOTAL_NUM_OF_FORMS { get { return _total_num_of_forms; } set { _total_num_of_forms = value; } }
        public string OFFICE_ENTRY_DESCRIPTION { get { return _office_entry_description; } set { _office_entry_description = value; } }
        public string TYPE_OF_DECLARATION { get { return _type_of_declaration; } set { _type_of_declaration = value; } }
        public string EXPORTER_NAME { get { return _exporter_name; } set { _exporter_name = value; } }
        public string CONSIGNEE_CODE { get { return _consignee_code; } set { _consignee_code = value; } }
        public string FINANCIAL_CODE { get { return _financial_code; } set { _financial_code = value; } }
        public string FINANCIAL_NAME { get { return _financial_name; } set { _financial_name = value; } }
        public string LOCATION_OF_GODS_DESC { get { return _location_of_gods_desc; } set { _location_of_gods_desc = value; } }
         public decimal FREIGHT_AMOUNT { get { return  _freight_amount; } set {  _freight_amount = value; } }
        public decimal COST_AMOUNT { get { return  _cost_amount; } set {  _cost_amount = value; } }
        public decimal OTHER_AMOUNT { get { return  _other_amount; } set {  _other_amount = value; } }
        public decimal INSUARANCE_AMOUNT { get { return  _insuarance_amount; } set {  _insuarance_amount = value; } }
        public decimal TOTAL_FOB_AMOUNT { get { return _total_fob_amount; } set { _total_fob_amount = value; } }
        public decimal TOTAL_GROSS_MASS { get { return _total_gross_mass; } set { _total_gross_mass = value; } }
        public string ENTRY_DESC { get { return _entry_desc; } set { _entry_desc = value; } }
        public string SUN_BOND_NUM { get { return _SUN_BOND_NUM; } set { _SUN_BOND_NUM = value; } }
        public static ASY_HEADER_DTLS Converter(DataRow row)
        {
            return new ASY_HEADER_DTLS
            {
                DECLRENT_1 = row["DECLRENT_1"] == DBNull.Value ? string.Empty : row["DECLRENT_1"].ToString(),
                DECLRENT_2 = row["DECLRENT_2"] == DBNull.Value ? string.Empty : row["DECLRENT_2"].ToString(),
                DECLRENT_3 = row["DECLRENT_3"] == DBNull.Value ? string.Empty : row["DECLRENT_3"].ToString(),

                DECLRENT_TIN = row["DECLRENT_TIN"] == DBNull.Value ? string.Empty : row["DECLRENT_TIN"].ToString(),
                DECLRENT_NAME = row["DECLRENT_NAME"] == DBNull.Value ? string.Empty : row["DECLRENT_NAME"].ToString(),
                DECLRENT_ADDRESS = row["DECLRENT_ADDRESS"] == DBNull.Value ? string.Empty : row["DECLRENT_ADDRESS"].ToString(),

                CONSIGNEE = row["CONSIGNEE"] == DBNull.Value ? string.Empty : row["CONSIGNEE"].ToString(),
                EXPORTER = row["EXPORTER"] == DBNull.Value ? string.Empty : row["EXPORTER"].ToString(),
                ITM_QTY = row["ITM_QTY"] == DBNull.Value ? 0 : Convert.ToDecimal(row["ITM_QTY"].ToString()),
                TOTAL_PAKAGE = row["TOTAL_PAKAGE"] == DBNull.Value ? string.Empty : row["TOTAL_PAKAGE"].ToString(),
                CITY_OF_LAST_CONSIGNEE = row["CITY_OF_LAST_CONSIGNEE"] == DBNull.Value ? string.Empty : row["CITY_OF_LAST_CONSIGNEE"].ToString(),
                DESCRIPTION_EXPR1 = row["DESCRIPTION_EXPR1"] == DBNull.Value ? string.Empty : row["DESCRIPTION_EXPR1"].ToString(),
                COUNTRY_OF_DESTINATION = row["COUNTRY_OF_DESTINATION"] == DBNull.Value ? string.Empty : row["COUNTRY_OF_DESTINATION"].ToString(),
                DESCRIPTION_EXPR2 = row["DESCRIPTION_EXPR2"] == DBNull.Value ? string.Empty : row["DESCRIPTION_EXPR2"].ToString(),
                VESSEL = row["VESSEL"] == DBNull.Value ? string.Empty : row["VESSEL"].ToString(),
                FCL = row["FCL"] == DBNull.Value ? string.Empty : row["FCL"].ToString(),
                CONTAINER_FCL = row["CONTAINER_FCL"] == DBNull.Value ? string.Empty : row["CONTAINER_FCL"].ToString(),
                DELIVERY_TERMS = row["DELIVERY_TERMS"] == DBNull.Value ? string.Empty : row["DELIVERY_TERMS"].ToString(),
                VOYAGE_DATE = row["VOYAGE_DATE"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["VOYAGE_DATE"].ToString()),
                CURRENCY = row["CURRENCY"] == DBNull.Value ? string.Empty : row["CURRENCY"].ToString(),
                TOTAL_AMOUNT = row["TOTAL_AMOUNT"] == DBNull.Value ? 0 : Convert.ToDecimal(row["TOTAL_AMOUNT"].ToString()),
                EXCHANGE_RATE = row["EXCHANGE_RATE"] == DBNull.Value ? 0 : Convert.ToDecimal(row["EXCHANGE_RATE"].ToString()),
                NATURE_OF_TRANCE = row["NATURE_OF_TRANCE"] == DBNull.Value ? string.Empty : row["NATURE_OF_TRANCE"].ToString(),
                BANK_CODE = row["BANK_CODE"] == DBNull.Value ? string.Empty : row["BANK_CODE"].ToString(),
                TERMS_OF_PAYMENT = row["TERMS_OF_PAYMENT"] == DBNull.Value ? string.Empty : row["TERMS_OF_PAYMENT"].ToString(),
                BANK_NAME = row["BANK_NAME"] == DBNull.Value ? string.Empty : row["BANK_NAME"].ToString(),
                BANK_BRANCH = row["BANK_BRANCH"] == DBNull.Value ? string.Empty : row["BANK_BRANCH"].ToString(),
                RELATED_DOCUMENT = row["RELATED_DOCUMENT"] == DBNull.Value ? string.Empty : row["RELATED_DOCUMENT"].ToString(),
                ACCOUNT_NO = row["ACCOUNT_NO"] == DBNull.Value ? string.Empty : row["ACCOUNT_NO"].ToString(),
                WARE_HOUSE_AND_PIRIOD = row["WARE_HOUSE_AND_PIRIOD"] == DBNull.Value ? string.Empty : row["WARE_HOUSE_AND_PIRIOD"].ToString(),
                OFFICE_OF_ENTRY = row["OFFICE_OF_ENTRY"] == DBNull.Value ? string.Empty : row["OFFICE_OF_ENTRY"].ToString(),
                LOCATION_OF_GOODS = row["LOCATION_OF_GOODS"] == DBNull.Value ? string.Empty : row["LOCATION_OF_GOODS"].ToString(),
                TOTAL_PAKAGE_UNIT = row["TOTAL_PAKAGE_UNIT"] == DBNull.Value ? string.Empty : row["TOTAL_PAKAGE_UNIT"].ToString(),
                PRINT_1_B37_1 = row["PRINT_1_B37_1"] == DBNull.Value ? string.Empty : row["PRINT_1_B37_1"].ToString(),
                PRINT_2_B37_2 = row["PRINT_2_B37_2"] == DBNull.Value ? string.Empty : row["PRINT_2_B37_2"].ToString(),
                LISION_NO = row["LISION_NO"] == DBNull.Value ? string.Empty : row["LISION_NO"].ToString(),
                COMPANY_NAME = row["COMPANY_NAME"] == DBNull.Value ? string.Empty : row["COMPANY_NAME"].ToString(),
                ADDRESS_LINE1 = row["ADDRESS_LINE1"] == DBNull.Value ? string.Empty : row["ADDRESS_LINE1"].ToString(),
                ADDRESS_LINE2 = row["ADDRESS_LINE2"] == DBNull.Value ? string.Empty : row["ADDRESS_LINE2"].ToString(),
                TIN_NO = row["TIN_NO"] == DBNull.Value ? string.Empty : row["TIN_NO"].ToString(),
                SUPPLIER_NAME = row["SUPPLIER_NAME"] == DBNull.Value ? string.Empty : row["SUPPLIER_NAME"].ToString(),
                ADDRESS_LINE_1_EXPR3 = row["ADDRESS_LINE_1_EXPR3"] == DBNull.Value ? string.Empty : row["ADDRESS_LINE_1_EXPR3"].ToString(),
                ADDRESS_LINE_2_EXPR4 = row["ADDRESS_LINE_2_EXPR4"] == DBNull.Value ? string.Empty : row["ADDRESS_LINE_2_EXPR4"].ToString(),
                TRADING_COUNTRY = row["TRADING_COUNTRY"] == DBNull.Value ? string.Empty : row["TRADING_COUNTRY"].ToString(),
                FINANCIAL_DOCUMENT_NO = row["FINANCIAL_DOCUMENT_NO"] == DBNull.Value ? string.Empty : row["FINANCIAL_DOCUMENT_NO"].ToString(),
                VOYAGE = row["VOYAGE"] == DBNull.Value ? string.Empty : row["VOYAGE"].ToString(),
                MAIN_HS = row["MAIN_HS"] == DBNull.Value ? string.Empty : row["MAIN_HS"].ToString(),
                COUNTRY_OF_ORIGIN = row["COUNTRY_OF_ORIGIN"] == DBNull.Value ? string.Empty : row["COUNTRY_OF_ORIGIN"].ToString(),
                ENTRY_DESC = row["ENTRY_DESC"] == DBNull.Value ? string.Empty : row["ENTRY_DESC"].ToString(),
                SUN_BOND_NUM = row["SUN_BOND_NUM"] == DBNull.Value ? string.Empty : row["SUN_BOND_NUM"].ToString()
            };
        } 
    }
}
