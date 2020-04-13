using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBAD.Terminal.Model
{
    [Serializable]
    class Data
    {
        public  double[] Start { get; set; }
        public double[] End { get; set; }
        public string[] Name { get; set; }
        public double deltaTape { get; set; }
        public int Coil01 { get; set; }
        public int Coil02 { get; set; }
        public bool F_Base_on { get;  set; }
        public bool Get_Set_Stat { get; set; }
        public bool AutoRun_stat { get; set; }
        public Data()
        {
            
            Start = new double[10];
            End = new double[10];
            Name = new string[10];
            
        }
    }
}
