using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork.Concrete;
using Antlr4.Runtime;

namespace CourseWork.Grammar.Js.EditToken {
    sealed class EditLiteralString : IChangeTokenSource {
        IFactoryNames factoryNames;

        public EditLiteralString(IFactoryNames factoryNames) {
            this.factoryNames = factoryNames;
        }

        public ITokenSource Edit(ITokenSource tokens) {
            CommonTokenStream s = new CommonTokenStream(tokens);
            JsParser parser = new JsParser(s);

            s.Fill();
            IVisitorTree v = new JsVisitorChangeLiteralString(factoryNames, tokens.TokenFactory);

            var program = parser.program();

            v.Visit(program, s.GetTokens());

            var source = new ListTokenSource(v.GetResult());
            source.TokenFactory = tokens.TokenFactory;
            return source;
        }
    }
}
