using System;
using System.Drawing.Imaging;
using System.IO;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;

namespace AForgeTest
{
    class Program
    {
        private static MotionDetector _motionDetector;
        private static float _motionAlarmLevel = 0.05f;
        private static bool _hasMotion;
        private static int _volgnr;
        private static string _path;

        static void Main()
        {
            _path =  Path.GetTempPath();
            Console.WriteLine("Motion Detector");
            Console.WriteLine("Detects motion in the integrated laptop webcam");
            Console.WriteLine("Threshold level: " + _motionAlarmLevel);
            _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            if (new FilterInfoCollection(FilterCategory.VideoInputDevice).Count > 0)
            {
                _path += "motions";

                if (!Directory.Exists(_path))
                {
                    Directory.CreateDirectory(_path);
                }
                else
                {
                    var dir = new DirectoryInfo(_path);
                    foreach (var fi in dir.GetFiles())
                    {
                        fi.Delete();
                    }
                }

                var videoDevice = new FilterInfoCollection(FilterCategory.VideoInputDevice)[0];
                var videoCaptureDevice = new VideoCaptureDevice(videoDevice.MonikerString);
                var videoSourcePlayer = new AForge.Controls.VideoSourcePlayer();
                videoSourcePlayer.NewFrame += VideoSourcePlayer_NewFrame;
                videoSourcePlayer.VideoSource = new AsyncVideoSource(videoCaptureDevice);
                videoSourcePlayer.Start();
            }
        }

        private static void VideoSourcePlayer_NewFrame(object sender, ref System.Drawing.Bitmap image)
        {
            var motionLevel = _motionDetector.ProcessFrame(image);

            if (motionLevel > _motionAlarmLevel)
            {
                if (_hasMotion) return;
                Console.WriteLine(DateTime.Now + ": Motion started. Motion level: " + motionLevel);
                var file = _path + @"\picture_" + _volgnr + ".jpg";
                Console.WriteLine(DateTime.Now + "Image saved as " + file);
                image.Save(file,ImageFormat.Jpeg);
                _volgnr++;
                _hasMotion = true;
            }
            else
            {
                if (_hasMotion)
                {
                    Console.WriteLine(DateTime.Now + ": Motion stopped. Motion level: " + motionLevel);
                }
                _hasMotion = false;
            }
        }
    }
}
