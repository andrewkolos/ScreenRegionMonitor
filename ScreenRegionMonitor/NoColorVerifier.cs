using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ScreenRegionMonitor
{
    class NoColorVerifier : ICaptureVerifier
    {
        public Color ColorToDetect { get; }

        private Action<string> logFunc;

        public NoColorVerifier(Color color, Action<string> logFunc)
        {
            ColorToDetect = color;
            this.logFunc = logFunc;
        }
        public bool verify(Bitmap captureImage)
        {
            // Lock the bitmap's bits.  
            Rectangle rect = new Rectangle(0, 0, captureImage.Width, captureImage.Height);
            BitmapData bmpData = captureImage.LockBits(rect, ImageLockMode.ReadWrite, PixelFormat.Format24bppRgb);

            // Get the address of the first line.
            IntPtr ptr = bmpData.Scan0;

            // Declare an array to hold the bytes of the bitmap.
            int bytes = bmpData.Stride * captureImage.Height;
            byte[] rgbValues = new byte[bytes];

            // Copy the RGB values into the array.
            Marshal.Copy(ptr, rgbValues, 0, bytes);

            int count = 0;
            int stride = bmpData.Stride;

            for (int column = 0; column < bmpData.Height; column++)
            {
                for (int row = 0; row < bmpData.Width; row++)
                {
                    var b= (byte)(rgbValues[(column * stride) + (row * 3)]);
                    var g= (byte)(rgbValues[(column * stride) + (row * 3) + 1]);
                    var r=  (byte)(rgbValues[(column * stride) + (row * 3) + 2]);

                    if (ColorToDetect.Equals(Color.FromArgb(r, g, b))) {
                        return false;
                    }
                }
            }
            return true;
        }

    }
}
    