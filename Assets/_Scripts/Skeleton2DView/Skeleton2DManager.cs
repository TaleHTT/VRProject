using System;
using System.Collections.Generic;
using Windows.Kinect;
using UnityEngine;
using OpenCvSharp;
using UnityEngine.UI;
using Random = System.Random;

// using System.Windows.Media.Imaging;


namespace _Scripts.Skeleton2DView
{
    public class Skeleton2DManager : MonoBehaviour
    {
        private readonly List<Tuple<JointType, JointType>> bones = new()
        {
            new Tuple<JointType, JointType>(JointType.Head, JointType.Neck),
            new Tuple<JointType, JointType>(JointType.Neck, JointType.SpineShoulder),
            new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.SpineMid),
            new Tuple<JointType, JointType>(JointType.SpineMid, JointType.SpineBase),
            new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderRight),
            new Tuple<JointType, JointType>(JointType.SpineShoulder, JointType.ShoulderLeft),
            new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipRight),
            new Tuple<JointType, JointType>(JointType.SpineBase, JointType.HipLeft),
            // Right Arm
            new Tuple<JointType, JointType>(JointType.ShoulderRight, JointType.ElbowRight),
            new Tuple<JointType, JointType>(JointType.ElbowRight, JointType.WristRight),
            new Tuple<JointType, JointType>(JointType.WristRight, JointType.HandRight),
            new Tuple<JointType, JointType>(JointType.HandRight, JointType.HandTipRight),
            new Tuple<JointType, JointType>(JointType.WristRight, JointType.ThumbRight),
            // Left Arm
            new Tuple<JointType, JointType>(JointType.ShoulderLeft, JointType.ElbowLeft),
            new Tuple<JointType, JointType>(JointType.ElbowLeft, JointType.WristLeft),
            new Tuple<JointType, JointType>(JointType.WristLeft, JointType.HandLeft),
            new Tuple<JointType, JointType>(JointType.HandLeft, JointType.HandTipLeft),
            new Tuple<JointType, JointType>(JointType.WristLeft, JointType.ThumbLeft),
            // Right Leg
            new Tuple<JointType, JointType>(JointType.HipRight, JointType.KneeRight),
            new Tuple<JointType, JointType>(JointType.KneeRight, JointType.AnkleRight),
            new Tuple<JointType, JointType>(JointType.AnkleRight, JointType.FootRight),
            // Left Leg
            new Tuple<JointType, JointType>(JointType.HipLeft, JointType.KneeLeft),
            new Tuple<JointType, JointType>(JointType.KneeLeft, JointType.AnkleLeft),
            new Tuple<JointType, JointType>(JointType.AnkleLeft, JointType.FootLeft)
        };


        private readonly Tuple<byte, byte, byte, byte> red = new(255, 0, 0, 255);
        private readonly Tuple<byte, byte, byte, byte> green = new(0, 255, 0, 255);
        private readonly Tuple<byte, byte, byte, byte> blue = new(0, 0, 255, 255);

        private KinectSensor sensor;
        private ColorFrameReader reader;
        private Texture2D texture;
        private CoordinateMapper coordinateMapper;
        private readonly List<Scalar> randomScalars = new();

        private byte[] data;

        public Texture2D GetColorTexture()
        {
            return texture;
        }

        private void Start()
        {
            sensor = KinectSensor.GetDefault();
            coordinateMapper = sensor.CoordinateMapper;
            if (sensor == null) return;
            reader = sensor.ColorFrameSource.OpenReader();

            for (var i = 0; i < 24; i++)
            {
                var rand = new Random();
                var r = rand.Next(70, 220);
                var g = rand.Next(80, 220);
                var b = rand.Next(100, 220);
                var randomScalar = new Scalar(r, g, b);
                randomScalars.Add(randomScalar);
            }


            var frameDesc = sensor.ColorFrameSource.CreateFrameDescription(ColorImageFormat.Rgba);

            texture = new Texture2D(frameDesc.Width, frameDesc.Height, TextureFormat.RGBA32, false);
            data = new byte[frameDesc.BytesPerPixel * frameDesc.LengthInPixels];

            if (!sensor.IsOpen)
            {
                sensor.Open();
            }
        }

        private void Update()
        {
            var frame = reader?.AcquireLatestFrame();
            //new SocketWorker.SocketWorker().GetInstance();
            if (frame == null) return;
            var body = SocketWorker.SocketWorker.Instance.getBody();
            frame.CopyConvertedFrameDataToArray(data, ColorImageFormat.Rgba);
            //DrawBodyMap(body, data);
            /*try
            {
                DrawBodyMap(body, data);
            }
            catch (Exception e)
            {
                // ReSharper disable once Unity.PerformanceCriticalCodeInvocation
                Debug.Log(e);
            }*/
            try
            {

            DrawBodyMap(body, data);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                
            }

            // texture.LoadRawTextureData(data);


            texture.Apply();
            frame.Dispose();
        }


        private Tuple<double, double> ConverterPointToScreen(CameraSpacePoint position)
        {
            var point = coordinateMapper.MapCameraPointToColorSpace(position);
            return new Tuple<double, double>(point.X, point.Y);
        }


        private void DrawBodyMap(Body body, byte[] dataBody)
        {
            if (body == null)
            {
                texture.LoadRawTextureData(dataBody);
                return;
            }
            // var image = new Utils.Image { Data = dataPreview };
            var joints = body.Joints;

            using (var mat = new Mat(1080, 1920, MatType.CV_8UC4, dataBody))
            {
                using (var matNew = new Mat())
                {
                    Cv2.CvtColor(mat, matNew, ColorConversionCodes.RGBA2BGR);
                    var i = 0;
                    foreach (var bone in bones)
                    {
                        var position1 = joints[bone.Item1].Position;
                        var position2 = joints[bone.Item2].Position;

                        var startPoint = ConverterPointToScreen(position1);
                        var endPoint = ConverterPointToScreen(position2);

                        // 随机颜色


                        Cv2.Line(matNew, (int)startPoint.Item1, (int)startPoint.Item2,
                            (int)endPoint.Item1, (int)endPoint.Item2, randomScalars[i % 24], 8);
                        Cv2.Circle(matNew, (int)startPoint.Item1, (int)startPoint.Item2, 10, new Scalar(0, 0, 255), -1);
                        Cv2.Circle(matNew, (int)endPoint.Item1, (int)endPoint.Item2, 10, new Scalar(0, 0, 255), -1);
                        i++;
                    }

                    using (var matNew1 = new Mat())
                    {
                        Cv2.Flip(matNew, matNew1, FlipMode.X);
                        Destroy(texture, 0.1f);
                        texture = OpenCvSharp.Unity.MatToTexture(matNew1);
                    }
                }
            }

            GC.Collect();

            /*foreach (var bone in bones)
            {
                var position1 = joints[bone.Item1].Position;
                var position2 = joints[bone.Item2].Position;

                var startPoint = ConverterPointToScreen(position1);
                var endPoint = ConverterPointToScreen(position2);

                // Utils.Painter.DrawPoint(startPoint, 5, image, red);


                #region Color_Skeleton_Mapping

                /*DrawLine(startP, endP, joints, bone.Item1, bone.Item2, drawPen, CanvasBody, 6);
                DrawPoint(startP, joints, bone.Item1, CanvasBody, 15, 15);
                DrawPoint(endP, joints, bone.Item2, CanvasBody, 15, 15);#1#

                #endregion
            }*/
        }


        private void OnApplicationQuit()
        {
            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }

            if (sensor == null) return;
            if (sensor.IsOpen)
            {
                sensor.Close();
            }

            sensor = null;
        }
    }
}