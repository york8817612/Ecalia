using Ecalia.Common;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecalia.Engine.Graphics
{
    public class CGraphicsEngine : IDisposable
    {
        private WZFile Map, UI;
        
        public CGraphicsEngine()
        {
            Map = new WZFile(GameConstants.FileLocation + @"\Map.wz", GameConstants.Variant, true);
            UI = new WZFile(GameConstants.FileLocation + @"\UI.wz", GameConstants.Variant, true);
        }

        public void Render(int id)
        {
            using (Map)
            {
                foreach (WZSubProperty SubProp in Map.MainDirectory["Map"]["Map" + GetTrueId(id)[0]][GetTrueId(id) + ".img"])
                {
                    foreach (var item in SubProp)
                    {
                        Console.WriteLine(item.Name);
                        if (item.HasChild("info"))
                            Console.WriteLine("Yes");
                    }
                }
            }
        }

        public void Update()
        {

        }

        private void LoadBackground(string bs, string no, int x, int y, int z = 0)
        {

        }

        private void LoadMapObjects(string os, int x, int y, int z = 0)
        {

        }

        private void LoadMapTiles()
        {

        }

        public void Dispose()
        {

        }

        public byte[] GetData(System.Drawing.Bitmap img)
        {
            byte[] byteArray = new byte[0];
            using (MemoryStream stream = new MemoryStream())
            {
                img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                stream.Close();

                byteArray = stream.ToArray();
            }

            return byteArray;
        }

        public string GetTrueId(int id)
        {
            if (id >= 10000)
                return "0000" + id.ToString();
            else if (id >= 1000000)
                return "00" + id.ToString();
            else if (id >= 100000000)
                return id.ToString();
            else
                return string.Empty;
        }
    }
}
