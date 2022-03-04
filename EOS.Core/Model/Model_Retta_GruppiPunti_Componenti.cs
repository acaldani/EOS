using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    public class Model_Retta_GruppiPunti_Componenti
    {
        public int m_IDRetta_GruppoPunti_Componenti;
        public int m_IDRetta;
        public int m_IDGruppoPunti;
        public int m_IDSchedaDocumenti;
        public int m_IDSoluzione;
        public int m_Tipologia_MR;
        public string m_Note;
        public DateTime m_DataSelezione;
        public DateTime m_DataInserimento;

        public int IDRetta_GruppoPunti_Componenti   // property
        {
            get { return m_IDRetta_GruppoPunti_Componenti; }   // get method
            set { m_IDRetta_GruppoPunti_Componenti = value; }  // set method
        }

        public int IDRetta   // property
        {
            get { return m_IDRetta; }   // get method
            set { m_IDRetta = value; }  // set method
        }

        public int IDGruppoPunti   // property
        {
            get { return m_IDGruppoPunti; }   // get method
            set { m_IDGruppoPunti = value; }  // set method
        }

        public int IDSchedaDocumenti   // property
        {
            get { return m_IDSchedaDocumenti; }   // get method
            set { m_IDSchedaDocumenti = value; }  // set method
        }

        public int IDSoluzione   // property
        {
            get { return m_IDSoluzione; }   // get method
            set { m_IDSoluzione = value; }  // set method
        }

        public int Tipologia_MR   // property
        {
            get { return m_Tipologia_MR; }   // get method
            set { m_Tipologia_MR = value; }  // set method
        }

        public string Note   // property
        {
            get { return m_Note; }   // get method
            set { m_Note = value; }  // set method
        }

        public DateTime DataSelezione   // property
        {
            get { return m_DataSelezione; }   // get method
            set { m_DataSelezione = value; }  // set method
        }

        public DateTime DataInserimento   // property
        {
            get { return m_DataInserimento; }   // get method
            set { m_DataInserimento = value; }  // set method
        }
    }
}
