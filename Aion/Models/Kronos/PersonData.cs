using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class PersonData
    {
        [XmlElement]
        public Person Person { get; set; }
    }
}