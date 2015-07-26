using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Monitor;
using System.Configuration;
using System.Threading;


namespace Transcribe
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        
        Operate _Tool;
        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
            
            _Tool = new Operate();
            _Tool.Init();

            try
            {
                new Thread(() =>
                {
                    
                   
                    var time = new System.Timers.Timer(60000);
                    time.AutoReset = true;
                    time.Elapsed += (sender, ee) => {
                        SetAllValids();
                    };
                    time.Start();
                  
                    

                }).Start();
                new Thread(() => {
                    Log.ImagesClear(DateTime.Now.AddHours(-2.0));
                }).Start();

            }
            catch (Exception e)
            {
                Log.WriteLine("Exception" + e.Message);
            }


        }

        private void SetAllValids()
        {
            try
            {
                var imageSaves = ConfigurationManager.AppSettings["ImageSave"] ?? "";
                var valids = imageSaves.Split(';');
                _Tool.ValidsSave = valids;
                Log.WriteLine("获得配置文件数据：" + valids.Join(","));
                SHDocVw.ShellWindows sws = new SHDocVw.ShellWindows();
                var list = new List<string>();
                //sws为当前打开的所有IE窗口
                foreach (SHDocVw.InternetExplorer iw in sws)
                {
                    //获取窗口的URL
                    list.Add(iw.LocationURL);
                }
                _Tool.AllValids = list.ToArray();
                Log.WriteLine("获得已打开应用程序:" + list.Join(","));
            }
            catch (Exception e) {
                Log.WriteLine("SetAllValids Exception:"+e.Message);
            }
        }

      

        void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            this.WindowStartupLocation = WindowStartupLocation.Manual;
            this.WindowStyle = WindowStyle.None;
            Left = SystemParameters.FullPrimaryScreenWidth - 200;
            Top = SystemParameters.FullPrimaryScreenHeight - 25;
            ResizeMode = ResizeMode.NoResize;
            Collapsed(true);

            this.Hide();
            //this.Opacity = 0;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            Collapsed(true);

        }
        /// <summary>
        /// 隐藏操作按钮
        /// </summary>
        /// <param name="isStart"></param>
        private void Collapsed(bool isStart)
        {
            BtnStart.Visibility = isStart ? Visibility.Collapsed : Visibility.Visible; ;
            BtnStop.Visibility = isStart ? Visibility.Visible : Visibility.Collapsed;
            _Tool.IsSaveImg = isStart;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            Collapsed(false);
        }

        private void BtnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
