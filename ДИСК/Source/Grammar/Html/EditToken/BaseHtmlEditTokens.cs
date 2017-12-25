using Antlr4.Runtime;
using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Grammar.Html.EditToken {
    class BaseHtmlEditTokens : IChangeTokenSource {
        IVisitorTree visitor;
        public BaseHtmlEditTokens(IVisitorTree visitor) {
            this.visitor = visitor;
        }
        public ITokenSource Edit(ITokenSource tokens) {
            CommonTokenStream s = new CommonTokenStream(tokens);
            HtmlParser parser = new HtmlParser(s);

            s.Fill();

            var program = parser.htmlDocument();

            visitor.Visit(program, s.GetTokens());
            var list = visitor.GetResult();
            var result = new ListTokenSource(list);
            result.TokenFactory = tokens.TokenFactory;
            return result;
        }
    }
}
