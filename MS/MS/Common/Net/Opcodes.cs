using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MS.Common.Net
{
    public partial class CNetwork
    {
        // Server Recv = Client Send
        // Server Send = Client Recv

        public enum TO_SERVER : short
        {
            LOGIN_STATUS = 0x00,
        };

        public enum FROM_SERVER : short
        {
            CLIENT_HELLO = 0x0D,
        };
    }
}
