using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Abstract {
    interface IVisitorTree {
        void Visit(IParseTree tree, IList<IToken> tokens);
        IList<IToken> GetResult();
    }
}
