using Antlr4.Runtime;
using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Grammar.Html.EditToken.ChangeToken {
    class ChangeTokenStyle : IChangeToken {
        ITokenFactory factoryTokens;
        ITextEditor textEditor;

        public ChangeTokenStyle(ITokenFactory factoryTokens, IFactoryEditor factoryEditor) {
            this.factoryTokens = factoryTokens;
            this.textEditor = factoryEditor.GetEditor(TypeEditor.Css);
        }
        public IToken GetChangedToken(IToken token) {
            string text = token.Text;

            int indexEndTag = text.LastIndexOf('<');
            string endTag = text.Substring(indexEndTag);

            string body = text.Substring(0, indexEndTag);
            string newBody = textEditor.EditText(body);

            return factoryTokens.Create(token.Type, newBody + endTag);
        }
    }
}
