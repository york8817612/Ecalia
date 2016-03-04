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
    public class CLoginLayer : CCLayer
    {
        private int FrameCount { get; set; }
        CMapEngine Map;

        public CLoginLayer()
        {
            Map = new CMapEngine();
            //Camera.SetUpXyz(0, 500, 0);
            //Camera.SetEyeXyz(0, 0, 800);
        }

        public override void Draw()
        {
            base.Draw();
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
            foreach (CCNode node in Map.Draw("MapLogin"))
            {
                AddChild(node);
            }
            AddChild(Map.DrawFrame());
            base.OnEnter();
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            base.OnExit();
        }

        public CCCamera GetCamera
        {
            get { return Camera; }
        }
    }
}
