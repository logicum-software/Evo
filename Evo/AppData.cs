using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo
{
    [Serializable]
    class AppData
    {
        public String strName { get; set; }
        public List<String> listNouns;

        public AppData()
        {
            strName = "";
            listNouns = new List<String>();
        }

        public AppData(String name, List<String> nouns)
        {
            strName = name;
            listNouns = new List<String>();

            foreach (String item in nouns)
                listNouns.Add(item);
        }

    }
}
