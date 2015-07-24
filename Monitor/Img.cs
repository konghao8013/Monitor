using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Monitor
{
    public class Img
    {
        /// <summary>
        /// 屏幕裁图
        /// </summary>
        /// <returns></returns>
        public static Image Cut()
        {
            Rectangle rc = Screen.PrimaryScreen.Bounds;
            int iWidth = rc.Width;
            int iHeight = rc.Height;
            Image myImage = new Bitmap(iWidth, iHeight);
            Graphics.FromImage(myImage).CopyFromScreen(new System.Drawing.Point(0, 0), new System.Drawing.Point(0, 0), new System.Drawing.Size(iWidth, iHeight));
            return myImage;
        }  
    }
}
