using Windows.Kinect;
using UnityEngine;

// using System.Windows.Media.Imaging;


namespace _Scripts.Scenes.SPPBScene
{
    public class Skeleton2DManagerInSppbScene : MonoBehaviour
    {
        
        private KinectSensor sensor;
        private ColorFrameReader reader;
        private Texture2D texture;
        private byte[] data;

        public Texture2D GetColorTexture()
        {
            return texture;
        }

        private void Start()
        {
            sensor = KinectSensor.GetDefault();
           
            if (sensor == null) return;
            reader = sensor.ColorFrameSource.OpenReader();


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
            frame?.CopyConvertedFrameDataToArray(data, ColorImageFormat.Rgba);
            texture.LoadRawTextureData(data);
            texture.Apply();
            frame?.Dispose();
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