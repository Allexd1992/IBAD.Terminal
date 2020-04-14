using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace IBAD.Terminal.ViewModel 
{
    class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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
        #endregion
        #region Buttons
        public ICommand WriteTape
        {
            get
            {
                return new DelegateCommand(() =>
                {

                    model.TapeName = sTapeName;
                    model.TapeStartPos = (float)Convert.ToDouble(sTapeStartPos);
                    model.TapeEndPos = (float)Convert.ToDouble(sTapeEndPos);
                    model.coil01 = Convert.ToInt32(sCoil01);
                    model.coil02 = Convert.ToInt32(sCoil02);
                    model.delta = (float)Convert.ToDouble(sDelta);
                    model.TapeSave();


                });
            }
        }
        public ICommand SaveTape
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.TargetName = sTargetName;
                    model.TargetSave();
                });
            }
        }
        public ICommand TapeUp
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.TapeUp();
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
                    model.TapeDown();
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
                    model.BaseWrRun();
                });
            }
        }
        public ICommand StopWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.BaseWrStop();
                });
            }
        }


        public ICommand OnAutoWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.AutoSettingRun();
                });
            }
        }
        public ICommand OffAutoWB
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.AutoSettingStop();
                });
            }
        }
        public ICommand IGetSet
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.GetSet();
                });
            }
        }
        public ICommand IResetSet
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    model.ResetSet();
                });
            }
        }
        #endregion
        private void RefreshSet()
        {
            sTapeNum = model.TapeNum.ToString();
           
            sTapeName = model.TapeName;
            sTargetName = model.TargetName;


            slehgth = String.Format("{0:0.0}", model.tapeLen);
            sTapeStartPos = String.Format("{0:0.0}", model.TapeStartPos);
            sTapeEndPos = String.Format("{0:0.0}", model.TapeEndPos);
            sDelta = String.Format("{0:0}", model.delta);
            sCoil01 = String.Format("{0:0}", model.coil01);
            sCoil02 = String.Format("{0:0}", model.coil02);
        }
        private void RefreshCV()
        {
            sTapeNameMon = model.TapeNameMon;
            sTargetNameMon = model.TargetNameMon;
            sTargetDeg = String.Format("{0:0.00}", model.targetDeg);
            sTapeCoor = String.Format("{0:0.00}", model.tapeCoord);
            sWrDbOn = model.WbOn ? "True" : "False";
            sWrDbOff = !model.WbOn ? "True" : "False";
            sAutoWrOn = model.SetWbOn ? "True" : "False";
            sAutoWrOff = !model.SetWbOn ? "True" : "False";
            sWrDbRun = model.WbRun ? "True" : "False";
            sGetSetStat = model.getSetStat ? "True" : "False";
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

        }
    }

}
