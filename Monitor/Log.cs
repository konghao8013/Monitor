using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace Monitor
{
    public static class Log
    {
        public static void WriteImg(Image img, string path = null)
        {

            try
            {
                //   var file = new FileStream(path,FileMode.OpenOrCreate,FileAccess.ReadWrite,FileShare.ReadWrite);
                var thread = new Thread((o) =>
                {
                    if (path == null)
                    {
                        path = AppDomain.CurrentDomain.BaseDirectory + "LogImages\\" + DateTime.Now.ToString("yyyyMMddhhmmss") + ".jpg";
                    }
                    path = CheckPath(path, false);
                    img.Save(path);
                    WriteLine("文件保存至 " + path);
                });
                thread.Start();

            }
            catch (Exception e)
            {
                WriteLine(e.Message);
            }
        }


        private static string ErrorWrite(string msg, string path, bool IsErrorWrite, Exception e)
        {
            if (IsErrorWrite == false)
            {
                path = path + Guid.NewGuid().ToString("N") + ".txt";
                WriteLine(msg + "\r\n" + e.Message, path, true);
            }
            return path;
        }
        public static void WriteLine(string msg, string path = null, bool isErrorWrite = false)
        {
            path = CheckPath(path);
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    LogMsg log = new LogMsg();
                    log.CreateTime = DateTime.Now;
                    log.Msg = msg;
                    sw.WriteLine(log.Json());

                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception e)
            {
                if (!isErrorWrite)
                {
                    WriteLine(msg, path+""+Guid.NewGuid().ToString("N"), true);
                }
            }
        }

        private static string CheckPath(string path, bool IsCreateFile = true)
        {
            if (path == null)
            {
                path = AppDomain.CurrentDomain.BaseDirectory + "log\\log" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            }
            if (!File.Exists(path))
            {

                var splits = path.Split('\\').ToList();
                if (splits.Last().IndexOf(".") > -1)
                {
                    splits.Remove(splits.Last());
                }
                string directory = splits.Join("\\");
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }
                if (IsCreateFile)
                {
                    FileStream file = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    file.Close();
                    file.Dispose();
                 
                    
                }

            }
            return path;
        }
    }
    [Serializable]
    public class LogMsg
    {
        public string CreateTimeString
        {
            get
            {
                return CreateTime.ToString("yyyy/MM/dd:hh:ss");
            }
        }

        public string Msg { set; get; }
        public DateTime CreateTime { set; get; }


    }

}
