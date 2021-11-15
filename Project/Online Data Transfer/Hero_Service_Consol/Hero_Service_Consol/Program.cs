using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using EMS_Upload_Console;

namespace Hero_Service_Consol
{
    class Program
    {
        const Int32 SW_MINIMIZE = 6;

        [DllImport("Kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        private static extern IntPtr GetConsoleWindow();

        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] Int32 nCmdShow);

        private static void MinimizeConsoleWindow()
        {
            IntPtr hWndConsole = GetConsoleWindow();
            ShowWindow(hWndConsole, SW_MINIMIZE);
        }

        static void Main(string[] args)
        {

            MinimizeConsoleWindow();
            
            #region Check the Application Exe Run at that time
            //Add Chamal 01-10-2012
            Process[] processes = Process.GetProcessesByName(Process.GetCurrentProcess().ProcessName);
            if (processes.GetUpperBound(0) > 0) return;
            #endregion

            Conn _conCls = new Conn();

            MRNA_SOA_AutoCancellation _clsObject = new MRNA_SOA_AutoCancellation(_conCls.GetEMSConnectionString().ToString());
            Console.WriteLine();
            Console.WriteLine("****************************** Cancel MRNA & SOA ******************************");
            Console.WriteLine("Process Started " + DateTime.Today.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss tt"));

            _clsObject.StartCancellationProcess();

            Console.WriteLine("Process Completed " + DateTime.Today.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss tt"));
            Console.WriteLine("******************************************************************************************");

            CancelTransferRequests _cancelRequest = new CancelTransferRequests(_conCls.GetEMSConnectionString().ToString());
            Console.WriteLine();
            Console.WriteLine("****************************** Cancel Transfer Order Reques ******************************");
            Console.WriteLine("Process Started " + DateTime.Today.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss tt"));
            _cancelRequest.CancelRequests();
            _cancelRequest.CancelRequestsByPeriod();
            Console.WriteLine("Process Completed " + DateTime.Today.Date.ToShortDateString() + " " + DateTime.Now.ToString("HH:mm:ss tt"));
            Console.WriteLine("******************************************************************************************");


            JobDetailsUpload oJobDetailsCls = new JobDetailsUpload();
            Send_SMS_Wellawatte oWellawatte = new Send_SMS_Wellawatte();
            oJobDetailsCls.GetJobDetails(1);// Suneth

            InterTransferRequests oInterTnsferReq = new InterTransferRequests();
            oInterTnsferReq.InterTrnsfReq("HMC", "AAL", 1, "CC");//Suneth
            oInterTnsferReq.So_Generation("HMC", "AAL", 1, "CC");// Nadeeka
            PuchaseOrderGeneration oPogeneration = new PuchaseOrderGeneration();
            oPogeneration.PO_Generation("AAL", "HMC", 1, "CC");// Nadeeka


            Console.WriteLine("                        ~ Big Deal Service Console ~                        ");
            Console.WriteLine("");
            Console.WriteLine("Date :-" + DateTime.Now.ToString("dd/MMM/yyyy") + "                               Time :-" + DateTime.Now.ToString("hh.mm.ss tt"));

            oInterTnsferReq = new InterTransferRequests();
            oInterTnsferReq.GetBigDealInvoice("BDL", "ABL", 2, "INVENTORY");//Suneth
            oInterTnsferReq.InterTrnsfReq("BDL", "ABL", 2, "INVENTORY");//Suneth
            oInterTnsferReq.So_Generation("BDL", "ABL", 2, "INVENTORY");// Nadeeka
            oPogeneration = new PuchaseOrderGeneration();
            oPogeneration.PO_Generation("ABL", "BDL", 2, "INVENTORY");// Nadeeka
            oWellawatte.Send_SMS_Wellawatte1("ABE", "ABE", 2, "INVENTORY");//Sanjeewa
       //Console.ReadKey();

            Console.WriteLine("                        ~ Big Deal  Console - Completed ~                        ");
            
        }
    }
}