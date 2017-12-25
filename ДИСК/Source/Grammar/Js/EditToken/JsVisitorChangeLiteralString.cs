using Antlr4.Runtime;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using CourseWork.Abstract;
using CourseWork.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Grammar.Js.EditToken {
    sealed class JsVisitorChangeLiteralString : JsBaseVisitor<object>, IVisitorTree {
        IChangeToken editString;
        IList<IToken> tokens;
        public JsVisitorChangeLiteralString(IFactoryNames factoryNames, ITokenFactory factory) {
            editString = new ChangeTokenLiteralString(factory, factoryNames);
        }

        public IList<IToken> GetResult() {
            return tokens;
        }

        void IVisitorTree.Visit(IParseTree tree, IList<IToken> tokens) {
            this.tokens = tokens;
            Visit(tree);
        }

        public override object VisitLiteral([NotNull] JsParser.LiteralContext context) {
            foreach (var item in context.children) {
                var node = item as TerminalNodeImpl;
                if (node != null) {
                    //Console.WriteLine("------------------------------------");
                    //Console.WriteLine(node.GetText());
                    //Console.WriteLine(node.GetType());
                    //Console.WriteLine();
                    //Console.WriteLine("------------------------------------");
                    int index = node.Payload.TokenIndex;
                    IToken newToken = editString.GetChangedToken(tokens[index]);
                    tokens[index] = newToken;
                }
            }
            return base.VisitLiteral(context);
        }
    }
}
