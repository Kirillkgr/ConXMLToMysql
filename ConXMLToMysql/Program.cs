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
            var cards = ParsingXml.Program.start();
            string queryCreateTable =
                "CREATE TABLE IF NOT EXISTS cards (CARDCODE bigint PRIMARY KEY,STARTDATE TIMESTAMP ,FINISHDATE TIMESTAMP,LASTNAME text,FIRSTNAME text,SURNAME text,FULLNAME text,GENDERID int,BIRTHDAY TIMESTAMP,PHONEHOME text,PHONEMOBIL text,EMAIL text,CITY text,STREET text,HOUSE text,APARTMENT  text,ALTADDRESS text,CARDTYPE text,OWNERGUID text,CARDPER int,TURNOVER FLOAT); ";
            string myConnectionString = "server=127.0.0.1;uid=root;" +
                                        "pwd=123456;database=testdb";
            var dbCon = Connect.Instance();
            dbCon.Server = "127.0.0.1";
            dbCon.DatabaseName = "testdb";
            dbCon.UserName = "root";
            dbCon.Password = "123456;";
            if (dbCon.IsConnect())
            {
                string query = "SELECT id,age FROM users";
                var cmd = new MySqlCommand(queryCreateTable, dbCon.Connection);
                var reader = cmd.ExecuteReader();
                dbCon.Close();


                foreach (var card in cards)
                {
                    Console.WriteLine($"key: {card.Key}  value: {card.Value.CARDCODE}");
                    if (!checkExistCard(card.Key.ToString()))
                    {
                        if (dbCon.IsConnect())
                        {
                            string Query = "";
                            if (card.Value.STARTDATE.Length < 8)
                                card.Value.STARTDATE = "1970-02-02";
                            if (card.Value.FINISHDATE.Length < 8)
                                card.Value.FINISHDATE = "1970-02-02";
                            if(card.Value.BIRTHDAY.Length<8)
                                card.Value.BIRTHDAY = "1970-02-02";
                            
                            if(card.Value.STARTDATE.Length>=6&&card.Value.FINISHDATE.Length>=6)
                            {
                                Query =
                                    string.Format(
                                        "insert into cards(CARDCODE ,STARTDATE ,FINISHDATE ,LASTNAME ,FIRSTNAME ,SURNAME ,FULLNAME ,GENDERID ,BIRTHDAY ,PHONEHOME ,PHONEMOBIL ,EMAIL ,CITY ,STREET ,HOUSE ,APARTMENT  ,ALTADDRESS ,CARDTYPE ,OWNERGUID ,CARDPER ,TURNOVER ) values('{0}','{1}','{2}','{3}' ,'{4}' ,'{5}' ,'{6}' ,'{7}' ,'{8}' ,'{9}' ,'{10}' ,'{11}' ,'{12}' ,'{13}' ,'{14}' ,'{15}' ,'{16}'  ,'{17}' ,'{18}' ,'{19}' ,'{20}' );",
                                        card.Value.CARDCODE, card.Value.STARTDATE, card.Value.FINISHDATE,
                                        card.Value.LASTNAME, card.Value.FIRSTNAME, card.Value.SURNAME,
                                        card.Value.FULLNAME,
                                        card.Value.GENDERID, card.Value.BIRTHDAY, card.Value.PHONEHOME,
                                        card.Value.PHONEMOBIL,
                                        card.Value.EMAIL, card.Value.CITY, card.Value.STREET, card.Value.HOUSE,
                                        card.Value.APARTMENT, card.Value.ALTADDRESS, card.Value.CARDTYPE,
                                        card.Value.OWNERGUID,
                                        card.Value.CARDPER, card.Value.TURNOVER);
                            }

                            var outT = new MySqlCommand(Query, dbCon.Connection);
                            var readerOutT = outT.ExecuteReader();
                        }

                        dbCon.Close();
                    }
                }

                dbCon.Close();
            }

            // for (int i = 0; i < ParsingXml.Program.mapCard.Count - 1; i++) // Для теста
            // {
            //     Console.WriteLine(ParsingXml.Program.mapCard.Values.ToString());
            // }
            // dbCon.Close();
        }

        static bool checkExistCard(string idCard) //  Производится проверка на наличие запись в базе. Если есть возвращается  true.  Если нет или ошибка возвращается false
        {
            var dbCon = Connect.Instance();
            dbCon.Server = "127.0.0.1";
            dbCon.DatabaseName = "testdb";
            dbCon.UserName = "root";
            dbCon.Password = "123456;";
            if (dbCon.IsConnect())
            {
                try
                {
                    string id = idCard;
                    string Query = string.Format("SELECT  *from cards where CARDCODE =" + idCard);
                    var outT = new MySqlCommand(Query, dbCon.Connection);
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
                    dbCon.Close();
                    return false;
                }
            }

            dbCon.Close();
            return false;
        }
    }
}