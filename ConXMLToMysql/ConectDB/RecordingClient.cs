using System;
using MySql.Data.MySqlClient;

namespace ConXMLToMysql.ConectDB
{
    public class RecordingClient
    {
        public RecordingClient(string[] dataToConnect)
        {
            RecordingClient._dataToConnect = dataToConnect;
        }

        private static string[] _dataToConnect;

        public bool StartParseClientToDb()
        {
            var clients = ParsingXml.ParseClients.start();
            string queryCreateTable =
                "CREATE TABLE IF NOT EXISTS clients (CARDCODE bigint PRIMARY KEY,STARTDATE text ,FINISHDATE text," +
                "LASTNAME text,FIRSTNAME text,SURNAME text,GENDER text,BIRTHDAY text,PHONEHOME text,PHONEMOBIL text,EMAIL text," +
                "CITY text,STREET text,HOUSE text,APARTMENT  text); ";
            var dbCon = Connect.Instance();
            dbCon.Server = _dataToConnect[0];
            dbCon.DatabaseName = _dataToConnect[1];
            dbCon.UserName = _dataToConnect[2];
            dbCon.Password = _dataToConnect[3];
            if (dbCon.IsConnect())
            {
                var cmd = new MySqlCommand(queryCreateTable, dbCon.Connection);
                cmd.ExecuteReader();
                dbCon.Close();


                foreach (var client in clients)
                {
                    Console.WriteLine($"key: {client.Key}  value: {client.Value.CARDCODE}");
                    if (!CheckExistCard(client.Key.ToString()))
                    {
                        if (dbCon.IsConnect())
                        {
                            string query = "";
                            if (client.Value.STARTDATE.Length < 8)
                                client.Value.STARTDATE = "1970-02-02";
                            if (client.Value.FINISHDATE.Length < 8)
                                client.Value.FINISHDATE = "1970-02-02";
                            if (client.Value.BIRTHDAY.Length < 8)
                                client.Value.BIRTHDAY = "1970-02-02";

                            if (client.Value.STARTDATE.Length >= 6 && client.Value.FINISHDATE.Length >= 6)
                            {
                                query =
                                    string.Format(
                                        "insert into clients(CARDCODE ,STARTDATE ,FINISHDATE ,LASTNAME ,FIRSTNAME ,SURNAME  " +
                                        ",BIRTHDAY ,PHONEHOME ,PHONEMOBIL ,EMAIL ,CITY ,STREET ,HOUSE ,APARTMENT   ) " +
                                        "values('{0}','{1}','{2}','{3}' ,'{4}' ,'{5}' ,'{6}'" +
                                        " ,'{7}' ,'{8}' ,'{9}' ,'{10}' ,'{11}' ,'{12}' ,'{13}');",
                                        client.Value.CARDCODE, client.Value.STARTDATE, client.Value.FINISHDATE,
                                        client.Value.LASTNAME, client.Value.FIRSTNAME, client.Value.SURNAME,
                                        client.Value.BIRTHDAY, client.Value.PHONEHOME, client.Value.PHONEMOBIL,
                                        client.Value.EMAIL, client.Value.CITY, client.Value.STREET,
                                        client.Value.HOUSE, client.Value.APARTMENT);
                            }

                            var outT = new MySqlCommand(query, dbCon.Connection);
                            outT.ExecuteReader();
                        }

                        dbCon.Close();
                    }
                }

                dbCon.Close();
            }

            return true;
        }

        static bool CheckExistCard(string idCard) //  Производится проверка на наличие запись в базе. Если есть возвращается  true.  Если нет или ошибка возвращается false
        {
            var dbCon = Connect.Instance();
            dbCon.Server = _dataToConnect[0];
            dbCon.DatabaseName = _dataToConnect[1];
            dbCon.UserName = _dataToConnect[2];
            dbCon.Password = _dataToConnect[3];
            if (dbCon.IsConnect())
            {
                try
                {
                    var query = string.Format("SELECT  *from clients where CARDCODE =" + idCard);
                    var outT = new MySqlCommand(query, dbCon.Connection);
                    var readerOutT = outT.ExecuteReader();
                    if (readerOutT.HasRows)
                    {
                        dbCon.Close();
                        return true;
                    }
                    else
                    {
                        dbCon.Close();
                        return false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    dbCon.Close();
                    return false;
                }
            }

            dbCon.Close();
            return false;
        }
    }
}