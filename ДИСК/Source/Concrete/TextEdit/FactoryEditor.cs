using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Concrete.TextEditors.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Concrete.TextEditors {
    enum TypeEditor : int{
        None = 0,
        Html = 1,
        Css = 2,
        Js = 3
    }

    class FactoryEditor : IFactoryEditor {
        Dictionary<TypeEditor, ITextEditor> editors;

        IFactoryNames factoryNames;
        public FactoryEditor(IFactoryNames factoryNames) {
            this.factoryNames = factoryNames;

            editors = new Dictionary<TypeEditor, ITextEditor>();

            editors.Add(
                    TypeEditor.Css,
                    new CssEditor(factoryNames)
                );
            editors.Add(
                    TypeEditor.Html,
                    new HtmlEditor(factoryNames, this)
                );
            editors.Add(
                    TypeEditor.Js,
                    new JsEditor(factoryNames)
                );
        }
        public ITextEditor GetEditor(int type) {
            TypeEditor typeEditor = (TypeEditor)type;
            if (editors.ContainsKey(typeEditor)) {
                return editors[typeEditor];
            }
            return null;
        }
    }
}
