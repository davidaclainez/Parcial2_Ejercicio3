using MediaManager;
using Plugin.AudioRecorder;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace Ejercicio3
{
    public partial class MainPage : ContentPage
    {
        AudioRecorderService recorder;
        public MainPage()
        {
            InitializeComponent();
            recorder = new AudioRecorderService
            {
                StopRecordingOnSilence = true, //will stop recording after 2 seconds (default)
                StopRecordingAfterTimeout = true,  //stop recording after a max timeout (defined below)
                TotalAudioTimeout = TimeSpan.FromSeconds(180) //audio will stop recording after 3 minutes
            };
        }
        async void RecordButton_Click(object sender, EventArgs e)
        {
            await RecordAudio();
        }
        async Task RecordAudio()
        {
            try
            {
                if (!recorder.IsRecording)
                {
                    btnGrabar.Text = "Detener Grabacion";
                    await recorder.StartRecording();
                }
                else
                {
                     await recorder.StopRecording();
                    btnGrabar.Text = "Grabar";
                }
            }
            catch (Exception ex)
            {
                //...
            }
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            //var audioFile = await recorder;
            if (recorder.FilePath != null) //non-null audioFile indicates audio was successfully recorded
            {
                //do something with the file
                var path = recorder.FilePath;
                System.Console.WriteLine("_________");
                System.Console.WriteLine("file://" + path);
                System.Console.WriteLine("_________");
                await CrossMediaManager.Current.Play("file://" + path);
            }
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            var path = recorder.FilePath;
            string rutaOriginal = "file:/" + path;
            string ruta = "file:/" + Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/" + DateTime.Now.ToString("MMddyyHmm") + ".wav";
            //File.Move(rutaOriginal, ruta);
            //Console.WriteLine("file://" + Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/" + DateTime.Now.ToString("MMddyyHmm") + ".wav");

            //await CrossMediaManager.Current.Play("file://" + path);
            //System.IO.File.Copy("file://" + path, "file://" + Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + "/" + DateTime.Now.ToString("MMddyyHmm") + ".wav");
            //await DisplayAlert("PAth", Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) +"/"+ DateTime.Now.ToString("MMddyyHmm") + ".wav", "OK");

        }
    }
}
