using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using IBAD.Terminal.Library;

namespace IBAD.Terminal.Model
{
    
    class MainModel
    {
        Timer timer;
        double Length { get; set; }
        bool reelOn { get; set; }
        Data data;
        ModbusClientTCP ClientTCP;
        ModbusServerTCP serverTCP;
        List<LentaDetect> Detects;
        MainModel()
        {
            try
            { data = new Data(); }
            catch
            { data = new Data(); }
            Detects = new List<LentaDetect>()
            {
            new LentaDetect(10.0),
            new LentaDetect(10.0),
            new LentaDetect(10.0),
            new LentaDetect(10.0),
            new LentaDetect(10.0),
            new LentaDetect(10.0),
             };
            foreach (var item in Detects)
            {
                item.TapeName = data.Name;
                item.TapeStart = data.Start;
                item.TapeEnd = data.End;
                item.tapeDelta = data.deltaTape;
            }
            ClientTCP = new ModbusClientTCP();
            ClientTCP.ScanSuccesNotify += ClientTCP_ScanSuccesNotify;
            serverTCP = new ModbusServerTCP();
            serverTCP.VariableChangeNotify += ServerTCP_VariableChangeNotify;
            timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        private void ServerTCP_VariableChangeNotify(object sender, ModbusServerWriteEventArgs e)
        {
            if (!e.getStatus)
            {
                data.Start=serverTCP.getStart();
                data.End= serverTCP.getEnd();
                data.Name= serverTCP.getName();
                data.Coil01 = serverTCP.getCoil()[0];
                data.Coil02 = serverTCP.getCoil()[1];
            }
        }
        private void writeCoil()
        {
         
        }
        private void ClientTCP_ScanSuccesNotify(object sender, ModbusClientScanCompletArgs e)
        {
            Length=e.len;
            reelOn = e.reelOn;
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
           
        }

        private void MainModel_EventResolveComplete(object sender, TapeDetectEventArgs e)
        {
           
        }
    }
}
