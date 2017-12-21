using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Misc;

namespace CourseWork.Grammar.Html.EditToken {
    class HtmlVisitorChangeAtributeValue : HtmlParserBaseVisitor<object>, IVisitorTree {
        IChangeToken editString;
        IList<IToken> tokens;
        ITokenFactory factory;
        public HtmlVisitorChangeAtributeValue(IFactoryNames factoryNames, ITokenFactory factory) {
            editString = new ChangeTokenAtributeValue(factory, factoryNames);
            this.factory = factory;
        }

        public IList<IToken> GetResult() {
            return tokens.Select(i=>factory.Create(i.Type,i.Text)).ToList();
        }

        public void Visit(IParseTree tree, IList<IToken> tokens) {
            this.tokens = tokens;
            Visit(tree);
        }
        private static readonly string attrId = "id";
        private static readonly string attrClass = "class";

        public override object VisitHtmlAttribute([NotNull] HtmlParser.HtmlAttributeContext context) {
            var contextName = context.htmlAttributeName();
            var name = contextName.GetText();

            bool isId = string.Equals(attrId, name, StringComparison.OrdinalIgnoreCase);
            bool isClass = string.Equals(attrClass, name, StringComparison.OrdinalIgnoreCase);

            if (isId || isClass) {
                var contextValue = context.htmlAttributeValue();
                
                var token = contextValue.ATTVALUE_VALUE();

                int index = token.Symbol.TokenIndex;
                IToken newToken = editString.GetChangedToken(tokens[index]);
                tokens[index] = newToken;
            }
            return base.VisitHtmlAttribute(context);
        }
    }
}
