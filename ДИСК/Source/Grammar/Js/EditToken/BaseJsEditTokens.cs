using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace CourseWork.Grammar.Js.EditToken {
    class BaseJsEditTokens : IChangeTokenSource {
        IVisitorTree visitor;
        public BaseJsEditTokens(IVisitorTree visitor) {
            this.visitor= visitor;
        }
        public virtual ITokenSource Edit(ITokenSource tokens) {
            CommonTokenStream s = new CommonTokenStream(tokens);
            JsParser parser = new JsParser(s);

            s.Fill();

            var program = parser.program();

            visitor.Visit(program,s.GetTokens());

            var source = new ListTokenSource(visitor.GetResult());
            source.TokenFactory = tokens.TokenFactory;
            return source;
        }
    }
}
