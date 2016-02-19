using System;
using reWZ;
using reWZ.WZProperties;
using Cocos2D;
using System.Threading;
using System.Collections.Generic;
using MS.Common.Imaging;
using MS.Common;

namespace MS.Layers.Login
{
    /// <summary>
    /// Layer that handles Wizet Logo
    /// </summary>
    public class CWizetLayer : CCLayer
    {
        private WZPointProperty origin;
        private CCTexture2D[] texture = new CCTexture2D[1024];
        private CCSprite[] sprite = new CCSprite[1024];
        private CCAnimation animation = new CCAnimation();
        private CCAnimate animate;

        private int FrameCount { get; set; }

        CTextureEngine texEngine = new CTextureEngine();


        public CWizetLayer()
        {

        }

        private void MoveToLogin()
        {
            ScheduleOnce(TransitionToLogin, 5);
        }

        private void TransitionToLogin(float obj)
        {
            CCTransitionScene transition = new CCTransitionFade(1, CLoginLayer.Scene);

            CCDirector.SharedDirector.ReplaceScene(transition);
        }

        public static CCScene Scene
        {
            get
            {
                var scene = new CCScene();

                var layer = new CWizetLayer();

                scene.AddChild(layer);

                return scene;
            }
        }

        public override void OnEnter()
        {
            base.OnEnter();
            using (WZFile ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @GameConstants.UI, GameConstants.Variant, true))
            {
                var wizet = ui.MainDirectory["Logo.img"]["Wizet"];

                foreach (WZCanvasProperty canvas in wizet)
                {
                    texEngine.AddTexture(canvas.Value);
                    texEngine.AddSize(canvas.Value.Width, canvas.Value.Height);
                    texEngine.AddOrigin(canvas);
                    texEngine.AddPos(texEngine.Origin.Value.X, texEngine.Origin.Value.Y);
                }
            }
            FrameCount = texEngine.Frame.Count;

            for (int t = 0; t < FrameCount; t++)
            {
                texture[t] = new CCTexture2D();
                texture[t].InitWithTexture(texEngine.Frame[t]);

                if (t == FrameCount)
                    break;
            }

            for (int s = 0; s < FrameCount; s++)
            {
                sprite[s] = new CCSprite(texture[s]);
                sprite[s].SetPosition(texEngine.X[s] * (float)2.91 / 2, texEngine.Y[s] * (float)2.86 / 2);
                sprite[s].SetTextureRect(new CCRect(0, 0, 800, 600));

                if (s == FrameCount)
                    break;
            }

            for (int a = 0; a < FrameCount; a++)
            {
                animation.AddSprite(sprite[a]);

                if (a == FrameCount)
                    break;
            }

            animation.Loops = 1;
            animation.DelayPerUnit = .1f;

            animate = new CCAnimate(animation);
            animate.Duration = 5.0f;

            sprite[0].RunAction(animate);
            AddChild(sprite[0]);

            var login = new Thread(new ThreadStart(MoveToLogin));
            login.Start();
            login.Join();
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            base.OnExit();
        }
    }
}
