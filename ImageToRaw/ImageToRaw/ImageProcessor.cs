using System.Drawing;

namespace ImageToRaw
{
    public static class ImageProcessor
    {
        public static byte[] GetRawDataFromBitmap(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;
            int stride = width * 3;
            byte[] pixelData = new byte[width * height * 3];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color color = bitmap.GetPixel(x, y);
                    int index = (y * stride) + (x * 3);
                    pixelData[index] = color.B;
                    pixelData[index + 1] = color.G;
                    pixelData[index + 2] = color.R;
                }
            }

            return pixelData;
        }
    }
}
