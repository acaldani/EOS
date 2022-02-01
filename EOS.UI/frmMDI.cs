using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EOS.UI
{
    public partial class frmMDI : Form
    {
        public int IDUtente = 0;
        public frmMDI()
        {
            InitializeComponent();
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            Version version = assembly.GetName().Version;
            this.Text = "EOS " + version;
        }

        private void sOLUZIONIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSoluzioni childSoluzioni = new frmSoluzioni();
            childSoluzioni.MdiParent = this;
            childSoluzioni.IDUtente = IDUtente;
            childSoluzioni.Show();
        }

        private void sOLVENTIToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSolventi childSolventi = new frmSolventi();
            childSolventi.MdiParent = this;
            childSolventi.IDUtente = IDUtente;
            childSolventi.Show();
        }
    }
}
