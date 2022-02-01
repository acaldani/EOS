using System;
using System.Collections.Generic;
using System.Text;

namespace EOS.Core.Model
{
    public class Model_Soluzioni_Details
    {
        public int m_IDSoluzioneDetail;
        public int m_IDSoluzioneMaster;
        public int m_IDSchedaDocumenti;
        public int m_IDSoluzione;
        public string m_UM_Prelievo;
        public decimal m_Quantita_Prelievo;
        public decimal m_Concentrazione;
        public string m_Note;
        public int m_IDApparecchio;
        public int m_IDUtensile;
        public int m_Tipologia_MR;
        public string m_CAS;
        public DateTime m_DataScadenza;
        public int m_IDApparecchio2;
        public int m_IDUtensile2;

        public int IDSoluzioneDetail   // property
        {
            get { return m_IDSoluzioneDetail; }   // get method
            set { m_IDSoluzioneDetail = value; }  // set method
        }

        public int IDSoluzioneMaster   // property
        {
            get { return m_IDSoluzioneMaster; }   // get method
            set { m_IDSoluzioneMaster = value; }  // set method
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

        public string UM_Prelievo   // property
        {
            get { return m_UM_Prelievo; }   // get method
            set { m_UM_Prelievo = value; }  // set method
        }

        public decimal Quantita_Prelievo   // property
        {
            get { return m_Quantita_Prelievo; }   // get method
            set { m_Quantita_Prelievo = value; }  // set method
        }

        public decimal Concentrazione   // property
        {
            get { return m_Concentrazione; }   // get method
            set { m_Concentrazione = value; }  // set method
        }

        public string Note   // property
        {
            get { return m_Note; }   // get method
            set { m_Note = value; }  // set method
        }

        public int IDApparecchio   // property
        {
            get { return m_IDApparecchio; }   // get method
            set { m_IDApparecchio = value; }  // set method
        }

        public int IDUtensile   // property
        {
            get { return m_IDUtensile; }   // get method
            set { m_IDUtensile = value; }  // set method
        }

        public int Tipologia_MR   // property
        {
            get { return m_Tipologia_MR; }   // get method
            set { m_Tipologia_MR = value; }  // set method
        }

        public string CAS   // property
        {
            get { return m_CAS; }   // get method
            set { m_CAS = value; }  // set method
        }

        public DateTime DataScadenza   // property
        {
            get { return m_DataScadenza; }   // get method
            set { m_DataScadenza = value; }  // set method
        }

        public int IDApparecchio2   // property
        {
            get { return m_IDApparecchio2; }   // get method
            set { m_IDApparecchio2 = value; }  // set method
        }

        public int IDUtensile2   // property
        {
            get { return m_IDUtensile2; }   // get method
            set { m_IDUtensile2 = value; }  // set method
        }

    }
}
