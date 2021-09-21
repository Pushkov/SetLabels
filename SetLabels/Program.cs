using SolidWorks.Interop.sldworks;
using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SetLabels
{
    static class Program
    {
        static SldWorks swApp;
        static SetLabelsForm mainForm;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //swApp = GetSolidWorks();
            swApp = GetSolidSldWorks();
            if (swApp != null)
            {
                AppDomain domain = AppDomain.CurrentDomain;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                mainForm = SetLabelsForm.getInstance(swApp, domain.BaseDirectory);
                Application.Run(mainForm);
            }
        }
        private static SldWorks GetSolidWorks()
        {
            SldWorks app = null;
            try
            {
                app = (SldWorks)Marshal.GetActiveObject("SldWorks.Application");
            }
            catch
            {
                string message = "SolidWorks должен быть запущен!";
                MessageBox.Show(message, "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return app;
        }
        public static void appExit()
        {
            Application.Exit();
        }

        private static bool checkEditDim()
        {
            int count = 0;
            foreach (System.Diagnostics.Process p in System.Diagnostics.Process.GetProcesses())
            {
                if (p.ProcessName.ToString() == "SetLabels")
                {
                    count++;
                }
            }
            bool res = count > 1 ? true : false;

            return res;
        }

        //****************************
        private static SldWorks GetSolidSldWorks()
        {
            Process[] processes = Process.GetProcessesByName("SLDWORKS");
            Process SolidWorks = processes[0]; int ID = SolidWorks.Id;
            try
            {
                return (SldWorks)ROTHelper.GetActiveObjectList(ID.ToString()).Where(keyvalue => (keyvalue.Key.ToLower().Contains("solidworks"))).Select(keyvalue => keyvalue.Value).First();
            }
            catch { return null; }
        }

    }
}