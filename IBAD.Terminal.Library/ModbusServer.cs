﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyModbus;

namespace IBAD.Terminal.Library
{
    public class ModbusServerWriteEventArgs 
    {
        // Сообщение
        public int register { get; }
        public int numberOfRegisters { get; }
        public bool getStatus{ get; }

        public ModbusServerWriteEventArgs(int register, int numberOfRegisters, bool getSet)
        {
            this.register = register;
            this.numberOfRegisters = numberOfRegisters;
            this.getStatus = getSet;
        }
    }
    public class ModbusServerTCP
    {
        public delegate void ModbusServerWriteEventHendel(object sender, ModbusServerWriteEventArgs e);
         public event ModbusServerWriteEventHendel VariableChangeNotify;

         private   ModbusServer modbus;
         public ModbusServerTCP()
         {
            modbus = new ModbusServer();
            modbus.Port = 505;
            modbus.Listen();
            modbus.HoldingRegistersChanged += Modbus_HoldingRegistersChanged;
         }

        public string[] getName()
        {
            string[] Out = new string[10];
            for (int i = 1; i < 11; i++)
            {
                Out[i]=ReadString(100*i, 100);
            }
            return Out ;
        }
        public double[] getStart()
        {
            double[] Out = new double[10];
            for (int i = 0; i < 10; i++)
            {
                Out[i] = ReadDouble(10+4 * i);
            }
            return Out;
        }
        public double[] getEnd()
        {
            double[] Out = new double[10];
            for (int i = 0; i < 10; i++)
            {
                Out[i] = ReadDouble(50+ 2 * i);
            }
            return Out;
        }
        public int[] getCoil()
        {
            int[] Out = new int[2];
            for (int i = 0; i < 2; i++)
            {
                Out[i]=ReadInt(94 + 2*i);
            }
            return Out;
            

        }
        private void Modbus_HoldingRegistersChanged(int register, int numberOfRegisters)
        {
            
            VariableChangeNotify?.Invoke(this, new ModbusServerWriteEventArgs(register, numberOfRegisters , ReadBool(1)));
        }

        void WriteDouble(double input, int adr)
        {
                byte[] data = BitConverter.GetBytes(input);
                for (int i = 0; i < 4; i++)
                {
                    modbus.holdingRegisters.localArray[adr + i] = BitConverter.ToInt16(data, i * 2);
                }
        }
        
        void WriteString( string input, int ad, int chNum)
            {
            for (int i = 0; i < chNum; i++)
            {
                modbus.holdingRegisters.localArray[i + ad] = 0;
            }

                char[] dataMsg =input.ToCharArray();
                byte[] dataAsciiMs = new byte[dataMsg.Length];
             
                for (int i = 0; i < dataMsg.Length; i++)
                {
                    dataAsciiMs[i] = (byte)dataMsg[i];
                }

                for (int i = 0; i < dataAsciiMs.Length/2; i++)
                {
                    modbus.holdingRegisters.localArray[i+ad] = BitConverter.ToInt16(dataAsciiMs, i*2);
                  
                }
                
           
            }
        void WriteString2(string input, int ad, int chNum)
        {
            
                for (int i = 0; i < chNum; i++)
                {
                    modbus.holdingRegisters.localArray[i + ad] = 0;
                }
                int[] buff = ModbusClient.ConvertStringToRegisters(input);
            for (int i = 0; i < buff.Length; i++)
            {
                modbus.holdingRegisters.localArray[i + ad] = (short)buff[i];
            }
        }
        void WriteDint(int input, int adr)
        {
            byte[] data = BitConverter.GetBytes(input);
            for (int i = 0; i < 4; i++)
            {
                modbus.holdingRegisters.localArray[adr + i] = BitConverter.ToInt16(data, i * 2);
            }
        }
        int ReadInt(int adr)
        {
            short[] buff = new short[2];
            byte[] buff2 = new byte[4];
            byte[] buff3 = new byte[2];
            for (int i = 0; i < 2; i++)
            {
                buff[i] = modbus.holdingRegisters.localArray[adr + i];
            }

            buff3 = BitConverter.GetBytes((Int16)buff[0]);
            buff2[0] = buff3[0];
            buff2[1] = buff3[1];
            buff3 = BitConverter.GetBytes((Int16)buff[1]);
            buff2[2] = buff3[0];
            buff2[3] = buff3[1];
         
          
            int var = BitConverter.ToInt32(buff2, 0);

            return var;
        }
        string ReadString(int adr, int chNum)
        {      
            short[] buff = new short[chNum/2];
            char[] buffful = new char[chNum];
            byte[] buffs = new byte[2];
            for (int i = 0; i < chNum / 2; i++)
            {
                buff[i] = modbus.holdingRegisters.localArray[adr + i];
            }
            for (int i = 0; i < chNum / 2; i++)
            {

                buffs = BitConverter.GetBytes((int)buff[i]);
                buffful[i * 2] = (char)buffs[1];
                buffful[i * 2 + 1] = (char)buffs[0];

            }
            string str = new string(buffful);
            char[] chr = str.ToCharArray();
            string str1 = "";
            foreach (var st in chr)
            {
                if ((int)st > 0)
                {
                    str1 += st;
                }
                if ((int)st == 0)
                    break;
            }
            return str1;
        }
        double ReadDouble( int adr)
        {
            short[] buff = new short[4];
            byte[] buff2 = new byte[8];
            byte[] buff3 = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                buff[i] = modbus.holdingRegisters.localArray[adr + i];
            }
            
            buff3 = BitConverter.GetBytes((Int16)buff[0]);
            buff2[0] = buff3[0];
            buff2[1] = buff3[1];
            buff3 = BitConverter.GetBytes((Int16)buff[1]);
            buff2[2] = buff3[0];
            buff2[3] = buff3[1];
            buff3 = BitConverter.GetBytes((Int16)buff[2]);
            buff2[4] = buff3[0];
            buff2[5] = buff3[1];
            buff3 = BitConverter.GetBytes((Int16)buff[3]);
            buff2[6] = buff3[0];
            buff2[7] = buff3[1];
            double var = BitConverter.ToDouble(buff2, 0);

            return var;
        }
        
        bool ReadBool(int adr)
        {
            return Convert.ToBoolean(modbus.holdingRegisters.localArray[adr]);
        }
        void WriteBool(int adr, bool value)
        {
            modbus.holdingRegisters.localArray[adr]=Convert.ToInt16(value);
        }
    }
}