using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class Employee
    {
        [XmlElement]
        public PersonIdentity PersonIdentity { get; set; }
    }
}