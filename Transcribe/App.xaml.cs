using Monitor;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Transcribe
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        
        Operate _Tool;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            bool isRedegit = false;
            if (e.Args != null && e.Args.Length > 0)
            {
                isRedegit = e.Args.Join("").IndexOf("-regedit") >= 0;
            }
            var name=AppDomain.CurrentDomain.FriendlyName;
            if (!Operate.IsAdministrator && !isRedegit && AppDomain.CurrentDomain.FriendlyName.IndexOf("vshost.exe")<0)
            {
                 Operate.RestartApplicationAsAdministrator();
            }
        }
    }
}