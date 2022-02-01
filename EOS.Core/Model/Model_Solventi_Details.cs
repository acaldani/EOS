using System;
using System.Collections.Generic;
using System.Text;

namespace EOS.Core.Model
{
    public class Model_Solventi_Details
    {
        public int m_IDSolventeDetail;
        public int m_IDSolventeMaster;
        public int m_IDSchedaDocumenti;
        public int m_IDSolvente;
        public string m_UM_Prelievo;
        public decimal m_Quantita_Prelievo;
        public int m_IDApparecchio;
        public int m_IDUtensile;
        public int m_Tipologia_MR;
        public string m_CAS;
        public string m_Note;
        public DateTime m_DataScadenza;
        public int m_IDApparecchio2;
        public int m_IDUtensile2;

        public int IDSolventeDetail   // property
        {
            get { return m_IDSolventeDetail; }   // get method
            set { m_IDSolventeDetail = value; }  // set method
        }

        public int IDSolventeMaster   // property
        {
            get { return m_IDSolventeMaster; }   // get method
            set { m_IDSolventeMaster = value; }  // set method
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

        public string Note   // property
        {
            get { return m_Note; }   // get method
            set { m_Note = value; }  // set method
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
