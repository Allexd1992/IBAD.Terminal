using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBAD.Terminal.Model
{
    [Serializable]
    public class Data
    {
        public  double[] Start { get; set; }
        public double[] End { get; set; }
        public string[] Name { get; set; }
        public double deltaTape { get; set; }
        public int Coil01 { get; set; }
        public int Coil02 { get; set; }
        public int MgOstat { get; set; }
        public bool autoRun { get; set; }
        public int runNumb { get; set; }
        public Data()
        {
    
            Start = new double[10];
            End = new double[10];
            Name = new string[10];
            for (int i = 0; i <10 ; i++)
            
            {
                Name[i] = "Tape0"+(i+1);
                Start[i] = i+1;
                End[i] = i+2;
            }
            
        }
    }
}
