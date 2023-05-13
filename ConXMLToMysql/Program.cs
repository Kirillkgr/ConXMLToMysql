using System;
using ConXMLToMysql.ConectDB;
using Google.Protobuf.Collections;
using MySql.Data.MySqlClient;

namespace ConXMLToMysql
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string[] DBCONNECT = { "127.0.0.1", "testdb", "root", "123456;" };
            // dbCon.Server = "127.0.0.1";
            // dbCon.DatabaseName = "testdb";
            // dbCon.UserName = "root";
            // dbCon.Password = "123456;";

            Console.WriteLine(new RecordingCard(DBCONNECT).StartParseCardToDb() == true
                ? "Card success write to DB"
                : "Card error write to DB");
            //     Console.WriteLine(new RecordingClient().startParseClientToDB() == true
            //         ? "Clients success write to DB"
            //         : "Clients error write to DB");
            // }
        }
    }
}