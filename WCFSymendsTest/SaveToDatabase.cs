using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace WCFSymendsTest
{
    public class SaveToDatabase
    {

        public bool SaveMessage(SMSMessage message)
        {
            string myConnectionString;

            myConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["symendsMySQL"].ConnectionString;

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                cmd.CommandText += string.Format("INSERT INTO messages (message_in, message_out, Timestamp)  VALUES (@message_in, @message_out, @Timestamp);");
                cmd.Parameters.AddWithValue("@message_in", message.message_in);
                cmd.Parameters.AddWithValue("@message_out", message.message_out);
                cmd.Parameters.AddWithValue("@Timestamp", message.Timestamp);

                Console.WriteLine("Executing insert.... this could take a second.");

                try
                {
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error: {0}", ex.ToString());
                    return false;
                }

            }

            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());
                return false;
            }

            finally
            {
                if (conn != null)
                {
                    conn.Close();

                }

            }
        }
    }
}