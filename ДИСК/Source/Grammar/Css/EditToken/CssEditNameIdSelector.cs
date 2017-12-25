using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace CourseWork.Grammar.Css.EditToken {
    class CssEditNameIdSelector : IChangeToken {
        ITokenFactory factoryTokens;
        IFactoryNames factoryNames;
        public CssEditNameIdSelector(ITokenFactory factoryTokens, IFactoryNames factoryNames) {
            this.factoryTokens = factoryTokens;
            this.factoryNames = factoryNames;
        }
        public IToken GetChangedToken(IToken token) {
            string value = token.Text.Remove(0, 1);
            string genName = factoryNames.GetShortName(value);
            return factoryTokens.Create(token.Type, "#" + genName);
        }
    }
}
