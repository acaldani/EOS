using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    public class Model_Configurazione
    {
        public int m_IDConfigurazione;
        public string m_Email;
        public int m_LoginAilitazioni;
        public bool m_LogTempiEsecuzione;
        public bool m_Attiva;
        public string m_AuthorizationPassword;
        public bool m_NewFromTemplate;
        public string m_DocumentsRoot;

        public int IDConfigurazione   // property
        {
            get { return m_IDConfigurazione; }   // get method
            set { m_IDConfigurazione = value; }  // set method
        }

        public string Email   // property
        {
            get { return m_Email; }   // get method
            set { m_Email = value; }  // set method
        }

        public int LoginAbilitazioni   // property
        {
            get { return m_LoginAilitazioni; }   // get method
            set { m_LoginAilitazioni = value; }  // set method
        }

        public bool LogTempiEsecuzione   // property
        {
            get { return m_LogTempiEsecuzione; }   // get method
            set { m_LogTempiEsecuzione = value; }  // set method
        }

        public bool Attiva   // property
        {
            get { return m_Attiva; }   // get method
            set { m_Attiva = value; }  // set method
        }

        public string AuthorizationPassword   // property
        {
            get { return m_AuthorizationPassword; }   // get method
            set { m_AuthorizationPassword = value; }  // set method
        }

        public bool NewFromTemplate   // property
        {
            get { return m_NewFromTemplate; }   // get method
            set { m_NewFromTemplate = value; }  // set method
        }
        public string DocumentsRoot   // property
        {
            get { return m_DocumentsRoot; }   // get method
            set { m_DocumentsRoot = value; }  // set method
        }

    }
}
