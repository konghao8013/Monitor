namespace Monitor.Server
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.MonitorServerInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.MonitorServer = new System.ServiceProcess.ServiceInstaller();
            // 
            // MonitorServerInstaller
            // 
            this.MonitorServerInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.MonitorServerInstaller.Password = null;
            this.MonitorServerInstaller.Username = null;
            // 
            // MonitorServer
            // 
            this.MonitorServer.DisplayName = "Hitek_Tool_Monitor";
            this.MonitorServer.ServiceName = "MonitorServer";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.MonitorServerInstaller,
            this.MonitorServer});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller MonitorServerInstaller;
        private System.ServiceProcess.ServiceInstaller MonitorServer;
    }
}