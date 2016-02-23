using Cocos2D;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WZ;

namespace MS.Common.Imaging.Map
{
    public class CMapEngine : CTextureEngine
    {
        private List<Microsoft.Xna.Framework.Graphics.Texture2D> BackgroundFrames = new List<Microsoft.Xna.Framework.Graphics.Texture2D>();
        private List<Microsoft.Xna.Framework.Graphics.Texture2D> ForegroundFrames = new List<Microsoft.Xna.Framework.Graphics.Texture2D>();
        private List<CCTexture2D> Backgrounds = new List<CCTexture2D>();
        private List<CCTexture2D> Foregrounds = new List<CCTexture2D>();
        private List<CCSprite> Background = new List<CCSprite>();
        private List<CCSprite> Foreground = new List<CCSprite>();
        private int CenterX, CenterY, Width, Height, VRLeft, VRTop, VRRight, VRBottom;
        public static CMapEngine instance;
        private double fly;
        private WZImage map;

        public CMapEngine(WZImage map)
        {
            this.map = map;
            WZImage Info = map["info"] as WZImage;
            WZImage MiniMap = map["miniMap"] as WZImage;
            if (Info["VRTop"] != null)
            {
                VRLeft = Info["VRLeft"].ValueOrDie<int>();
                VRTop = Info["VRTop"].ValueOrDie<int>();
                VRRight = Info["VRRight"].ValueOrDie<int>();
                VRBottom = Info["VRBottom"].ValueOrDie<int>();
            }
            if (MiniMap == null)
            {
                if (VRTop == 0) throw new Exception("Unhandled Map");
                CenterX = -VRLeft + 50;
                CenterY = -VRTop + 50;
                Width = VRRight + CenterX + 100;
                Height = VRBottom + CenterY + 100;
            }
            else
            {
                CenterX = MiniMap["centerX"].ValueOrDie<int>();
                CenterY = MiniMap["centerY"].ValueOrDie<int>();
                Width = MiniMap["width"].ValueOrDie<int>();
                Height = MiniMap["height"].ValueOrDie<int>();
                if (VRTop == 0)
                {
                    VRLeft = -CenterX + 69;
                    VRTop = -CenterY + 86;
                    VRRight = Width - CenterX - 69;
                    VRBottom = Height - CenterY - 86;
                }
            }

            fly = Info["swim"].ValueOrDie<bool>() ? -1 : 1;

            int maxX = Int32.MinValue;
            int maxY = Int32.MinValue;
            int minX = Int32.MaxValue;
            int minY = Int32.MaxValue;
        }
    }
}
