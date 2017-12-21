using Antlr4.Runtime;
using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Grammar.Html;
using CourseWork.Grammar.Html.EditToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Concrete.TextEditors.Editors {
    class HtmlEditor : ITextEditor {
        IFactoryNames factoryNames;
        IFactoryEditor factoryEditor;
        public HtmlEditor(IFactoryNames factoryNames,IFactoryEditor factoryEditor) {
            this.factoryNames = factoryNames;
            this.factoryEditor = factoryEditor;
        }
        public string EditText(string text) {
            AntlrInputStream inputStream = new AntlrInputStream(text);

            ITokenSource lexer;
            IVisitorTree visitor;
            IChangeTokenSource editorTokens;

            lexer = new HtmlLexer(inputStream);

            visitor = new HtmlVisitorChangeAtributeValue(factoryNames, lexer.TokenFactory);
            editorTokens = new BaseHtmlEditTokens(visitor);

            lexer = editorTokens.Edit(lexer);
            visitor = new HtmlVisitorEditorScriptTag(factoryNames, lexer.TokenFactory, factoryEditor);
            editorTokens = new BaseHtmlEditTokens(visitor);

            lexer = editorTokens.Edit(lexer);
            visitor = new HtmlVisitorEditStyleTag(factoryNames, lexer.TokenFactory, factoryEditor);
            editorTokens = new BaseHtmlEditTokens(visitor);

            lexer = editorTokens.Edit(lexer);
            visitor = new HtmlVisitorAddSpace(lexer.TokenFactory);
            editorTokens = new BaseHtmlEditTokens(visitor);

            lexer = editorTokens.Edit(lexer);
            CommonTokenStream cs = new CommonTokenStream(lexer);
            cs.Fill();
            return cs.GetText();
        }
    }
}
