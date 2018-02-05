using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class PersonIdentity
    {
        [XmlAttribute]
        public string PersonNumber { get; set; }
    }
}