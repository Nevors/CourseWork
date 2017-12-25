using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Abstract.TextEditors {
    interface IFactoryEditor {
        ITextEditor GetEditor(int type);
    }
}
