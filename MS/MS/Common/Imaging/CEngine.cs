using Cocos2D;
using reWZ;
using reWZ.WZProperties;
using System;
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
                        texture.InitWithTexture(GetTexture(nxLogo));

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
                            Textures[i].InitWithTexture(GetTexture(nexonImage[i]));
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
                        Textures[i].InitWithTexture(GetTexture(wizetImage[i]));
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
        private WZFile map = new WZFile(GameConstants.FileLocation + GameConstants.MAP, GameConstants.Variant, true);

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
                texture[i].InitWithTexture(GetTexture(ObjImg[i]));

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

    /// <summary>
    /// Handles the drawing of backgrounds for the login and maps
    /// </summary>
    public class CBackgroundEngine : CTextureEngine
    {
        private List<System.Drawing.Bitmap> Layers = new List<System.Drawing.Bitmap>(8);
        //private CTextureEngine tex = new CTextureEngine();

        /// <summary>
        /// Locates the background using the WZ file and the name of the map without the .img
        /// </summary>
        /// <param name="file">WZ file with background</param>
        /// <param name="name">Name of the background without .img</param>
        public CBackgroundEngine(WZImage file)
        {
            WZSubProperty mT = (WZSubProperty)file["back"];
            foreach (WZCanvasProperty prop in mT)
            {
                Layers.Add(prop.Value);

                for (int i = 0; i < mT.ChildCount; i++)
                {
                    AddPos(((WZPointProperty)prop["origin"]).Value.X, ((WZPointProperty)prop["origin"]).Value.Y);
                }
            }

        }

        /// <summary>
        /// Draws the background
        /// </summary>
        /// <returns></returns>
        public CCSprite Draw()
        {
            CCTexture2D[] texture = new CCTexture2D[Layers.Count];
            CCSprite[] sprite = new CCSprite[Layers.Count];
            Console.WriteLine(Layers.Count);
            for (int i = 0; i < Layers.Count; i++)
            {
                texture[i] = new CCTexture2D();
                texture[i].InitWithTexture(GetTexture(Layers[i]));
            }

            for (int i = 0; i < Layers.Count; i++)
            {
                sprite[i] = new CCSprite(texture[i]);
                sprite[i].SetPosition(X[i], Y[i]);
            }

            for (int i = 1; i < Layers.Count; i++)
            {
                sprite[0].AddChild(sprite[i], i);
            }

            return sprite[0];
        }
    }

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
        public CForegroundEngine(WZFile map)
        {

        }

        /// <summary>
        /// Draws the login frame
        /// </summary>
        /// <returns></returns>
        public CCSprite DrawFrame()
        {
            WZFile ui = new WZFile(GameConstants.FileLocation + GameConstants.UI, GameConstants.Variant, true);
            var frLoc = (WZCanvasProperty)ui.MainDirectory["Login.img"]["Common"]["frame"];

            CCTexture2D texFr = new CCTexture2D();
            texFr.InitWithTexture(GetTexture(frLoc.Value));

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
    public class CMapEngine
    {
        #region Variables

        private int id;
        private int forceReturn;
        private int returnMap;
        private bool town;
        private bool hasClock;
        private byte fieldType;
        private bool fly;
        private bool swim;
        private int mobRate;
        private string bS, // background set
        tS; // tile set
        private int a, // Alpha?
         ani,
         cx, // CenterX
         cy, // CenterY
         f, // 
         front, // 
         no, // 
         rx, // 
         ry, // 
         type, // 
         x, // Position X
            y; // Position Y
        private string l0, // First Sub
            l1, // Second Sub
            l2, // Third Sub
            oS; // Obj Img Name without the .img

        #endregion

        #region Get/Set

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        public int Ani
        {
            get { return ani; }
            set { ani = value; }
        }

        /// <summary>
        /// CenterX
        /// </summary>
        public int CX
        {
            get { return cx; }
            set { cx = value; }
        }

        /// <summary>
        /// CenterY
        /// </summary>
        public int CY
        {
            get { return cy; }
            set { cy = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int F
        {
            get { return f; }
            set { f = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Front
        {
            get { return front; }
            set { front = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NO
        {
            get { return no; }
            set { no = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RX
        {
            get { return rx; }
            set { rx = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RY
        {
            get { return ry; }
            set { ry = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Position X
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Position Y
        /// </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// First Sub
        /// </summary>
        public string L0
        {
            get { return l0; }
            set { l0 = value; }
        }

        /// <summary>
        /// Second Sub
        /// </summary>
        public string L1
        {
            get { return l1; }
            set { l1 = value; }
        }

        /// <summary>
        /// Third sub (the WZCanvas)
        /// </summary>
        public string L2
        {
            get { return l2; }
            set { l2 = value; }
        }

        /// <summary>
        /// Obj Image name without .img
        /// </summary>
        public string OS
        {
            get { return oS; }
            set { oS = value; }
        }

        /// <summary>
        /// ID of map
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Force Return of map
        /// </summary>
        public int ForceReturn
        {
            get { return forceReturn; }
            set { forceReturn = value; }
        }

        public int ReturnMap
        {
            get { return returnMap; }
            set { returnMap = value; }
        }

        public bool Town
        {
            get { return town; }
            set { town = value; }
        }

        public bool HasClock
        {
            get { return hasClock; }
            set { hasClock = value; }
        }

        public byte FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        public bool Fly
        {
            get { return fly; }
            set { fly = value; }
        }

        public bool Swim
        {
            get { return swim; }
            set { swim = value; }
        }

        public int MobRate
        {
            get { return mobRate; }
            set { mobRate = value; }
        }

        public string BS
        {
            get { return bS; }
            set { bS = value; }
        }

        #endregion

        public CMapEngine(WZFile mapFile, bool login)
        {
            if (login)
            {
                foreach (WZImage WzImg in mapFile.MainDirectory)
                {
                    if (WzImg.Name == "MapLogin.img")
                    {
                        foreach (WZSubProperty WzSub in WzImg)
                        {
                            //Console.WriteLine(WzSub.Name);
                            foreach (var objs in WzSub)
                            {
                                foreach (var obj in objs)
                                {
                                    //Console.WriteLine(obj.Name);
                                    if (obj.HasChild("f"))
                                        f = obj["f"].ValueOrDie<int>();
                                    if (obj.HasChild("l0"))
                                        l0 = obj["l0"].ValueOrDie<string>();
                                    if (obj.HasChild("l1"))
                                        l1 = obj["l1"].ValueOrDie<string>();
                                    if (obj.HasChild("l2"))
                                        l2 = obj["l2"].ValueOrDie<string>();
                                    if (obj.HasChild("oS"))
                                        oS = obj["oS"].ValueOrDie<string>();
                                    if (obj.HasChild("x"))
                                        x = obj["x"].ValueOrDie<int>();
                                    if (obj.HasChild("y"))
                                        y = obj["y"].ValueOrDie<int>();
                                    if (obj.HasChild("cx"))
                                        cx = obj["cx"].ValueOrDie<int>();
                                    if (obj.HasChild("cy"))
                                        cy = obj["cy"].ValueOrDie<int>();
                                    if (obj.HasChild("bS"))
                                        bS = obj["bS"].ValueOrDie<string>();
                                    if (obj.HasChild("a"))
                                        a = obj["a"].ValueOrDie<int>();
                                    if (obj.HasChild("ani"))
                                        ani = obj["ani"].ValueOrDie<int>();
                                    if (obj.HasChild("front"))
                                        front = obj["front"].ValueOrDie<int>();
                                    if (obj.HasChild("rx"))
                                        rx = obj["rx"].ValueOrDie<int>();
                                    if (obj.HasChild("ry"))
                                        ry = obj["ry"].ValueOrDie<int>();
                                    if (obj.HasChild("no"))
                                        no = obj["no"].ValueOrDie<int>();
                                    if (obj.HasChild("type"))
                                        type = obj["type"].ValueOrDie<int>();
                                    /*if (obj.HasChild("zM"))
                                        Console.WriteLine("Has child");*/
                                }
                            }
                        }

                    }
                }
            }
            else
            {

            }
        }

        public void test()
        {
            Console.WriteLine(l0);
        }

        public CCSprite DrawObj()
        {

            CObjectEngine objEngine = new CObjectEngine(OS, L0, L1, L2);

            return objEngine.Draw();
        }

        public CCSprite DrawBackground()
        {
            CBackgroundEngine bk = new CBackgroundEngine(null);

            return bk.Draw();
        }

        public CCSprite DrawForeground()
        {
            return null;
        }

        public CCSprite DrawAll()
        {
            CCSprite spr = new CCSprite();

            spr.AddChild(DrawObj());

            return spr;
        }

        private void Write(string msg, params object[] obj)
        {
            Console.WriteLine(msg, obj);
        }
    }

    #region MapStruct

    /// <summary>
    /// Contains all the information of the maps
    /// </summary>
    public struct CMapStruct
    {

        private int id;
        private int forceReturn;
        private int returnMap;
        private bool town;
        private bool hasClock;
        private byte fieldType;
        private bool fly;
        private bool swim;
        private int mobRate;
        private string onFirstUserEnter;
        private string onUserEnter;
        private int weatherId;
        private string weatherMessage;
        private bool weatherAdmin;
        private string bS, // background set
        tS; // tile set
        private int a, // Alpha?
         ani,
         cx, // CenterX
         cy, // CenterY
         f, // 
         front, // 
         no, // 
         rx, // 
         ry, // 
         type, // 
         x, // Position X
            y; // Position Y
        private string l0, // First Sub
            l1, // Second Sub
            l2, // Third Sub
            oS; // Obj Img Name without the .img

        public int A
        {
            get { return a; }
            set { a = value; }
        }

        public int Ani
        {
            get { return ani; }
            set { ani = value; }
        }

        /// <summary>
        /// CenterX
        /// </summary>
        public int CX
        {
            get { return cx; }
            set { cx = value; }
        }

        /// <summary>
        /// CenterY
        /// </summary>
        public int CY
        {
            get { return cy; }
            set { cy = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int F
        {
            get { return f; }
            set { f = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Front
        {
            get { return front; }
            set { front = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int NO
        {
            get { return no; }
            set { no = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RX
        {
            get { return rx; }
            set { rx = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int RY
        {
            get { return ry; }
            set { ry = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// Position X
        /// </summary>
        public int X
        {
            get { return x; }
            set { x = value; }
        }

        /// <summary>
        /// Position Y
        /// </summary>
        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        /// <summary>
        /// First Sub
        /// </summary>
        public string L0
        {
            get { return l0; }
            set { l0 = value; }
        }

        /// <summary>
        /// Second Sub
        /// </summary>
        public string L1
        {
            get { return l1; }
            set { l1 = value; }
        }

        /// <summary>
        /// Third sub (the WZCanvas)
        /// </summary>
        public string L2
        {
            get { return l2; }
            set { l2 = value; }
        }

        /// <summary>
        /// Obj Image name without .img
        /// </summary>
        public string OS
        {
            get { return oS; }
            set { oS = value; }
        }

        /// <summary>
        /// ID of map
        /// </summary>
        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Force Return of map
        /// </summary>
        public int ForceReturn
        {
            get { return forceReturn; }
            set { forceReturn = value; }
        }

        public int ReturnMap
        {
            get { return returnMap; }
            set { returnMap = value; }
        }

        public bool Town
        {
            get { return town; }
            set { town = value; }
        }

        public bool HasClock
        {
            get { return hasClock; }
            set { hasClock = value; }
        }

        public byte FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        public bool Fly
        {
            get { return fly; }
            set { fly = value; }
        }

        public bool Swim
        {
            get { return swim; }
            set { swim = value; }
        }

        public int MobRate
        {
            get { return mobRate; }
            set { mobRate = value; }
        }

        public string OnFirstUserEnter
        {
            get { return onFirstUserEnter; }
            set { onFirstUserEnter = value; }
        }

        public string OnUserEnter
        {
            get { return onUserEnter; }
            set { onUserEnter = value; }
        }

        public int WeatherID
        {
            get { return weatherId; }
            set { weatherId = value; }
        }

        public string WeatherMessage
        {
            get { return weatherMessage; }
            set { weatherMessage = value; }
        }

        public bool WeatherAdmin
        {
            get { return weatherAdmin; }
            set { weatherAdmin = value; }
        }

        public string BS
        {
            get { return bS; }
            set { bS = value; }
        }

        public CMapStruct(int mapid) : this()
        {
            id = mapid;
        }
    }

    #endregion

    #endregion

    #region Texture Engine

    /// <summary>
    /// Handles the creation of textures
    /// </summary>
    public abstract class CTextureEngine
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

    #endregion
}
