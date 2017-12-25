using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Abstract {
    interface IFactoryNames {
        string GetShortName(string name);
        bool ContainsName(string name);
    }
}
