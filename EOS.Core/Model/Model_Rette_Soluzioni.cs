using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    class Model_Rette_Soluzioni
    {
        public int m_IDRettaSoluzione;
        public int m_IDRetta;
        public int m_IDSoluzione;
        public DateTime m_DataCorrelazione;

        public int IDRettaSoluzione   // property
        {
            get { return m_IDRettaSoluzione; }   // get method
            set { m_IDRettaSoluzione = value; }  // set method
        }

        public int IDRetta   // property
        {
            get { return m_IDRetta; }   // get method
            set { m_IDRetta = value; }  // set method
        }

        public int IDSoluzione   // property
        {
            get { return m_IDSoluzione; }   // get method
            set { m_IDSoluzione = value; }  // set method
        }

        public DateTime DataCorrelazione   // property
        {
            get { return m_DataCorrelazione; }   // get method
            set { m_DataCorrelazione = value; }  // set method
        }
    }
}
