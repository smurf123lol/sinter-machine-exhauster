using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.DB_Objects
{
    public class KafkaObj
    {
        //public KafkaObj(string code,
        //                string exgausterNo,
        //                string exgausterModel,
        //                string dataSourse,
        //                string characterType,
        //                string type,
        //                string name,
        //                string description,
        //                string signalType,
        //                string activity)
        //{
        //    Code = code;
        //    ExgausterNo = exgausterNo;
        //    ExgausterModel = exgausterModel;
        //    DataSourse = dataSourse;
        //    CharacterType = characterType;
        //    Type = type;
        //    Name = name;
        //    Description = description;
        //    SignalType = signalType;
        //    Activity = activity;
        //}

        public string Code;
        public string ExgausterNo;
        public string ExgausterModel;
        public string DataSourse;
        public string CharacterType;
        public string Type;
        public string Name;
        public string Description;
        public string SignalType;
        public int Activity;
    }
}
