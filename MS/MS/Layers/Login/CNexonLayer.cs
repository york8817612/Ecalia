using System;
using Cocos2D;
using Microsoft.Xna.Framework;
using reWZ.WZProperties;
using System.IO;
using System.Threading;
using MS.Common.Imaging;

namespace MS.Layers.Login
{
    /// <summary>
    /// Layer that handles Nexon Logo
    /// </summary>
    public class CNexonLayer : CCLayer
    {
        private WZPointProperty origin;


        public float X { get; set; }
        public float Y { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }

        public CNexonLayer()
        {
            using (reWZ.WZFile ui = new reWZ.WZFile(AppDomain.CurrentDomain.BaseDirectory + @"/UI.wz", reWZ.WZVariant.GMS, true))
            {
                CCTexture2D texture = new CCTexture2D();

                foreach (WZImage main in ui.MainDirectory)
                {
                    if (main.Name == "Logo.img")
                    {
                        foreach (WZSubProperty sub in main)
                        {
                            if (sub.Name == "Nexon")
                            {
                                foreach (WZCanvasProperty canvas in sub)
                                {
                                    texture.InitWithTexture(CT2B.GetTexture(canvas.Value));
                                    origin = (WZPointProperty)canvas["origin"];

                                    Width = canvas.Value.Width;
                                    Height = canvas.Value.Height;

                                    var wizet = new Thread(new ThreadStart(MoveToWizet));
                                    wizet.Start();
                                }
                            }
                        }
                    }
                }

                X = origin.Value.X;
                Y = origin.Value.Y;

                Console.WriteLine("X: {0} Y: {1}", X, Y);
                Console.WriteLine("Width: {0} Height: {1}", Width, Height);

                CCSprite spr = new CCSprite(texture);
                spr.SetPosition(X, Y);
                AddChild(spr);
            }
        }

        private void MoveToWizet()
        {
            ScheduleOnce(TransitionOut, 2);
        }

        void TransitionOut(float delta)
        {

            CCLog.Log("Make Transition to Wizet Logo");

            CCTransitionScene transition = Fade;

            CCDirector.SharedDirector.ReplaceScene(transition);
        }

        #region Transitions

        CCTransitionScene FadeDown
        {
            get { return new CCTransitionFadeDown(1, CWizetLayer.Scene); }
        }

        CCTransitionScene FilpX
        {
            get { return new CCTransitionFlipX(1, CWizetLayer.Scene, CCTransitionOrientation.RightOver); }
        }

        CCTransitionScene Fade
        {
            get { return new CCTransitionFade(1, CWizetLayer.Scene, new CCColor3B(Microsoft.Xna.Framework.Color.White)); }
        }

        CCTransitionScene FlipAngular
        {
            get { return new CCTransitionFlipAngular(1, CWizetLayer.Scene, CCTransitionOrientation.DownOver); }
        }

        CCTransitionScene FadeTR
        {
            get { return new CCTransitionFadeTR(1, CWizetLayer.Scene); }
        }

        CCTransitionScene PageTurn
        {
            get { return new CCTransitionPageTurn(1, CWizetLayer.Scene, false); }
        }

        CCTransitionScene TurnOffTiles
        {
            get { return new CCTransitionTurnOffTiles(1, CWizetLayer.Scene); }
        }

        #endregion

        public static CCScene Scene
        {
            get
            {
                // 'scene' is an autorelease object.
                var scene = new CCScene();

                // 'layer' is an autorelease object.
                var layer = new CNexonLayer();

                // add layer as a child to scene
                scene.AddChild(layer);

                // return the scene
                return scene;

            }
        }
    }
}

