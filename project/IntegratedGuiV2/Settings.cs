using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IntegratedGuiV2
{
    [Serializable]
    [XmlRoot("Settings")]
    public class Settings
    {
        [XmlArray("Components")]
        [XmlArrayItem("Component")]
        public List<ComponentVisibility> Components { get; set; }

        [XmlArray("Permissions")]
        [XmlArrayItem("Permission")]
        public List<RolePermission> Permissions { get; set; }
    }
}
