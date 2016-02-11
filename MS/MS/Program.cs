using System;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MS
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        [STAThread]
        static void Main(string[] args)
        {
            using (MainGame game = new MainGame())
            {
                game.Run();
            }
        }
    }


}

