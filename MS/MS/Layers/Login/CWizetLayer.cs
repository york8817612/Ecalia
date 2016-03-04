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
        WZFile UI;
        CLogoEngine logoEngine;


        public CWizetLayer()
        {
            try
            {
                object uilock = new object();
                UI = new WZFile(GameConstants.FileLocation + "UI.wz", GameConstants.Variant, true);

                lock (uilock)
                    logoEngine = new CLogoEngine(UI);

                AddChild(logoEngine.Draw(false));

                var login = new Thread(new ThreadStart(MoveToLogin));
                login.Start();
                login.Join();
            }
            catch { }
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
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            UI.Dispose();
            base.OnExit();
        }
    }
}
