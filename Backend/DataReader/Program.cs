using Confluent.Kafka;
using DataReader;
using DataReader.DB_Objects;
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
                    List<Bearing> bears = new();
                    List<Exgauster> exgausters = new();
                    while (true)
                    {
                        var cr = consumer.Consume();

                        string timeID = DB_Manager.InsertTimestamp(cr.Message.Timestamp).ToString();

                        Regex expressionFindCodes = new(@"\[[0-9]*[.:][0-9]*\]");
                        Regex expressionFindValues = new(@" [0-9-]*\.[0-9e-]*");
                        MatchCollection matchedCodes = expressionFindCodes.Matches(cr.Message.Value);
                        MatchCollection matchedValues = expressionFindValues.Matches(cr.Message.Value);

                        //List<Bearing> bears = new();                        
                        //List<Exgauster> exgausters = new();                                               

                        for (int i = 0; i < matchedCodes.Count; i++)
                        {
                            string code = matchedCodes[i].Value;
                            string value = matchedValues[i].Value;
                            KafkaObj kafkaInfo = DB_Manager.GetInfoByKafkaCode(code);

                            if (kafkaInfo == default)
                                continue;

                            if (kafkaInfo.DataSourse.Contains("Подшипник"))
                            {
                                char bearID = kafkaInfo.DataSourse.Last();
                                string exgID = kafkaInfo.ExgausterNo;
                                Bearing tmpBear =  bears.FindLast(el => el.name == kafkaInfo.DataSourse && el.exgauster_id == exgID);

                                if (tmpBear == default)
                                    bears.Add(new() { exgauster_id = exgID, name = kafkaInfo.DataSourse, entry_id = timeID});
                                
                                int index = bears.FindLastIndex(el => el.name == kafkaInfo.DataSourse && el.exgauster_id == exgID);
                                bears[index].SetParam(kafkaInfo, value);                                
                            }
                            else
                            {
                                char bearID = kafkaInfo.DataSourse.Last();
                                string exgID = kafkaInfo.ExgausterNo;
                                Exgauster tmpExgausters = exgausters.FindLast(el => el.name == kafkaInfo.ExgausterModel);

                                if (tmpExgausters == default)
                                    exgausters.Add(new() { name = kafkaInfo.ExgausterModel, exgauster_id = kafkaInfo.ExgausterNo, aglomachine_id = kafkaInfo.ExgausterNo });

                                int index = exgausters.FindLastIndex(el => el.name == kafkaInfo.ExgausterModel);
                                exgausters[index].SetParam(kafkaInfo, value);
                            }
                        }
                        DB_Manager.InsertData(bears, exgausters);
                        Thread.Sleep(10000);
                        //new Exception();
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

