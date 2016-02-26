using Cocos2D;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using WZ;

namespace MS.Common.Imaging.Map
{
    public class CMapEngine
    {

        public CMapEngine(WZFile mapFile)
        {
            
        }
    }

    public struct CMapStruct
    {

        private int id;
        private int forceReturn;
        private int returnMap;
        private bool town;
        private bool hasClock;
        private byte fieldType;
        private bool fly;
        private bool swim;
        private int mobRate;
        private string onFirstUserEnter;
        private string onUserEnter;
        private int weatherId;
        private string weatherMessage;
        private bool weatherAdmin;
        private string bS;
        private string tS;

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public int ForceReturn
        {
            get { return forceReturn; }
            set { forceReturn = value; }
        }

        public int ReturnMap
        {
            get { return returnMap; }
            set { returnMap = value; }
        }

        public bool Town
        {
            get { return town; }
            set { town = value; }
        }

        public bool HasClock
        {
            get { return hasClock; }
            set { hasClock = value; }
        }

        public byte FieldType
        {
            get { return fieldType; }
            set { fieldType = value; }
        }

        public bool Fly
        {
            get { return fly; }
            set { fly = value; }
        }

        public bool Swim
        {
            get { return swim; }
            set { swim = value; }
        }

        public int MobRate
        {
            get { return mobRate; }
            set { mobRate = value; }
        }

        public string OnFirstUserEnter
        {
            get { return onFirstUserEnter; }
            set { onFirstUserEnter = value; }
        }

        public string OnUserEnter
        {
            get { return onUserEnter; }
            set { onUserEnter = value; }
        }

        public int WeatherID
        {
            get { return weatherId; }
            set { weatherId = value; }
        }

        public string WeatherMessage
        {
            get { return weatherMessage; }
            set { weatherMessage = value; }
        }

        public bool WeatherAdmin
        {
            get { return weatherAdmin; }
            set { weatherAdmin = value; }
        }

        public string BS
        {
            get { return bS; }
            set { bS = value; }
        }

        public CMapStruct(int mapid) : this()
        {
            id = mapid;
        }
    }
}
