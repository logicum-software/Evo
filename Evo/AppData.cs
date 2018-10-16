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
        public List<Noun> listNouns;

        public AppData()
        {
            strName = "";
            listNouns = new List<Noun>();
        }

        public AppData(String name, List<Noun> nouns)
        {
            strName = name;
            listNouns = new List<Noun>();

            foreach (Noun item in nouns)
                listNouns.Add(item);
        }

    }
}
