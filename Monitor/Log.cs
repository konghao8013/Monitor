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
                        path = AppDomain.CurrentDomain.BaseDirectory + "LogImages\\" + DateTime.Now.ToString("yyyyMMddhhmmss")+".png";
                    }
                    path = CheckPath(path,false);
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
        //连续不换行写入
        public static void Write(string msg, string path = null, bool isErrorWrite = false)
        {
            path = CheckPath(path);
            try
            {
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.Write(msg);
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception e)
            {
                ErrorWrite(msg, path, isErrorWrite, e);
            }
        }

        private static string ErrorWrite(string msg, string path, bool IsErrorWrite, Exception e)
        {
            if (IsErrorWrite == false)
            {
                path = path + Guid.NewGuid().ToString("N") + ".txt";
                Write(msg + "\r\n" + e.Message, path, true);
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
                    sw.WriteLine();
                    sw.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"));
                    sw.WriteLine(msg);
                    sw.WriteLine();
                    sw.WriteLine();
                    sw.Close();
                    sw.Dispose();
                }
            }
            catch (Exception e)
            {
                ErrorWrite(msg, path, isErrorWrite, e);
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
                    File.Create(path);
                }

            }
            return path;
        }
    }
}
