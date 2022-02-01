using System;
using System.Collections.Generic;
using System.Text;

namespace EOS.Core.Model
{
    public class Model_Solventi
    {
        public int m_IDSolvente;
        public string m_CodiceSolvente;
        public string m_Tipologia;
        public string m_Nome;
        public string m_NotePrescrittive;
        public string m_NoteDescrittive;
        public int m_IDUbicazione;
        public int m_DefaultGiorniScadenza;
        public DateTime m_DataPreparazione;
        public DateTime m_DataScadenza;
        public int m_IDStato;
        public DateTime m_DataCreazione;
        public int m_IDUtente;

        public int IDSolvente   // property
        {
            get { return m_IDSolvente; }   // get method
            set { m_IDSolvente = value; }  // set method
        }

        public string CodiceSolvente   // property
        {
            get { return m_CodiceSolvente; }   // get method
            set { m_CodiceSolvente = value; }  // set method
        }

        public string Tipologia   // property
        {
            get { return m_Tipologia; }   // get method
            set { m_Tipologia = value; }  // set method
        }

        public string Nome   // property
        {
            get { return m_Nome; }   // get method
            set { m_Nome = value; }  // set method
        }

        public string NotePrescrittive   // property
        {
            get { return m_NotePrescrittive; }   // get method
            set { m_NotePrescrittive = value; }  // set method
        }
        public string NoteDescrittive   // property
        {
            get { return m_NoteDescrittive; }   // get method
            set { m_NoteDescrittive = value; }  // set method
        }

        public int IDUbicazione   // property
        {
            get { return m_IDUbicazione; }   // get method
            set { m_IDUbicazione = value; }  // set method
        }

        public int DefaultGiorniScadenza   // property
        {
            get { return m_DefaultGiorniScadenza; }   // get method
            set { m_DefaultGiorniScadenza = value; }  // set method
        }

        public DateTime DataPreparazione   // property
        {
            get { return m_DataPreparazione; }   // get method
            set { m_DataPreparazione = value; }  // set method
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

