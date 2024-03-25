using System;
using OpenCvSharp;

namespace _Scripts.Skeleton2DView
{
    public abstract class Utils
    {
        public abstract class ArrayReShaper
        {
            public static T[,] Reshape<T>(T[] array, params int[] dimensions)
            {
                var totalSize = 1;
                foreach (var dim in dimensions)
                {
                    if (dim <= 0)
                    {
                        throw new ArgumentException("维度大小必须大于零。");
                    }

                    totalSize *= dim;
                }

                if (array.Length != totalSize)
                {
                    throw new ArgumentException("无法将数组重新形状为指定的维度。");
                }

                var reshapedArray = new T[dimensions[0], GetTotalSize(dimensions, 1)];

                ReshapeRecursive(array, reshapedArray, dimensions, 0, new int[dimensions.Length]);

                return reshapedArray;
            }

            private static int GetTotalSize(int[] dimensions, int startIndex)
            {
                var totalSize = 1;
                for (var i = startIndex; i < dimensions.Length; i++)
                {
                    totalSize *= dimensions[i];
                }

                return totalSize;
            }

            private static void ReshapeRecursive<T>(T[] sourceArray, T[,] reshapedArray, int[] dimensions, int dimIndex,
                int[] indices)
            {
                if (dimIndex == dimensions.Length - 1)
                {
                    var sourceIndex = 0;
                    for (var i = 0; i < reshapedArray.GetLength(dimIndex); i++)
                    {
                        indices[dimIndex] = i;
                        reshapedArray.SetValue(sourceArray[sourceIndex], indices);
                        sourceIndex++;
                    }
                }
                else
                {
                    var subArraySize = GetTotalSize(dimensions, dimIndex + 1);
                    for (var i = 0; i < reshapedArray.GetLength(dimIndex); i++)
                    {
                        indices[dimIndex] = i;
                        var subArray = new T[subArraySize];
                        Array.Copy(sourceArray, i * subArraySize, subArray, 0, subArraySize);
                        ReshapeRecursive(subArray, reshapedArray, dimensions, dimIndex + 1, indices);
                    }
                }
            }
        }


        public class Image
        {
            public byte[] Data { get; set; }
        }

        public class Painter
        {
            private const int PictureWidth = 1920;
            private const int PictureHeight = 1080;
            private const int PictureChannels = 4;
            private const double Eps = 1e-5;

            public static void DrawPoint(Tuple<double, double> point, int radius, Image data,
                Tuple<byte, byte, byte, byte> color)
            {
                var x = point.Item1;
                var y = point.Item2;

                var image = new Mat(PictureHeight, PictureWidth, MatType.CV_8UC4, data.Data);
                Cv2.Circle(image, (int)x, (int)y, radius,
                    new Scalar(color.Item1, color.Item2, color.Item3, color.Item4),
                    -1);
                // transform back to byte array
                data.Data = image.ToBytes();

                /*for (var i = (int)(x - radius); i < x + radius; i++)
                {
                    for (var j = (int)(y - radius); j < y + radius; j++)
                    {
                        if (i < 0 || i >= PictureWidth || j < 0 || j >= PictureHeight) continue;
                        var index = (j * PictureWidth + i) * PictureChannels;
                        data.Data[index] = color.Item1;
                        data.Data[index + 1] = color.Item2;
                        data.Data[index + 2] = color.Item3;
                        data.Data[index + 3] = color.Item4;
                    }
                }*/
            }

            public static void DrawLine(Tuple<double, double> start, Tuple<double, double> end, int strokeWidth,
                Image data,
                Tuple<byte, byte, byte, byte> color)
            {
                // draw line with opencv
                var image = new Mat(PictureHeight, PictureWidth, MatType.CV_8UC4, data.Data);
                Cv2.Line(image, (int)start.Item1, (int)start.Item2, (int)end.Item1, (int)end.Item2,
                    new Scalar(color.Item1, color.Item2, color.Item3, color.Item4), strokeWidth);
                // transform back to byte array
                data.Data = image.ToBytes();
                
                /*var x0 = start.Item1;
                var y0 = start.Item2;
                var x1 = end.Item1;
                var y1 = end.Item2;
                var dx = Math.Abs(x1 - x0);
                var dy = Math.Abs(y1 - y0);
                var sx = (x0 < x1) ? 1 : -1;
                var sy = (y0 < y1) ? 1 : -1;
                var err = dx - dy;

                while (true)
                {
                    DrawPoint(start, strokeWidth, data, color);
                    if (Math.Abs(x0 - x1) < Eps && Math.Abs(y0 - y1) < Eps) break;
                    var e2 = 2 * err;
                    if (e2 > -dy)
                    {
                        err -= dy;
                        x0 += sx;
                    }

                    if (e2 < dx)
                    {
                        err += dx;
                        y0 += sy;
                    }
                }*/
            }
        }
    }
}