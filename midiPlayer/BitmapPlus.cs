using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;

namespace WavePicture
{
    class BitmapPlus
    {
        private Bitmap _bmp = null;

        private BitmapData _img = null;

        public BitmapPlus(Bitmap Original)
        {
            _bmp = Original;
        }

        public void BeginAcces()
        {
            _img = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        public void EndAccess()
        {
            if (_img != null)
            {
                _bmp.UnlockBits(_img);
                _img = null;
            }
        }

        public Color GetPixel(int x, int y)
        {
            if (_img == null)
            {
                return _bmp.GetPixel(x, y);
            }

            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 0);
            byte g = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 1);
            byte r = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 2);
            return Color.FromArgb(r, g, b);
        }

        public byte GetR(int x, int y)
        {
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            byte r = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 2);
            return r;
        }

        public byte GetG(int x, int y)
        {
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            byte g = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 1);
            return g;
        }

        public byte GetB(int x, int y)
        {
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 0);
            return b;
        }



        public void SetPixel(int x, int y, Color col)
        {
            if (_img == null)
            {
                _bmp.SetPixel(x, y, col);
                return;
            }

            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 0, col.B);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 1, col.G);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 2, col.R);
        }
    }
}
