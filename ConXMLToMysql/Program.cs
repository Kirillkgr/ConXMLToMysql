using System;
using ConXMLToMysql.ConectDB;
namespace ConXMLToMysql
{
    internal abstract class Program
    {
        public static void Main(string[] args)
        {
            string[] connect = { "127.0.0.1", "testdb", "root", "123456;" };
            const string pathCardsFile = ("/home/rill/Documents/Cards_20211005080948.xml");
            const string pathClientsFile = ("/home/rill/Documents/Clients.xml");
            

            Console.WriteLine(new RecordingCard(connect).StartParseCardToDb(pathCardsFile)
                ? "Card success write to DB"
                : "Card error write to DB");
            Console.WriteLine(new RecordingClient(connect).StartParseClientToDb(pathClientsFile)
                ? "Clients success write to DB"
                : "Clients error write to DB");
        }
    }
}