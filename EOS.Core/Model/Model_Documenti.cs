using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    public class Model_Documenti
    {
        int m_IDDocumento;
        string m_CodiceComposto;
        string m_NomeDocumento;
        string m_DescrizioneDocumento;
        string m_PathDocumento;
        DateTime m_DataDocumento;

        public int IDDocumento   // property
        {
            get { return m_IDDocumento; }   // get method
            set { m_IDDocumento = value; }  // set method
        }

        public string CodiceComposto   // property
        {
            get { return m_CodiceComposto; }   // get method
            set { m_CodiceComposto = value; }  // set method
        }

        public string NomeDocumento   // property
        {
            get { return m_NomeDocumento; }   // get method
            set { m_NomeDocumento = value; }  // set method
        }

        public string DescrizioneDocumento   // property
        {
            get { return m_DescrizioneDocumento; }   // get method
            set { m_DescrizioneDocumento = value; }  // set method
        }

        public string PathDocumento   // property
        {
            get { return m_PathDocumento; }   // get method
            set { m_PathDocumento = value; }  // set method
        }

        public DateTime DataDocumento   // property
        {
            get { return m_DataDocumento; }   // get method
            set { m_DataDocumento = value; }  // set method
        }
    }
}
