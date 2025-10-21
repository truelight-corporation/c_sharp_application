using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegratedGuiV2
{
    [Serializable]
    public class ComponentVisibility
    {
        [XmlAttribute("name")]
        public string Name { get; set; }

        [XmlAttribute("visible")]
        public bool Visible { get; set; }
    }

}
