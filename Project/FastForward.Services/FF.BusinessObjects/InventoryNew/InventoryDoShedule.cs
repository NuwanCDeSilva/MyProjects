using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
    [Serializable]
    public class InventoryDoShedule
    {
        //
        // Function             - Invnetory Header
        // Function Wriiten By  - P.Wijetunge
        // Date                 - 12/03/2012
        // Table                - INT_HDR
        //

        /// <summary>
        /// Private Data Members
        /// </summary>
        #region Private Members

        private Int32 _sid_seq_no;                   
        private Int32 _sid_itm_line;               
        private Int32 _sid_del_line;                  
        private string _sid_inv_no;                    
        private string _sid_itm_cd;                   
        private string _sid_itm_stus;                  
        //private Int32 _sid_qty;
        //private Int32 _sid_do_qty;
        private Decimal _sid_qty;
        private Decimal _sid_do_qty;
         private string _sid_del_com;
         private string _sid_del_loc;
         private string _sid_d_cust_tp;
         private string _sid_d_cust_cd;
         private string _sid_d_cust_name;
         private string _sid_d_cust_add1;
         private string _sid_d_cust_add2;
         private string _sid_d_town;
         private Int32 _sid_act;
         private string _sid_cre_by;
         private string _sid_cre_session;
         private string _sid_mod_by; 
         private string _sid_mod_session;


      
        #endregion

        /// <summary>
        /// Definitions for the private data members
        /// </summary>
        /// 
        #region Definition - Properties
         public Int32 sid_seq_no { get { return _sid_seq_no; } set { _sid_seq_no = value; } }
         public Int32 sid_itm_line { get { return _sid_itm_line; } set { _sid_itm_line = value; } }
         public Int32 sid_del_line { get { return _sid_del_line; } set { _sid_del_line = value; } }
         public string sid_inv_no { get { return _sid_inv_no; } set { _sid_inv_no = value; } }
         public string sid_itm_cd { get { return _sid_itm_cd; } set { _sid_itm_cd = value; } }
         public string sid_itm_stus { get { return _sid_itm_stus; } set { _sid_itm_stus = value; } }
         //public Int32 sid_qty { get { return _sid_qty; } set { _sid_qty = value; } }
         //public Int32 sid_do_qty { get { return _sid_do_qty; } set { _sid_do_qty = value; } }
         public Decimal sid_qty { get { return _sid_qty; } set { _sid_qty = value; } }
         public Decimal sid_do_qty { get { return _sid_do_qty; } set { _sid_do_qty = value; } }
         public string sid_del_com { get { return _sid_del_com; } set { _sid_del_com = value; } }
         public string sid_del_loc { get { return _sid_del_loc; } set { _sid_del_loc = value; } }
         public string sid_d_cust_tp { get { return _sid_d_cust_tp; } set { _sid_d_cust_tp = value; } }
         public string sid_d_cust_cd { get { return _sid_d_cust_cd; } set { _sid_d_cust_cd = value; } }
  
        public string sid_d_cust_name { get { return _sid_d_cust_name; } set { _sid_d_cust_name = value; } }
        public string sid_d_cust_add1 { get { return _sid_d_cust_add1; } set { _sid_d_cust_add1 = value; } }
        public string sid_d_cust_add2 { get { return _sid_d_cust_add2; } set { _sid_d_cust_add2 = value; } }
        public string sid_d_town { get { return _sid_d_town; } set { _sid_d_town = value; } }
        public Int32 sid_act { get { return _sid_act; } set { _sid_act = value; } }
        public string sid_cre_by { get { return _sid_cre_by; } set { _sid_cre_by = value; } }
        public string sid_cre_session  { get { return _sid_cre_session; } set { _sid_cre_session = value; } }
        public string sid_mod_by  { get { return _sid_mod_by ; } set { _sid_mod_by  = value; } }
        public string sid_mod_session { get { return _sid_mod_session; } set { _sid_mod_session = value; } }
       
        #endregion

        /// <summary>
        /// Convert and map to the data table into a list
        /// </summary>
        /// <param name="row">Used to allocate data table row</param>
        /// <returns>Maped Inventory Header</returns>
        #region Converter
        public static InventoryDoShedule ConvertTotal(DataRow row)
        {
            return new InventoryDoShedule
            {
                sid_seq_no=row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                sid_itm_line=row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),                
                sid_del_line=row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                sid_inv_no=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_itm_cd=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_itm_stus=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_qty=row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                sid_do_qty=row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                sid_del_com=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_del_loc=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_d_cust_tp=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_d_cust_cd=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_d_cust_name=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_d_cust_add1=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_d_cust_add2=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_d_town=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_act=row["ITH_SEQ_NO"] == DBNull.Value ? 0 : Convert.ToInt32(row["ITH_SEQ_NO"]),
                sid_cre_by=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_cre_session=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString(),
                sid_mod_by=row["ITH_ACC_NO"] == DBNull.Value ? string.Empty : row["ITH_ACC_NO"].ToString()
            };
        }
        #endregion
    }
}





