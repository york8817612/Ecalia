using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging.Map
{
    public class CBackgroundEngine
    {
        private List<System.Drawing.Bitmap> Layers = new List<System.Drawing.Bitmap>(8);
        private WZFile map;
        private CTextureEngine tex = new CTextureEngine();

        public CBackgroundEngine(WZFile file, int name)
        {
            var mT = file.MainDirectory["Back"][name + ".img"];

            for (int i = 0; i < Layers.Capacity; i++)
            {
                Layers.Add(((WZCanvasProperty)mT[i.ToString()]).Value);
            }
        }
    }
}
