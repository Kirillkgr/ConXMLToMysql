using System;
using ConXMLToMysql.ConectDB;

namespace ConXMLToMysql
{
    internal abstract class Program
    {
        public static void Main(string[] args)
        {
            var pathCardsFile = "";
            var pathClientsFile = "";
            var server = "";
            var database = "";
            var username = "";
            var pass = "";
            // var pathCardsFile = ("/home/rill/Documents/Cards_20211005080948.xml");
            // var pathClientsFile = ("/home/rill/Documents/Clients.xml");
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-card")
                {
                    pathCardsFile = args[i - 1];
                }

                if (args[i] == "-client")
                {
                    pathClientsFile = args[i - 1];
                }

                if (args[i] == "-server")
                {
                    server = args[i - 1];
                }

                if (args[i] == "-database")
                {
                    database = args[i - 1];
                }

                if (args[i] == "-pass")
                {
                    pass = args[i - 1];
                }

                if (args[i] == "-username")
                {
                    username = args[i - 1];
                }
            }

            // /home/rill/Documents/Clients.xml -client /home/rill/Documents/Cards_20211005080948.xml -card 127.0.0.1 -server  testdb -database root -username 123456 -pass
            // {pathToClientFile} -client {pathToCardFile} -card {ipAddressToDB} -server  {nameDB} -database {userName} -username {pass} -pass

            if (pathClientsFile.Length < 1)
                pathClientsFile = "/home/rill/Documents/Clients.xml";
            if (pathCardsFile.Length < 1)
                pathCardsFile = "/home/rill/Documents/Cards_20211005080948.xml";
            string[] connect ={ "127.0.0.1", "testdb", "root", "123456;" };
            if (server.Length > 7 && database.Length > 2 && pass.Length > 4 && username.Length > 3)
            {
                connect= new[] { server, database, username, pass };
            }


            Console.WriteLine(new RecordingCard(connect).StartParseCardToDb(pathCardsFile)
                ? "Card success write to DB"
                : "Card error write to DB");
            Console.WriteLine(new RecordingClient(connect).StartParseClientToDb(pathClientsFile)
                ? "Clients success write to DB"
                : "Clients error write to DB");
        }
    }
}