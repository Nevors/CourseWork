using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Antlr4.Runtime;

namespace CourseWork.Grammar.Html.EditToken {
    class ChangeTokenAtributeValue : IChangeToken {
        ITokenFactory factoryTokens;
        IFactoryNames factoryNames;
        bool isCreateNew;
        public ChangeTokenAtributeValue(ITokenFactory factoryTokens, IFactoryNames factoryNames, bool isCreateNew = true) {
            this.factoryTokens = factoryTokens;
            this.factoryNames = factoryNames;
            this.isCreateNew = isCreateNew;
        }
        public IToken GetChangedToken(IToken token) {
            string newText = token.Text;
            newText = newText.Replace('"', ' ').Replace('\'', ' ').Trim();
            StringBuilder sb = new StringBuilder();
            sb.Append('\'');
            foreach (var item in newText.Split(' ')) {
                if (item.Length!=0) {
                    if(factoryNames.ContainsName(newText) || isCreateNew) {
                        sb.Append(factoryNames.GetShortName(item));
                    }else {
                        sb.Append(item);
                    }
                    sb.Append(' ');
                }
            }
            sb.Length--;
            sb.Append('\'');
            return factoryTokens.Create(token.Type, sb.ToString());
        }
    }
}
