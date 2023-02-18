using Confluent.Kafka;
using DataReader;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SQLite;
using System.Globalization;
using System.Text.RegularExpressions;

namespace CCloud
{
    class Program
    {
        public static void Main(string[] args)
        {
            Kafka_Manager.Init();           
            DB_Manager.Init();

            using (var consumer = new ConsumerBuilder<string, string>(Kafka_Manager.Consumer_Config).Build())
            {
                consumer.Subscribe(Kafka_Manager.TOPIC);
                try
                {
                    while (true)
                    {
                        var cr = consumer.Consume();

                        DB_Manager.InsertTimestamp(cr.Message.Timestamp);

                        Regex expressionFindCodes = new(@"\[[0-9]*[.:][0-9]*\]");
                        Regex expressionFindValues = new(@" [0-9-]*\.[0-9e-]*");
                        MatchCollection matchedCodes = expressionFindCodes.Matches(cr.Message.Value);
                        MatchCollection matchedValues = expressionFindValues.Matches(cr.Message.Value);

                        for (int i = 0; i < matchedCodes.Count; i++)
                        {
                            string code = matchedCodes[i].Value;
                            string value = matchedValues[i].Value;

                            DB_Manager.InsertData(code, value);
                        }

                        Thread.Sleep(10000);
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Стоп");
                }
                finally
                {
                    consumer.Close();
                }
            };
        }

    }

}








//var config = new ProducerConfig()
//{
//    host = 'rc1a-b5e65f36lm3an1d5.mdb.yandexcloud.net:9091'
//const string topic = "zsmk-9433-dev-01";
//const int cancellationToken = 1000;
//    user = '9433_reader'
//    password = 'eUIpgWu0PWTJaTrjhjQD3.hoyhntiK'

//    SASL_MECHANISM = SCRAM - SHA - 512
//    SASL_SSL = SASL_SSL

//};

