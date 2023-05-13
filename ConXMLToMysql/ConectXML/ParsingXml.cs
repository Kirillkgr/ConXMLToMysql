using System;
using System.Text;
using System.Xml;
using Google.Protobuf.Collections;
using Lombok.NET;

namespace ParsingXml
{
    class Program
    {
        public static MapField<long, Card> mapCard = new MapField<long, Card>();

        public static MapField<long, Card> start()
        {
            XmlReader xmlReader = XmlReader.Create("/home/rill/Documents/Cards_20211005080948.xml");
            
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Card"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        var card = new Card();
                        card.CARDCODE = xmlReader.GetAttribute("CARDCODE");
                        card.STARTDATE = xmlReader.GetAttribute("STARTDATE");
                        card.FINISHDATE = xmlReader.GetAttribute("FINISHDATE");
                        card.LASTNAME = xmlReader.GetAttribute("LASTNAME");
                        card.FIRSTNAME = xmlReader.GetAttribute("FIRSTNAME");
                        card.SURNAME = xmlReader.GetAttribute("SURNAME");
                        card.FULLNAME = xmlReader.GetAttribute("FULLNAME");
                        card.GENDERID = xmlReader.GetAttribute("GENDERID");
                        card.BIRTHDAY = xmlReader.GetAttribute("BIRTHDAY");
                        card.PHONEHOME = xmlReader.GetAttribute("PHONEHOME");
                        card.PHONEMOBIL = xmlReader.GetAttribute("PHONEMOBIL");
                        card.EMAIL = xmlReader.GetAttribute("EMAIL");
                        card.CITY = xmlReader.GetAttribute("CITY");
                        card.STREET = xmlReader.GetAttribute("STREET");
                        card.HOUSE = xmlReader.GetAttribute("HOUSE");
                        card.APARTMENT = xmlReader.GetAttribute("APARTMENT");
                        card.ALTADDRESS = xmlReader.GetAttribute("ALTADDRESS");
                        card.CARDTYPE = xmlReader.GetAttribute("CARDTYPE");
                        card.OWNERGUID = xmlReader.GetAttribute("OWNERGUID");
                        card.CARDPER = xmlReader.GetAttribute("CARDPER");
                        card.TURNOVER = xmlReader.GetAttribute("TURNOVER");
                        mapCard.Add(long.Parse(card.CARDCODE), card);
                        Console.WriteLine(xmlReader.GetAttribute("CARDCODE") + ": " +
                                          xmlReader.GetAttribute("STARTDATE"));
                    }
                }
            }
            return mapCard;
        }

        [With]
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