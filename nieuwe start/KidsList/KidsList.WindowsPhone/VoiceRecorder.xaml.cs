using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.MediaProperties;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace KidsList
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class VoiceRecorder : Page
    {
        private MediaCapture _mediaCaptureManager;
        private StorageFile _recordStorageFile;
        private bool _recording;
        private bool _userRequestedRaw;
        private bool _rawAudioSupported;

        public VoiceRecorder()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            InitializeAudioRecording();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        private async void InitializeAudioRecording()
        {

            _mediaCaptureManager = new MediaCapture();
            var settings = new MediaCaptureInitializationSettings();
            settings.StreamingCaptureMode = StreamingCaptureMode.Audio;
            settings.MediaCategory = MediaCategory.Other;
            settings.AudioProcessing = (_rawAudioSupported && _userRequestedRaw) ? AudioProcessing.Raw : AudioProcessing.Default;

            await _mediaCaptureManager.InitializeAsync(settings);

            Debug.WriteLine("Device initialised successfully");

            _mediaCaptureManager.RecordLimitationExceeded += new RecordLimitationExceededEventHandler(RecordLimitationExceeded);
            _mediaCaptureManager.Failed += new MediaCaptureFailedEventHandler(Failed);
        }

        private void Failed(MediaCapture sender, MediaCaptureFailedEventArgs errorEventArgs)
        {
            throw new NotImplementedException();
        }

        private void RecordLimitationExceeded(MediaCapture sender)
        {
            throw new NotImplementedException();
        }

        private async void CaptureAudio()
        {
            try
            {
                Debug.WriteLine("Starting record");
                String fileName = "record.m4a";

                _recordStorageFile = await KnownFolders.VideosLibrary.CreateFileAsync(fileName, CreationCollisionOption.GenerateUniqueName);

                Debug.WriteLine("Create record file successfully");

                MediaEncodingProfile recordProfile = MediaEncodingProfile.CreateM4a(AudioEncodingQuality.Auto);


                await _mediaCaptureManager.StartRecordToStorageFileAsync(recordProfile, this._recordStorageFile);

                Debug.WriteLine("Start Record successful");

                _recording = true;
            }
            catch (Exception e)
            {
                Debug.WriteLine("Failed to capture audio");
            }
        }

        private async void StopCapture()
        {
            if (_recording)
            {
                Debug.WriteLine("Stopping recording");
                await _mediaCaptureManager.StopRecordAsync();
                Debug.WriteLine("Stop recording successful");
                _recording = false;
            }
        }

        private async void PlayRecordedCapture()
        {
            if (!_recording)
            {
                var stream = await _recordStorageFile.OpenAsync(FileAccessMode.Read);
                Debug.WriteLine("Recording file opened");
                playbackElement1.AutoPlay = true;
                playbackElement1.SetSource(stream, _recordStorageFile.FileType);
                playbackElement1.Play();
            }
        }

        private void CaptureButton_Click(object sender, RoutedEventArgs e)
        {
            CaptureAudio();
        }

        private void StopCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            StopCapture();
        }

        private void PlayRecordButton_Click(object sender, RoutedEventArgs e)
        {
            PlayRecordedCapture();
        }


    }
}
