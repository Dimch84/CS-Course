using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.Xml;

namespace DataSerialization
{
    public class Car : IXmlSerializable
    {
        private int _Year;
        private string _Make;
        private string _Model;

        public int Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public string Make
        {
            get { return _Make; }
            set { _Make = value; }
        }

        public string Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        #region IXmlSerializable Members

        public System.Xml.Schema.XmlSchema GetSchema()
        {
            return null;
        }

        public void ReadXml(XmlReader reader)
        {
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "Make":
                        this.Make = reader.ReadString();
                        break;
                    case "Model":
                        this.Model = reader.ReadString();
                        break;
                    case "Year":
                        this.Year = reader.ReadElementContentAsInt();
                        break;
                }
            }
        }

        public void WriteXml(XmlWriter writer)
        {
            writer.WriteElementString("Make", this.Make);
            writer.WriteElementString("Model", this.Model);
            writer.WriteElementString("Year", this.Year.ToString());
        }

        #endregion
    }
}
