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
        double LengthOld { get; set; }
        bool reelOn { get; set; }
        public bool getSetStat { get; set; }
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
            TapeNum = 1;
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
            new LentaDetect(2.0),
            new LentaDetect(13.3),
            new LentaDetect(34.4),
            new LentaDetect(45.6),
            new LentaDetect(58.7),
            new LentaDetect(63.5),
            new LentaDetect(74.7),
            new LentaDetect(97.2),
            new LentaDetect(109.6),
             };
            curPos = new double[9];
            curName = new string[9];
            WriteSettingsToBlock();


            ClientTCP = new ModbusClientTCP();
            ClientTCP.ScanSuccesNotify += ClientTCP_ScanSuccesNotify;
            serverTCP = new ModbusServerTCP();
            serverTCP.VariableChangeNotify += ServerTCP_VariableChangeNotify;
            WriteSettingsToMbServer();
            timer = new Timer(100);
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
            Detects[0].EventResolveComplete += MainModel_EventResolveCompleteTm01;
            Detects[1].EventResolveComplete += MainModel_EventResolveCompleteTm02;
            Detects[2].EventResolveComplete += MainModel_EventResolveCompleteTm03;
            Detects[3].EventResolveComplete += MainModel_EventResolveCompleteTm04;
            Detects[4].EventResolveComplete += MainModel_EventResolveCompleteTm05;
            Detects[5].EventResolveComplete += MainModel_EventResolveCompleteTm06;
            Detects[6].EventResolveComplete += MainModel_EventResolveCompleteTm07;
            Detects[7].EventResolveComplete += MainModel_EventResolveCompleteTm08;
            Detects[8].EventResolveComplete += MainModel_EventResolveCompleteTm09;


        }
        void WriteSettingsToMbServer()
        {
            serverTCP.setStart(data.Start);
            serverTCP.setEnd(data.End);
            serverTCP.setName(data.Name);
            serverTCP.setCoil(new int[2] { data.Coil01, data.Coil02 });
            serverTCP.setRunNumb(data.runNumb);
            if (data.MgOstat == 1)
            {
                serverTCP.MgoSet();
            }
            else
            {
                serverTCP.MgoReset();
            }
        }
        void WriteSettingsToBlock()
        {
            foreach (var item in Detects)
            {
                item.TapeName = data.Name;
                item.TapeStart = data.Start;
                item.TapeEnd = data.End;
                item.tapeDelta = data.deltaTape;

            }
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
        private void MainModel_EventResolveCompleteTm07(object sender, TapeDetectEventArgs e)
        {
            curName[6] = e.name;
            curPos[6] = e.position;

        }
        private void MainModel_EventResolveCompleteTm08(object sender, TapeDetectEventArgs e)
        {
            curName[7] = e.name;
            curPos[7] = e.position;

        }
        private void MainModel_EventResolveCompleteTm09(object sender, TapeDetectEventArgs e)
        {
            curName[8] = e.name;
            curPos[8] = e.position;

        }

        private void ServerTCP_VariableChangeNotify(object sender, ModbusServerWriteEventArgs e)
        {
            if (!e.getStatus && getSetStat)
            {

                data.Start = serverTCP.getStart();
                data.End = serverTCP.getEnd();
                data.Name = serverTCP.getName();
                data.Coil01 = serverTCP.getCoil()[0];
                data.Coil02 = serverTCP.getCoil()[1];
                if (!reelOn)
                {
                    WriteSettingsToBlock();
                }
                getSetStat = false;
                Serialz();
            }
        }

        private void ClientTCP_ScanSuccesNotify(object sender, ModbusClientScanCompletArgs e)
        {

            Console.WriteLine("{0:0.00}", e.len);
            Length = e.len;
            reelOn = e.reelOn;
            if (Length == 0 && LengthOld > 0)
            {
                Length = LengthOld;
            }

            Autorun();

            foreach (var item in Detects)
            {
                item.Lenght = e.len;

                item.ResolveParam();
            }
            LengthOld = Length;

        }
        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {

            //for (int i = 0; i < curName.Length; i++)
            //{
            //    Console.WriteLine(curName[i]);
            //}

            if (curName[0] != null && curName[1] != null && curName[2] != null && curName[3] != null && curName[4] != null && curName[5] != null && curName[6] != null && curName[7] != null && curName[8] != null)

            {
                dBaseStat = serverTCP.getBaseStat();
                serverTCP.setCurPos(curPos);
                serverTCP.setCurName(curName);
            }

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
                LengthOld = 0;
            }
        }
        public void Save()
        {
            Serialz();
            WriteSettingsToMbServer();
            WriteSettingsToBlock();

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

            if (Length < 0.5 && Length > 0 && reelOn && !serverTCP.getFlDbase())
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
            if (TapeNum < 10)
            {
                TapeNum++;
            }
        }
        public void tapeDown()
        {
            if (TapeNum > 1)
            {
                TapeNum--;
            }
        }
        public void MgOSet()
        {
            data.MgOstat = 1;
            serverTCP.MgoSet();
        }
        public void MgOReset()
        {
            data.MgOstat = 0;
            serverTCP.MgoReset();
        }
        public void clearTape()
        {
            for (int i = 0; i < 10; i++)
            {
                data.Name[i] = "";
                data.Start[i] = 0;
                data.End[i] = 0;
            }
            Serialz();
            WriteSettingsToMbServer();
            WriteSettingsToBlock();
        }

    }
}
