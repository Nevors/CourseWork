using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace CourseWork.Grammar.Css.EditToken {
    class BaseCssEditTokens : IChangeTokenSource {
        IVisitorTree visitor;
        public BaseCssEditTokens(IVisitorTree visitor) {
            this.visitor = visitor;
        }

        public ITokenSource Edit(ITokenSource tokens) {
            CommonTokenStream s = new CommonTokenStream(tokens);
            CssParser parser = new CssParser(s);

            s.Fill();

            var program = parser.stylesheet();

            visitor.Visit(program, s.GetTokens());
            var list = visitor.GetResult();
            var result = new ListTokenSource(list);
            result.TokenFactory = tokens.TokenFactory;
            return result;
        }
    }
}
