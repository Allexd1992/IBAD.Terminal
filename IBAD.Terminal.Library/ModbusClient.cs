using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;
using System.Timers;

namespace IBAD.Terminal.Library
{
    public class ModbusClientScanCompletArgs
    {
        // Сообщение
        public float len { get; }
        public bool reelOn { get; }
        //public int[] data { get; }

        public ModbusClientScanCompletArgs(float len, bool reelOn /*int[] data*/)
        {
            this.reelOn = reelOn;
            this.len = len;
            //this.data = data;
        }
    }
         public class ModbusClientTCP
    {
        public delegate void ModbusClientScanCompletHendel(object sender, ModbusClientScanCompletArgs e);
        public event ModbusClientScanCompletHendel ScanSuccesNotify;
        public float lenght { get; private set; }
        public bool reelRun { get; private set; }
        Timer timerMbClient;
        ModbusClient client;
        ModbusClient client2;
        private float lengthOld;

        public ModbusClientTCP()
        {
            //client = new ModbusClient();
            //client.UnitIdentifier = 0;
            client = new ModbusClient();
            client.ConnectionTimeout = 1000;
            client.UnitIdentifier = 0;
            client.Connect("10.177.3.20", 502);
            timerMbClient = new Timer(100);
            client2 = new ModbusClient();
            client2.UnitIdentifier = 1;
            client2.Connect("10.177.3.24", 502);
            timerMbClient.Elapsed += TimerMbClient_Elapsed;
            timerMbClient.AutoReset = true;
            timerMbClient.Enabled = true;
            
        }
        async void Scan2()
        {
            await Task.Run(() =>
            {
                //try
                //{
                //    client2 = new ModbusClient();
                //    client2.UnitIdentifier = 0;
                //    client2.Connect("10.177.3.20", 502);
                //    int[] data =client2.ReadHoldingRegisters(0, 125);
                //    foreach (var item in data)
                //    {
                //        Console.WriteLine(item);
                //    }
                  
                //    client2.Disconnect();
                //}
                //catch
                //{ }
            });
            
            
        }
        void Scan()
        {
            try
            {

            

            //    int[] data = client.ReadHoldingRegisters(0, 100);
            //Console.WriteLine(data[23]);
            //int[] data1 = client.ReadHoldingRegisters(100, 100);
            //Console.WriteLine(" Speed {0:0.00 }", ModbusClient.ConvertRegistersToFloat(new int[] { data1[80], data1[79] }));
            //Console.WriteLine(data1[23]);
            lenght = MbGetFloat(2004);
            Console.WriteLine("{0:0.00 }", lenght);
            reelRun = MbGetBool(184);
            Console.WriteLine(reelRun);
            //int[] data = client.ReadHoldingRegisters(0, 10);
            //Console.WriteLine(data[0]);
            //client.Disconnect();
            ScanSuccesNotify?.Invoke(this, new ModbusClientScanCompletArgs(lenght, reelRun));
            if (lenght == lengthOld && reelRun)
            {
                Reconnect();
            }
            else
            {
                lengthOld = lenght;
            }

            }
            catch
            {

                Reconnect();
            }


        }
        private void TimerMbClient_Elapsed(object sender, ElapsedEventArgs e)
        {
            Scan();
           Scan2();
            Console.WriteLine("Scan Run");
        }

        void Reconnect()
        {
           if ( !client.Connected)
            {
                client.Connect("10.177.3.20", 502);
            }
            else
            {
                client.Disconnect();  
            }
          
        }
          private void Client_ConnectedChanged(object sender)
          {
            Reconnect();
         }

        float MbGetFloat(int adr)
        {
            try
            {
                int[] buf = client2.ReadHoldingRegisters(adr, 2);
                return ModbusClient.ConvertRegistersToFloat(buf, ModbusClient.RegisterOrder.LowHigh);
            }
            catch
            {
                Console.WriteLine("Modbus read float error");

                return 999;
            }
        }
       
        bool MbGetBool(int adr)
        {
            try
            {
                int[] buf= client.ReadHoldingRegisters(adr - 1,1);
                return Convert.ToBoolean(buf[0]);
            }
            catch
            {
                Console.WriteLine("Modbus read float error");

                return false;
            }
        }
    }
}
