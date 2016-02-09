using System;
using reWZ;
using reWZ.WZProperties;
using Cocos2D;
using System.Threading;
using System.Collections.Generic;
using MS.Common.Imaging;

namespace MS.Layers.Login
{
    /// <summary>
    /// Layer that handles Wizet Logo
    /// </summary>
    public class CWizetLayer : CCLayer, IDisposable
    {
        List<Microsoft.Xna.Framework.Graphics.Texture2D> frames = new List<Microsoft.Xna.Framework.Graphics.Texture2D>();
        CCTexture2D texture = new CCTexture2D();
        CCTexture2D[] textures = new CCTexture2D[1024];
        CCSprite[] sprite = new CCSprite[1024];

        private WZPointProperty origin;

        private float X { get; set; }
        private float Y { get; set; }

        private List<int> Width = new List<int>();
        private List<int> Height = new List<int>();

        public CWizetLayer()
        {

        }

        private void MoveToLogin()
        {
            //ScheduleOnce(TransitionToLogin, 2);
        }

        private void TransitionToLogin(float obj)
        {
            CCTransitionScene transition = new CCTransitionFade(1, CLoginLayer.Scene);

            CCDirector.SharedDirector.ReplaceScene(transition);
        }

        public virtual void Dispose(bool b)
        {
            if (b)
            {
                GC.SuppressFinalize(this);
                Dispose();
            } else
            {
                Dispose();
            }
        }

        public void Dispose()
        {
            ((IDisposable)texture).Dispose();
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

        public override void OnExit()
        {
            base.OnExit();
            var login = new Thread(new ThreadStart(MoveToLogin));
            login.Start();
        }

        public override void OnEnter()
        {
            base.OnEnter();
            using (WZFile ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @"/UI.wz", reWZ.WZVariant.GMS, true))
            {
                foreach (WZImage main in ui.MainDirectory)
                {
                    if (main.Name == "Logo.img")
                    {
                        foreach (WZSubProperty sub in main)
                        {
                            if (sub.Name == "Wizet")
                            {
                                foreach (WZCanvasProperty canvas in sub)
                                {

                                    frames.Add(CT2B.GetTexture(canvas.Value));
                                    origin = (WZPointProperty)canvas["origin"];

                                    Width.Add(canvas.Value.Width);
                                    Height.Add(canvas.Value.Height);

                                    if (frames.Count == sub.ChildCount)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
                var frameCount = 0;

                for (int i = 0; i < frames.Count; i++)
                {
                    textures[i] = new CCTexture2D();
                    textures[i].InitWithTexture(frames[i]);
                    frameCount = i;
                }

                //textures[0].InitWithTexture(frames[0]);
                X = origin.Value.X;
                Y = origin.Value.Y;

                Console.WriteLine("X: {0} Y: {1}", X * (float)2.91 / 2, Y * (float)2.86 / 2);
                Console.WriteLine("Width: {0} Height: {1}", Width[0], Height[0]);

                CCAnimation anim = new CCAnimation();

                for (int ii = 0; ii < frames.Count; ii++)
                {
                    sprite[ii] = new CCSprite(textures[ii]);
                    sprite[ii].SetPosition(X * (float)2.91 / 2, Y * (float)2.86 / 2);
                    sprite[ii].SetTextureRect(new CCRect(0, 0, 800, 600)); // using this for now since 550/420 is too small for a windowed screen
                    anim.AddSprite(sprite[ii]);
                }

                //anim.AddSprite(sprite[0]);
                //anim.AddSprite(sprite[1]);
                //anim.AddSprite(sprite[2]);
                //anim.AddSprite(sprite[50]);
                anim.Loops = Convert.ToUInt32(1);
                anim.DelayPerUnit = 0.2f;

                CCAnimate ate = new CCAnimate(anim);
                ate.Duration = 1.8f;
                //ate
                
                sprite[0].RunAction(new CCAnimate(anim));
                AddChild(sprite[0]);
                //sprite[0] = new CCSprite(textures[0]);
                //sprite[0].SetPosition(X * (float)2.91 / 2, Y * (float)2.86 / 2);
                //sprite[0].SetTextureRect(new CCRect(0, 0, 800, 600)); // using this for now since 550/420 is too small for a windowed screen

            }
        }

        public override void Update(float dt)
        {
            base.Update(dt);

            int current = 0;
            float elapse = dt;
         }
    }
}
