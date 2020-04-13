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

        public ModbusClientScanCompletArgs(float len, bool reelOn)
        {
            this.reelOn = reelOn;
            this.len = len;
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
         public ModbusClientTCP()
        {
            client = new ModbusClient("10.177.3.20", 502);
            client.UnitIdentifier = 0;
            client.Connect();
            client.ConnectedChanged += Client_ConnectedChanged;
            timerMbClient = new Timer(100);
            timerMbClient.Elapsed += TimerMbClient_Elapsed;
            timerMbClient.AutoReset = true;
            timerMbClient.Enabled = true;
            
        }
        void Scan()
        {
           lenght=MbGetFloat(179);
           reelRun= MbGetBool(184);
           ScanSuccesNotify?.Invoke(this, new ModbusClientScanCompletArgs(lenght, reelRun));
        }
        private void TimerMbClient_Elapsed(object sender, ElapsedEventArgs e)
        {
            Scan();
        }

        void Reconnect()
        {
           if ( !client.Connected)
            {
                client.Connect("10.177.3.20", 502);
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
                int[] buf = client.ReadHoldingRegisters(adr-1, 2);
                return ModbusClient.ConvertRegistersToFloat(buf);
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
