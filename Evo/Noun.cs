using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Evo
{
    [Serializable]
    class Noun
    {
        public String strName { get; set; }
        public List<String> listProps { get; set; } 
        public List<String> listMeths { get; set; }

        public Noun()
        {
            strName = "";
            listProps = new List<String>();
            listMeths = new List<String>();
        }

        public Noun(String name, List<String> props, List<String> meths)
        {
            strName = name;

            listProps = new List<String>();
            listMeths = new List<String>();

            foreach (String item in props)
                listProps.Add(item);

            foreach (String item in meths)
                listMeths.Add(item);
        }
    }
}
