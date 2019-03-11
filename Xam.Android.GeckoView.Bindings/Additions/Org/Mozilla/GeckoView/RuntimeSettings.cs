using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Mozilla.Geckoview
{
    public sealed partial class GeckoRuntimeSettings
    {
        public new sealed partial class Builder
        {
            protected override Java.Lang.Object NewSettings(Java.Lang.Object p0)
            {
                return NewSettings((GeckoRuntimeSettings)p0);
            }
        }
    }
}
