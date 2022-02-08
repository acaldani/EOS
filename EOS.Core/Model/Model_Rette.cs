using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Control
{
    class Model_Rette
    {

        public int m_IDRetta;
        public string m_CodiceRetta;
        public string m_Nome;
        public int m_DefaultGiorniScadenza;
        public DateTime m_DataScadenza;
        public int m_IDStato;
        public string m_Tipologia;
        public int m_IDSchedaDocumenti;
        public int m_IDSolvente;
        public string m_Note;
        public DateTime m_DataCreazione;
        public int m_IDUtente;
        
        public int IDRetta   // property
        {
            get { return m_IDRetta; }   // get method
            set { m_IDRetta = value; }  // set method
        }

        public string CodiceRetta   // property
        {
            get { return m_CodiceRetta; }   // get method
            set { m_CodiceRetta = value; }  // set method
        }

        public string Nome   // property
        {
            get { return m_Nome; }   // get method
            set { m_Nome = value; }  // set method
        }

        public int DefaultGiorniScadenza   // property
        {
            get { return m_DefaultGiorniScadenza; }   // get method
            set { m_DefaultGiorniScadenza = value; }  // set method
        }

        public DateTime DataScadenza   // property
        {
            get { return m_DataScadenza; }   // get method
            set { m_DataScadenza = value; }  // set method
        }
        public int IDStato   // property
        {
            get { return m_IDStato; }   // get method
            set { m_IDStato = value; }  // set method
        }

        public string Tipologia   // property
        {
            get { return m_Tipologia; }   // get method
            set { m_Tipologia = value; }  // set method
        }
        public int IDSchedaDocumenti   // property
        {
            get { return m_IDSchedaDocumenti; }   // get method
            set { m_IDSchedaDocumenti = value; }  // set method
        }

        public int IDSolvente   // property
        {
            get { return m_IDSolvente; }   // get method
            set { m_IDSolvente = value; }  // set method
        }

        public string Note   // property
        {
            get { return m_Note; }   // get method
            set { m_Note = value; }  // set method
        }

        public DateTime DataCreazione   // property
        {
            get { return m_DataCreazione; }   // get method
            set { m_DataCreazione = value; }  // set method
        }

        public int IDUtente   // property
        {
            get { return m_IDUtente; }   // get method
            set { m_IDUtente = value; }  // set method
        }
    }
}
