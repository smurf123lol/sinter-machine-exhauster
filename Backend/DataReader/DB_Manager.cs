using Confluent.Kafka;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using DataReader.DB_Objects;

namespace DataReader
{
    public static class DB_Manager
    {
        //static Timestamp timestamp;
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
        public static void InsertData(List<Bearing> bearings, List<Exgauster> exgausters)
        {
            InsertBears(bearings);
            InsertExgausters(exgausters);         
        }

        private static void InsertExgausters(List<Exgauster> exgausters)
        {
            SQLiteCommand sqlite_cmd = Connection.CreateCommand();
            foreach (var exgauster in exgausters)
            {
                sqlite_cmd.CommandText = $"UPDATE exgausters SET oil_temperature_before = {exgauster.oil_temperature_before}, oil_temperature_after = {exgauster.oil_temperature_after}, " +
                                                          $"water_temperature_before = {exgauster.water_temperature_before},water_temperature_after = {exgauster.water_temperature_after}," +
                                                          $"gas_temperature_before = {exgauster.gas_temperature_before}, gas_underpressure_before = {exgauster.gas_underpressure_before}," +
                                                          $"gas_valve_closed = {exgauster.gas_valve_closed}, gas_valve_open = {exgauster.gas_valve_open}, gas_valve_position = {exgauster.gas_valve_position}," +
                                                          $"rotor_current = {exgauster.rotor_current}, rotor_voltage = {exgauster.rotor_voltage}, stator_current = {exgauster.stator_current}," +
                                                          $"stator_voltage = {exgauster.stator_voltage}, oil_level = {exgauster.oil_level}, oil_pressure = {exgauster.oil_pressure}, work = {exgauster.work} "+
                                                          $"WHERE name ='{exgauster.name}'";
                sqlite_cmd.ExecuteNonQuery();
            }


            sqlite_cmd.CommandText = $"INSERT INTO `exgausters_entries` (exgauster_id,entries_id,oil_t_after,oil_t_before,water_t_after,water_t_before,gas_t_after,underpressure_before," +
                                                                        "gas_valve_c,gas_valve_o,gas_valve_position,rotor_current,rotor_voltage," +
                                                                        "stator_current,stator_voltage,oil_level,oil_pressure,work) VALUES";
            foreach (var exgauster in exgausters)
            {
                sqlite_cmd.CommandText += $"({exgauster.exgauster_id},{exgauster.entry_id},{exgauster.oil_temperature_after},{exgauster.oil_temperature_before},{exgauster.water_temperature_after},{exgauster.water_temperature_before}," +
                                            $"{exgauster.gas_temperature_before},{exgauster.gas_underpressure_before},{exgauster.gas_valve_closed},{exgauster.gas_valve_open},{exgauster.gas_valve_position},{exgauster.rotor_current}," +
                                            $"{exgauster.rotor_voltage},{exgauster.stator_current},{exgauster.stator_voltage},{exgauster.oil_level},{exgauster.oil_pressure},{exgauster.work}),";
            }
            sqlite_cmd.CommandText = sqlite_cmd.CommandText[..^1] + ';';
            sqlite_cmd.ExecuteNonQuery();
        }

        private static void InsertBears(List<Bearing> bearings)
        {
            SQLiteCommand sqlite_cmd = Connection.CreateCommand();

            foreach (var bearing in bearings)
            {
                sqlite_cmd.CommandText = $"UPDATE bearings SET alarm_t_min = {bearing.alarm_t_min}, alarm_t_max = {bearing.alarm_t_max}, " +
                                                           $"warning_t_min = {bearing.warning_t_min},warning_t_max = {bearing.warning_t_max},alarm_va_min = {bearing.alarm_va_min},"+
                                                           $"alarm_va_max = {bearing.alarm_va_max},warning_va_min = {bearing.alarm_va_min}," +
                                                           $"warning_va_max = {bearing.warning_va_max}, alarm_vh_min = {bearing.alarm_vh_min}, alarm_vh_max = {bearing.alarm_vh_max},"+
                                                           $"warning_vh_min = {bearing.warning_vh_min}, warning_vh_max = {bearing.warning_vh_max}," +
                                                           $"alarm_vv_min = {bearing.alarm_vv_min}, alarm_vv_max = {bearing.alarm_vv_max}, warning_vv_min = {bearing.warning_vv_min},"+
                                                           $"warning_vv_max = {bearing.warning_vv_max}, temperature = {bearing.temperature},va = {bearing.va},vh = {bearing.vh},vv = {bearing.vv} " +
                                    $"WHERE exgauster_id = {bearing.exgauster_id} AND name = \'{bearing.name}\'";
                sqlite_cmd.ExecuteNonQuery();
            }


            sqlite_cmd.CommandText = $"INSERT INTO `bearings-entries` (entry_id, temperature, vibration_axial, vibration_horizontal, vibration_vertical) VALUES";

            foreach (var bearing in bearings)
            {
                sqlite_cmd.CommandText += $"({bearing.entry_id},{bearing.temperature},{bearing.va},{bearing.vh},{bearing.vv}),";
            }

            sqlite_cmd.CommandText = sqlite_cmd.CommandText[..^1] + ';' ;

            sqlite_cmd.ExecuteNonQuery();
        }

        public static int InsertTimestamp(Timestamp timestamp)
        {
            
            SQLiteCommand sqlite_cmd;

            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"INSERT INTO entries (timestamp) VALUES({timestamp.UtcDateTime.Ticks})";
            SQLiteDataReader resReader = sqlite_cmd.ExecuteReader();

            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = $"SELECT MAX(id) FROM entries";
            SQLiteDataReader resReader2 = sqlite_cmd.ExecuteReader();

            resReader2.Read();
            return resReader2.GetInt32(0);
        }

        public static void ReadData()
        {
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = Connection.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM [KafkaCode]";

            sqlite_datareader = sqlite_cmd.ExecuteReader();
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                
            }
            Connection.Close();
        }
        public static KafkaObj GetInfoByKafkaCode(string code)
        {
            SQLiteCommand sqlite_cmd = Connection.CreateCommand();                          //[0:46]
            sqlite_cmd.CommandText = $"SELECT * FROM [KafkaCode] WHERE Code = @Code";
            sqlite_cmd.Parameters.Add(new SQLiteParameter("@Code", code));
            SQLiteDataReader sqlite_datareader = sqlite_cmd.ExecuteReader();

            var parser = sqlite_datareader.GetRowParser<KafkaObj>(typeof(KafkaObj));

            if (sqlite_datareader.Read())
            {
                return parser(sqlite_datareader);
            }
            Console.WriteLine("Я упал");
            return default;            
        }


        //private static void ClearPreviousRecords()
        //{
        //    SQLiteCommand sqlite_cmd;
        //    SQLiteDataReader sqlite_datareader;

        //    sqlite_cmd = Connection.CreateCommand();
        //    sqlite_cmd.CommandText =    @"DELETE FROM bearings;" +
        //                                @"DELETE FROM exgausters;" +
        //                                @"VACUUM;";
        //    sqlite_cmd.ExecuteReader();

            //Bearings[] bears = new Bearings[6];
            //BearingsEntries[] bearsEntires = new BearingsEntries[6];
            //Exgausters[] exgausters = new Exgausters[6];
            //ExgaustersEntries[] exgaustersEntries = new ExgaustersEntries[6];

            //var bearingsParser = sqlite_datareader.GetRowParser<Bearings>(typeof(Bearings));
            //var exgaustersParser = sqlite_datareader.GetRowParser<Exgausters>(typeof(Exgausters));

            //int recordNo = 0;

            //while (sqlite_datareader.Read())
            //    bears[recordNo++] = bearingsParser(sqlite_datareader);

            //for (int i = 0; i < bears.Length; i++)
            //{
            //    bearsEntires[i].entry_id =              bears[i].id
            //    bearsEntires[i].temperature =           bears[i]
            //    bearsEntires[i].vibration_axial =       bears[i]
            //    bearsEntires[i].vibration_horizontal =  bears[i]
            //    bearsEntires[i].vibration_vertical =    bears[i]
            //}


            //foreach (var bear in bearsEntires)
            //{

                               
            //}
       // }

    }
}
