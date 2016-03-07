using Cocos2D;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reWZ;
using reWZ.WZProperties;
using Microsoft.Xna.Framework.Input;
using Ecalia.Common.Imaging;

namespace Ecalia.Layers.Login
{
    public class CLoginLayer : CCLayer
    {
        CMapEngine Map;
        MouseState state;

        public CLoginLayer()
        {
            Map = new CMapEngine();
            state = Mouse.GetState();
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
            AddChild(Map.Draw("MapLogin"));
            //AddChild(Map.DrawFrame());

            //CreateButtons();

            CheckLocation();

            base.OnEnter();
        }

        void CreateButtons()
        {
        }

        void TextFields()
        {
            CCTextFieldTTF User = new CCTextFieldTTF("Type here", "Arial", 12f);
            User.Position = new CCPoint(400, 300);
            AddChild(User, 10);
        }

        void CheckLocation()
        {
            Camera.SetEyeXyz(0, 0, 700);
            CCMoveTo moveto = new CCMoveTo(20.0f, new CCPoint(0, -2000));

            RunAction(moveto);
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
