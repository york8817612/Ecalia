using Cocos2D;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging
{
    /// <summary>
    /// Handles the Logos in the beginning of the client process
    /// </summary>
    public class CLogoEngine
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
        private CTextureEngine tex = new CTextureEngine();
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
                        texture.InitWithTexture(tex.GetTexture(nxLogo));

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
                            Textures[i].InitWithTexture(tex.GetTexture(nexonImage[i]));
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
                        Textures[i].InitWithTexture(tex.GetTexture(wizetImage[i]));
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

    /// <summary>
    /// Handles the drawing of backgrounds for the login and maps
    /// </summary>
    public class CBackgroundEngine
    {
        private List<System.Drawing.Bitmap> Layers = new List<System.Drawing.Bitmap>(8);
        private CTextureEngine tex = new CTextureEngine();

        /// <summary>
        /// Locates the background using the WZ file and the name of the map without the .img
        /// </summary>
        /// <param name="file">WZ file with background</param>
        /// <param name="name">Name of the background without .img</param>
        public CBackgroundEngine(WZFile file, string name)
        {
            var mT = file.MainDirectory["Back"][name + ".img"]["back"];

            if (name == "login")
            {
                Layers.Add(((WZCanvasProperty)mT["11"]).Value);
            }
            else
            {
                for (int i = 0; i < Layers.Capacity; i++)
                {
                    Layers.Add(((WZCanvasProperty)mT[i.ToString()]).Value);
                }
            }
        }

        /// <summary>
        /// Draws the background
        /// </summary>
        /// <returns></returns>
        public CCSprite Draw()
        {
            CCTexture2D[] texture = new CCTexture2D[Layers.Capacity];
            CCSprite[] sprite = new CCSprite[Layers.Capacity];

            for (int i = 0; i < Layers.Count; i++)
            {
                texture[i] = new CCTexture2D();
                texture[i].InitWithTexture(tex.GetTexture(Layers[i]));

                sprite[i] = new CCSprite(texture[i]);
                sprite[i].SetPosition(400, 300);
            }

            return sprite[0];
        }
    }

    /// <summary>
    /// Handles drawing the foreground for the login and maps
    /// </summary>
    public class CForegroundEngine
    {
        private List<System.Drawing.Bitmap> Layers = new List<System.Drawing.Bitmap>(8);
        private CTextureEngine tex = new CTextureEngine();

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
            texFr.InitWithTexture(tex.GetTexture(frLoc.Value));

            CCSprite spr = new CCSprite(texFr);
            spr.SetPosition(400, 300);

            return spr;
        }
    }

    /// <summary>
    /// Puts the maps completely together using the Foreground Engine and the Background Engine
    /// </summary>
    public class CMapEngine
    {

        public CMapEngine(WZFile mapFile)
        {

        }
    }

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
        private string bS;
        private string tS;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

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
}
