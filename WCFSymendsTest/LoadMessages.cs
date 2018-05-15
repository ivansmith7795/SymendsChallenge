using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data.MySqlClient;

namespace WCFSymendsTest
{
    public class LoadMessages
    {
        
        public List<SMSMessage> GetMessages()
        {
            List<SMSMessage> messages = new List<SMSMessage>();

            string myConnectionString;

            myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["symendsMySQL"].ConnectionString;

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText = "SELECT id, message_in, message_out, Timestamp FROM messages";

                cmd.Prepare();

                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    SMSMessage message = new SMSMessage();
                    message.id = reader.GetInt32(0);
                    message.message_in = reader.GetString(1);
                    message.message_out = reader.GetString(2);
                    message.Timestamp = reader.GetDateTime(3);

                    messages.Add(message);
                }

            }

            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
               
            }

            finally
            {
                if (conn != null)
                {
                    conn.Close();

                }

            }

            return messages;
        }

    }
}