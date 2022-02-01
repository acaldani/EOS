using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOS.UI
{
    static class Program
    {
        /// <summary>
        /// Punto di ingresso principale dell'applicazione.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            frmLogin flogin = new frmLogin();

            Application.Run(flogin);

            if (flogin.login == true)
            {
                frmMDI MainForm = new frmMDI();
                MainForm.IDUtente = flogin.IDUtente;
                Application.Run(MainForm);
            }
            else
            {
                Application.Exit();
            }
        }
    }
}
