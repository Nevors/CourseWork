using Antlr4.Runtime;
using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Grammar.Css.EditToken {
    class CssEditNameClassSelector : IChangeToken {
        ITokenFactory factoryTokens;
        IFactoryNames factoryNames;
        public CssEditNameClassSelector(ITokenFactory factoryTokens, IFactoryNames factoryNames) {
            this.factoryTokens = factoryTokens;
            this.factoryNames = factoryNames;
        }
        public IToken GetChangedToken(IToken token) {
            string name = token.Text;
            var genName = factoryNames.GetShortName(name);
            return factoryTokens.Create(token.Type, genName);
        }
    }
}
