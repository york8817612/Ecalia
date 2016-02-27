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
        private int FrameCount { get; set; }

        public CLoginLayer()
        {
            var map = new WZFile(GameConstants.FileLocation + @GameConstants.UI, GameConstants.Variant, true);
            CMapEngine mapEngine = new CMapEngine(map, true);
            AddChild(mapEngine.DrawAll());
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
            if (Camera.Dirty)
                Camera.Restore();

            base.OnEnter();
        }

        public override void OnExit()
        {
            GC.SuppressFinalize(this);
            base.OnExit();
        }
    }
}
