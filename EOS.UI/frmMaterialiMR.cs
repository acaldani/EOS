using DevExpress.XtraBars;
using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EOS.Core;
using DevExpress.XtraSplashScreen;

namespace EOS.UI
{
    public partial class frmMaterialiMR : Form
    {
        public int IDSchedaDocumentiCalled = 0;
        public frmMaterialiMR()
        {
            InitializeComponent();
        }

        private void frmMaterialiMR_Load(object sender, EventArgs e)
        {
            EOS.Core.Model.Model_MaterialiMR ModelMaterialiMR = new EOS.Core.Model.Model_MaterialiMR();
            EOS.Core.Control.Control_MaterialiMR ControlMaterialiMR = new Core.Control.Control_MaterialiMR();
            ControlMaterialiMR.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString;
            ModelMaterialiMR = ControlMaterialiMR.GetByIDSchedaDocumenti(IDSchedaDocumentiCalled).First().Value;

            txtCodiceMateriale.EditValue = ModelMaterialiMR.Codice_Materiale;
            txtCASMateriale.EditValue = ModelMaterialiMR.CAS_Materiale;
            txtDenominazioneMateriale.EditValue = ModelMaterialiMR.Denominazione_Materiale;
            txtTipoMateriale.EditValue = ModelMaterialiMR.Tipo_Materiale;
            txtStatoMateriale.EditValue = ModelMaterialiMR.Stato_Materiale;
            txtPMMisurandoMateriale.EditValue = ModelMaterialiMR.PMMisurando_Materiale;
            txtPMSostanzaMateriale.EditValue = ModelMaterialiMR.PMSostanza_Materiale;
            txtNumeroLotto.EditValue = ModelMaterialiMR.Numero_Lotto;
            txtDataScadenzaLotto.EditValue = ModelMaterialiMR.DataScadenza_Lotto;
            txtConcentrazioneLotto.EditValue = ModelMaterialiMR.Concentrazione_Lotto;
            txtDensitaLotto.EditValue = ModelMaterialiMR.Densita_Lotto;
            txtPurezzaLotto.EditValue = ModelMaterialiMR.Purezza_Lotto;
            txtAcquaLotto.EditValue = ModelMaterialiMR.Acqua_Lotto;

            try
            {
                int idSoluzioneMaster = 0;

                if (gviewWorkingSolution.SelectedRowsCount == 1)
                {
                    idSoluzioneMaster = Int32.Parse(gviewWorkingSolution.GetFocusedRowCellValue("IDSoluzione").ToString());
                }

                global::EOS.Core.Common.DataBase DB = new global::EOS.Core.Common.DataBase();

                string SQLString = "";

                SQLString = SQLString + "Select ";
                SQLString = SQLString + "NomeComponente, ";
                SQLString = SQLString + "CasComponente, ";
                SQLString = SQLString + "UMConcentrazione, ";
                SQLString = SQLString + "Concentrazione, ";
                SQLString = SQLString + "UMIncertezza, ";
                SQLString = SQLString + "Incertezza ";
                SQLString = SQLString + "from [SERVER026].[LUPIN].dbo.WorkingSolutionsDetails ";
                SQLString = SQLString + "where IDSchedaDocumenti={0} ";

                SQLString = string.Format(SQLString, IDSchedaDocumentiCalled);

                DataTable dt = new DataTable();
                dt = DB.GetDataTable(SQLString, System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionStringEOS"].ConnectionString);

                gridWorkingSolution.DataSource = null;
                gviewWorkingSolution.Columns.Clear();
                gridWorkingSolution.DataSource = dt;
                gviewWorkingSolution.PopulateColumns();
                gridWorkingSolution.ForceInitialize();
            }
            catch (Exception a)
            {

            }

            ModelMaterialiMR = null;
            ControlMaterialiMR = null;
        }

        private void butChiudi_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelControl1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
