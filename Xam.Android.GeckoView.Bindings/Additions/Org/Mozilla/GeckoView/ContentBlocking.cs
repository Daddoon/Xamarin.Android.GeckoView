using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Mozilla.Geckoview
{
    public partial class ContentBlocking
    {
        public partial class Settings
        {
            public new partial class Builder
            {
                protected override Java.Lang.Object NewSettings(Java.Lang.Object p0)
                {
                    return NewSettings((Settings)p0);
                }
            }
        }
    }
}
