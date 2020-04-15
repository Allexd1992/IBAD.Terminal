using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBAD.Terminal.Library
{
    public class TapeDetectEventArgs
    {
        // Сообщение
        public string name { get;  }
        public double position { get;  }

        public TapeDetectEventArgs(string name, double pos)
        {
            this.name = name;
            this.position = pos;
        }
    }
    public class LentaDetect
    {
        public delegate void TapeDetectEventHendler(object sender, TapeDetectEventArgs e);
        public event TapeDetectEventHendler EventResolveComplete;
        public string[] TapeName { get; set; }
        public double[] TapeStart { get; set; }
        public double[] TapeEnd { get; set; }
        public double tapeDelta { get; set; }
        public double systemDelta { get; set; }
        public double position { get; set; }
        public double Lenght { get; set; }
        public string currentTape { get; set; }

        double[] getInterval(double[] Start, double[] End, double DeltaTape, double Delta)
        {
            double[] Out = new double[Start.Length+1];

            for (int i = 0; i < Out.Length; i++)
            {
                if (i == 0)
                {
                    Out[0] = DeltaTape + Delta;
                }
                else
                {
                    double Lenght = Math.Abs(End[i - 1] - Start[i - 1]);
                    Out[i] = Out[i - 1] + Lenght;
                }
            }

            return Out;
        }
        int tapeDetect(double Lenght, double[] setPoint)
            {
            int Out=606;
            for (int i = 0; i < setPoint.Length; i++)
            {
                
                    if (i!= 0 &&Lenght < setPoint[i] && Lenght > setPoint[i - 1]  )
                    {
                        Out = i - 1;
                    }
                    else
                    {
                        if (Lenght < setPoint[0]) Out = -1;
                        if (Lenght > setPoint[setPoint.Length - 1]) Out = setPoint.Length;
                    }
                
              

            }
            return Out;
            }
        double positionResolve(double[] Start, double[] End, double Lenght, int setPoin, double[] interval)
        {


            if (setPoin != -1 && setPoin != Start.Length+1 && setPoin != 606 && Start[setPoin] != End[setPoin])
            {
                double sign = 1.0;
                if (Start[setPoin] > End[setPoin])
                {
                    sign = -1;
                }

                return (Lenght - interval[setPoin]) * sign + Start[setPoin];
            }
            if (setPoin == -1) return Lenght;
            if (setPoin == Start.Length + 1) return Lenght - interval[setPoin-1];
            else return 0;
        }
        string getNameTape(string[] Name, double[] Start, double[] End, int setPoint)
        {
            if (setPoint != -1 && setPoint != 11 && setPoint != 606 && Start[setPoint] != End[setPoint])
            {
                return "#" + Name[setPoint] + "_" + string.Format("{0:0.00}", Start[setPoint]) + "-" + string.Format("{0:0.00}", End[setPoint]);
            }
            if (setPoint == -1) return "TransportStart";
            if (setPoint == Name.Length+1) return "TransportEnd";
            return "Error";
        }
        public LentaDetect(double deltaSys)
        {
            systemDelta = deltaSys;
        }
        public void ResolveParam()
        {
          double[] setPoins= getInterval(TapeStart, TapeEnd, tapeDelta, systemDelta);
          int tapeNum = tapeDetect(Lenght, setPoins);
          position = positionResolve(TapeStart, TapeEnd, Lenght, tapeNum, setPoins);
          currentTape = getNameTape(TapeName, TapeStart, TapeEnd,tapeNum);
          EventResolveComplete?.Invoke(this, new TapeDetectEventArgs(currentTape,position));
        }

    }

}
