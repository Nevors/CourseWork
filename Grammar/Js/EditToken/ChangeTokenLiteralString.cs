using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace CourseWork.Grammar.Js.EditToken {
    class ChangeTokenLiteralString: IChangeToken {
        ITokenFactory factoryTokens;
        IFactoryNames factoryNames;
        public ChangeTokenLiteralString(ITokenFactory factoryTokens,IFactoryNames factoryNames) {
            this.factoryTokens = factoryTokens;
            this.factoryNames = factoryNames;
        }
        public IToken GetChangedToken(IToken token) {
            string newText = token.Text;
            newText = newText.Replace('"', ' ').Replace('\'', ' ').Trim();
            if (factoryNames.ContainsName(newText)) {
                StringBuilder sb = new StringBuilder();
                sb.Append('\'');
                sb.Append(factoryNames.GetShortName(newText));
                sb.Append('\'');
                newText = sb.ToString();
                return factoryTokens.Create(token.Type, newText);
            }
            return token;
        }
    }
}