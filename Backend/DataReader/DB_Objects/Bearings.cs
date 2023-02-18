using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.DB_Objects
{
    public class Bearing
    {
        public string bearing_number = "0";
        public string exgauster_id = "0";
        public string entry_id = "0";                //non db
        public string name = "0";
        public string alarm_t_min = "0";
        public string alarm_t_max = "0";
        public string warning_t_min = "0";
        public string warning_t_max = "0";
        public string alarm_va_min = "0";
        public string alarm_va_max = "0";
        public string warning_va_min = "0";
        public string warning_va_max = "0";
        public string alarm_vh_min = "0";
        public string alarm_vh_max = "0";
        public string warning_vh_min = "0";
        public string warning_vh_max = "0";
        public string alarm_vv_min = "0";
        public string alarm_vv_max = "0";
        public string warning_vv_min = "0";
        public string warning_vv_max = "0";
        public string temperature = "0";
        public string va = "0";
        public string vh = "0";
        public string vv = "0";
        public void SetParam(KafkaObj kafkaObj, string value)
        {
            if (kafkaObj.SignalType == "analog")
            {
                if (float.TryParse(value, out float validValue))
                    value = validValue.ToString("0.0000").Replace(',', '.');
            }          

            if (kafkaObj.Name == "warning_max")
            {
                if (kafkaObj.CharacterType == "Температура нагрева")
                {
                    warning_t_max = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ОВ"))
                {
                    warning_va_max = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ГВ"))
                {
                    warning_vh_max = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ВВ"))
                {
                    warning_vv_max = value;
                    return;
                }
            }
            if (name == "warning_min")
            {
                if (kafkaObj.CharacterType == "Температура нагрева")
                {
                    warning_t_min = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ОВ"))
                {
                    warning_va_min = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ГВ"))
                {
                    warning_vh_min = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ВВ"))
                {
                    warning_vv_min = value;
                    return;
                }
            }
            if (kafkaObj.Name == "alarm_max")
            {
                if (kafkaObj.CharacterType == "Температура нагрева")
                {
                    alarm_t_max = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ОВ"))
                {
                    alarm_va_max = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ГВ"))
                {
                    alarm_vh_max = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ВВ"))
                {
                    alarm_vv_max = value;
                    return;
                }
            }
            if (name == "alarm_min")
            {
                if (kafkaObj.CharacterType == "Температура нагрева")
                {
                    alarm_t_min = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ОВ"))
                {
                    alarm_va_min = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ГВ"))
                {
                    alarm_vh_min = value;
                    return;
                }
                else if (kafkaObj.Description.Contains("ВВ"))
                {
                    alarm_vv_min = value;
                    return;
                }
            }
            if (kafkaObj.Name == "temperature")
            {
                temperature = value;
                return;
            }
            else if (kafkaObj.Name == "vibration_axial")
            {
                va = value;
                return;
            }
            else if (kafkaObj.Name == "vibration_horizontal")
            {
                vh = value;
                return;
            }
            else if (kafkaObj.Name == "vibration_vertical")
            {
                vv = value;
                return;
            }

        }
    }
}
