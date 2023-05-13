using System;
using System.Xml;
using Google.Protobuf.Collections;

namespace ConXMLToMysql.ConectXML
{
    public class ParseClients
    {

        public static MapField<long, Client>  MapField = new MapField<long, Client>();

        public static MapField<long, Client> start(string path)
        {
            // XmlReader xmlReader = XmlReader.Create("/home/rill/Documents/Clients.xml");
            XmlReader xmlReader = XmlReader.Create(path);
            
            while (xmlReader.Read())
            {
                if ((xmlReader.NodeType == XmlNodeType.Element) && (xmlReader.Name == "Client"))
                {
                    if (xmlReader.HasAttributes)
                    {
                        var client = new Client();
                        client.CARDCODE = xmlReader.GetAttribute("CARDCODE");
                        client.STARTDATE = xmlReader.GetAttribute("STARTDATE");
                        client.FINISHDATE = xmlReader.GetAttribute("FINISHDATE");
                        client.LASTNAME = xmlReader.GetAttribute("LASTNAME");
                        client.FIRSTNAME = xmlReader.GetAttribute("FIRSTNAME");
                        client.SURNAME = xmlReader.GetAttribute("SURNAME");
                        client.GENDER = xmlReader.GetAttribute("GENDER");
                        client.BIRTHDAY = xmlReader.GetAttribute("BIRTHDAY");
                        client.PHONEHOME = xmlReader.GetAttribute("PHONEHOME");
                        client.PHONEMOBIL = xmlReader.GetAttribute("PHONEMOBIL");
                        client.EMAIL = xmlReader.GetAttribute("EMAIL");
                        client.CITY = xmlReader.GetAttribute("CITY");
                        client.STREET = xmlReader.GetAttribute("STREET");
                        client.HOUSE = xmlReader.GetAttribute("HOUSE");
                        client.APARTMENT = xmlReader.GetAttribute("APARTMENT");
                        
                     
                        MapField.Add(long.Parse(client.CARDCODE), client);
                        Console.WriteLine(xmlReader.GetAttribute("CARDCODE") + ": " +
                                          xmlReader.GetAttribute("STARTDATE"));
                    }
                }
            }
            return MapField;
        }


        public class Client
        {
            public string CARDCODE="12345667890",
                STARTDATE="2018-07-19",
                FINISHDATE="",
                LASTNAME="Иванов ",
                FIRSTNAME="Виталий",
                SURNAME="Петрович",
                GENDER="M",
                BIRTHDAY="1980-01-01",
                PHONEHOME="",
                PHONEMOBIL="+7999-554-44-66",
                EMAIL="vv@robotx.ru",
                CITY="Калининград г", 
                STREET="Московский пр-кт",
                HOUSE="40",
                APARTMENT="2";
        }
    }
}