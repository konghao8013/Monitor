using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;

namespace Monitor.Server
{
    public partial class MonitorStart : ServiceBase
    {
        public MonitorStart()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            var thread = new Thread((o) =>
            {
                var operate = new Operate();
                operate.Init();
            });
            thread.Start();
            Log.Write("MonitorStart:" + DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss"));
        }

        protected override void OnStop()
        {
            Log.Write("MonitorStop:" + DateTime.Now.ToString("yyyy年MM月dd日 hh:mm:ss"));
        }
    }
}
