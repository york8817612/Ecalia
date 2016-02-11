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

        private List<Microsoft.Xna.Framework.Graphics.Texture2D> frames = new List<Microsoft.Xna.Framework.Graphics.Texture2D>();
        private List<int> Width = new List<int>();
        private List<int> Height = new List<int>();
        private List<float> X = new List<float>();
        private List<float> Y = new List<float>();
        private int FrameCount { get; set; }

        public CWizetLayer()
        {

        }

        private void MoveToLogin()
        {
            //ScheduleOnce(TransitionToLogin, 10);
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
            using (WZFile ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"/UI.wz", GameConstants.Variant, true))
            {
                var wizet = ui.MainDirectory["Logo.img"]["Wizet"];

                foreach (WZCanvasProperty canvas in wizet)
                {
                    frames.Add(CT2B.GetTexture(canvas.Value));

                    origin = (WZPointProperty)canvas["origin"];

                    Width.Add(canvas.Value.Width);
                    Height.Add(canvas.Value.Height);

                    X.Add(origin.Value.X);
                    Y.Add(origin.Value.Y);
                }
            }
            FrameCount = frames.Count;

            for (int t = 0; t < FrameCount; t++)
            {
                texture[t] = new CCTexture2D();
                texture[t].InitWithTexture(frames[t]);

                if (t == FrameCount)
                    break;
            }

            for (int s = 0; s < FrameCount; s++)
            {
                sprite[s] = new CCSprite(texture[s]);
                sprite[s].SetPosition(X[s] * (float)2.91 / 2, Y[s] * (float)2.86 / 2);
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
            animate.Duration = 7.0f;

            sprite[0].RunAction(animate);
            AddChild(sprite[0]);

            var login = new Thread(new ThreadStart(MoveToLogin));
            login.Start();

        }

        public override void OnExit()
        {
            base.OnExit();
        }

        /* var login = new Thread(new ThreadStart(MoveToLogin));
                login.Start();*/
    }
}
