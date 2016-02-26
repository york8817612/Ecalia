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
using MS.Common.Imaging.Map;

namespace MS.Layers.Login
{
    public class CLoginLayer : CCLayer
    {
        CTextureEngine tex = new CTextureEngine();

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
            base.OnEnter();
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            base.OnExit();
        }
    }
}
