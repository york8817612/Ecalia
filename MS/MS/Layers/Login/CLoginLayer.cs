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
using Microsoft.Xna.Framework.Input;

namespace MS.Layers.Login
{
    public class CLoginLayer : CCLayer
    {
        CMapEngine Map;
        MouseState state;

        public CLoginLayer()
        {
            Map = new CMapEngine();
            state = Mouse.GetState();

            //Camera.SetEyeXyz(0, 0, 22);
            //Camera.SetUpXyz(0, 500, 0);
            //Camera.SetCenterXyz(0, 0, 0);
        }

        public override void Draw()
        {
            base.Draw();
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

            CCMoveTo moveto = new CCMoveTo(20.0f, new CCPoint(0, -2500));

            //RunAction(moveto); // only use this for testing reasons

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
