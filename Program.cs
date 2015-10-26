using System;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Vision.Motion;

namespace AForgeTest
{
    class Program
    {
        private static MotionDetector _motionDetector;
        private static float _motionAlarmLevel = 0.03f;
        private static bool hasMotion = false;

        static void Main()
        {
            Console.WriteLine("Motion Detector");
            Console.WriteLine("Detects motion in the integrated laptop webcam");
            Console.WriteLine("Threshold level: " + _motionAlarmLevel);
            _motionDetector = new MotionDetector(new TwoFramesDifferenceDetector(), new MotionAreaHighlighting());
            if (new FilterInfoCollection(FilterCategory.VideoInputDevice).Count > 0)
            {
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
                if (hasMotion) return;
                Console.WriteLine("Alarm motion started. Motion level: " + motionLevel);
                hasMotion = true;
            }
            else
            {
                if (hasMotion)
                {
                    Console.WriteLine("Alarm motion stopped. Motion level: " + motionLevel);
                }
                hasMotion = false;
            }
        }
    }
}
