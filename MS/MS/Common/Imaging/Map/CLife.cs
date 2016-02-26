using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging.Map
{
    public class CLife
    {
        public int ID { get; set; }

        public uint SpawnID { get; set; }

        public string Type { get; set; }

        public int RespawnTime { get; set; }

        public ushort Foothold { get; set; }

        public bool FacesLeft { get; set; }

        public short X { get; set; }

        public short Y { get; set; }

        public short Cy { get; set; }

        public short Rx0 { get; set; }

        public short Rx1 { get; set; }

        public byte Hide { get; set; }
    }
}
