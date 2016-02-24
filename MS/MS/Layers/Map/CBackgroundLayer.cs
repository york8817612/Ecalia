using Cocos2D;
using MS.Common;
using MS.Common.Imaging;
using reWZ;
using reWZ.WZProperties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Layers.Map
{
    public class CBackgroundLayer : CCLayer
    {
        private int rx, ry, cx, cy;
        private DisplayOrder order;
        private WZFile mapWz = new WZFile(AppDomain.CurrentDomain.BaseDirectory + @GameConstants.MAP, GameConstants.Variant, true);
        private WZDirectory map, back;
        private List<System.Drawing.Bitmap> Layers = new List<System.Drawing.Bitmap>(8);
        private CTextureEngine tex = new CTextureEngine();

        public CBackgroundLayer(string name)
        {
            back = (WZDirectory)mapWz.MainDirectory["Back"];

        }
    }
}
