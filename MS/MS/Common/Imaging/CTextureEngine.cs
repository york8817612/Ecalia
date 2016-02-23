using Cocos2D;
using reWZ.WZProperties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging
{
    public class CTextureEngine : CCSprite
    {
        private List<Microsoft.Xna.Framework.Graphics.Texture2D> frames = new List<Microsoft.Xna.Framework.Graphics.Texture2D>();
        private List<int> Width_ = new List<int>();
        private List<int> Height_ = new List<int>();
        private List<float> X_ = new List<float>();
        private List<float> Y_ = new List<float>();
        private WZPointProperty origin;

        public CTextureEngine()
        {

        }

        public WZPointProperty AddOrigin(WZCanvasProperty img)
        {
            origin = (WZPointProperty)img["origin"];
            return origin;
        }

        public void AddSize(int width, int height)
        {
            Width_.Add(width);
            Height_.Add(height);
        }

        public void AddPos(float x, float y)
        {
            if (x == 0)
                x = 400;
            else if (y == 0)
                y = 300;

            X_.Add(x);
            Y_.Add(y);
        }

        public void AddTexture(System.Drawing.Bitmap img)
        {
            frames.Add(GetTexture(img));
        }

        public List<float> X
        {
            get { return X_; }
        }

        public List<float> Y
        {
            get { return Y_; }
        }

        public List<int> Width
        {
            get { return Width_; }
        }

        public List<int> Height
        {
            get { return Height_; }
        }

        public List<Microsoft.Xna.Framework.Graphics.Texture2D> Frame
        {
            get { return frames; }
        }

        public WZPointProperty Origin
        {
            get { return origin; }
        }

        /// <summary>
        /// Converts Bitmap into byte array
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public byte[] GetData(System.Drawing.Bitmap img)
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
        public Microsoft.Xna.Framework.Graphics.Texture2D GetTexture(System.Drawing.Bitmap img)
        {
            int data = img.Height * img.Width * 4;
            MemoryStream stream = new MemoryStream(data);
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            var tex = Microsoft.Xna.Framework.Graphics.Texture2D.FromStream(CCDrawManager.GraphicsDevice, stream);

            return tex;
        }
    }
}
