using Cocos2D;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging
{
    /// <summary>
    /// Class used to convert Bitmap into Texture2D & byte array
    /// </summary>
    public class CT2B
    {
        /// <summary>
        /// Converts Bitmap into byte array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static byte[] GetData(System.Drawing.Bitmap img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
//                stream.Dispose();
            }

            return byteArray;
        }

        /// <summary>
        /// Converts bitmap to Texture2D
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public static Microsoft.Xna.Framework.Graphics.Texture2D GetTexture(System.Drawing.Bitmap img)
        {
            int data = img.Height * img.Width * 4;
            MemoryStream stream = new MemoryStream(data);
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            var tex = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(CCDrawManager.GraphicsDevice, stream);

            return tex;
        }
    }
}
