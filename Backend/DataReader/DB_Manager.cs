using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader
{
    public static class DB_Manager
    {
        static SQLiteConnection Connection;
        public static void Init()
        {
            Connection = CreateConnection();
        }
        public static SQLiteConnection CreateConnection()
        {

            SQLiteConnection sqlite_conn;
            // Create a new database connection:
            sqlite_conn = new SQLiteConnection("Data Source= C:\\Users\\Painkiller\\source\\repos\\sinter-machine-exhauster\\Backend\\maindb.db");
            // Open the connection:
            try
            {
                sqlite_conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Connect Error: " + ex);
            }
            return sqlite_conn;
        }
        public static void InsertData(string code, string value)
        {
            if (!float.TryParse(value, CultureInfo.InvariantCulture, out float validValue))
                new Exception("Parsing error");


            SQLiteCommand sqlite_cmd;

            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = "INSERT INTO bearings (exgauster_id, name, alarm_t_min, alarm_t_max, warning_t_minwarning_t_max, alarm_va_min, alarm_va_max, warning_va_min, +" +
                 "warning_va_max, alarm_vh_min, alarm_vh_max, warning_vh_min, warning_vh_max, alarm_vv_min, alarm_vv_max, warning_vv_min, warning_vv_max) VALUES('Test Text ', 1); ";
            sqlite_cmd.ExecuteNonQuery();
        }

        public static void InsertTimestamp(Timestamp timestamp)
        {
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO entries (timestamp) VALUES({timestamp.UtcDateTime.Ticks})";
            sqlite_cmd.ExecuteNonQuery();
        }



        public static void ReadData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM [Kafka Code]";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            Connection.Close();
        }
    }
}
