using Ecalia.Common;
using Ecalia.Screens;
using reWZ;
using reWZ.WZProperties;
using SharpDX.Direct3D9;
using SharpDX.Mathematics.Interop;
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

        private int a, ani, cx, cy, f, front, no, rx, ry, type, x, y;
        private string bS, tS, oS;

        public int ID { get; set; }

        public CGraphicsEngine()
        {
            Map = new WZFile(GameConstants.FileLocation + @"\Map.wz", GameConstants.Variant, true);
            UI = new WZFile(GameConstants.FileLocation + @"\UI.wz", GameConstants.Variant, true);
        }

        public virtual void OnEnter()
        {
            if (Map == null)
                Drop();
            OnRender();
            //var render = RenderOnce(OnRender);
            //render();
        }

        private void Drop()
        {
            // There suppose to be nothing here so just ignore this for now.
        }

        protected virtual void OnRender()
        {
            RenderMap(ID);
        }

        public void RenderMap(int id)
        {
            try
            {
                using (Map)
                {
                    foreach (WZSubProperty SubProp in Map.MainDirectory["Map"]["Map" + GetTrueId(id)[0]][GetTrueId(id) + ".img"])
                    {
                        foreach (var item in SubProp)
                        {
                            // Background
                            if (item.HasChild("a"))
                                a = item["a"].ValueOrDie<int>();
                            if (item.HasChild("ani"))
                                ani = item["ani"].ValueOrDie<int>();
                            if (item.HasChild("bS"))
                                bS = item["bS"].ValueOrDie<string>();
                            if (item.HasChild("cx"))
                                cx = item["cx"].ValueOrDie<int>();
                            if (item.HasChild("cy"))
                                cy = item["cy"].ValueOrDie<int>();
                            if (item.HasChild("f"))
                                f = item["f"].ValueOrDie<int>();
                            if (item.HasChild("front"))
                                front = item["front"].ValueOrDie<int>();
                            if (item.HasChild("no"))
                                no = item["no"].ValueOrDie<int>();
                            if (item.HasChild("rx"))
                                rx = item["rx"].ValueOrDie<int>();
                            if (item.HasChild("ry"))
                                ry = item["ry"].ValueOrDie<int>();
                            if (item.HasChild("type"))
                                type = item["type"].ValueOrDie<int>();
                            if (item.HasChild("x"))
                                x = item["x"].ValueOrDie<int>();
                            if (item.HasChild("y"))
                                y = item["y"].ValueOrDie<int>();

                            if (bS != null)
                                RenderBackground(a, ani, bS, cx, cy, f, front, no, rx, ry, type, x, y);

                            // Map Objects


                            // Map Tiles
                        }
                    }
                }
            }
            catch { }
        }

        private void RenderBackground(int a, int ani, string bS, int cx, int cy, int f, int front, int no, int rx, int ry, int type, int x, int y, int z = 0)
        {
            var spr = new Sprite(CApplication.GraphicsDevice);
            try
            {
                var back = Map.MainDirectory["Back"][bS + ".img"]["back"][no.ToString()] as WZCanvasProperty;

                var texture = Texture.FromMemory(CApplication.GraphicsDevice, GetData(back.Value));

                spr.Begin(SpriteFlags.SortDepthBackToFront);
                spr.Draw(texture, new RawColorBGRA(0xFF, 0xFF, 0xFF, 0xFF));
                
            }
            catch { }
            finally
            {
                spr.End();
            }
        }

        public void Update()
        {

        }

        private void RenderObjects(string oS, int x, int y, int z = 0)
        {

        }

        private void RenderTiles()
        {

        }

        public void Dispose()
        {
            Map.Dispose();
            UI.Dispose();
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

        Action RenderOnce(Action action)
        {
            var context = new RenderOnlyOnce();
            Action ret = () =>
            {
                if (false == context.AlreadyCalled)
                {
                    action();
                    context.AlreadyCalled = true;
                }
            };

            return ret;
        }

        class RenderOnlyOnce
        {
            public bool AlreadyCalled;
        }
    }
}
