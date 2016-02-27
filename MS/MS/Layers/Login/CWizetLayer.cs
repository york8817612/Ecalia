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

        //CTextureEngine texEngine = new CTextureEngine();
        CLogoEngine logoEngine;


        public CWizetLayer()
        {
            WZFile ui = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @GameConstants.UI, GameConstants.Variant, true);
            
            object uilock = new object();

            lock(uilock)
                logoEngine = new CLogoEngine(ui);

            AddChild(logoEngine.Draw(false));
            
            var login = new Thread(new ThreadStart(MoveToLogin));
            login.Start();
            login.Join();
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
            base.OnExit();
        }
    }
}
