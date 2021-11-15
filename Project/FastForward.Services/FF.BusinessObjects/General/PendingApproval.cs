using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace FF.BusinessObjects
{
   public class PendingApproval
    {


        #region Private Members
        private string _parent_menu;
        private string _sart_main_tp;
        private Int32 _num_of_trans;
        private string _sart_desc;
        private Int32 _no_of_tot;

       
        #endregion



        #region Public Property Definition

        public string Parent_menu
        {
            get { return _parent_menu; }
            set { _parent_menu = value; }
        }

        public string Sart_main_tp
        {
            get { return _sart_main_tp; }
            set { _sart_main_tp = value; }
        }

        public Int32 Num_of_trans
        {
            get { return _num_of_trans; }
            set { _num_of_trans = value; }
        }

        public Int32 No_of_tot
        {
            get { return _no_of_tot; }
            set { _no_of_tot = value; }
        }
        public string Sart_desc
        {
            get { return _sart_desc; }
            set { _sart_desc = value; }
        }
        #endregion


        #region Converters
        public static PendingApproval Converter(DataRow row)
        {
            return new PendingApproval
            {
                Sart_main_tp = row["SART_MAIN_TP"] == DBNull.Value ? string.Empty : row["SART_MAIN_TP"].ToString(),
                Num_of_trans = row["NUM_OF_TRANS"] == DBNull.Value ? 0 : Convert.ToInt32(row["NUM_OF_TRANS"])

            };
        }
        #endregion


        #region SartDescConverters

        public static PendingApproval SartDescConverters(DataRow row)
        {
            return new PendingApproval
            {
               
                Sart_desc = row["SART_DESC"] == DBNull.Value ? string.Empty : row["SART_DESC"].ToString(),
                No_of_tot = row["Trn_Tot_Count"] == DBNull.Value ? 0 : Convert.ToInt32(row["Trn_Tot_Count"])

            };
        }



        #endregion


    }
}
