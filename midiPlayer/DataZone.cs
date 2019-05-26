using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MidiReader;

namespace Narcissus_F2
{
    static class SVal
    {
        static public int A4 = 69;
    }

    static class DataZone
    {
        static int top = 128;
        static public string fn_midi;
        static public MidiSequence ms;
        static public int endCount;
        static public float tenpo = 120.0f;
        static public int initF = 0;
        static public int stKiller = 0;

        static public List<Int16> _WAVEDATA;

        static public List<float>[] knots = new List<float>[top];

        public static float calcHz(int note_num)
        {
            double n = note_num - SVal.A4;

            return (float)(441.0 * Math.Pow(2.0, n / 12.0));
        }

        static public void mkSong2()
        {
            Console.WriteLine("Song1 : Complete.");
            List<List<Int16>> waveData = new List<List<Int16>>();

            Parallel.For(0, knots.Length, i =>
            {
                if (knots[i].Count == 0)
                { }
                else
                {
                    waveData.Add(
                        SoundGenerater.Generater.mkOnOffWave(
                        WavePicture.sound.mkHzWave(calcHz(i)),
                        knots[i]
                        ));
                }
            });
            Console.WriteLine("Song2 : WaveDataComplete.");

            List<Int32> AllWave =
                SoundGenerater.Generater.mkSSWave(
                endCount * 60.0f / (tenpo * ms.TimeBase));

            Parallel.For(0, waveData.Count, i =>
            {
                for (int n = 0; n < waveData[i].Count; n++)
                    AllWave[n] += waveData[i][n];
            });

            if (stKiller % 2 == 0)
            {
                List<Int16> ResWave = new List<Int16>();
                for (int i = 0; i < AllWave.Count; i++)
                {
                    if (AllWave[i] < Int16.MinValue)
                        ResWave.Add(Int16.MinValue);
                    else if (Int16.MaxValue < AllWave[i])
                        ResWave.Add(Int16.MaxValue);
                    else
                        ResWave.Add((Int16)AllWave[i]);
                }
                Console.WriteLine("Song2 : RESULTDataComplete.");

                _WAVEDATA = ResWave;
            }

            Int32 mv = 0;
            for (int i = 0; i < AllWave.Count; i++)
                if (mv < AllWave[i])
                    mv = AllWave[i];

            float ratio = (float)Int16.MaxValue / (float)mv;

            List<Int16> ResWave_sk = new List<Int16>();
            for (int i = 0; i < AllWave.Count; i++)
                    ResWave_sk.Add((Int16)(AllWave[i] * ratio));

            Console.WriteLine("Song2 : RESULTDataComplete.");

            _WAVEDATA = ResWave_sk;
        }

        static public void mkSong()
        {
            int eNum = ms.Events.Count();
            endCount = ms.Events[ms.Events.Count() - 1].Position;

            for (int i = 0; i < top; i++)
                knots[i] = new List<float>();

            int div = ms.TimeBase;
            float t = 60.0f / tenpo;

            for (int i = 0; i < eNum; i++)
            {
                if (ms.Events[i].EventType != EventType.NoteOn &&
                    ms.Events[i].EventType != EventType.NoteOff)
                    continue;

                knots[ms.Events[i].NoteNumber].Add(
                    (float)ms.Events[i].Position * t / (float)div
                    );
            }
        }
    }
}
