using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataReader.DB_Objects
{
    internal class ExgaustersEntries
    {
        public int entries_id;
        public int exgauster_id;
        public float oil_t_after;
        public float oil_t_before;
        public string water_t_after;
        public string water_t_before;
        public float gas_t_after;
        public float underpressure_before;
        public int gas_valve_c;
        public int gas_valve_o;
        public float gas_valve_position;
        public float rotor_current;
        public float rotor_voltage;
        public float stator_current;
        public float stator_voltage;
        public float oil_level;
        public float oil_pressure;
        public int work;
    }
}
