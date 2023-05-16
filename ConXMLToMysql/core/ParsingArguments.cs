using System;
using System.Dynamic;

namespace ConXMLToMysql.core
{
    public class ParsingArguments
    {
        public static Args StartParseArguments(string []arguments)
        {
            var args = new Args();
            for (var i = 0; i < arguments.Length; i++)
            {
                switch (arguments[i])
                {
                    case "-card":    args.PathCardsFile   = arguments[i - 1]; break;
                    case "-client":  args.PathClientsFile = arguments[i - 1]; break;
                    case "-server":  args.Server          = arguments[i - 1]; break;
                    case "-database":args.Database        = arguments[i - 1]; break;
                    case "-pass":    args.Pass            = arguments[i - 1]; break;
                    case "-username":args.Username        = arguments[i - 1]; break;
                }
                
            }

            return args;
        }
    }

    public class Args
    {
        public string PathCardsFile { get; set; }
        public string PathClientsFile { get; set; }
        public string Server { get; set; }
        public string Database { get; set; }
        public string Username { get; set; }
        public string Pass { get; set; }
    }
}