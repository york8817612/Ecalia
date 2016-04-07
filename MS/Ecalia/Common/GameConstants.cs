using reWZ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecalia.Common
{
    public class GameConstants
    {
        #region Version Settings

        public static ushort MAJOR_VERSION = 62;
        public static string MINOR_VERSION = "1";
        public static bool GREATER_VERSION = (MAJOR_VERSION > 79 ? true : false); // change the number to whatever the version maple changed the nexon logo animation, I think its 79 but idk.. 


        #endregion

        #region Client Settings

        public static string WINDOW_TITLE = "Ecalia";
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

        public static WZVariant Variant = (GREATER_VERSION ? WZVariant.BMS : WZVariant.GMS);
        public static string FileLocation = (GREATER_VERSION ? @"D:\v117\MapleStory" : @"D:\ClassicMS");//AppDomain.CurrentDomain.BaseDirectory;
        
        
        /*/// <summary>
        /// List of WZ files (Temp)
        /// </summary>
        public static WZFile BaseWz = new WZFile(FileLocation + @"\Base.wz", Variant, true),
            CharacterWz = new WZFile(FileLocation + @"\Character.wz", Variant, true),
            EffectWz = new WZFile(FileLocation + @"\Effect.wz", Variant, true),
            EtcWz = new WZFile(FileLocation + @"\Etc.wz", Variant, true),
            ItemWz = new WZFile(FileLocation + @"\Item.wz", Variant, true),
            ListWz = new WZFile(FileLocation + @"\List.wz", Variant, true),
            MapWz = new WZFile(FileLocation + @"\Map.wz", Variant, true),
            MobWz = new WZFile(FileLocation + @"\Mob.wz", Variant, true),
            MorphWz = new WZFile(FileLocation + @"\Morph.wz", Variant, true),
            NpcWz = new WZFile(FileLocation + @"\Npc.wz", Variant, true),
            QuestWz = new WZFile(FileLocation + @"\Quest.wz", Variant, true),
            ReactorWz = new WZFile(FileLocation + @"\Reactor.wz", Variant, true),
            SkillWz = new WZFile(FileLocation + @"\Skill.wz", Variant, true),
            SoundWz = new WZFile(FileLocation + @"\Sound.wz", Variant, true),
            StringWz = new WZFile(FileLocation + @"\String.wz", Variant, true),
            TamingMobWz = new WZFile(FileLocation + @"\TamingMob.wz", Variant, true),
            UiWz = new WZFile(FileLocation + @"UI.wz", Variant, true);*/

        #endregion
    }
}
