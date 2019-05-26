using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

namespace WavePicture
{
    public static class pict
    {
        static Color BackGround = Color.FromArgb(20, 20, 22);
        static Color Axiss = Color.FromArgb(200, 200, 220);

        static Color MainColor = Color.FromArgb(255, 200, 0);
        static Color SubColor = Color.FromArgb(200, 150, 0);

        static int w = 400;
        static int h = 201;

        public static Bitmap mkDef()
        {
            Bitmap def = new Bitmap(w, h);
            BitmapPlus bp = new BitmapPlus(def);
            bp.BeginAcces();
            for (int y = 0; y < h; y++)
                for (int x = 0; x < w; x++)
                    bp.SetPixel(x, y, BackGround);
            for (int x = 0; x < w; x++)
                bp.SetPixel(x, 100, Axiss);
            for (int y = 75; y < 125; y++)
            {
                bp.SetPixel(100, y, Axiss);
                bp.SetPixel(200, y, Axiss);
                bp.SetPixel(300, y, Axiss);
            }
            bp.EndAccess();

            return def;
        }

        public static Bitmap testSine()
        {
            int vmax = 99;
            Bitmap res = mkDef();
            BitmapPlus bp = new BitmapPlus(res);
            bp.BeginAcces();
            for (int x = 0; x < w; x++)
            {
                int val = (int)(
                    (50.0 * Math.Sin(2.0 * Math.PI * (double)x / (double)w)) +
                    (50.0 * Math.Sin(8.0 * Math.PI * (double)x / (double)w)) +
                    (50.0 * Math.Sin(4.0 * Math.PI * (double)x / (double)w))
                    );

                if (-vmax < val && val < vmax)
                {
                    bp.SetPixel(x, val + 102, SubColor);
                    bp.SetPixel(x, val + 101, SubColor);
                    bp.SetPixel(x, val + 99, SubColor);
                    bp.SetPixel(x, val + 98, SubColor);
                    bp.SetPixel(x, val + 100, MainColor);   
                }
                else if (val < -vmax)
                {
                    val = -vmax;
                    bp.SetPixel(x, val + 101, SubColor);
                    bp.SetPixel(x, val + 100, MainColor);
                }
                else
                {
                    val = vmax;
                    bp.SetPixel(x, val + 99, SubColor);
                    bp.SetPixel(x, val + 100, MainColor);   
                }
            }
            bp.EndAccess();

            return res;
        }

        public static Bitmap mkWave(
            float sin1, float sin2, float sin3, float sin4,
            float cos1, float cos2, float cos3, float cos4)
        {
            int vmax = Int16.MaxValue;
            Bitmap res = mkDef();
            BitmapPlus bp = new BitmapPlus(res);
            bp.BeginAcces();
            for (int x = 0; x < w; x++)
            {
                int val = (int)(
                    (vmax * sin1 * Math.Sin(2.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sin2 * Math.Sin(4.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sin3 * Math.Sin(6.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sin4 * Math.Sin(8.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * cos1 * Math.Cos(2.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * cos2 * Math.Cos(4.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * cos3 * Math.Cos(6.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * cos4 * Math.Cos(8.0 * Math.PI * (double)x / (double)w)) 
                    );

                if (val < Int16.MinValue)
                    val = Int16.MinValue;
                else if (Int16.MaxValue < val)
                    val = vmax;

                int plotval = (-val * 99 / Int16.MaxValue) + 100;

                bp.SetPixel(x, plotval, MainColor);
            }
            bp.EndAccess();

            return res;
        }

        public static Bitmap mkWave(SoundData sd)
        {
            int vmax = Int16.MaxValue;
            Bitmap res = mkDef();
            BitmapPlus bp = new BitmapPlus(res);
            bp.BeginAcces();
            for (int x = 0; x < w; x++)
            {
                int val = (int)(
                    (vmax * sd.sin1 * Math.Sin(2.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.sin2 * Math.Sin(4.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.sin3 * Math.Sin(6.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.sin4 * Math.Sin(8.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.sin8 * Math.Sin(16.0 * Math.PI * (double)x / (double)w)) +

                    (vmax * sd.cos1 * Math.Cos(2.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.cos2 * Math.Cos(4.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.cos3 * Math.Cos(6.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.cos4 * Math.Cos(8.0 * Math.PI * (double)x / (double)w)) +
                    (vmax * sd.cos8 * Math.Cos(16.0 * Math.PI * (double)x / (double)w))
                    );

                #region ks1
                if (x < 0.5 * w )
                    val += (int)(vmax * sd.ksin1);
                else
                    val -= (int)(vmax * sd.ksin1);
                #endregion

                #region ks2
                if (x < 0.25 * w)
                    val += (int)(vmax * sd.ksin2);
                else if (x < 0.5 * w)
                    val -= (int)(vmax * sd.ksin2);
                else if (x < 0.75 * w)
                    val += (int)(vmax * sd.ksin2);
                else
                    val -= (int)(vmax * sd.ksin2);
                #endregion

                #region ks3
                if (x < w / 6)
                    val += (int)(vmax * sd.ksin3);
                else if (x < w / 3)
                    val -= (int)(vmax * sd.ksin3);
                else if (x < w / 2)
                    val += (int)(vmax * sd.ksin3);
                else if (x < 2 * w / 3)
                    val -= (int)(vmax * sd.ksin3);
                else if (x < 5 * w / 6)
                    val += (int)(vmax * sd.ksin3);
                else
                    val -= (int)(vmax * sd.ksin3);
                #endregion

                #region ks4
                if (x < w * 0.125)
                    val += (int)(vmax * sd.ksin4);
                else if (x < w * 0.25)
                    val -= (int)(vmax * sd.ksin4);
                else if (x < w * 0.375)
                    val += (int)(vmax * sd.ksin4);
                else if (x < w * 0.5)
                    val -= (int)(vmax * sd.ksin4);
                else if (x < w * 0.625)
                    val += (int)(vmax * sd.ksin4);
                else if (x < w * 0.75)
                    val -= (int)(vmax * sd.ksin4);
                else if (x < w * 0.875)
                    val += (int)(vmax * sd.ksin4);
                else
                    val -= (int)(vmax * sd.ksin4);
                #endregion


                if (val < Int16.MinValue)
                    val = Int16.MinValue;
                else if (Int16.MaxValue < val)
                    val = vmax;

                int plotval = (-val * 99 / Int16.MaxValue) + 100;

                bp.SetPixel(x, plotval, MainColor);
            }
            bp.EndAccess();

            return res;
        }
    }
}
