using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    public class Model_Soluzioni_Details_Concentration
    {
        public int m_IDSoluzioneDetailConcentration;
        public int m_IDSoluzioneMaster;
        public int m_IDSchedaDocumenti;
        public string m_CAS;
        public decimal m_ConcentrazioneFinale;
        public DateTime m_DataCalcolo;

        public int IDSoluzioneDetailConcentration   // property
        {
            get { return m_IDSoluzioneDetailConcentration; }   // get method
            set { m_IDSoluzioneDetailConcentration = value; }  // set method
        }

        public int IDSoluzioneMaster   // property
        {
            get { return m_IDSoluzioneMaster; }   // get method
            set { m_IDSoluzioneMaster = value; }  // set method
        }

        public String CAS   // property
        {
            get { return m_CAS; }   // get method
            set { m_CAS = value; }  // set method
        }

        public decimal ConcentrazioneFinale   // property
        {
            get { return m_ConcentrazioneFinale; }   // get method
            set { m_ConcentrazioneFinale = value; }  // set method
        }

        public DateTime DataCalcolo   // property
        {
            get { return m_DataCalcolo; }   // get method
            set { m_DataCalcolo = value; }  // set method
        }
    }
}
