using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using WavePicture;
using SoundGenerater;

namespace Narcissus_F2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string PRINT_TEXT;

        void print(string txt)
        {
            PRINT_TEXT = txt + "\r\n" + PRINT_TEXT;
            tb_exePrint.Text = PRINT_TEXT;
        }

        void setGraph(Bitmap input)
        {
            using (Stream stream = new MemoryStream())
            {
                input.Save(stream, ImageFormat.Png);
                stream.Seek(0, SeekOrigin.Begin);

                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.CacheOption = BitmapCacheOption.OnLoad;
                bmp.StreamSource = stream;
                bmp.EndInit();
                this.Picture1.Source = bmp;
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            this.MouseLeftButtonDown += (sender, e) => this.DragMove();

            PRINT_TEXT = "EXECUTE PRINT";

            SoundData sd = mkSd();
            setGraph(WavePicture.pict.mkWave(sd));
            WavePicture.sound.mkWave(sd);

            DataZone.initF++;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        SoundData mkSd()
        {
            SoundData sd = new SoundData(
                float.Parse(tb_sin1.Text),
                float.Parse(tb_sin2.Text),
                float.Parse(tb_sin3.Text),
                float.Parse(tb_sin4.Text),
                0,
                float.Parse(tb_cos1.Text),
                float.Parse(tb_cos2.Text),
                float.Parse(tb_cos3.Text),
                float.Parse(tb_cos4.Text),
                0,
                float.Parse(tb_ks1.Text),
                float.Parse(tb_ks2.Text),
                float.Parse(tb_ks3.Text),
                float.Parse(tb_ks4.Text)
                );

            return sd;
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //setGraph(WavePicture.pict.testSine());
            SoundData sd = mkSd();
            setGraph(WavePicture.pict.mkWave(sd));
            WavePicture.sound.mkWave(sd);
        }

        private void b_reset_Click(object sender, RoutedEventArgs e)
        {
            tb_sin1.Text = "0";
            tb_sin2.Text = "0";
            tb_sin3.Text = "0";
            tb_sin4.Text = "0";
            tb_cos1.Text = "0";
            tb_cos2.Text = "0";
            tb_cos3.Text = "0";
            tb_cos4.Text = "0";

            tb_ks1.Text = "0";
            tb_ks2.Text = "0";
            tb_ks3.Text = "0";
            tb_ks4.Text = "0";

            SoundData sd = mkSd();
            setGraph(WavePicture.pict.mkWave(sd));
            print("Wave Reseted.");
        }

        private void b_rand_Click(object sender, RoutedEventArgs e)
        {
            System.Random r = new System.Random();
            float[] valbox = new float[64];
            for (int i = 0; i < 64; i++)
                valbox[i] = (float)(r.Next() % 100) / 100.0f;

            tb_sin1.Text = valbox[0].ToString();
            tb_sin2.Text = valbox[1].ToString();
            tb_sin3.Text = valbox[2].ToString();
            tb_sin4.Text = valbox[3].ToString();
            tb_cos1.Text = valbox[4].ToString();
            tb_cos2.Text = valbox[5].ToString();
            tb_cos3.Text = valbox[6].ToString();
            tb_cos4.Text = valbox[7].ToString();

            tb_ks1.Text = valbox[8].ToString();
            tb_ks2.Text = valbox[9].ToString();
            tb_ks3.Text = valbox[10].ToString();
            tb_ks4.Text = valbox[11].ToString();

            SoundData sd = mkSd();
            setGraph(WavePicture.pict.mkWave(sd));
            print("Random Seted.");
        }

        private void b_play441_Click(object sender, RoutedEventArgs e)
        {
            SoundData sd = mkSd();
            WavePicture.sound.mkWave(sd);
            SoundGenerater.Generater.play(WavePicture.sound.mkHzWave(441), 1);
            print("play 441Hz.");
        }

        private void b_gplay_Click(object sender, RoutedEventArgs e)
        {
            SoundData sd = mkSd();
            WavePicture.sound.mkWave(sd);
            float t = 0.3f;
            int s = 270;
            int v = 3;
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(57 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(59 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(61 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(62 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(64 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(66 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(68 + v)), t);
            System.Threading.Thread.Sleep(s);
            SoundGenerater.Generater.play(
                WavePicture.sound.mkHzWave(DataZone.calcHz(69 + v)), t);
            System.Threading.Thread.Sleep(s);

            print("play c scale.");
        }

        private void b_midiLoad_Click(object sender, RoutedEventArgs e)
        {
            SoundData sd = mkSd();
            WavePicture.sound.mkWave(sd);


            OpenFileDialog ofd = new OpenFileDialog();

            ofd.ShowDialog();

            DataZone.fn_midi = ofd.FileName;
            try
            {
                DataZone.ms = new MidiReader.MidiSequence(DataZone.fn_midi);
                //DataZone.mkSong();
                Console.WriteLine("Midi Load Success.");
                print("Midi Load Success.");
            }
            catch
            {
                Console.WriteLine("Midi Load Failed.");
                print("Midi Load Failed.");
            }
        }

        private void b_midiPlay_Click(object sender, RoutedEventArgs e)
        {
            print("Play music sequence start.");
            SoundData sd = mkSd();
            WavePicture.sound.mkWave(sd);
            DataZone.mkSong();
            print("step 1/3 done.");
            DataZone.mkSong2();
            print("step 2/3 done.");
            SoundGenerater.Generater.playSong(DataZone._WAVEDATA);
            print("sound compile finished.");
        }

        private void t_bpm_TextChanged(object sender, TextChangedEventArgs e)
        {
            float pbpm = 0;

            try
            {
                pbpm = float.Parse(t_bpm.Text);
            }
            catch
            {
                return;
            }

            DataZone.tenpo = pbpm;
        }

        void valChange(string sv)
        {
            if (DataZone.initF == 0)
                return;

            float val = 0;

            try
            {
                val = float.Parse(sv);
            }
            catch
            {
                return;
            }

            SoundData sd = mkSd();
            setGraph(WavePicture.pict.mkWave(sd));
            WavePicture.pict.mkWave(sd);
        }

        #region KSIN
        private void tb_ks1_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_ks1.Text);
        }
        private void tb_ks2_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_ks2.Text);
        }
        private void tb_ks3_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_ks3.Text);
        }
        private void tb_ks4_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_ks4.Text);
        }
        #endregion

        #region SIN
        private void tb_sin1_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_sin1.Text);
        }
        private void tb_sin2_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_sin2.Text);
        }
        private void tb_sin3_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_sin3.Text);
        }
        private void tb_sin4_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_sin4.Text);
        }
        #endregion

        #region COS
        private void tb_cos1_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_cos1.Text);
        }
        private void tb_cos2_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_cos2.Text);
        }
        private void tb_cos3_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_cos3.Text);
        }
        private void tb_cos4_TextChanged(object sender, TextChangedEventArgs e)
        {
            valChange(tb_cos4.Text);
        }
        #endregion

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            DataZone.stKiller++;
        }

    }
}