﻿using Antlr4.Runtime;
using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Grammar.Css;
using CourseWork.Grammar.Css.EditToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Concrete.TextEditors.Editors {
    class CssEditor : ITextEditor {
        IFactoryNames factoryNames;
        public CssEditor(IFactoryNames factoryNames) {
            this.factoryNames = factoryNames;
        }
        public string EditText(string text) {
            AntlrInputStream inputStream = new AntlrInputStream(text);

            ITokenSource lexer;
            IVisitorTree visitor;
            IChangeTokenSource editorTokens;

            lexer = new CssLexer(inputStream);
            visitor = new CssVisitorEditNameSelector(factoryNames, lexer.TokenFactory);
            editorTokens = new BaseCssEditTokens(visitor);

            lexer = editorTokens.Edit(lexer);
            CommonTokenStream cs = new CommonTokenStream(lexer);
            cs.Fill();
            return cs.GetText();
        }
    }
}
