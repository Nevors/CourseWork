using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;
using Antlr4.Runtime.Tree;
using Antlr4.Runtime.Misc;

namespace CourseWork.Grammar.Css.EditToken {
    class CssVisitorEditNameSelector : CssBaseVisitor<object>, IVisitorTree {

        IFactoryNames factoryNames;
        ITokenFactory factoryTokens;
        IList<IToken> tokens;
        IChangeToken editId;
        IChangeToken editClass;

        public CssVisitorEditNameSelector(IFactoryNames factoryNames, ITokenFactory factoryTokens) {
            this.factoryNames = factoryNames;
            this.factoryTokens = factoryTokens;
            editId = new CssEditNameIdSelector(factoryTokens, factoryNames);
            editClass = new CssEditNameClassSelector(factoryTokens, factoryNames);
        }

        public IList<IToken> GetResult() {
            return tokens;
        }

        public void Visit(IParseTree tree, IList<IToken> tokens) {
            this.tokens = tokens;
            Visit(tree);
        }

        public override object VisitSimpleSelectorSequence([NotNull] CssParser.SimpleSelectorSequenceContext context) {
            var item = context.GetChild(0);
            if (item is TerminalNodeImpl) {
                var token = (TerminalNodeImpl)item;
                int index = token.Symbol.TokenIndex;
                IToken newToken = editId.GetChangedToken(tokens[index]);
                tokens[index] = newToken;
            }
            return base.VisitSimpleSelectorSequence(context);
        }

        public override object VisitClassName([NotNull] CssParser.ClassNameContext context) {
            var ident = context.ident();
            var nameIdent = ident.Ident();
            if (nameIdent != null) {
                int index = nameIdent.Symbol.TokenIndex;
                IToken newToken = editClass.GetChangedToken(tokens[index]);
                tokens[index] = newToken;
            }
            return base.VisitClassName(context);
        }
    }
}
