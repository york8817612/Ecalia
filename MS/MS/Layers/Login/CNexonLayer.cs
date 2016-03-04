using System;
using Cocos2D;
using Microsoft.Xna.Framework;

using System.IO;
using System.Threading;
using MS.Common.Imaging;
using MS.Common.Net;
using MS.Common;
using System.Collections.Generic;
using reWZ;

namespace MS.Layers.Login
{
    /// <summary>
    /// Layer that handles Nexon Logo
    /// </summary>
    public class CNexonLayer : CCLayer
    {
        private CLogoEngine logoEngine;
        WZFile UI;

        public CNexonLayer()
        {
            try
            {
                UI = new WZFile(GameConstants.FileLocation + "UI.wz", GameConstants.Variant, true);

                object uilock = new object();
                lock (uilock)
                    logoEngine = new CLogoEngine(UI);

                AddChild(logoEngine.Draw(true));

                var next = new Thread(new ThreadStart(MoveToWizet));
                next.Start();
                next.Join();
            }
            catch { }
        }

        ~CNexonLayer()
        {
            
        }

        public override void Draw()
        {
            
            base.Draw();
        }

        public override void OnEnter()
        {
            base.OnEnter();
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            UI.Dispose();
            base.OnExit();
        }

        private void MoveToWizet()
        {

            if (!GameConstants.GREATER_VERSION)
            {
                ScheduleOnce(TransitionOut, 1);
            }
            else
            {
                ScheduleOnce(TransitionOut, 6);
            }
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

