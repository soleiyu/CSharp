using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;

namespace SoundGenerater
{
    static class HEADER
    {
        static public UInt32 _count = 44100;

        static public char[] _riff = { 'R', 'I', 'F', 'F' };
        static public char[] _wave = { 'W', 'A', 'V', 'E' };
        static public char[] _fmt = { 'f', 'm', 't', ' ' };
        static public UInt32 _fmtSize = 16;
        static public UInt16 _fmtId = 1;
        static public UInt16 _chNum = 2;
        static public UInt32 _sampleRate = 44100;
        static public UInt32 _dataSpeed = 176400;
        static public UInt16 _blockSize = 4;
        static public UInt16 _bitNum = 16;
        static public char[] _data = { 'd', 'a', 't', 'a' };
        static public UInt32 _waveBytes = _count * 2 * 2; //kari
        static public UInt32 _size = _waveBytes + 36; //kari
    }

    public static class Generater
    {
        static public List<Int16> mkOnOffWave(List<Int16> HzWave, List<float> onoff)
        {
            if (onoff.Count % 2 != 0)
                Console.WriteLine("ONOFF ERROR");
            
            float nowtime = 0;
            int flag = 0;
            List<Int16> res = new List<Int16>();

            for(int i = 0; i < onoff.Count; i++)
            {
                float t = onoff[i] - nowtime;

                if (flag % 2 == 0)
                    addList(res, mkSWave(t));
                else
                    addList(res, mkWave(HzWave, t));

                nowtime = onoff[i];
                flag++;
            }

            return res;
        }

        static List<Int16> addList(List<Int16> bef, List<Int16>aft)
        {
            List<Int16> res = bef;
            for (int i = 0; i < aft.Count; i++)
                res.Add(aft[i]);

            return res;
        }

        static List<Int16> mkWave(List<Int16> HzWave, float sec)
        {
            List<Int16> res = new List<Int16>();

            int hwc = HzWave.Count;
            UInt32 DataCount =
                (UInt32)(HEADER._sampleRate * sec);

            for (int c = 0; c < DataCount; c++)
                res.Add(HzWave[c % hwc]);

            return res;
        }

        static List<Int16> mkSWave(float sec)
        {
            List<Int16> res = new List<Int16>();

            UInt32 DataCount =
                (UInt32)(HEADER._sampleRate * sec);

            for (int c = 0; c < DataCount; c++)
                res.Add(0);

            return res;
        }

        static public List<Int32> mkSSWave(float sec)
        {
            List<Int32> res = new List<Int32>();

            UInt32 DataCount =
                (UInt32)(HEADER._sampleRate * sec);

            for (int c = 0; c < DataCount; c++)
                res.Add(0);

            return res;
        }



        static void writeWave(List<Int16> HzWave, float sec)
        {
            string fn = "HzWave";

            UInt32 DataCount =
                (UInt32)(HEADER._sampleRate * sec);
            HEADER._waveBytes = DataCount * 4;
            HEADER._size = HEADER._waveBytes + 36;

            FileStream fs = new FileStream(fn,
                System.IO.FileMode.OpenOrCreate,
                System.IO.FileAccess.Write);

            byte[] cache;

            #region HEADER
            //----- "RIFF" -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._riff[i]);

            //----- FILESIZE -----//
            cache = BitConverter.GetBytes(HEADER._size);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- "WAVE" -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._wave[i]);

            //----- "fmt " -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._fmt[i]);

            //----- FMTCSIZE -----//
            cache = BitConverter.GetBytes(HEADER._fmtSize);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- FMTID -----//
            cache = BitConverter.GetBytes(HEADER._fmtId);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- CHNUM -----//
            cache = BitConverter.GetBytes(HEADER._chNum);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- SAMPLINGRATE -----//
            cache = BitConverter.GetBytes(HEADER._sampleRate);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- DATASPEED -----//
            cache = BitConverter.GetBytes(HEADER._dataSpeed);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- BLOCKSIZE -----//
            cache = BitConverter.GetBytes(HEADER._blockSize);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- BITNUM -----//
            cache = BitConverter.GetBytes(HEADER._bitNum);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- "data" -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._data[i]);

            //----- WAVEBYTES -----//
            cache = BitConverter.GetBytes(HEADER._waveBytes);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);
            #endregion

            int hwc = HzWave.Count;

            for (int c = 0; c < DataCount; c++)
            {
                Int16 pdata = HzWave[c % hwc];
                cache = BitConverter.GetBytes(pdata);

                for (int i = 0; i < 2; i++)
                    fs.WriteByte(cache[i]);
                for (int i = 0; i < 2; i++)
                    fs.WriteByte(cache[i]);
            }


            fs.Close();
        }

        static void writeSong(List<Int16> _Data)
        {
            string fn = "SongWave";

            UInt32 DataCount = (UInt32)_Data.Count;
            HEADER._waveBytes = DataCount * 4;
            HEADER._size = HEADER._waveBytes + 36;

            FileStream fs = new FileStream(fn,
                System.IO.FileMode.OpenOrCreate,
                System.IO.FileAccess.Write);

            byte[] cache;

            #region HEADER
            //----- "RIFF" -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._riff[i]);

            //----- FILESIZE -----//
            cache = BitConverter.GetBytes(HEADER._size);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- "WAVE" -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._wave[i]);

            //----- "fmt " -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._fmt[i]);

            //----- FMTCSIZE -----//
            cache = BitConverter.GetBytes(HEADER._fmtSize);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- FMTID -----//
            cache = BitConverter.GetBytes(HEADER._fmtId);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- CHNUM -----//
            cache = BitConverter.GetBytes(HEADER._chNum);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- SAMPLINGRATE -----//
            cache = BitConverter.GetBytes(HEADER._sampleRate);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- DATASPEED -----//
            cache = BitConverter.GetBytes(HEADER._dataSpeed);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);

            //----- BLOCKSIZE -----//
            cache = BitConverter.GetBytes(HEADER._blockSize);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- BITNUM -----//
            cache = BitConverter.GetBytes(HEADER._bitNum);
            for (int i = 0; i < 2; i++)
                fs.WriteByte(cache[i]);

            //----- "data" -----//
            for (int i = 0; i < 4; i++)
                fs.WriteByte((byte)HEADER._data[i]);

            //----- WAVEBYTES -----//
            cache = BitConverter.GetBytes(HEADER._waveBytes);
            for (int i = 0; i < 4; i++)
                fs.WriteByte(cache[i]);
            #endregion

            for (int c = 0; c < DataCount; c++)
            {
                Int16 pdata = _Data[c];
                cache = BitConverter.GetBytes(pdata);

                for (int i = 0; i < 2; i++)
                    fs.WriteByte(cache[i]);
                for (int i = 0; i < 2; i++)
                    fs.WriteByte(cache[i]);
            }

            fs.Close();
        }

        static void playWave()
        {
            System.Media.SoundPlayer p = new System.Media.SoundPlayer("HzWave");
            p.Play();
        }

        static void playSong()
        {
            System.Media.SoundPlayer p = new System.Media.SoundPlayer("SongWave");
            p.Play();
        }

        public static void play(List<Int16> HzWave, float sec)
        {
            writeWave(HzWave, sec);
            playWave();
        }

        public static void playSong(List<Int16> _Data)
        {
            writeSong(_Data);
            playSong();
        }


    }
}
