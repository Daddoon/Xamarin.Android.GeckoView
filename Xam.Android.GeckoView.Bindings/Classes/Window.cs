using Android.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Mozilla.Geckoview
{
    public partial class GeckoSession
    {
        public sealed partial class Window
        {
            IBinder IInterface.AsBinder()
            {
                return AsBinder();
            }
        }
    }
}
