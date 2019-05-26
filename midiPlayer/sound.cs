using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WavePicture
{
    public class SoundData
    {
        public float sin1;
        public float sin2;
        public float sin3;
        public float sin4;
        public float sin8;

        public float cos1;
        public float cos2;
        public float cos3;
        public float cos4;
        public float cos8;

        public float ksin1;
        public float ksin2;
        public float ksin3;
        public float ksin4;

        public SoundData(
            float _sin1, float _sin2, float _sin3, float _sin4, float _sin8,
            float _cos1, float _cos2, float _cos3, float _cos4, float _cos8,
            float _ksin1, float _ksin2, float _ksin3, float _ksin4
            )
        {
            sin1 = _sin1;
            sin2 = _sin2;
            sin3 = _sin3;
            sin4 = _sin4;
            sin8 = _sin8;

            cos1 = _cos1;
            cos2 = _cos2;
            cos3 = _cos3;
            cos4 = _cos4;
            cos8 = _cos8;

            ksin1 = _ksin1;
            ksin2 = _ksin2;
            ksin3 = _ksin3;
            ksin4 = _ksin4;
        }

        public SoundData(){}
    }

    public static class sound
    {
        static public Int16[] plotData;
        static int plotNum = 25600;
        static int sampleRate = 44100;

        static public void mkWave(SoundData sd)
        {
            Int16[] prePlot = new Int16[plotNum];

            for(int i = 0; i < plotNum; i++)
            {
                double theta = 2.0 * Math.PI * (double)i / (double)plotNum;

                double val = (double)Int16.MaxValue *(
                    sd.sin1 * Math.Sin(theta) +
                    sd.sin2 * Math.Sin(2.0 * theta) +
                    sd.sin3 * Math.Sin(3.0 * theta) +
                    sd.sin4 * Math.Sin(4.0 * theta) +
                    sd.sin8 * Math.Sin(8.0 * theta) +
                    sd.cos1 * Math.Sin(theta) +
                    sd.cos2 * Math.Cos(2.0 * theta) +
                    sd.cos3 * Math.Cos(3.0 * theta) +
                    sd.cos4 * Math.Cos(4.0 * theta) +
                    sd.cos8 * Math.Cos(8.0 * theta)
                    );

                #region ksin1
                if (i < plotNum * 0.5)
                    val += (double)Int16.MaxValue * sd.ksin1;
                else
                    val -= (double)Int16.MaxValue * sd.ksin1;
                #endregion

                #region ksin2
                if (i < plotNum * 0.25)
                    val += (double)Int16.MaxValue * sd.ksin2;
                else if (i < plotNum * 0.5)
                    val -= (double)Int16.MaxValue * sd.ksin2;
                else if (i < plotNum * 0.75)
                    val += (double)Int16.MaxValue * sd.ksin2;
                else
                    val -= (double)Int16.MaxValue * sd.ksin2;
                #endregion

                #region ksin3
                if (i < plotNum / 6)
                    val += (double)Int16.MaxValue * sd.ksin3;
                else if (i < plotNum / 3)
                    val -= (double)Int16.MaxValue * sd.ksin3;
                else if (i < plotNum / 2)
                    val += (double)Int16.MaxValue * sd.ksin3;
                else if (i < 2 * plotNum / 3)
                    val -= (double)Int16.MaxValue * sd.ksin3;
                else if (i < 5 * plotNum / 6)
                    val += (double)Int16.MaxValue * sd.ksin3;
                else
                    val -= (double)Int16.MaxValue * sd.ksin3;
                #endregion

                #region ksin4
                if (i < plotNum * 0.125)
                    val += (double)Int16.MaxValue * sd.ksin4;
                else if (i < plotNum * 0.25)
                    val -= (double)Int16.MaxValue * sd.ksin4;
                else if (i < plotNum * 0.375)
                    val += (double)Int16.MaxValue * sd.ksin4;
                else if (i < plotNum * 0.5)
                    val -= (double)Int16.MaxValue * sd.ksin4;
                else if (i < plotNum * 0.625)
                    val += (double)Int16.MaxValue * sd.ksin4;
                else if (i < plotNum * 0.75)
                    val -= (double)Int16.MaxValue * sd.ksin4;
                else if (i < plotNum * 0.875)
                    val += (double)Int16.MaxValue * sd.ksin4;
                else
                    val -= (double)Int16.MaxValue * sd.ksin4;
                #endregion



                if (val < Int16.MinValue)
                    prePlot[i] = Int16.MinValue;
                else if (Int16.MaxValue < val)
                    prePlot[i] = Int16.MaxValue;
                else
                    prePlot[i] = (Int16)val;
            }

            plotData = prePlot;
        }

        static public List<Int16> mkHzWave(float Hz)
        {
            int num = (int)((float)sampleRate / Hz);

            float ratio = plotNum / num;
            List<Int16> HzWave = new List<short>();

            for(int i = 0; i < num; i++)
            {
                int count = (int)(ratio * i);
                HzWave.Add(plotData[count]);
            }

            return HzWave;
        }
    }
}
