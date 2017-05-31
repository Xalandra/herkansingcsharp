﻿using MySql.Data.MySqlClient;
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
        MySqlConnection conn;
        string myConnectionString;

        public dbConn() {
            myConnectionString = "Server=127.0.0.1;Database=vidarr;Uid=root;Pwd='';SslMode=None;charset=utf8";

            try
            {
                conn = new MySqlConnection(myConnectionString);
                conn.Open();
            }
            catch (Exception)
            {
                ErrorDialog.showMessage("Fout bij dbConn constructor");
            }
        }
        public void dbTruncate()
        {
            try { 
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "truncate video";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();

                dbClose();
            }
            catch (Exception)
            {
                ErrorDialog.showMessage("Fout bij dbTruncate");
            }
        }

        public void dbClose()
        {
            conn.Close();
        }

        public void insertQuery(string query)
        {
            try { 
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;

                cmd.CommandType = CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                ErrorDialog.showMessage("Fout bij insertQuery" + ex.Message);
            }
        }
    }
}

