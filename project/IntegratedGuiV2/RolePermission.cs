using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegratedGuiV2
{
    [Serializable]
    public class RolePermission
    {
        [XmlAttribute("role")]
        public string Role { get; set; }

        [XmlElement("Component")]
        public List<ComponentVisibility> Components { get; set; }
    }
}
