using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common
{
    public class GameConstants
    {
        #region Version Settings

        public static ushort MAJOR_VERSION = 100;
        public static string MINOR_VERSION = "1";
        public static bool GREATER_VERSION = (MAJOR_VERSION > 79 ? true : false); // change the number to whatever the version maple changed the nexon logo animation, I think its 79 but idk.. 
        public static reWZ.WZVariant Variant = (GREATER_VERSION ? reWZ.WZVariant.BMS : reWZ.WZVariant.GMS);

        #endregion

        #region Client Settings

        public static int WINDOW_WIDTH = 800;
        public static int WINDOW_HEIGHT = 600;
        public static bool WINDOWED = true;
        public static float WIN_FPS = 60;

        #endregion 
    }
}
