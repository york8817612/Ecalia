using Cocos2D;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging
{

    #region Logo Engine

    /// <summary>
    /// Handles the Logos in the beginning of the client process
    /// </summary>
    public class CLogoEngine : CTextureEngine
    {
        private int width, height;
        private float x, y;
        private System.Drawing.Bitmap nxLogo;
        private List<System.Drawing.Bitmap> wizetImage = new List<System.Drawing.Bitmap>();
        private List<System.Drawing.Bitmap> nexonImage = new List<System.Drawing.Bitmap>();
        private List<float> PosX = new List<float>();
        private List<float> PosY = new List<float>();
        private List<int> imageWidth = new List<int>();
        private List<int> imageHeight = new List<int>();
       // private CTextureEngine tex = new CTextureEngine();
        private WZFile ui;

        public int Width
        {
            get { return width; }
        }

        public int Height
        {
            get { return height; }
        }

        public float X
        {
            get { return x; }
        }

        public float Y
        {
            get { return y; }
        }

        public List<int> ImageWidth
        {
            get { return imageWidth; }
        }

        public List<int> ImageHeight
        {
            get { return imageHeight; }
        }

        public List<float> PositionX
        {
            get { return PosX; }
        }

        public List<float> PositionY
        {
            get { return PosY; }
        }

        public int WizetCount
        {
            get { return wizetImage.Count; }
        }

        public int NexonCount
        {
            get { return nexonImage.Count; }
        }

        /// <summary>
        /// Gets all the textures and images for the logos
        /// </summary>
        /// <param name="file">The wz file containing the logos</param>
        public CLogoEngine(WZFile file)
        {
            ui = file;
            var logo = ui.MainDirectory["Logo.img"];

            WZSubProperty nexon = logo["Nexon"] as WZSubProperty;
            WZSubProperty wizet = logo["Wizet"] as WZSubProperty;

            if (!GameConstants.GREATER_VERSION)
            {
                width = ((WZCanvasProperty)nexon["0"]).Value.Width;
                height = ((WZCanvasProperty)nexon["0"]).Value.Height;
                x = ((WZPointProperty)nexon["0"]["origin"]).Value.X;
                y = ((WZPointProperty)nexon["0"]["origin"]).Value.Y;
                nxLogo = ((WZCanvasProperty)nexon["0"]).Value;
            }
            else
            {
                foreach (WZCanvasProperty image in nexon)
                {
                    nexonImage.Add(image.Value);
                }

                foreach (System.Drawing.Bitmap bitmap in nexonImage)
                {
                    imageWidth.Add(bitmap.Width);
                    imageHeight.Add(bitmap.Height);

                    for (int i = 0; i < NexonCount; i++)
                    {
                        PosX.Add(((WZPointProperty)nexon[i.ToString()]["origin"]).Value.X);
                        PosY.Add(((WZPointProperty)nexon[i.ToString()]["origin"]).Value.Y);

                        if (i == NexonCount)
                            break;
                    }
                }
            }

            foreach (WZCanvasProperty image in wizet)
            {
                wizetImage.Add(image.Value);
            }

            foreach (System.Drawing.Bitmap bitmap in wizetImage)
            {
                imageWidth.Add(bitmap.Width);
                imageHeight.Add(bitmap.Height);

                for (int i = 0; i < (GameConstants.GREATER_VERSION ? WizetCount - 1 : WizetCount); i++)
                {
                    PosX.Add(((WZPointProperty)wizet[i.ToString()]["origin"]).Value.X);
                    PosY.Add(((WZPointProperty)wizet[i.ToString()]["origin"]).Value.Y);

                    if (i == wizetImage.Count)
                        break;
                }
            }
        }

        /// <summary>
        /// Draws the logos (new or old)
        /// </summary>
        /// <param name="isFirst">This is to check whether its the nexon logo or the wizet logo</param>
        /// <returns></returns>
        public CCSprite Draw(bool isFirst)
        {
            try
            {
                CCTexture2D[] Textures;
                CCSprite[] Sprites;
                CCAnimation animation; ;
                CCAnimate animate;

                if (isFirst)
                {
                    if (!GameConstants.GREATER_VERSION)
                    {
                        CCTexture2D texture = new CCTexture2D();
                        texture.InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, nxLogo));

                        CCSprite sprite = new CCSprite(texture);
                        sprite.SetPosition(X, Y);

                        return sprite;
                    }
                    else
                    {

                        Textures = new CCTexture2D[NexonCount];
                        Sprites = new CCSprite[NexonCount];
                        animation = new CCAnimation();

                        for (int i = 0; i < NexonCount; i++)
                        {
                            Textures[i] = new CCTexture2D();
                            Textures[i].InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, nexonImage[i]));
                        }

                        for (int i = 0; i < NexonCount; i++)
                        {
                            Sprites[i] = new CCSprite(Textures[i]);
                            Sprites[i].SetPosition(PositionX[i], PositionY[i]);
                            Sprites[i].SetTextureRect(new CCRect(0, 0, 800, 600));
                            animation.AddSprite(Sprites[i]);
                        }

                        animation.DelayPerUnit = 1.0f;
                        animation.Loops = 1;
                        animate = new CCAnimate(animation);
                        animate.Duration = 5.0f;

                        Sprites[0].RunAction(animate);

                        return Sprites[0];
                    }
                }
                else
                {
                    Textures = new CCTexture2D[WizetCount];
                    Sprites = new CCSprite[WizetCount];
                    animation = new CCAnimation();



                    for (int i = 0; i < WizetCount; i++)
                    {
                        Textures[i] = new CCTexture2D();
                        Textures[i].InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, wizetImage[i]));
                    }


                    for (int i = 0; i < WizetCount; i++)
                    {
                        Sprites[i] = new CCSprite(Textures[i]);
                        if (!GameConstants.GREATER_VERSION)
                        {
                            Sprites[i].SetPosition(PositionX[i] * (float)2.91 / 2, PositionY[i] * (float)2.86 / 2);
                        }
                        else
                        {
                            Sprites[i].SetPosition(PositionX[i], PositionY[i]);
                        }
                        Sprites[i].SetTextureRect(new CCRect(0, 0, 800, 600));
                        animation.AddSprite(Sprites[i]);

                        if (i == WizetCount)
                            break;
                    }
                    animation.DelayPerUnit = 1.0f; // How much time passes between frames (will be affected by duration of animation)
                    animation.Loops = 1; // How many times it will loop. 0 = None, 1 = 1, etc.

                    animate = new CCAnimate(animation);
                    animate.Duration = 5.5f;

                    Sprites[0].RunAction(animate);

                    return Sprites[0];
                }
            }
            finally
            {

            }
        }

        private void Write(string msg, params object[] obj)
        {
            Console.WriteLine(msg, obj);
        }
    }

    #endregion

    #region Object Engine

    public class CObjectEngine : CTextureEngine
    {
        private List<System.Drawing.Bitmap> ObjImg = new List<System.Drawing.Bitmap>();
        private WZFile map = new WZFile(GameConstants.FileLocation + @"Map.wz", GameConstants.Variant, true);

        public CObjectEngine(string oS, string l0, string l1, string l2)
        {
            Console.WriteLine(oS + " " + l0 + " " + l1 + " " + l2);
            if (oS != null && l0 != null && l1 != null && l2 != null)
            {
                var objs = (WZSubProperty)map.MainDirectory["Obj"][oS + ".img"][l0][l1][l2];

                foreach (WZCanvasProperty CanvasObjs in objs)
                {
                    ObjImg.Add(CanvasObjs.Value);
                    AddPos(((WZPointProperty)CanvasObjs["origin"]).Value.X, ((WZPointProperty)CanvasObjs["origin"]).Value.Y);
                }
            }
        }

        public CCSprite Draw()
        {
            CCTexture2D[] texture = new CCTexture2D[ObjImg.Count];
            CCSprite[] spr = new CCSprite[ObjImg.Count];

            for (int i = 0; i < ObjImg.Count; i++)
            {
                texture[i] = new CCTexture2D();
                texture[i].InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, ObjImg[i]));

                spr[i] = new CCSprite(texture[i]);
                spr[i].SetPosition(X[i], Y[i]);
            }

            for (int i = 1; i < ObjImg.Count; i++)
            {
                spr[0].AddChild(spr[i]);
            }

            return spr[0];
        }
    }

    #endregion

    #region Background Engine


    #endregion

    #region Foreground Engine

    /// <summary>
    /// Handles drawing the foreground for the login and maps
    /// </summary>
    public class CForegroundEngine : CTextureEngine
    {
        private List<System.Drawing.Bitmap> Layers = new List<System.Drawing.Bitmap>(8);
        //private CTextureEngine tex = new CTextureEngine();

        /// <summary>
        /// Locates the foreground
        /// </summary>
        /// <param name="map">WZ map file</param>
        public CForegroundEngine()
        {

        }

        /// <summary>
        /// Draws the login frame
        /// </summary>
        /// <returns></returns>
        public CCSprite DrawFrame()
        {
            WZFile UI = new WZFile(GameConstants.FileLocation + "UI.wz", GameConstants.Variant, true);
            var frLoc = UI.MainDirectory["Login.img"]["Common"]["frame"] as WZCanvasProperty; 

            CCTexture2D texFr = new CCTexture2D();
            texFr.InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, frLoc.Value));

            CCSprite spr = new CCSprite(texFr);
            spr.SetPosition(400, 300);

            return spr;
        }
    }

    #endregion

    #region Map Engine

    /// <summary>
    /// Puts the maps completely together using the Foreground Engine and the Background Engine
    /// </summary>
    public class CMapEngine : CTextureEngine
    {

        public string bS, oS, l0, l1, l2, l3;
        public int x, y, cx, cy, rx, ry, no;
        public bool ani;
        public Vector3 Pos;
        public GraphicsAdapter adapter;
        public GraphicsDevice device;

        public Texture2D texture;

        WZFile MapWz = null, UiWz = null;
        CCNode batch;

        public CMapEngine()
        {
            MapWz = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"\Map.wz", WZVariant.GMS, true);
            UiWz = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"\UI.wz", WZVariant.GMS, true);
            batch = new CCNode();
        }

        public CCRawList<CCNode> Draw(string Imagename)
        {
            try
            {
                if (Imagename == "MapLogin")
                {
                    foreach (WZSubProperty Node in UiWz.MainDirectory["MapLogin.img"])
                    {
                        if (Node.HasChild("obj"))
                        {

                        }

                        if (Node.Name == "back")
                        {
                            foreach (WZSubProperty BackNode in Node)
                            {
                                if (BackNode.HasChild("x"))
                                    x = BackNode["x"].ValueOrDie<int>();
                                if (BackNode.HasChild("y"))
                                    y = BackNode["y"].ValueOrDie<int>();
                                if (BackNode.HasChild("cx"))
                                    cx = BackNode["cx"].ValueOrDie<int>();
                                if (BackNode.HasChild("cy"))
                                    cy = BackNode["cy"].ValueOrDie<int>();
                                if (BackNode.HasChild("rx"))
                                    rx = BackNode["rx"].ValueOrDie<int>();
                                if (BackNode.HasChild("ry"))
                                    ry = BackNode["ry"].ValueOrDie<int>();
                                if (BackNode.HasChild("bS"))
                                    bS = BackNode["bS"].ValueOrDie<string>();
                                if (BackNode.HasChild("no"))
                                    no = BackNode["no"].ValueOrDie<int>();

                                batch.AddChild(DrawBackgrounds(bS, no.ToString(), x, y, cx, cy, rx, ry));
                                
                            }
                        }
                    }
                }
            }
            finally
            {
                write("Children Count: {0}", batch.ChildrenCount);
            }

            return batch.Children;
        }


        public CCSprite DrawBackgrounds(string bS, string no, int x, int y, int cx, int cy, int rx, int ry)
        {
            CCSprite spr = null;
            write("bS: {0} no: {1} X: {2} Y: {3} (Absolute Position)", bS, no, x, Math.Abs(y));

            var background = MapWz.MainDirectory["Back"][bS + ".img"]["back"][no] as WZCanvasProperty;
            texture = GetTexture(CCDrawManager.GraphicsDevice, background.Value);

            //SpriteBatch batch = new SpriteBatch(CCDrawManager.GraphicsDevice);

            //batch.Begin();

            //batch.Draw(texture, new Vector2(x, y), Color.White);

            //batch.End();

            CCTexture2D tex = new CCTexture2D();
            tex.InitWithTexture(texture);

            spr = new CCSprite(tex);

            if (no == "18")
            {
                spr.SetPosition(17, 900);
            }
            else
            {
                spr.SetPosition(x + 400, (y > 0 ? y : Math.Abs(y)) + 300);
            }

            return spr;
        }

        public CCSprite DrawFrame()
        {
            UiWz = new WZFile(GameConstants.FileLocation + "UI.wz", GameConstants.Variant, true);
            var frLoc = UiWz.MainDirectory["Login.img"]["Common"]["frame"] as WZCanvasProperty;

            CCTexture2D texFr = new CCTexture2D();
            texFr.InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, frLoc.Value));

            CCSprite spr = new CCSprite(texFr);
            spr.SetPosition(400, 300);

            return spr;
        }

        public void write(string msg, params object[] sender)
        {
            Console.WriteLine(msg, sender);
        }
    }

        #endregion

        #region Texture Engine

        /// <summary>
        /// Handles the creation of textures
        /// </summary>
        public class CTextureEngine
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
            X_.Add(x);
            Y_.Add(y);
        }

        public void AddTexture(System.Drawing.Bitmap img)
        {
            frames.Add(GetTexture(CCDrawManager.GraphicsDevice, img));
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
            }

            return byteArray;
        }

        /// <summary>
        /// Converts bitmap to Texture2D
        /// </summary>
        /// <param name="img"></param>
        /// <returns></returns>
        public Texture2D GetTexture(GraphicsDevice g, System.Drawing.Bitmap img)
        {
            int data = img.Height * img.Width * 4;
            MemoryStream stream = new MemoryStream(data);
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);

            var tex = Texture2D.FromStream(g, stream);

            return tex;
        }
    }

    #endregion
}
