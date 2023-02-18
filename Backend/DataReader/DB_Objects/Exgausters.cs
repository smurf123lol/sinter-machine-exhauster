using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DataReader.DB_Objects
{
    public class Exgauster
    {
        public string name = "0";
        public string entry_id = "0";                    //non db
        public string exgauster_id;                      //non db
        public string aglomachine_id = "0";
        public string oil_temperature_before = "0";
        public string oil_temperature_after = "0";
        public string water_temperature_before = "0";
        public string water_temperature_after = "0";
        public string gas_temperature_before = "0";
        public string gas_underpressure_before = "0";
        public string gas_valve_closed = "0";
        public string gas_valve_open = "0";
        public string gas_valve_position = "0";
        public string rotor_current = "0";
        public string rotor_voltage = "0";
        public string stator_current = "0";
        public string stator_voltage = "0";
        public string oil_level = "0";
        public string oil_pressure = "0";
        public string work = "0";

        public void SetParam(KafkaObj kafkaObj, string value)
        {
            if (kafkaObj.SignalType == "analog")
            {
                if (float.TryParse(value, out float validValue))
                    value = validValue.ToString("0.0000").Replace(',', '.');
            }

            if (kafkaObj.Name == "gas_valve_closed")
            {
                gas_valve_closed = value;
            }
            else if (kafkaObj.Name == "gas_valve_open")
            {
                gas_valve_open = value;
            }
            else if (kafkaObj.Name == "gas_valve_position")
            {
                gas_valve_position = value;
            }
            else if (kafkaObj.Name == "oil_level")
            {
                oil_level = value;
            }
            else if (kafkaObj.Name == "oil_pressure")
            {
                oil_pressure = value;
            }
            else if (kafkaObj.Name == "rotor_current")
            {
                rotor_current = value;
            }
            else if (kafkaObj.Name == "rotor_voltage")
            {
                rotor_voltage = value;
            }
            else if (kafkaObj.Name == "stator_current")
            {
                stator_current = value;
            }
            else if (kafkaObj.Name == "stator_voltage")
            {
                stator_voltage = value;
            }
            else if (kafkaObj.Name == "temperature_after")
            {
                if (kafkaObj.CharacterType == "Масло")
                {
                    oil_temperature_after = value;
                }
                else
                {
                    water_temperature_after = value;
                }
            }
            else if (kafkaObj.Name == "temperature_before")
            {
                if (kafkaObj.CharacterType == "Масло")
                {
                    oil_temperature_before = value;
                }
                else if (kafkaObj.CharacterType == "Вода")
                {
                    water_temperature_before = value;
                }
                else
                {
                    gas_temperature_before = value;
                }
            }
            else if (kafkaObj.Name == "underpressure_before")
            {
                gas_underpressure_before = value;
            }
            else if (kafkaObj.Name == "work")
            {
                work = value;
            }
        }
    }
}
