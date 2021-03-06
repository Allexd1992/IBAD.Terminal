﻿using IBAD.Terminal.Model;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;

namespace IBAD.Terminal.ViewModel 
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        MainModel model;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
        #region Tags
        private string tapeName;
        public string sTapeName
        {
            get { return tapeName; }
            set { if (tapeName != value) { tapeName = value; OnPropertyChanged(); GC.Collect(); } }
        }

       
        private string tapeStartPos;
        public string sTapeStartPos
        {
            get { return tapeStartPos; }
            set { if (tapeStartPos != value) { tapeStartPos = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string tapeEndPos;
        public string sTapeEndPos
        {
            get { return tapeEndPos; }
            set { if (tapeEndPos != value) { tapeEndPos = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string delta;
        public string sDelta
        {
            get { return delta; }
            set { if (delta != value) { delta = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string lehgth;
        public string slehgth
        {
            get { return lehgth; }
            set { if (lehgth != value) { lehgth = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string coil01;
        public string sCoil01
        {
            get { return coil01; }
            set { if (coil01 != value) { coil01 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string coil02;
        public string sCoil02
        {
            get { return coil02; }
            set { if (coil02 != value) { coil02 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string tapeNum;
        public string sTapeNum
        {
            get { return tapeNum; }
            set { if (tapeNum != value) { tapeNum = value; OnPropertyChanged(); GC.Collect(); } }
        }

     

        private string tapeNameMon01;
        public string sTapeNameMon01
        {
            get { return tapeNameMon01; }
            set { if (tapeNameMon01 != value) { tapeNameMon01 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon02;
        public string sTapeNameMon02
        {
            get { return tapeNameMon02; }
            set { if (tapeNameMon02 != value) { tapeNameMon02 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon03;
        public string sTapeNameMon03
        {
            get { return tapeNameMon03; }
            set { if (tapeNameMon03 != value) { tapeNameMon03 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon04;
        public string sTapeNameMon04
        {
            get { return tapeNameMon04; }
            set { if (tapeNameMon04 != value) { tapeNameMon04 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon05;
        public string sTapeNameMon05
        {
            get { return tapeNameMon05; }
            set { if (tapeNameMon05 != value) { tapeNameMon05 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon06;
        public string sTapeNameMon06
        {
            get { return tapeNameMon06; }
            set { if (tapeNameMon06!= value) { tapeNameMon06 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string tapeNameMon07;
        public string sTapeNameMon07
        {
            get { return tapeNameMon07; }
            set { if (tapeNameMon07 != value) { tapeNameMon07 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon08;
        public string sTapeNameMon08
        {
            get { return tapeNameMon08; }
            set { if (tapeNameMon08 != value) { tapeNameMon08 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeNameMon09;
        public string sTapeNameMon09
        {
            get { return tapeNameMon09; }
            set { if (tapeNameMon09 != value) { tapeNameMon09 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeCoor01;
        public string sTapeCoor01
        {
            get { return tapeCoor01; }
            set { if (tapeCoor01 != value) { tapeCoor01 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeCoor02;
        public string sTapeCoor02
        {
            get { return tapeCoor02; }
            set { if (tapeCoor02 != value) { tapeCoor02 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeCoor03;
        public string sTapeCoor03
        {
            get { return tapeCoor03; }
            set { if (tapeCoor03 != value) { tapeCoor03 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeCoor04;
        public string sTapeCoor04
        {
            get { return tapeCoor04; }
            set { if (tapeCoor04 != value) { tapeCoor04 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeCoor05;
        public string sTapeCoor05
        {
            get { return tapeCoor05; }
            set { if (tapeCoor05!= value) { tapeCoor05 = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string tapeCoor06;
        public string sTapeCoor06
        {
            get { return tapeCoor06; }
            set { if (tapeCoor06 != value) { tapeCoor06 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string tapeCoor07;
        public string sTapeCoor07
        {
            get { return tapeCoor07; }
            set { if (tapeCoor07 != value) { tapeCoor07 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string tapeCoor08;
        public string sTapeCoor08
        {
            get { return tapeCoor08; }
            set { if (tapeCoor08 != value) { tapeCoor08 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string tapeCoor09;
        public string sTapeCoor09
        {
            get { return tapeCoor09; }
            set { if (tapeCoor09 != value) { tapeCoor09 = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string wrDbOn;
        public string sWrDbOn
        {
            get { return wrDbOn; }
            set { if (wrDbOn != value) { wrDbOn = value; OnPropertyChanged(); GC.Collect(); } }

        }
        private string wrDbOff;
        public string sWrDbOff
        {
            get { return wrDbOff; }
            set { if (wrDbOff != value) { wrDbOff = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string autoWrOn;
        public string sAutoWrOn
        {
            get { return autoWrOn; }
            set { if (autoWrOn != value) { autoWrOn = value; OnPropertyChanged(); GC.Collect(); } }

        }
        private string autoWrOff;
        public string sAutoWrOff
        {
            get { return autoWrOff; }
            set { if (autoWrOff != value) { autoWrOff = value; OnPropertyChanged(); GC.Collect(); } }
        }

        private string wrDbRun;
        public string sWrDbRun
        {
            get { return wrDbRun; }
            set { if (wrDbRun != value) { wrDbRun = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string GetSetStat;
        public string sGetSetStat
        {
            get { return GetSetStat; }
            set { if (GetSetStat != value) { GetSetStat = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string MgOStat;
        public string sMgOStat
        {
            get { return MgOStat; }
            set { if (MgOStat != value) { MgOStat = value; OnPropertyChanged(); GC.Collect(); } }
        }
        private string LMOStat;
        public string sLMOStat
        {
            get { return LMOStat; }
            set { if (LMOStat != value) { LMOStat = value; OnPropertyChanged(); GC.Collect(); } }
        }
        #endregion
        #region Buttons
        public ICommand IClearTape
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    model.clearTape();
                    sTapeName = "";
                    sTapeStartPos = "0";
                    sTapeEndPos = "0";


                });
            }
        }
        public ICommand InewProcess
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    model.newProcess();
                });
            }
        }
        public ICommand WriteTape
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    WriteSettings();


                });
            }
        }
        public ICommand SaveTape
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.Save();
                });
            }
        }
        public ICommand TapeUp
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.tapeUp();
                    RefreshSet();

                });
            }
        }
        public ICommand TapeDown
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.tapeDown();
                    RefreshSet();
                });
            }
        }
      
        public ICommand RunWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.setFlBase();
                });
            }
        }
        public ICommand StopWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.resetFlBase();
                });
            }
        }


        public ICommand OnAutoWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.setAutoRun();
                });
            }
        }
        public ICommand OffAutoWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.resetAutoRun();
                });
            }
        }
        public ICommand IGetSet
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.setSetStat();
                });
            }
        }
        public ICommand IResetSet
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.resetSetStat();
                });
            }
        }
        public ICommand ISetLMO
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    sMgOStat = "False";
                    sLMOStat = "True";
                    model.MgOReset();
                });
            }
        }
        public ICommand ISetMgO
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    sMgOStat = "True";
                    sLMOStat = "False";
                    model.MgOSet();
                });
            }
        }
        #endregion
        private void RefreshSet()
        {
            sTapeNum = model.TapeNum.ToString();
           
            sTapeName = model.data.Name[model.TapeNum - 1];
          
            
            slehgth = String.Format("{0:0.0}", Math.Abs(model.data.End[model.TapeNum-1] - model.data.Start[model.TapeNum - 1]));
            sTapeStartPos = String.Format("{0:0.0}", model.data.Start[model.TapeNum - 1]);
            sTapeEndPos = String.Format("{0:0.0}", model.data.End[model.TapeNum - 1]);
            sDelta = String.Format("{0:0}", model.data.deltaTape);
            sCoil01 = String.Format("{0}", model.data.Coil01);
            sCoil02 = String.Format("{0}", model.data.Coil02);
        }
        private void WriteSettings()
        {
            model.data.Name[model.TapeNum-1] = sTapeName;
            model.data.Start[model.TapeNum - 1] = double.Parse(sTapeStartPos.Replace('.',','));
            model.data.End[model.TapeNum - 1] = double.Parse(sTapeEndPos.Replace('.', ','));
            model.data.deltaTape = double.Parse(sDelta.Replace('.', ','));
            model.data.Coil01 = int.Parse(sCoil01);
            model.data.Coil02 = int.Parse(sCoil02);
        }
        private void RefreshCV()
        {

            sTapeCoor01 = String.Format("{0:0.00}", model.curPos[0]);
            sTapeCoor02 = String.Format("{0:0.00}", model.curPos[1]);
            sTapeCoor03 = String.Format("{0:0.00}", model.curPos[2]);
            sTapeCoor04 = String.Format("{0:0.00}", model.curPos[3]);
            sTapeCoor05 = String.Format("{0:0.00}", model.curPos[4]);
            sTapeCoor06 = String.Format("{0:0.00}", model.curPos[5]);
            sTapeCoor07= String.Format("{0:0.00}", model.curPos[6]);
            sTapeCoor08= String.Format("{0:0.00}", model.curPos[7]);
            sTapeCoor09= String.Format("{0:0.00}", model.curPos[8]);
            sTapeNameMon01 = model.curName[0];
            sTapeNameMon02 = model.curName[1];
            sTapeNameMon03 = model.curName[2];
            sTapeNameMon04 = model.curName[3];
            sTapeNameMon05 = model.curName[4];
            sTapeNameMon06 = model.curName[5];
            sTapeNameMon07 = model.curName[6];
            sTapeNameMon08 = model.curName[7];
            sTapeNameMon09 = model.curName[8];
            sWrDbOn =  model.flDbase? "True" : "False";
            sWrDbOff = !model.flDbase ? "True" : "False";
            sAutoWrOn =  model.data.autoRun? "True" : "False";
            sAutoWrOff = !model.data.autoRun ? "True" : "False";
            sWrDbRun =  model.dBaseStat? "True" : "False";
            sGetSetStat = model.getSetStat ? "True" : "False";
            if (model.data.MgOstat == 1)
            {
                sMgOStat = "True";
                sLMOStat = "False";
            }
            else
            {
                sMgOStat = "False";
                sLMOStat = "True";
            }
        }
        public MainViewModel()
        {
            model = new MainModel();
          
            RefreshSet();
            var timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            RefreshCV();
            Console.WriteLine("Mgo Stat " + sMgOStat);
        }
    }

}
