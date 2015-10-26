# MotionDetector
Detects motion from a camera. When there is motion detected a bitmap of the camera is saved in the temporary folder. Motion detection is calculated by the [AForge.NET](http://www.aforgenet.com/) library.

Sample output:

```
Motion Detector
Detects motion in the integrated laptop webcam
Threshold level: 0.05
10/26/2015 3:09:38 PM: Motion started. Motion level: 0.06461697
10/26/2015 3:09:38 PMImage saved as C:\Users\bertt\AppData\Local\Temp\motions\picture_0.jpg
10/26/2015 3:09:39 PM: Motion stopped. Motion level: 0.03365126
10/26/2015 3:09:39 PM: Motion started. Motion level: 0.05013238
10/26/2015 3:09:39 PMImage saved as C:\Users\bertt\AppData\Local\Temp\motions\picture_1.jpg
10/26/2015 3:09:39 PM: Motion stopped. Motion level: 0.04389431

```
