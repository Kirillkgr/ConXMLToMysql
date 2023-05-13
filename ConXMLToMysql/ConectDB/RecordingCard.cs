using System;
using MySql.Data.MySqlClient;
namespace ConXMLToMysql.ConectDB
{
    public class RecordingCard
    {
        public RecordingCard(string []dataToConnect)
        {
            RecordingCard._dataToConnect = dataToConnect;
        }

        private static string[] _dataToConnect;
      public  bool StartParseCardToDb()
        {
            
            var cards = ParsingXml.Program.start();
            var dbCon = Connect.Instance();
            dbCon.Server = _dataToConnect[0];
            dbCon.DatabaseName = _dataToConnect[1];
            dbCon.UserName = _dataToConnect[2];
            dbCon.Password = _dataToConnect[3];
            if (!dbCon.IsConnect()) return true;
            const string queryCreateTable = "CREATE TABLE IF NOT EXISTS cards (CARDCODE bigint PRIMARY KEY,STARTDATE TIMESTAMP ,FINISHDATE TIMESTAMP,LASTNAME text,FIRSTNAME text," +
                                            "SURNAME text,FULLNAME text,GENDERID int,BIRTHDAY TIMESTAMP,PHONEHOME text,PHONEMOBIL text,EMAIL text,CITY text,STREET text,HOUSE text," +
                                            "APARTMENT  text,ALTADDRESS text,CARDTYPE text,OWNERGUID text,CARDPER int,TURNOVER FLOAT); ";
            var cmd = new MySqlCommand(queryCreateTable, dbCon.Connection); // Create table cards if not exist
            cmd.ExecuteReader();
            dbCon.Close();
            foreach (var card in cards)
            {
                Console.WriteLine($"key: {card.Key}  value: {card.Value.CARDCODE}");
                if (CheckExistCard(card.Key.ToString())) continue;
                if (dbCon.IsConnect())
                {
                    var query = "";
                    if (card.Value.STARTDATE.Length < 8)
                        card.Value.STARTDATE = "1970-02-02";
                    if (card.Value.FINISHDATE.Length < 8)
                        card.Value.FINISHDATE = "1970-02-02";
                    if(card.Value.BIRTHDAY.Length<8)
                        card.Value.BIRTHDAY = "1970-02-02";
                            
                    if(card.Value.STARTDATE.Length>=6&&card.Value.FINISHDATE.Length>=6)
                    {
                        query =
                            $"insert into cards(CARDCODE ,STARTDATE ,FINISHDATE ,LASTNAME ,FIRSTNAME ,SURNAME ,FULLNAME ,GENDERID ,BIRTHDAY ,PHONEHOME ,PHONEMOBIL ,EMAIL ,CITY ,STREET ,HOUSE ,APARTMENT  ,ALTADDRESS ,CARDTYPE ,OWNERGUID ,CARDPER ,TURNOVER ) values('{card.Value.CARDCODE}','{card.Value.STARTDATE}','{card.Value.FINISHDATE}','{card.Value.LASTNAME}' ,'{card.Value.FIRSTNAME}' ,'{card.Value.SURNAME}' ,'{card.Value.FULLNAME}' ,'{card.Value.GENDERID}' ,'{card.Value.BIRTHDAY}' ,'{card.Value.PHONEHOME}' ,'{card.Value.PHONEMOBIL}' ,'{card.Value.EMAIL}' ,'{card.Value.CITY}' ,'{card.Value.STREET}' ,'{card.Value.HOUSE}' ,'{card.Value.APARTMENT}' ,'{card.Value.ALTADDRESS}'  ,'{card.Value.CARDTYPE}' ,'{card.Value.OWNERGUID}' ,'{card.Value.CARDPER}' ,'{card.Value.TURNOVER}' );";
                    }

                    var outT = new MySqlCommand(query, dbCon.Connection);
                    outT.ExecuteReader();
                }

                dbCon.Close();
            }

            dbCon.Close();

            return true;
        }

      private static bool CheckExistCard(string idCard) //  Производится проверка на наличие запись в базе. Если есть возвращается  true.  Если нет или ошибка возвращается false
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
                    var query = string.Format("SELECT  *from cards where CARDCODE =" + idCard);
                    var outT = new MySqlCommand(query, dbCon.Connection);
                    var readerOutT = outT.ExecuteReader();
                    if (readerOutT.HasRows)
                    {
                        dbCon.Close();
                        return true;
                    }
                    else
                    {dbCon.Close();
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
            return false;
        }
    }
}