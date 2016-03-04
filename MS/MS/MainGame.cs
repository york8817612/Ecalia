using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Cocos2D;
using MS.Common.Net;
using MS.Common;
using MS.Common.Imaging;
//using System.Windows.Forms;

namespace MS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        public KeyboardState state;
        private readonly GraphicsDeviceManager graphics;
        //private CNetwork network = new CNetwork(GameConstants.IP, GameConstants.LOGIN_PORT);

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);

            graphics.IsFullScreen = false;

            // Frame rate is 30 fps by default for Windows Phone.
            TargetElapsedTime = TimeSpan.FromTicks(333333 / 2);

            // Extend battery life under lock.
            //InactiveSleepTime = TimeSpan.FromSeconds(1);

            CCApplication application = new AppDelegate(this, graphics);
            Components.Add(application);
            //network.Initialize();

            state = Keyboard.GetState();
        }

        private void ProcessBackClick()
        {
            if (CCDirector.SharedDirector.CanPopScene)
            {
                CCDirector.SharedDirector.PopScene();
            }
            else
            {
                Exit();
            }
        }

        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                ProcessBackClick();
            }

            UpdateInput();

            base.Update(gameTime);
        }

        private void UpdateInput()
        {
            KeyboardState newState = Keyboard.GetState();
            CParallaxCamera cam = new CParallaxCamera(graphics.GraphicsDevice.Viewport);
            cam.Pos = new Vector2(0, 0);
            cam.Rotation = 1;
            cam.Update();
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            //network.Disconnect();
            base.OnExiting(sender, args);
        }
    }
}