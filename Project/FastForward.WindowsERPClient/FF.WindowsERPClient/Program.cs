using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace FF.WindowsERPClient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
             
            Application.Run(new Login());
          //   Application.Run(new MainMenu());
         // Application.Run(new FF.WindowsERPClient.Reports.Sales.invoice1());
           // Application.Run(new FF.WindowsERPClient.Reports.Sales.testprint());
           
          //  Application.Run(new Login());
           // Application.Run(new MainMenu());
        }
    }
}
