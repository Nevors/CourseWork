using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Grammar.Html.EditToken.ChangeToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Grammar.Html.EditToken {
    class HtmlVisitorEditStyleTag : HtmlParserBaseVisitor<object>, IVisitorTree {
        IChangeToken editScript;
        IList<IToken> tokens;
        ITokenFactory factory;
        public HtmlVisitorEditStyleTag(IFactoryNames factoryNames, ITokenFactory factory, IFactoryEditor factoryEditor) {
            editScript = new ChangeTokenStyle(factory, factoryEditor);
            this.factory = factory;
        }

        public IList<IToken> GetResult() {
            return tokens.Select(i => factory.Create(i.Type, i.Text)).ToList();
        }

        public void Visit(IParseTree tree, IList<IToken> tokens) {
            this.tokens = tokens;
            Visit(tree);
        }
        public override object VisitStyle([NotNull] HtmlParser.StyleContext context) {
            var token = context.STYLE_BODY();
            int index = token.Symbol.TokenIndex;
            IToken newToken = editScript.GetChangedToken(tokens[index]);
            tokens[index] = newToken;
            return base.VisitStyle(context);
        }
    }
}
