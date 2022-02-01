using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    public class Model_WorkingSolution_Details
    {
        public int m_IDWorkingSolutionDetail;
        public int m_IDSchedaDocumenti;
        public string m_NomeComponente;
        public string m_CasComponente;
        public string m_UMConcentrazione;
        public decimal m_Concentrazione;
        public string m_UMIncertezza;
        public decimal m_Incertezza;

        public int IDWorkingSolutionDetail   // property
        {
            get { return m_IDWorkingSolutionDetail; }   // get method
            set { m_IDWorkingSolutionDetail = value; }  // set method
        }

        public int IDSchedaDocumenti   // property
        {
            get { return m_IDSchedaDocumenti; }   // get method
            set { m_IDSchedaDocumenti = value; }  // set method
        }

        public string NomeComponente   // property
        {
            get { return m_NomeComponente; }   // get method
            set { m_NomeComponente = value; }  // set method
        }

        public string CASComponente   // property
        {
            get { return m_CasComponente; }   // get method
            set { m_CasComponente = value; }  // set method
        }

        public string UMConcentrazione   // property
        {
            get { return m_UMConcentrazione; }   // get method
            set { m_UMConcentrazione = value; }  // set method
        }

        public decimal Concentrazione   // property
        {
            get { return m_Concentrazione; }   // get method
            set { m_Concentrazione = value; }  // set method
        }

        public string UMIncertezza   // property
        {
            get { return m_UMIncertezza; }   // get method
            set { m_UMIncertezza = value; }  // set method
        }

        public decimal Incertezza   // property
        {
            get { return m_Incertezza; }   // get method
            set { m_Incertezza = value; }  // set method
        }

    }
}
