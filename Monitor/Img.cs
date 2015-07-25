using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Imaging;

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

        /// <summary>
        /// 生成缩略图重载方法1，返回缩略图的Image对象
        /// </summary>
        /// <param name="Width">缩略图的宽度</param>
        /// <param name="Height">缩略图的高度</param>
        /// <returns>缩略图的Image对象</returns>
        public static Image GetReducedImage(Image img, int Width, int Height)
        {
            try
            {
                Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                Graphics graphics = Graphics.FromImage(bitmap);
                graphics.Clear(Color.Transparent);
                graphics.DrawImage(img, new Rectangle(0, 0, Width, Height));
                return bitmap;
            }
            catch (Exception e)
            {
                throw;
            }
        }
        /// <summary>
        /// 去想缩放
        /// </summary>
        /// <param name="img"></param>
        /// <param name="ratio">缩放比例</param>
        /// <returns></returns>
        public static Image GetReducedImage(Image img, double ratio)
        {
            return GetReducedImage(img, ((int)(img.Width * ratio)), (int)(img.Height * ratio));
        }
        /**/
        /// <summary>
        /// 变成黑白图
        /// </summary>
        /// <param name="bmp">原始图</param>
        /// <param name="mode">模式。0:加权平均  1:算数平均</param>
        /// <returns></returns>
        public static Image ToGray(Image image, int mode)
        {
            Bitmap bmp = new Bitmap(image);
            if (bmp == null)
            {
                return null;
            }

            int w = bmp.Width;
            int h = bmp.Height;
            try
            {
                byte newColor = 0;
                BitmapData srcData = bmp.LockBits(new Rectangle(0, 0, w, h), ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);
                unsafe
                {
                    byte* p = (byte*)srcData.Scan0.ToPointer();
                    for (int y = 0; y < h; y++)
                    {
                        for (int x = 0; x < w; x++)
                        {

                            if (mode == 0)　// 加权平均
                            {
                                newColor = (byte)((float)p[0] * 0.114f + (float)p[1] * 0.587f + (float)p[2] * 0.299f);
                            }
                            else　　　　// 算数平均
                            {
                                newColor = (byte)((float)(p[0] + p[1] + p[2]) / 3.0f);
                            }
                            p[0] = newColor;
                            p[1] = newColor;
                            p[2] = newColor;

                            p += 3;
                        }
                        p += srcData.Stride - w * 3;
                    }
                    bmp.UnlockBits(srcData);
                    return bmp.GetThumbnailImage(image.Width, image.Height, () => { return true; }, IntPtr.Zero);
                }
            }
            catch
            {
                return null;
            }

        }
    }
}
