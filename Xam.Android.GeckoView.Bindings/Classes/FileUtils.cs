using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Org.Mozilla.Gecko.Util
{
    public partial class FileUtils
    {
        public partial class FileLastModifiedComparator
        {
            public int Compare(Java.Lang.Object o1, Java.Lang.Object o2)
            {
                return Compare((Java.IO.File)o1, (Java.IO.File)o2);
            }
        }
    }
}
