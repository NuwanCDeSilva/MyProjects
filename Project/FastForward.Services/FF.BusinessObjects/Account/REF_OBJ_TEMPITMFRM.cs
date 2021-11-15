using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace FF.BusinessObjects.Account
{
    public class REF_OBJ_TEMPITMFRM
    {
        public Int32 TEMPLATE_ID { get; set; }
        public String TEMPLATE_NAME { get; set; }
        public Int32 DETAIL_ID { get; set; }
        public Int32 ITEMOBJ_ID { get; set; }
        public String FIELD_TYPE { get; set; }
        public Int32 IS_HAVE_SEARCH { get; set; }
        public Int32 FIELD_LENGTH { get; set; }
        public String FIELD_NAME { get; set; }
        public string SAVED_CODE { get; set; }
        public string SAVED_VALUE { get; set; }
        public Int32 SEQ { get; set; }
        public string MODULE { get; set; }
        public Int32 DEF_VAL_FLD { get; set; }
        public string SEARCH_FLD { get; set; }
        public Int32 HAS_SEARCH { get; set; }
        public static REF_OBJ_TEMPITMFRM Converter(DataRow row)
        {
            return new REF_OBJ_TEMPITMFRM
            {
                TEMPLATE_ID = row["TEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TEMPLATE_ID"].ToString()),
                TEMPLATE_NAME = row["TEMPLATE_NAME"] == DBNull.Value ? string.Empty : row["TEMPLATE_NAME"].ToString(),
                DETAIL_ID = row["DETAIL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["DETAIL_ID"].ToString()),
                ITEMOBJ_ID = row["ITEMOBJ_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITEMOBJ_ID"].ToString()),
                FIELD_TYPE = row["FIELD_TYPE"] == DBNull.Value ? string.Empty : row["FIELD_TYPE"].ToString(),
                IS_HAVE_SEARCH = row["IS_HAVE_SEARCH"] == DBNull.Value ? 0 : Convert.ToInt32(row["IS_HAVE_SEARCH"].ToString()),
                FIELD_LENGTH = row["FIELD_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["FIELD_LENGTH"].ToString()),
                FIELD_NAME = row["FIELD_NAME"] == DBNull.Value ? string.Empty : row["FIELD_NAME"].ToString(),
                DEF_VAL_FLD = row["DEF_VAL_FLD"] == DBNull.Value ? 0 : Convert.ToInt32(row["DEF_VAL_FLD"].ToString())
            };
        }
        public static REF_OBJ_TEMPITMFRM ConverterWithVal(DataRow row)
        {
            return new REF_OBJ_TEMPITMFRM
            {
                TEMPLATE_ID = row["TEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TEMPLATE_ID"].ToString()),
                TEMPLATE_NAME = row["TEMPLATE_NAME"] == DBNull.Value ? string.Empty : row["TEMPLATE_NAME"].ToString(),
                DETAIL_ID = row["DETAIL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["DETAIL_ID"].ToString()),
                ITEMOBJ_ID = row["ITEMOBJ_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITEMOBJ_ID"].ToString()),
                FIELD_TYPE = row["FIELD_TYPE"] == DBNull.Value ? string.Empty : row["FIELD_TYPE"].ToString(),
                IS_HAVE_SEARCH = row["IS_HAVE_SEARCH"] == DBNull.Value ? 0 : Convert.ToInt32(row["IS_HAVE_SEARCH"].ToString()),
                FIELD_LENGTH = row["FIELD_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["FIELD_LENGTH"].ToString()),
                FIELD_NAME = row["FIELD_NAME"] == DBNull.Value ? string.Empty : row["FIELD_NAME"].ToString(),
                SAVED_CODE = row["SAVED_CODE"] == DBNull.Value ? string.Empty : row["SAVED_CODE"].ToString(),
                SEARCH_FLD = row["SEARCH_FLD"] == DBNull.Value ? string.Empty : row["SEARCH_FLD"].ToString(),
                DEF_VAL_FLD = row["DEF_VAL_FLD"] == DBNull.Value ? 0 : Convert.ToInt32(row["DEF_VAL_FLD"].ToString())
            };
        }
        public static REF_OBJ_TEMPITMFRM ConverterVal(DataRow row)
        {
            return new REF_OBJ_TEMPITMFRM
            {
                TEMPLATE_ID = row["TEMPLATE_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["TEMPLATE_ID"].ToString()),
                TEMPLATE_NAME = row["TEMPLATE_NAME"] == DBNull.Value ? string.Empty : row["TEMPLATE_NAME"].ToString(),
                DETAIL_ID = row["DETAIL_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["DETAIL_ID"].ToString()),
                ITEMOBJ_ID = row["ITEMOBJ_ID"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITEMOBJ_ID"].ToString()),
                FIELD_TYPE = row["FIELD_TYPE"] == DBNull.Value ? string.Empty : row["FIELD_TYPE"].ToString(),
                IS_HAVE_SEARCH = row["IS_HAVE_SEARCH"] == DBNull.Value ? 0 : Convert.ToInt32(row["IS_HAVE_SEARCH"].ToString()),
                FIELD_LENGTH = row["FIELD_LENGTH"] == DBNull.Value ? 0 : Convert.ToInt32(row["FIELD_LENGTH"].ToString()),
                FIELD_NAME = row["FIELD_NAME"] == DBNull.Value ? string.Empty : row["FIELD_NAME"].ToString(),
                SAVED_CODE = row["SAVED_CODE"] == DBNull.Value ? string.Empty : row["SAVED_CODE"].ToString(),
                SAVED_VALUE = row["SAVED_VALUE"] == DBNull.Value ? string.Empty : row["SAVED_VALUE"].ToString(),
                SEQ = row["SEQ"] == DBNull.Value ? 0 : Convert.ToInt32(row["SEQ"].ToString()),
                MODULE = row["MODULE"] == DBNull.Value ? string.Empty : row["MODULE"].ToString(),
                DEF_VAL_FLD = row["DEF_VAL_FLD"] == DBNull.Value ? 0 : Convert.ToInt32(row["DEF_VAL_FLD"].ToString()),
                SEARCH_FLD = row["SEARCH_FLD"] == DBNull.Value ? string.Empty : row["SEARCH_FLD"].ToString(),
                HAS_SEARCH = row["HAS_SEARCH"] == DBNull.Value ? 0 : Convert.ToInt32(row["HAS_SEARCH"].ToString())
            };
        }
    }
    public class REF_OBJ_TEMPITMFRMSELECT
    {
        public Int32 TEMPLATE_ID { get; set; }
        public Int32 DETAIL_ID { get; set; }
    }

    public class TEMPLATE_HED_DET
    {
        public Int32 TEMPLATE_ID { get; set; }
        public string TEMPLATE_NAME { get; set; }
        public List<TEMPLATE_ITM_DET> TEMPLATE_DET { get; set; }
    }
    public class TEMPLATE_ITM_DET
    {
        public Int32 DETAIL_ID { get; set; }
        public Int32 ITEMOBJ_ID { get; set; }
        public String FIELD_TYPE { get; set; }
        public Int32 IS_HAVE_SEARCH { get; set; }
        public Int32 FIELD_LENGTH { get; set; }
        public String FIELD_NAME { get; set; }
        public String FIELD_VALUE { get; set; }
        public string SAVED_CODE { get; set; }
        public Int32 DEF_VAL_FLD { get; set; }
        public string SEARCH_FLD { get; set; }
        public Int32 HAS_SEARCH { get; set; }
    }
    public class FORM_TMPLT_VALUE
    {
        public Int32 SEQ { get; set; }
        public string UNQ_CD { get; set; }
        public string MODULE { get; set; }
        public List<TEMPLATE_HED_DET> ITEM { get; set; }
    }

}
