using System; 
using System.Data;

namespace FF.BusinessObjects
{

    //===========================================================================================================
    // This code is generated by Code gen V.1 
    // All rights reserved.
    // Suneththaraka02@gmail.com 
    // Computer :- ITPD11  | User :- suneth On 09-Jul-2015 08:42:59
    //===========================================================================================================
    [Serializable]
    public class mst_itm_container
    {
        public String Ic_item_code { get; set; }
        public String Ic_container_type_code { get; set; }
        public Int32 Ic_no_of_unit_per_one_item { get; set; }
        public String Ic_description { get; set; }
        public String Ic_create_by { get; set; }
        public DateTime Ic_create_when { get; set; }
        public String Ic_last_modify_by { get; set; }
        public DateTime Ic_last_modify_when { get; set; }
        public Int32 Ic_act { get; set; }
        public static mst_itm_container Converter(DataRow row)
        {
            return new mst_itm_container
            {
                Ic_item_code = row["IC_ITEM_CODE"] == DBNull.Value ? string.Empty : row["IC_ITEM_CODE"].ToString(),
                Ic_container_type_code = row["IC_CONTAINER_TYPE_CODE"] == DBNull.Value ? string.Empty : row["IC_CONTAINER_TYPE_CODE"].ToString(),
                Ic_no_of_unit_per_one_item = row["IC_NO_OF_UNIT_PER_ONE_ITEM"] == DBNull.Value ? 0 : Convert.ToInt32(row["IC_NO_OF_UNIT_PER_ONE_ITEM"].ToString()),
                Ic_description = row["IC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["IC_DESCRIPTION"].ToString(),
                Ic_create_by = row["IC_CREATE_BY"] == DBNull.Value ? string.Empty : row["IC_CREATE_BY"].ToString(),
                Ic_create_when = row["IC_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IC_CREATE_WHEN"].ToString()),
                Ic_last_modify_by = row["IC_LAST_MODIFY_BY"] == DBNull.Value ? string.Empty : row["IC_LAST_MODIFY_BY"].ToString(),
                Ic_last_modify_when = row["IC_LAST_MODIFY_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IC_LAST_MODIFY_WHEN"].ToString())
            };
        }
        public static mst_itm_container ConverterNew(DataRow row)
        {
            return new mst_itm_container
            {
                Ic_item_code = row["IC_ITEM_CODE"] == DBNull.Value ? string.Empty : row["IC_ITEM_CODE"].ToString(),
                Ic_container_type_code = row["IC_CONTAINER_TYPE_CODE"] == DBNull.Value ? string.Empty : row["IC_CONTAINER_TYPE_CODE"].ToString(),
                Ic_no_of_unit_per_one_item = row["IC_NO_OF_UNIT_PER_ONE_ITEM"] == DBNull.Value ? 0 : Convert.ToInt32(row["IC_NO_OF_UNIT_PER_ONE_ITEM"].ToString()),
                Ic_description = row["IC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["IC_DESCRIPTION"].ToString(),
                Ic_create_by = row["IC_CREATE_BY"] == DBNull.Value ? string.Empty : row["IC_CREATE_BY"].ToString(),
                Ic_create_when = row["IC_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IC_CREATE_WHEN"].ToString()),
                Ic_last_modify_by = row["IC_LAST_MODIFY_BY"] == DBNull.Value ? string.Empty : row["IC_LAST_MODIFY_BY"].ToString(),
                Ic_last_modify_when = row["IC_LAST_MODIFY_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IC_LAST_MODIFY_WHEN"].ToString()),
                Ic_act = row["Ic_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["Ic_act"].ToString())
            };
        }
        //Add by lakshan 17 Jan 2016
        public static mst_itm_container Converter2(DataRow row)
        {
            return new mst_itm_container
            {
                Ic_item_code = row["IC_ITEM_CODE"] == DBNull.Value ? string.Empty : row["IC_ITEM_CODE"].ToString(),
                Ic_container_type_code = row["IC_CONTAINER_TYPE_CODE"] == DBNull.Value ? string.Empty : row["IC_CONTAINER_TYPE_CODE"].ToString(),
                Ic_no_of_unit_per_one_item = row["IC_NO_OF_UNIT_PER_ONE_ITEM"] == DBNull.Value ? 0 : Convert.ToInt32(row["IC_NO_OF_UNIT_PER_ONE_ITEM"].ToString()),
                Ic_description = row["IC_DESCRIPTION"] == DBNull.Value ? string.Empty : row["IC_DESCRIPTION"].ToString(),
                Ic_create_by = row["IC_CREATE_BY"] == DBNull.Value ? string.Empty : row["IC_CREATE_BY"].ToString(),
                Ic_create_when = row["IC_CREATE_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IC_CREATE_WHEN"].ToString()),
                Ic_last_modify_by = row["IC_LAST_MODIFY_BY"] == DBNull.Value ? string.Empty : row["IC_LAST_MODIFY_BY"].ToString(),
                Ic_last_modify_when = row["IC_LAST_MODIFY_WHEN"] == DBNull.Value ? DateTime.MinValue : Convert.ToDateTime(row["IC_LAST_MODIFY_WHEN"].ToString()),
                Ic_act= row["Ic_act"] == DBNull.Value ? 0 : Convert.ToInt32(row["Ic_act"].ToString())
            };
        }
    }
} 
