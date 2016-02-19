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
        private Stack<CCSprite> test = new Stack<CCSprite>();
        private CCTexture2D[] texture = new CCTexture2D[1024];
        private CCTexture2D[] backgrounds = new CCTexture2D[1024];
        private CCTexture2D[] foregrounds = new CCTexture2D[1024];
        private CCSprite[] sprite = new CCSprite[1024];
        private CCSprite[] background = new CCSprite[1024];
        private CCSprite[] foreground = new CCSprite[1024];
        private CCAnimation animation = new CCAnimation();
        private CCAnimate animate;
        CTextureEngine texEngine = new CTextureEngine();

        private int FrameCount { get; set; }

        public CLoginLayer()
        {

        }

        public override void Update(float dt)
        {
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

        public override void OnEnter()
        {
            var ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @GameConstants.UI, GameConstants.Variant, true);
            var map = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @GameConstants.MAP, GameConstants.Variant, true);

            using (ui)
            {
                var frame = (WZCanvasProperty)ui.MainDirectory["Login.img"]["Common"]["frame"];
                texEngine.AddTexture(frame.Value);
                texEngine.AddSize(frame.Value.Width, frame.Value.Height);
                texEngine.AddOrigin(frame);
                texEngine.AddPos(texEngine.Origin.Value.X, texEngine.Origin.Value.Y);

                for (int t = 0; t < texEngine.Frame.Count; t++)
                {
                    foregrounds[t] = new CCTexture2D();
                    foregrounds[t].InitWithTexture(texEngine.Frame[t]);
                }

                for (int s = 0; s < texEngine.Frame.Count; s++)
                {
                    foreground[s] = new CCSprite(foregrounds[s]);
                    foreground[s].SetPosition(texEngine.X[s], texEngine.Y[s]);
                }

                for (int c = 0; c < texEngine.Frame.Count; c++)
                {
                    AddChild(foreground[c], 0);
                }
            }

            using (map)
            {
                var lbk = (WZCanvasProperty)map.MainDirectory["Back"]["login.img"]["back"]["11"];
                texEngine.AddTexture(lbk.Value);
                texEngine.AddSize(lbk.Value.Width, lbk.Value.Height);
                texEngine.AddOrigin(lbk);
                texEngine.AddPos(texEngine.Origin.Value.X, texEngine.Origin.Value.Y);

                for (int t = 0; t < texEngine.Frame.Count; t++)
                {
                    backgrounds[t] = new CCTexture2D();
                    backgrounds[t].InitWithTexture(texEngine.Frame[t]);
                }

                for (int s = 0; s < texEngine.Frame.Count; s++)
                {
                    background[s] = new CCSprite(backgrounds[s]);
                    background[s].SetPosition(texEngine.X[s], texEngine.Y[s]);
                }

                for (int c = 0; c < texEngine.Frame.Count; c++)
                {
                    AddChild(background[c], (int)DisplayOrder.BACKGROUND);
                }
            }

            base.OnEnter();
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            base.OnExit();
        }
    }
}
