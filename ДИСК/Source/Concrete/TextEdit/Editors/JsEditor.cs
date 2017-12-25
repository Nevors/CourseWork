using Antlr4.Runtime;
using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Grammar.Css.EditToken;
using CourseWork.Grammar.Js;
using CourseWork.Grammar.Js.EditToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Concrete.TextEditors.Editors {
    class JsEditor : ITextEditor {
        IFactoryNames factoryNames;
        public JsEditor(IFactoryNames factoryNames) {
            this.factoryNames = factoryNames;
        }
        public string EditText(string text) {
            AntlrInputStream inputStream = new AntlrInputStream(text);

            ITokenSource lexer;
            IVisitorTree visitor;
            IChangeTokenSource editorTokens;

            lexer = new JsLexer(inputStream);
            visitor = new JsVisitorChangeLiteralString(factoryNames, lexer.TokenFactory);
            editorTokens = new BaseJsEditTokens(visitor);

            lexer = editorTokens.Edit(lexer);
            CommonTokenStream cs = new CommonTokenStream(lexer);
            cs.Fill();
            return cs.GetText();
        }
    }
}
