using System;
using ConXMLToMysql.ConectDB;
using ConXMLToMysql.core;

namespace ConXMLToMysql
{
    internal abstract class Program
    {
        public static void Main(string[] args)
        {
            var parsArg = ParsingArguments.StartParseArguments(args);
            var pathCardsFile = parsArg.PathCardsFile;
            var pathClientsFile = parsArg.PathClientsFile;
            var server = parsArg.Server;
            var database = parsArg.Database;
            var username = parsArg.Username;
            var pass = parsArg.Pass;
         
            if (pathClientsFile.Length < 1)
                pathClientsFile = "/home/rill/Documents/Clients.xml";
            if (pathCardsFile.Length < 1)
                pathCardsFile = "/home/rill/Documents/Cards_20211005080948.xml";
            string[] connect = { "127.0.0.1", "testdb", "root", "123456;" };
            if (server.Length > 7 && database.Length > 2 && pass.Length > 4 && username.Length > 3)
            {
                connect = new[] { server, database, username, pass };
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