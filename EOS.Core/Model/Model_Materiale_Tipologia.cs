using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EOS.Core.Model
{
    public class Model_Materiale_Tipologia
    {
        public int m_ID;
        public string m_Nome;
        public string m_MultiploSingolo;
        public string m_PesoVolumeConcentrazione;
        public string m_UM;

        public int ID   // property
        {
            get { return m_ID; }   // get method
            set { m_ID = value; }  // set method
        }

        public String Nome   // property
        {
            get { return m_Nome; }   // get method
            set { m_Nome = value; }  // set method
        }

        public String MultiploSingolo   // property
        {
            get { return m_MultiploSingolo; }   // get method
            set { m_MultiploSingolo = value; }  // set method
        }

        public String PesoVolumeConcentrazione   // property
        {
            get { return m_PesoVolumeConcentrazione; }   // get method
            set { m_PesoVolumeConcentrazione = value; }  // set method
        }

        public String UM   // property
        {
            get { return m_UM; }   // get method
            set { m_UM = value; }  // set method
        }
    }
}
