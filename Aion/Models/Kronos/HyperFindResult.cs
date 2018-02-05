using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    [XmlRoot]
    public class HyperFindResult
    {
        [XmlAttribute]
        public string FullName { get; set; }

        [XmlAttribute]
        public string PersonNumber { get; set; }

        [XmlElement]
        public PersonData PersonData { get; set; }

        public string parsedPerson
        {
            get
            {
                return int.Parse(PersonNumber.Replace("UK", "")).ToString();
            }
        }
    }
}