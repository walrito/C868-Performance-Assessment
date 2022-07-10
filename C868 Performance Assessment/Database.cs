using Elements.Logger;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Elements.Database
{
    class MySqlDatabase
    {
        public static List<MySqlParameter> spl = new List<MySqlParameter>();

        public static MySqlDataReader ExecuteReader(string query, List<MySqlParameter> spl, string dbConn)
        {
            try
            {
                MySqlConnection conn = new MySqlConnection(dbConn);
                if (conn.State == ConnectionState.Closed) { conn.Open(); }
                using (MySqlCommand cmd = new MySqlCommand(query, conn))
                {
                    if (spl.Count > 0) { cmd.Parameters.AddRange(spl.ToArray()); }
                    MySqlDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                    spl.Clear();
                    return dr;
                }
            }
            catch (Exception ex)
            {
                spl.Clear();
                Logging.LogMessage("ErrorLog", ex.Message, "error", "SQL_ExecuteReader");
                return null;
            }
        }

        public static void ExecuteNonQuery(string query, List<MySqlParameter> spl, string dbConn)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConn))
                {
                    if (conn.State == ConnectionState.Closed) { conn.Open(); }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (spl.Count > 0) { cmd.Parameters.AddRange(spl.ToArray()); }
                        cmd.ExecuteNonQuery();
                        spl.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                spl.Clear();
                Logging.LogMessage("ErrorLog", ex.Message, "error", "SQL_ExecuteNonQuery");
            }
        }

        public static long GetCount(string query, List<MySqlParameter> spl, string dbConn)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConn))
                {
                    if (conn.State == ConnectionState.Closed) { conn.Open(); }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (spl.Count > 0) { cmd.Parameters.AddRange(spl.ToArray()); }
                        long count = (long)cmd.ExecuteScalar();
                        spl.Clear();
                        return count;
                    }
                }
            }
            catch (Exception ex)
            {
                spl.Clear();
                Logging.LogMessage("ErrorLog", ex.Message, "error", "SQL_GetCount");
                return 0;
            }
        }

        public static DataTable FillDataTable(string query, List<MySqlParameter> spl, string dbConn)
        {
            try
            {
                using (MySqlConnection conn = new MySqlConnection(dbConn))
                {
                    if (conn.State == ConnectionState.Closed) { conn.Open(); }
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (spl.Count > 0) { cmd.Parameters.AddRange(spl.ToArray()); }
                        using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                        {
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            spl.Clear();
                            return dt;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                spl.Clear();
                Logging.LogMessage("ErrorLog", ex.Message, "error", "SQL_FillDataTable");
                return null;
            }
        }
    }
}