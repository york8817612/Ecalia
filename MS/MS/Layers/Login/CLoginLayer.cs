using Cocos2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using MS.Common.Imaging;
using MS.Common;

namespace MS.Layers.Login
{
    public class CLoginLayer : CCSprite
    {
        private CCTexture2D[] texture = new CCTexture2D[1024];
        private CCSprite[] sprite = new CCSprite[1024];
        private CCAnimation animation = new CCAnimation();
        private CCAnimate animate;
        private WZPointProperty origin;
        CTextureEngine texEngine = new CTextureEngine();

        CCPoint mousepos = new CCPoint();

        private int FrameCount { get; set; }

        public CLoginLayer()
        {
            CCTexture2D tex = new CCTexture2D();

            using (WZFile ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @GameConstants.UI, GameConstants.Variant, true))
            {
                var LoginImg = ui.MainDirectory["Login.img"];

                #region Frame

                var bkgrn = (WZCanvasProperty)LoginImg["Common"]["frame"];
                origin = (WZPointProperty)bkgrn["origin"];

                texEngine.AddTexture(bkgrn.Value);
                texEngine.AddSize(bkgrn.Value.Width, bkgrn.Value.Height);
                
                texEngine.AddPos(origin.Value.X, origin.Value.Y);
                
                #endregion

                #region Testing Purposes

                /*

                var step = (WZCanvasProperty)LoginImg["GameGrade"]["GameGrade"];

                origin = (WZPointProperty)step["origin"];

                frames.Add(CT2B.GetTexture(step.Value));
                Width.Add(step.Value.Width);
                Height.Add(step.Value.Height);

                X.Add(origin.Value.X + 400);
                Y.Add(origin.Value.Y + 300);

                */

                #endregion

                #region LoginLocation

                var LoginLocation = (WZCanvasProperty)ui.MainDirectory["MapLogin.img"]["miniMap"]["canvas"];

                    //origin = (WZPointProperty)LoginLocation["origin"];
                    

                #endregion

                #region Common
                #endregion

                #region Notice
                #endregion

                #region Title
                #endregion
            }

            CCMenuItemImage BtLogin = new CCMenuItemImage();
            
            FrameCount = texEngine.Frame.Count;

            for (int t = 0; t < FrameCount; t++)
            {
                texture[t] = new CCTexture2D();
                texture[t].InitWithTexture(texEngine.Frame[t]);
            }

            for (int s = 0; s < FrameCount; s++)
            {
                sprite[s] = new CCSprite(texture[s]);
                sprite[s].SetPosition(texEngine.X[s], texEngine.Y[s]);
                sprite[s].SetTextureRect(new CCRect(0, 0, texEngine.Width[s], texEngine.Height[s]));
            }

            for (int c = 0; c < FrameCount; c++)
            {
                AddChild(sprite[c]);
            }
        }

        public override void Update(float dt)
        {
            
            Microsoft.Xna.Framework.Input.MouseState ms = Microsoft.Xna.Framework.Input.Mouse.GetState();
            mousepos.X = ms.X;
            mousepos.Y = ms.Y;
            base.Update(dt);
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
