using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using FF.BusinessObjects;
using System.Globalization;


namespace FF.AbansTours
{
    public class Hp_AccountSummary : BasePage
    {
        public Decimal getAccountBal(string loc, HpAccount Account, DateTime reciptDt)
        {
            return CHNLSVC.Sales.Get_AccountBalance(reciptDt, Account.Hpa_acc_no);
        }
        // Get_MonthlyRental
        public Decimal getMonthlyRental(HpAccount Account, DateTime reciptDt)
        {
            return CHNLSVC.Sales.Get_MonthlyRental(reciptDt, Account.Hpa_acc_no);
        }
        //Get_FutureRentals
        public Decimal getFutureRentals(HpAccount Account, DateTime reciptDt)
        {
            return CHNLSVC.Sales.Get_FutureRentals(reciptDt.Date, Account.Hpa_acc_no);        
        }

        public Decimal getAdditionalCommRate(HpAccount Account, DateTime reciptDt, Decimal instComRt)
        {
            return CHNLSVC.Sales.Get_hp_additionalCommision(reciptDt, Account.Hpa_sch_cd, instComRt);
        }

        public DataTable getHP_Hierachy(string selectedPC)
        {
            return CHNLSVC.Sales.Get_hpHierachy(selectedPC);
        }
        //public DataTable get_hp_ECD(string ecdTp, DateTime date, string scheme, string partyTP, string partyCD, Decimal futureRentals, string AccNo)
        //{ 
        // return CHNLSVC.Sales.Get_ECD(ecdTp,date, scheme, partyTP,partyCD, futureRentals, AccNo);
        //}
        public DataTable get_hp_ECD(string ecdTp, DateTime date, string scheme, string partyTP, string partyCD, Decimal futureRentals, string AccNo, Int32 IsReduce)
        {
            return CHNLSVC.Sales.Get_ECD(ecdTp, date, scheme, partyTP, partyCD, futureRentals, AccNo, IsReduce);
        }

        //To fill the Detail Grid in UC
        public DataTable getAccDetails(string AccNo)
        {
            return CHNLSVC.Sales.Get_hpAcc_TransactionDet(AccNo);
        }
        public Decimal getTotFutureRentalVAl(HpAccount Account, DateTime reciptDt)
        {
           DateTime fDateOfNxtMon= GetFirstDayOfNextMonth(reciptDt);
           return CHNLSVC.Sales.Get_TotFutureRentalValue(fDateOfNxtMon, Account.Hpa_acc_no);   
     
        }
        public DataTable getArrearsInfo(string party_tp,string party_cd,DateTime reciptDt)
        {
           DateTime lastDateof_prvMon= GetLastDayOfPreviousMonth(reciptDt);
           return CHNLSVC.Sales.Get_ArrearsInfo(lastDateof_prvMon, party_tp, party_cd);
        }
        public DataTable getArrearsInfo_calAlldue(string party_tp, string party_cd, DateTime reciptDt)
        {//------------
            DateTime next_prvMonth = GetLastDayOfPreviousMonth(reciptDt.AddMonths(1));
            return CHNLSVC.Sales.Get_ArrearsInfo(next_prvMonth, party_tp, party_cd);
        }
        public Decimal  getTotDue(string AccNo,DateTime hadd_ars_dt)
        {
            return CHNLSVC.Sales.Get_hp_TotalDue(AccNo, hadd_ars_dt.Date);
        }

        public Decimal getDueOnType(string AccNo, DateTime hadd_ars_dt,string type,string receipt_no,DateTime receiptDt)
        {
            return CHNLSVC.Sales.Get_hp_TotalDue_onType(AccNo, hadd_ars_dt.Date, type, receipt_no, receiptDt);
        }
        public Decimal getArrearsSettlement(string AccNo,DateTime hadd_sup_dt)
        { 
           return CHNLSVC.Sales.Get_hp_ArrearsSettlement(AccNo,hadd_sup_dt);
        }
        public Decimal getMinArrears(string party_tp, string party_cd)
        { 
             return CHNLSVC.Sales.Get_hp_MinArrears(party_tp,party_cd);
        }

        public Decimal getAjustments(string AccNo)
        {
            return CHNLSVC.Sales.Get_hp_Adjustment(AccNo);
        }

        public Decimal getTotReceipts(string AccNo, DateTime dt)
        {
            return CHNLSVC.Sales.Get_hp_Tot_Receipts(AccNo,dt);
        }
        public string getCustomerName(string AccNo)
        {
            return CHNLSVC.Sales.GetHpCustomerName(AccNo);
        }

        public DateTime getEndDate(string AccNo)
        {
            return CHNLSVC.Sales.Get_EndingDate(AccNo);
        }

        public Decimal getProtectionPayment_refund(string AccNo)
        {
            return CHNLSVC.Sales.Get_ProtectionPayment_RefundValue(AccNo);
        }

        public static DateTime GetFirstDayOfNextMonth(DateTime startDate)
        {
            if (startDate.Month == 12) // its end of year , we need to add another year to new date:
            {
                startDate = new DateTime((startDate.Year + 1), 1, 1);
            }
            else
            {
                startDate = new DateTime(startDate.Year, (startDate.Month + 1), 1);
            }
            return startDate;
        }
        public static DateTime GetLastDayOfPreviousMonth(DateTime startDate)
        {

                DateTime lastDayLastMonth = new DateTime(startDate.Year, startDate.Month, 1);
                lastDayLastMonth = lastDayLastMonth.AddDays(-1);

                startDate = lastDayLastMonth;

            return startDate;
        }

        //private Decimal getInsDueInfo_OnType(HpAccount Acc, string selectedPC, DateTime receiptDate,DateTime DueDateUpTo,string Type_)
        //{
        //   // Decimal Arrears = 0;
        //    //Uc_AllDue = 0;
        //    Decimal TotDue = 0;
        //    DataTable hierchy_tbl = new DataTable();
        //   // hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
        //    //if (hierchy_tbl.Rows.Count > 0)
        //    //{
        //    //    foreach (DataRow da in hierchy_tbl.Rows)
        //    //    {
        //    //        string party_tp = Convert.ToString(da["MPI_CD"]);
        //    //        string party_cd = Convert.ToString(da["MPI_VAL"]);
        //    //        DataTable info_tbl = new DataTable();
        //    //        info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
        //    //        if (info_tbl.Rows.Count > 0)
        //    //        {
        //                //DataRow DrECD = info_tbl.Rows[0];
        //                //DateTime HADD_ARS_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_ARS_DT"]);//hadd_ars_dt
        //               // DateTime HADD_SUP_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_SUP_DT"]);//hadd_sup_dt
        //                //call common function
        //                //Decimal AllDue = 0;
        //                // Decimal ar = calculateArears(Acc.Hpa_acc_no, HADD_ARS_DT, HADD_SUP_DT, SUMMARY, selectedPC, Uc_MonthlyRental, out AllDue);
        //                //Decimal ar = calculateInsDue(Acc.Hpa_acc_no, HADD_ARS_DT, HADD_SUP_DT, SUMMARY, selectedPC, Uc_MonthlyRental, "VEHINS", out AllDue);
        //                 TotDue = getDueOnType(Acc.Hpa_acc_no, DueDateUpTo, Type_);
        //                //Uc_AllDue = AllDue;
        //                //return Uc_AllDue;
        //               // Uc_VehInsDue = AllDue;
        //                return TotDue;
        //        //    }
        //        //    else
        //        //    {

        //        //    }

        //        //}
        //        //Decimal AllDueFinal = 0;
        //        // info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
        //        //DateTime dt1 = GetLastDayOfPreviousMonth(receiptDate.AddMonths(1));

        //        // Decimal arrVl = calculateInsDue(Acc.Hpa_acc_no, dt1, dt1, SUMMARY, selectedPC, Uc_MonthlyRental, out AllDueFinal);
        //        // TotDue = getDueOnType(Acc.Hpa_acc_no, DueDateUpTo, Type_);
        //        // Uc_AllDue = AllDueFinal;
        //        //Uc_VehInsDue = AllDueFinal;
        //        //return Uc_AllDue;
        //       // return TotDue;

        //   // }
        //    //else
        //    //{
        //        // Uc_AllDue = 0;
        //       // Uc_VehInsDue = 0;
        //         //return 0;
        //    //}
        //    //return Uc_AllDue;
        //    //return TotDue;
        //}


        //***********************************added by shani on 07-10-2012***************************************
        public static void get_ArearsDate_SupDate(string selectedPC, DateTime receiptDate, out DateTime arr_date,out DateTime sup_date)
        {
            DataTable hierchy_tbl = new DataTable();
            Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
            hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            if (hierchy_tbl.Rows.Count > 0)
            {
                foreach (DataRow da in hierchy_tbl.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);
                    DataTable info_tbl = new DataTable();
                    info_tbl = SUMMARY.getArrearsInfo(party_tp, party_cd, receiptDate);//returns one row
                    if (info_tbl.Rows.Count > 0)
                    {
                        DataRow DrECD = info_tbl.Rows[0];
                        DateTime HADD_ARS_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_ARS_DT"]);//hadd_ars_dt
                        DateTime HADD_SUP_DT = Convert.ToDateTime(info_tbl.Rows[0]["HADD_SUP_DT"]);//hadd_sup_dt

                        arr_date = HADD_ARS_DT;
                        sup_date = HADD_SUP_DT;
                        return;
                      
                    }
                }
                arr_date = receiptDate;
                sup_date = receiptDate;
            }
            arr_date = receiptDate;
            sup_date = receiptDate;
        }

        public static Decimal getArears(string AccNo, Hp_AccountSummary SUMMARY, string selectedPC, DateTime receiptDate, DateTime HADD_ARS_DT, DateTime HADD_SUP_DT)
        {
            Decimal Arrears = 0;         

            
                Decimal AllDue = 0;
                Arrears = calculateArears(AccNo, HADD_ARS_DT, HADD_SUP_DT, SUMMARY, selectedPC, out AllDue);                      
                    
               
                // info_tbl = SUMMARY.getArrearsInfo_calAlldue(party_tp, party_cd, receiptDate);//returns one row
               // DateTime dt1 = GetLastDayOfPreviousMonth(receiptDate);

               // Arrears = calculateArears(AccNo, dt1, dt1, SUMMARY, selectedPC, out AllDueFinal);

                return Arrears;
         
            
            

        }
        private static Decimal calculateArears(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt, Hp_AccountSummary SUMMARY, string selectedPC, out Decimal AllDue)
        {
            // SUMMARY is just an object. it doesn't need to be filled.
            Decimal temp_arrears = 0;
            Decimal TotDue = 0; //call proc 1 (sum(RNT_VAL));
            Decimal settlement = 0;

          
       
            Decimal Arrears = 0;
            Decimal overLimit = 0;

          //88  TotDue = SUMMARY.getTotDue(AccNo, hadd_ars_dt);
          //88  settlement = SUMMARY.getArrearsSettlement(AccNo, hadd_sup_dt);
          //88  temp_arrears = TotDue - settlement;
          //88  AllDue = temp_arrears;           
            temp_arrears = SUMMARY.Get_HP_AllDue(AccNo, hadd_ars_dt, hadd_sup_dt);//88
            AllDue = temp_arrears;  
            //---------------------------
            #region Commented
            //*************commented***********************
            //DataTable hierchy_tbl = new DataTable();
            //hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            //if (hierchy_tbl.Rows.Count > 0)
            //{
            //    int hasval = -99;
            //    foreach (DataRow da in hierchy_tbl.Rows)
            //    {

            //        string party_tp = Convert.ToString(da["MPI_CD"]);
            //        string party_cd = Convert.ToString(da["MPI_VAL"]);
            //        //proc- arrears ignore limit
            //        Decimal min_arrVal = SUMMARY.getMinArrears(party_tp, party_cd);//get hsy_val of (hpr_sys_para)table
            //        if (min_arrVal == -999)
            //        {
            //            break;
            //        }
            //        if (temp_arrears >= min_arrVal)
            //        {
            //            Arrears = temp_arrears;
            //            //uc_lblArr_Ovp.Text = "Arrears";
            //        }
            //        else if (temp_arrears < 0)
            //        {
            //            overLimit = temp_arrears * (-1);
            //            //uc_lblArr_Ovp.Text = "Over Limit";
            //            Arrears = overLimit;
            //            // Uc_OverDue = overLimit;

            //        }
            //        hasval = 1;
            //        return Arrears;
            //    }
            //    //if (hasval == 99)
            //    //{
            //    //    Arrears = 0;
            //    //}
            //}
            //--------------------------
          //  return Arrears;
            //************************************
            #endregion
            return temp_arrears;//added
        }
    
        //----------
        public static Decimal Get_Minimum_Arrears(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt, string selectedPC)
        {
            DataTable hierchy_tbl = new DataTable();
            Hp_AccountSummary SUMMARY = new Hp_AccountSummary();
            hierchy_tbl = SUMMARY.getHP_Hierachy(selectedPC);//call sp_get_hp_hierachy
            Decimal min_arrVal = 0;
            if (hierchy_tbl.Rows.Count > 0)
            {
                foreach (DataRow da in hierchy_tbl.Rows)
                {
                    string party_tp = Convert.ToString(da["MPI_CD"]);
                    string party_cd = Convert.ToString(da["MPI_VAL"]);
                    min_arrVal = SUMMARY.getMinArrears(party_tp, party_cd);//get hsy_val of (hpr_sys_para)table
                    if (min_arrVal == -99)
                    {
                        break;
                    }
                    return min_arrVal;
                }
                
            }
            return min_arrVal;

        }
       
        //03-12-2012
        public Decimal Get_HP_AllDue(string AccNo, DateTime hadd_ars_dt, DateTime hadd_sup_dt)
        {
            return CHNLSVC.Sales.Get_hp_AllDue(AccNo, hadd_ars_dt, hadd_sup_dt);
        }
    }
}