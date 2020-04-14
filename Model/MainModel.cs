using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Serialization;
using IBAD.Terminal.Library;

namespace IBAD.Terminal.Model
{

    class MainModel
    {
        Timer timer;
        double Length { get; set; }
        bool reelOn { get; set; }
        public bool getSetStat {get; set;}
        public bool flDbase { get; set; }
        public bool dBaseStat { get; set; }
        public string[] curName { get; set; }
        public double[] curPos { get; set; }
        public int TapeNum { get; set; }


        public Data data;
        ModbusClientTCP ClientTCP;
        ModbusServerTCP serverTCP;
        List<LentaDetect> Detects;
        public MainModel()
        {
            try
            {
                XmlSerializer formatter = new XmlSerializer(typeof(Data));
                using (FileStream fs = new FileStream("Data.xml", FileMode.OpenOrCreate))
                {
                    data = formatter.Deserialize(fs) as Data;
                    Console.WriteLine("Объект десериализован");
                }
            }
            catch
            { data = new Data(); }

            Detects = new List<LentaDetect>()
            {
            new LentaDetect(3.0),
            new LentaDetect(45.2),
            new LentaDetect(54.5),
            new LentaDetect(63.05),
            new LentaDetect(64.2),
            new LentaDetect(85.6),
             };
            curPos = new double[6];
            curName = new string[6];
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
            Detects[0].EventResolveComplete += MainModel_EventResolveCompleteTm01;
            Detects[1].EventResolveComplete += MainModel_EventResolveCompleteTm02;
            Detects[2].EventResolveComplete += MainModel_EventResolveCompleteTm03;
            Detects[4].EventResolveComplete += MainModel_EventResolveCompleteTm04;
            Detects[5].EventResolveComplete += MainModel_EventResolveCompleteTm05;
            Detects[6].EventResolveComplete += MainModel_EventResolveCompleteTm06;


        }

        private void MainModel_EventResolveCompleteTm01(object sender, TapeDetectEventArgs e)
        {
            curName[0] = e.name;
            curPos[0] = e.position;
            
        }
        private void MainModel_EventResolveCompleteTm02(object sender, TapeDetectEventArgs e)
        {
            curName[1] = e.name;
            curPos[1] = e.position;

        }
        private void MainModel_EventResolveCompleteTm03(object sender, TapeDetectEventArgs e)
        {
            curName[2] = e.name;
            curPos[2] = e.position;

        }
        private void MainModel_EventResolveCompleteTm04(object sender, TapeDetectEventArgs e)
        {
            curName[3] = e.name;
            curPos[3] = e.position;

        }
        private void MainModel_EventResolveCompleteTm05(object sender, TapeDetectEventArgs e)
        {
            curName[4] = e.name;
            curPos[4] = e.position;

        }
        private void MainModel_EventResolveCompleteTm06(object sender, TapeDetectEventArgs e)
        {
            curName[5] = e.name;
            curPos[5] = e.position;

        }
        private void ServerTCP_VariableChangeNotify(object sender, ModbusServerWriteEventArgs e)
        {
            if (!e.getStatus && getSetStat)
            {
                data.Start=serverTCP.getStart();
                data.End= serverTCP.getEnd();
                data.Name= serverTCP.getName();
                data.Coil01 = serverTCP.getCoil()[0];
                data.Coil02 = serverTCP.getCoil()[1];
                getSetStat = false;
                Serialz();
            }
        }
      
        private void ClientTCP_ScanSuccesNotify(object sender, ModbusClientScanCompletArgs e)
        {
            Length=e.len;
            reelOn = e.reelOn;
            Autorun();
            if (e.reelOn)
            {
                foreach (var item in Detects)
                {
                    item.Lenght = e.len;
                    
                    item.ResolveParam();
                }
            }


        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            dBaseStat = serverTCP.getBaseStat();
            serverTCP.setCurPos(curPos);
            serverTCP.setCurName(curName);

        }
        private void Autorun()
        {
            if (reelOn && !serverTCP.getFlDbase() && data.autoRun)
            {
                serverTCP.setFlDbase();
                flDbase = true;
                if (Length < 0.5 && Length > 0)
                {
                    data.runNumb++;
                    serverTCP.setRunNumb(data.runNumb);
                    Serialz();
                }

            }
            if (!reelOn && serverTCP.getFlDbase() && data.autoRun)
            {
                serverTCP.resetFlDbase();
                flDbase = false;
            }
         }
        public void Save()
        {
            Serialz();
            serverTCP.setStart(data.Start);
            serverTCP.setEnd(data.End);
            serverTCP.setName(data.Name);
            serverTCP.setCoil(new int[2]{ data.Coil01, data.Coil02 });
            serverTCP.setRunNumb(data.runNumb);
            
        }
        private void Serialz()
        {
            XmlSerializer formatter = new XmlSerializer(typeof(Data));
            // получаем поток, куда будем записывать сериализованный объект
            if (File.Exists("Data.xml"))
            { 
            File.Delete("Data.xml"); 
            }
            using (FileStream fs = new FileStream("Data.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, data);

                Console.WriteLine("Объект сериализован");
            }
        }
        public void setSetStat()
        {
            getSetStat = true;
            serverTCP.setGetSetStat();
        }
        public void resetSetStat()
        {
            getSetStat = false;
            serverTCP.resetGetSetStat();
        }
        public void setFlBase()
        {
            
            if (Length < 0.5 && Length > 0 && reelOn && !serverTCP.getFlDbase() )
            {
                data.runNumb++;
                serverTCP.setRunNumb(data.runNumb);
                Serialz();
            }
            flDbase = true;
            serverTCP.setFlDbase();
        }
        public void resetFlBase()
        {
            flDbase = false;
            serverTCP.resetFlDbase();
        }
        public void setAutoRun()
        {
           data.autoRun = true;   
        }
        public void resetAutoRun()
        {
            data.autoRun = false;
        }
       public void tapeUp()
       {
        if (TapeNum<10)
            {
                TapeNum++;
            }
       }
        public void tapeDown()
        {
            if (TapeNum >1 )
            {
                TapeNum--;
            }
        }

    }
}
