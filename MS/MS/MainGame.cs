using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Cocos2D;
using MS.Common.Net;
using MS.Common;
using MS.Common.Imaging;

namespace MS
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainGame : Game
    {
        private readonly GraphicsDeviceManager graphics;
        private CNetwork network = new CNetwork(GameConstants.IP, GameConstants.LOGIN_PORT);
        public CParallaxCamera camera = new CParallaxCamera();

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

            // TODO: Add your update logic here


            base.Update(gameTime);
        }

        protected override void OnExiting(object sender, EventArgs args)
        {
            network.Disconnect();
            base.OnExiting(sender, args);
        }
    }
}