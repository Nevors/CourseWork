using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Misc;
using CourseWork.Concrete;

namespace CourseWork.Grammar.Html.EditToken {
    class HtmlVisitorAddSpace : HtmlParserBaseVisitor<object>, IVisitorTree {
        IList<IToken> tokens;
        ManagerAction manager = new ManagerAction();
        ITokenFactory factory;
        public HtmlVisitorAddSpace(ITokenFactory factory) {
            this.factory = factory;
        }

        public IList<IToken> GetResult() {
            manager.DoAction();
            return tokens.Select(i => factory.Create(i.Type, i.Text)).ToList(); ;
        }

        public void Visit(IParseTree tree, IList<IToken> tokens) {
            this.tokens = tokens;
            Visit(tree);
        }

        public override object VisitHtmlAttributeName([NotNull] HtmlParser.HtmlAttributeNameContext context) {
            var token = context.TAG_NAME();
            int index = token.Symbol.TokenIndex;
            manager.AddAction(new ActionOrder {
                Order = index,
                Action = () => {
                    tokens.Insert(index, factory.Create(HtmlLexer.SEA_WS, " "));
                }
            });
            return base.VisitHtmlAttributeName(context);
        }
    }
}
