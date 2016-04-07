using Ecalia.Screens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ecalia
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CApplication app = new CApplication())
            {
                app.Run();
            }
        }
    }
}
