using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml.Serialization;

namespace tutorial2.Models
{
    public class Student
    {

        [JsonPropertyName("indexNumber")]
        [XmlAttribute(AttributeName = "indexNumber")]
        public string indexNumber { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string BirthDate { get; set; }

        public string Email { get; set; }

        public string MothersName { get; set; }

        public string FathersName { get; set; }

        public Studies Studies { get; set; }


    }
}