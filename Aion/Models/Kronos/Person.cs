using System;
using System.Xml.Serialization;

namespace Aion.Models.Kronos
{
    public class Person
    {
        [XmlAttribute]
        public string BirthDate { get; set; }

        [XmlAttribute]
        public string FirstName { get; set; }

        [XmlAttribute]
        public string FullName { get; set; }

        [XmlAttribute]
        public string HireDate { get; set; }

        [XmlAttribute]
        public string LastName { get; set; }

        [XmlAttribute]
        public string PersonNumber { get; set; }

        [XmlAttribute]
        public string ShortName { get; set; }

        [XmlAttribute]
        public int EmployeeStandardHours { get; set; }

        [XmlAttribute]
        public int FullTimeStandardHours { get; set; }

        [XmlAttribute]
        public string AccrualProfileName { get; set; }

        [XmlAttribute]
        public string ManagerSignoffThruDateTime { get; set; }

        [XmlAttribute]
        public string PayrollLockoutThruDateTime { get; set; }

        [XmlAttribute]
        public bool FingerRequiredFlag { get; set; }

        [XmlAttribute]
        public decimal BaseWageHourly { get; set; }

        public DateTime SignOffDate => DateTime.Parse(ManagerSignoffThruDateTime);
    }
}