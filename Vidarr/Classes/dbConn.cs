using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;

namespace Vidarr.Classes
{
    class dbConn
    {
        public static void dbConnection()
        {
            MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "Server=127.0.0.1;Database=vidarr;Uid=root;Pwd='';SslMode=None;charset=utf8";

            try
            {
                conn = new MySqlConnection(myConnectionString);
                MySqlCommand cmd = new MySqlCommand();

                cmd.CommandText = "truncate video";
                conn.Open();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (MySqlException ex)
            {
                var dialog = new MessageDialog(ex.Message);
            }
        }
    }
}

