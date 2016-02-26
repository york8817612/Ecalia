using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Imaging.Map
{
    public class CPortal
    {
        public byte ID { get; set; }
        public short X { get; set; }
        public short Y { get; set; }
        public string Name { get; set; }
        public int ToMapID { get; set; }
        public string ToName { get; set; }
    }
}
