using Cocos2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using MS.Common.Imaging;

namespace MS.Layers.Login
{
    public class CLoginLayer : CCSprite
    {
        private float x, y;
        private WZPointProperty origin;
        private WZCanvasProperty bk;

        public float PosX
        {
            get { return x; }
            set { x = value; }
        }

        public float PosY
        {
            get { return y; }
            set { y = value; }
        }

        public CLoginLayer()
        {
            CCTexture2D tex = new CCTexture2D();

            using (WZFile ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"/UI.wz", reWZ.WZVariant.GMS, true))
            {
                foreach (WZImage img in ui.MainDirectory)
                {
                    if (img.Name == "Login.img")
                    {
                        foreach (WZSubProperty d1 in img)
                        {
                            if (d1.Name == "WorldSelect")
                            {
                                bk = (WZCanvasProperty)d1["chBackgrn"];
                                tex.InitWithTexture(CT2B.GetTexture(bk.Value));

                                origin = (WZPointProperty)bk["origin"];
                                                                                                          
                            }
                        }
                    }
                }
            }

            CCSprite spr = new CCSprite(tex);
            spr.SetTextureRect(new CCRect(origin.Value.X, origin.Value.Y, 400, 300));
            spr.SetPosition(origin.Value.X, origin.Value.Y);


            AddChild(spr);
        }

        public static CCScene Scene
        {
            get
            {
                var scene = new CCScene();

                var layer = new CLoginLayer();

                scene.AddChild(layer);

                return scene;
            }
        }
    }
}
