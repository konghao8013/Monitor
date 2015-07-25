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
