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

        public static ushort MAJOR_VERSION = 62;
        public static string MINOR_VERSION = "1";
        public static bool GREATER_VERSION = (MAJOR_VERSION > 79 ? true : false); // change the number to whatever the version maple changed the nexon logo animation, I think its 79 but idk.. 
        

        #endregion

        #region Client Settings

        public static int WINDOW_WIDTH = 800;
        public static int WINDOW_HEIGHT = 600;
        public static bool WINDOWED = true;
        public static float WIN_FPS = 60;

        #endregion

        #region Network Settings

        public static string IP = "127.0.0.1";
        public static int LOGIN_PORT = 8484;
        public static int CHANNEL_PORT;
        public static int WORLD_PORT;

        #endregion

        #region WZ File Settings

        public static reWZ.WZVariant Variant = (GREATER_VERSION ? reWZ.WZVariant.BMS : reWZ.WZVariant.GMS);
        public static string FileLocation = AppDomain.CurrentDomain.BaseDirectory;
        /// <summary>
        /// List of WZ files (Temp)
        /// </summary>
        public static string BASE = (GREATER_VERSION ? "2.wz" : "1.wz"),
            CHARACTER = (GREATER_VERSION ? "2.wz" : "1.wz"),
            EFFECT = (GREATER_VERSION ? "2.wz" : "1.wz"),
            ETC = (GREATER_VERSION ? "2.wz" : "1.wz"),
            ITEM = (GREATER_VERSION ? "2.wz" : "1.wz"),
            LIST = (GREATER_VERSION ? "2.wz" : "1.wz"),
            MAP = (GREATER_VERSION ? "Map.wz" : "Map.wz"),
            MOB = (GREATER_VERSION ? "2.wz" : "1.wz"),
            MORPH = (GREATER_VERSION ? "2.wz" : "1.wz"),
            NPC = (GREATER_VERSION ? "2.wz" : "1.wz"),
            QUEST = (GREATER_VERSION ? "2.wz" : "1.wz"),
            REACTOR = (GREATER_VERSION ? "2.wz" : "1.wz"),
            SKILL = (GREATER_VERSION ? "2.wz" : "1.wz"),
            SOUND = (GREATER_VERSION ? "2.wz" : "1.wz"),
            STRING = (GREATER_VERSION ? "2.wz" : "1.wz"),
            TAMING_MOB = (GREATER_VERSION ? "2.wz" : "1.wz"),
            UI = (GREATER_VERSION ? "UI2.wz" : "UI1.wz");

        #endregion
    }
}
