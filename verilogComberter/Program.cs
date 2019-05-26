using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;

using System.IO;

namespace veriPict
{
    static class Program
    {
        static Bitmap pict;

        static byte[,,] pixelData;

        static void Main(string[] args)
        {
            Console.Write("fname : ");
            string fn = Console.ReadLine();

            pict = new Bitmap(fn);
            Console.WriteLine("{0} x {1}", pict.Width, pict.Height);

            mkPix();

            mkPixFile(fn);

            //vWrite(fn);

            VWriter.main(pixelData, pict.Width, pict.Height);

            Console.Read();
        }

        static void mkPix()
        {
            pixelData = new byte[pict.Width, pict.Height, 3];

            BitmapPlus bp = new BitmapPlus(pict);
            bp.BeginAccess();

            for (int y = 0; y < pict.Height; y++)
            {
                for (int x = 0; x < pict.Width; x++)
                {
                    Color cc = bp.GetPixel(x, y);
                    pixelData[x, y, 0] = cc.R;
                    pixelData[x, y, 1] = cc.G;
                    pixelData[x, y, 2] = cc.B;
                }
            }

            bp.EndAccess();
        }

        static void mkPixFile(string fn)
        {
            #region MAKE FILE NAME
            string[] fns = fn.Split('.');
            string ofn = fns[0];
            for (int i = 1; i < fns.Length - 1; i++)
                ofn += "." + fns[i];
            ofn += ".pix";
            #endregion

            StreamWriter sw = new StreamWriter(ofn);

            sw.WriteLine(pict.Width);
            sw.WriteLine(pict.Height);

            for (int y = 0; y < pict.Height; y++)
                for (int x = 0; x < pict.Width; x++)
                    sw.WriteLine(pixelData[x, y, 0]);
            for (int y = 0; y < pict.Height; y++)
                for (int x = 0; x < pict.Width; x++)
                    sw.WriteLine(pixelData[x, y, 1]);
            for (int y = 0; y < pict.Height; y++)
                for (int x = 0; x < pict.Width; x++)
                    sw.WriteLine(pixelData[x, y, 2]);
            sw.Close();
        }

    }

    static class VWriter
    {
        static byte[,,] pixData;
        static int w, h;

        static int[] rmax;
        static int[] gmax;
        static int[] bmax;
        
        static public void main(byte[,,] inp, int wi, int hi)
        {
            pixData = inp;
            w = wi;
            h = hi;

            anal();

            mkVTopOC(0);
            mkVTopOC(1);
            mkVTopOC(2);
        }

        static void mkVTop(int type)
        {
            {
                StreamWriter sw;
                if (type == 0)
                    sw = new StreamWriter("pixelr.v");
                else if (type == 1)
                    sw = new StreamWriter("pixelg.v");
                else
                    sw = new StreamWriter("pixelb.v");

                sw.WriteLine("//PIXEL DATA");
                sw.WriteLine();

                if (type == 0)
                    sw.WriteLine("module pixelr");
                else if (type == 1)
                    sw.WriteLine("module pixelg");
                else
                    sw.WriteLine("module pixelb");

                sw.WriteLine("#(parameter integer HCOUNT_WIDTH = -1, parameter integer VCOUNT_WIDTH = -1)");
                sw.WriteLine("(h, v, out);");
                sw.WriteLine();

                sw.WriteLine("input wire [HCOUNT_WIDTH-1:0] h;");
                sw.WriteLine("input wire [VCOUNT_WIDTH-1:0] v;");
                sw.WriteLine();

                sw.WriteLine("output wire [7:0] out;");
                sw.WriteLine();

                sw.WriteLine("assign out = pr(h, v);");

                sw.WriteLine("// {0} x {1}", w, h);
                sw.WriteLine();

                mkFunction(type, sw);

                sw.Write("endmodule");

                sw.Close();
            }
        }

        static void mkVTopOC(int type)
        {
            {
                StreamWriter sw;
                if (type == 0)
                    sw = new StreamWriter("pixelr.v");
                else if (type == 1)
                    sw = new StreamWriter("pixelg.v");
                else
                    sw = new StreamWriter("pixelb.v");

                sw.WriteLine("//PIXEL DATA");
                sw.WriteLine();

                if (type == 0)
                    sw.WriteLine("module pixelr");
                else if (type == 1)
                    sw.WriteLine("module pixelg");
                else
                    sw.WriteLine("module pixelb");

                sw.WriteLine("#(parameter integer HCOUNT_WIDTH = -1, parameter integer VCOUNT_WIDTH = -1)");
                sw.WriteLine("(h, v, out);");
                sw.WriteLine();

                sw.WriteLine("input wire [HCOUNT_WIDTH-1:0] h;");
                sw.WriteLine("input wire [VCOUNT_WIDTH-1:0] v;");
                sw.WriteLine();

                sw.WriteLine("output wire [7:0] out;");
                sw.WriteLine();

                sw.WriteLine("assign out = pr(h, v);");

                sw.WriteLine("// {0} x {1}", w, h);
                sw.WriteLine();

                mkFunctionOC(type, sw);

                sw.Write("endmodule");

                sw.Close();
            }
        }

        static void mkFunction(int type, StreamWriter sw)
        {
            sw.WriteLine("function [7:0] pr;");
            sw.WriteLine("input [HCOUNT_WIDTH-1:0] hi;");
            sw.WriteLine("input [VCOUNT_WIDTH-1:0] vi;");
            sw.WriteLine("begin");

            int ofs = 0;

            int[] maxs = new int[0];

            if (type == 0)
                maxs = rmax;
            else if (type == 1)
                maxs = gmax;
            else
                maxs = bmax;

            for (int y = 0; y < h; y++)
            {

                if (y == 0)
                {
                    sw.WriteLine("if((v % {0}) == {1}) begin", h, ofs + y);

                    for (int x = 0; x < w; x++)
                    {
                        int val = pixData[x, y, type];

                        if (x == 0)
                        {
                            sw.WriteLine("if((h % {0}) == {1}) begin", w, ofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                        else if (val != maxs[y])
                        {
                            sw.WriteLine("else if((h % {0}) == {1}) begin", w, ofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                    }
                    sw.WriteLine("else begin");
                    sw.WriteLine("pr = {0}; end end", maxs[y]);
                }
                else
                {
                    sw.WriteLine("else if((v % {0}) == {1}) begin", h, ofs + y);

                    for (int x = 0; x < w; x++)
                    {
                        #region trueBeginEnd
                        int val = pixData[x, y, type];

                        if (x == 0)
                        {
                            sw.WriteLine("if((h % {0}) == {1}) begin", w, ofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                        else if (val != maxs[y])
                        {
                            sw.WriteLine("else if((h % {0}) == {1}) begin", w, ofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                        #endregion
                    }
                    sw.WriteLine("else begin");
                    sw.WriteLine("pr = {0}; end end", maxs[y]);
                }
                //sw.WriteLine("else begin");
                //sw.WriteLine("pr = 0; end");
                //sw.WriteLine("end");
            }
            sw.WriteLine("end");
            sw.WriteLine("endfunction");
            sw.WriteLine();
        }

        static void mkFunctionOC(int type, StreamWriter sw)
        {
            int xofs = (858 / 2) - (w / 2);
            int yofs = (525 / 2) - (h / 2);

            sw.WriteLine("function [7:0] pr;");
            sw.WriteLine("input [HCOUNT_WIDTH-1:0] hi;");
            sw.WriteLine("input [VCOUNT_WIDTH-1:0] vi;");
            sw.WriteLine("begin");
            
            for (int y = 0; y < h; y++)
            {

                if (y == 0)
                {
                    sw.WriteLine("if(v == {0}) begin", yofs + y);

                    for (int x = 0; x < w; x++)
                    {
                        int val = pixData[x, y, type];

                        if (x == 0)
                        {
                            sw.WriteLine("if(h == {0}) begin", xofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                        else if (val != 0)
                        {
                            sw.WriteLine("else if(h == {0}) begin", xofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                    }
                    sw.WriteLine("else begin");
                    sw.WriteLine("pr = {0}; end end", 0);
                }
                else
                {
                    sw.WriteLine("else if(v == {0}) begin", yofs + y);

                    for (int x = 0; x < w; x++)
                    {
                        #region trueBeginEnd
                        int val = pixData[x, y, type];

                        if (x == 0)
                        {
                            sw.WriteLine("if(h == {0}) begin", xofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                        else if (val != 0)
                        {
                            sw.WriteLine("else if(h == {0}) begin", xofs + x);
                            sw.WriteLine("pr = {0}; end", val);
                        }
                        #endregion
                    }
                    sw.WriteLine("else begin");
                    sw.WriteLine("pr = {0}; end end", 0);
                }
                //sw.WriteLine("else begin");
                //sw.WriteLine("pr = 0; end");
                //sw.WriteLine("end");
            }
            sw.WriteLine("end");
            sw.WriteLine("endfunction");
            sw.WriteLine();
        }

        static void anal()
        {
            rmax = new int[h];
            gmax = new int[h];
            bmax = new int[h];

            for (int y = 0; y < h; y++)
            {
                int[] rvals = new int[256];
                int[] gvals = new int[256];
                int[] bvals = new int[256];

                for (int x = 0; x < w; x++)
                {
                    rvals[pixData[x, y, 0]]++;
                    gvals[pixData[x, y, 1]]++;
                    bvals[pixData[x, y, 2]]++;
                }

                int rm = -1;
                int gm = -1;
                int bm = -1;
                int rmc = -1;
                int gmc = -1;
                int bmc = -1;

                for (int i = 0; i < 256; i++)
                {
                    if (rm < rvals[i])
                    {
                        rm = rvals[i];
                        rmc = i;
                    }
                    if (gm < gvals[i])
                    {
                        gm = gvals[i];
                        gmc = i;
                    }
                    if (bm < bvals[i])
                    {
                        bm = bvals[i];
                        bmc = i;
                    }
                }

                rmax[y] = rmc;
                gmax[y] = gmc;
                bmax[y] = bmc;
            }
        }
    }

    class BitmapPlus
    {
        /// <summary>
        /// オリジナルのBitmapオブジェクト
        /// </summary>
        private Bitmap _bmp = null;

        /// <summary>
        /// Bitmapに直接アクセスするためのオブジェクト
        /// </summary>
        private BitmapData _img = null;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="original"></param>
        public BitmapPlus(Bitmap original)
        {
            // オリジナルのBitmapオブジェクトを保存
            _bmp = original;
        }

        /// <summary>
        /// Bitmap処理の高速化開始
        /// </summary>
        public void BeginAccess()
        {
            // Bitmapに直接アクセスするためのオブジェクト取得(LockBits)
            _img = _bmp.LockBits(new Rectangle(0, 0, _bmp.Width, _bmp.Height),
                System.Drawing.Imaging.ImageLockMode.ReadWrite,
                System.Drawing.Imaging.PixelFormat.Format24bppRgb);
        }

        /// <summary>
        /// Bitmap処理の高速化終了
        /// </summary>
        public void EndAccess()
        {
            if (_img != null)
            {
                // Bitmapに直接アクセスするためのオブジェクト開放(UnlockBits)
                _bmp.UnlockBits(_img);
                _img = null;
            }
        }

        /// <summary>
        /// BitmapのGetPixel同等
        /// </summary>
        /// <param name="x">Ｘ座標</param>
        /// <param name="y">Ｙ座標</param>
        /// <returns>Colorオブジェクト</returns>
        public Color GetPixel(int x, int y)
        {
            if (_img == null)
            {
                // Bitmap処理の高速化を開始していない場合はBitmap標準のGetPixel
                return _bmp.GetPixel(x, y);
            }

            // Bitmap処理の高速化を開始している場合はBitmapメモリへの直接アクセス
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            byte b = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 0);
            byte g = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 1);
            byte r = System.Runtime.InteropServices.Marshal.ReadByte(adr, pos + 2);
            return Color.FromArgb(r, g, b);
        }

        /// <summary>
        /// BitmapのSetPixel同等
        /// </summary>
        /// <param name="x">Ｘ座標</param>
        /// <param name="y">Ｙ座標</param>
        /// <param name="col">Colorオブジェクト</param>
        public void SetPixel(int x, int y, Color col)
        {
            if (_img == null)
            {
                // Bitmap処理の高速化を開始していない場合はBitmap標準のSetPixel
                _bmp.SetPixel(x, y, col);
                return;
            }

            // Bitmap処理の高速化を開始している場合はBitmapメモリへの直接アクセス
            IntPtr adr = _img.Scan0;
            int pos = x * 3 + _img.Stride * y;
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 0, col.B);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 1, col.G);
            System.Runtime.InteropServices.Marshal.WriteByte(adr, pos + 2, col.R);
        }
    }
}
