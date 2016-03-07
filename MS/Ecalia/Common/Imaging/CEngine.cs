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

namespace Ecalia.Common.Imaging
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

    #region Map Engine

    /// <summary>
    /// Puts the maps completely together using the Foreground Engine and the Background Engine
    /// </summary>
    public class CMapEngine : CTextureEngine
    {

        public string bS, oS, l0, l1, l2, l3;
        public int x, y, z, cx, cy, rx, ry, no;
        public bool ani;
        public Vector3 Pos;
        public GraphicsAdapter adapter;
        public GraphicsDevice device;

        public Texture2D texture;

        public WZFile MapWz = null, UiWz = null;
        private CCNode batch;
        private CCParallaxNode layers;

        public CMapEngine()
        {
            MapWz = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"\Map.wz", GameConstants.Variant, true);
            UiWz = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"\UI.wz", GameConstants.Variant, true);
            batch = new CCNode();
        }

        public CCNode Draw(string Imagename)
        {
            try
            {
                if (Imagename == "MapLogin")
                {
                    foreach (WZSubProperty Node in UiWz.MainDirectory["MapLogin.img"])
                    {
                        if (Node.HasChild("obj"))
                        {
                            foreach (var obj in Node)
                            {
                                if (obj.Name == "obj")
                                {
                                    foreach (var objs in obj)
                                    {
                                        //Console.WriteLine(obj.Name);
                                        //if (obj.HasChild("f"))
                                        //f = obj["f"].ValueOrDie<int>();
                                        if (objs.HasChild("l0"))
                                            l0 = objs["l0"].ValueOrDie<string>();
                                        if (objs.HasChild("l1"))
                                            l1 = objs["l1"].ValueOrDie<string>();
                                        if (objs.HasChild("l2"))
                                            l2 = objs["l2"].ValueOrDie<string>();
                                        if (objs.HasChild("oS"))
                                            oS = objs["oS"].ValueOrDie<string>();
                                        if (objs.HasChild("x"))
                                            x = objs["x"].ValueOrDie<int>();
                                        if (objs.HasChild("y"))
                                            y = objs["y"].ValueOrDie<int>();
                                        if (objs.HasChild("z"))
                                            z = objs["z"].ValueOrDie<int>(); 

                                        batch.AddChild(DrawMapObjects(oS, l0, l1, l2, x, y, z));
                                    }
                                }
                            }
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

                                batch.AddChild(DrawBackgrounds(bS, no.ToString(), x, y, z, cx, cy, rx, ry));
                                
                            }
                        }
                    }

                    batch.AddChild(DrawFrame());
                }
                else
                {
                    foreach (WZSubProperty Mapimg in MapWz.MainDirectory["Map"]["Map" + Imagename[0]][Imagename + ".img"])
                    {
                        Console.WriteLine(Mapimg.Name);

                        if (Mapimg.Name == "back")
                        {
                            foreach (WZSubProperty BackNode in Mapimg)
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
                                if (BackNode.HasChild("z"))
                                    z = BackNode["z"].ValueOrDie<int>();

                                batch.AddChild(DrawBackgrounds(bS, no.ToString(), x, y, z, cx, cy, rx, ry));

                            }
                        }
                    }
                }
            }
            finally
            {
            }

            return batch;
        }

        private CCSprite DrawMapObjects(string oS, string l0, string l1, string l2, int x, int y, int z)
        {
            Console.WriteLine("oS: {0} l0: {1} l1: {2} l2: {3} X: {4} Y: {5} Z: {6}", oS, l0, l1, l2, x, y, z);
            var MapObj = MapWz.MainDirectory["Obj"][oS + ".img"][l0][l1][l2] as WZSubProperty;
            CCTexture2D[] tex = new CCTexture2D[5];
            CCSprite[] spr = new CCSprite[5];

            CCAnimation animation = new CCAnimation();
            CCAnimate animate = null;

            foreach (var Objs in MapObj)
            {
                if (Objs is WZCanvasProperty)
                {
                    tex[0] = new CCTexture2D();
                    tex[0].InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, ((WZCanvasProperty)Objs).Value));
                    spr[0] = new CCSprite(tex[0]);

                    if (oS == "login" && l0 == "Title" && l1 == "effect")
                    {
                        spr[0].SetPosition(x, Math.Abs(y));
                    }
                    else
                    {
                        spr[0].SetPosition(x + 400, Math.Abs(y) + 300);
                    }
                    if (l1 == "pirate")
                    {
                        spr[0].ZOrder = 0;
                    }
                    else
                    {
                        spr[0].ZOrder = z;
                    }
                }
            }

            return spr[0];
        }

        private CCSprite DrawBackgrounds(string bS, string no, int x, int y, int z, int cx, int cy, int rx, int ry)
        {
            CCSprite spr = null;
            //write("bS: {0} no: {1} X: {2} Y: {3} (Absolute Position)", bS, no, x, Math.Abs(y));

            var background = MapWz.MainDirectory["Back"][bS + ".img"]["back"][no] as WZCanvasProperty;
            texture = GetTexture(CCDrawManager.GraphicsDevice, background.Value);

            CCTexture2D tex = new CCTexture2D();
            tex.InitWithTexture(texture);

            spr = new CCSprite(tex);

            if (bS == "login" && no == "18")
            {
                spr.SetPosition(273, 2320); // this took a bit to find....
            }
            else if (bS == "login" && no == "0" || no == "1" || no == "2")
            {
                spr.SetTextureRect(new CCRect(0, 0, 800, 600));
            }
            else
            {
                //spr.SetPosition(x + 400, (y > 0 ? y : Math.Abs(y)) + 300);
                spr.SetPosition(x + 400, Math.Abs(y) + 300);
            }

            spr.ZOrder = z;

            return spr;
        }

        public CCSprite DrawFrame()
        {
            UiWz = new WZFile(GameConstants.FileLocation + "UI.wz", GameConstants.Variant, true);
            var frLoc = UiWz.MainDirectory["Login.img"]["Common"]["frame"] as WZCanvasProperty;

            //var frLoc = MapWz.MainDirectory["Obj"]["login.img"]["Common"]["frame"]["0"]["0"] as WZCanvasProperty;

            CCTexture2D texFr = new CCTexture2D();
            texFr.InitWithTexture(GetTexture(CCDrawManager.GraphicsDevice, frLoc.Value));

            if (frLoc.HasChild("x"))
                x = frLoc["x"].ValueOrDefault(0);
            if (frLoc.HasChild("y"))
                y = frLoc["y"].ValueOrDefault(0);
            if (frLoc.HasChild("z"))
                z = ((WZInt32Property)frLoc["z"]).Value;

            if (x == 0)
                x = frLoc.Value.Width / 2;
            if (y == 0)
                y = frLoc.Value.Height / 2;

            CCSprite spr = new CCSprite(texFr);
            spr.SetPosition(Math.Abs(x) + 245, Math.Abs(y));

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
