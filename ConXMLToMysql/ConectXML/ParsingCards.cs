using System;
using System.Xml;
using Google.Protobuf.Collections;

namespace ConXMLToMysql.ConectXML
{
    internal abstract class Program
    {
        private static readonly MapField<long, Card> MapCard = new MapField<long, Card>();

        public static MapField<long, Card> Start(string path)
        {
            // var xmlReader = XmlReader.Create("/home/rill/Documents/Cards_20211005080948.xml");
            var xmlReader = XmlReader.Create(path);
            
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType != XmlNodeType.Element) || (xmlReader.Name != "Card")) continue;
                if (!xmlReader.HasAttributes) continue;
                var card = new Card
                {
                    CARDCODE = xmlReader.GetAttribute("CARDCODE"),
                    STARTDATE = xmlReader.GetAttribute("STARTDATE"),
                    FINISHDATE = xmlReader.GetAttribute("FINISHDATE"),
                    LASTNAME = xmlReader.GetAttribute("LASTNAME"),
                    FIRSTNAME = xmlReader.GetAttribute("FIRSTNAME"),
                    SURNAME = xmlReader.GetAttribute("SURNAME"),
                    FULLNAME = xmlReader.GetAttribute("FULLNAME"),
                    GENDERID = xmlReader.GetAttribute("GENDERID"),
                    BIRTHDAY = xmlReader.GetAttribute("BIRTHDAY"),
                    PHONEHOME = xmlReader.GetAttribute("PHONEHOME"),
                    PHONEMOBIL = xmlReader.GetAttribute("PHONEMOBIL"),
                    EMAIL = xmlReader.GetAttribute("EMAIL"),
                    CITY = xmlReader.GetAttribute("CITY"),
                    STREET = xmlReader.GetAttribute("STREET"),
                    HOUSE = xmlReader.GetAttribute("HOUSE"),
                    APARTMENT = xmlReader.GetAttribute("APARTMENT"),
                    ALTADDRESS = xmlReader.GetAttribute("ALTADDRESS"),
                    CARDTYPE = xmlReader.GetAttribute("CARDTYPE"),
                    OWNERGUID = xmlReader.GetAttribute("OWNERGUID"),
                    CARDPER = xmlReader.GetAttribute("CARDPER"),
                    TURNOVER = xmlReader.GetAttribute("TURNOVER")
                };
                MapCard.Add(long.Parse(card.CARDCODE), card);
                Console.WriteLine(xmlReader.GetAttribute("CARDCODE") + ": " +
                                  xmlReader.GetAttribute("STARTDATE"));
            }
            return MapCard;
        }

        internal class Card
        {
            public string CARDCODE = "9999002655657";
            public string STARTDATE = "2020-07-15";
            public string FINISHDATE = "";
            public string LASTNAME = "";
            public string FIRSTNAME = "";
            public string SURNAME = "";
            public string FULLNAME = "";
            public string GENDERID = "3";
            public string BIRTHDAY = "";
            public string PHONEHOME = "";
            public string PHONEMOBIL = "";
            public string EMAIL = "";
            public string CITY = "";
            public string STREET = "";
            public string HOUSE = "";
            public string APARTMENT = "";
            public string ALTADDRESS = "";
            public string CARDTYPE = "";
            public string OWNERGUID = "92E74B8A-C6A9-11EA-8DF5-B42E99C93B3E";
            public string CARDPER = "1";
            public string TURNOVER = "1518.55";
        }
    }
}