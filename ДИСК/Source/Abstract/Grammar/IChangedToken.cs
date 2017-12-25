﻿using Antlr4.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Abstract {
    interface IChangeToken {
        IToken GetChangedToken(IToken token);
    }
}
