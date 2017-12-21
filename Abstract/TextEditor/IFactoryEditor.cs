using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Abstract.TextEditors {
    enum TypeEditor {
        None,
        Html,
        Css,
        Js
    }
    interface IFactoryEditor {
        ITextEditor GetEditor(TypeEditor type);
    }
}
