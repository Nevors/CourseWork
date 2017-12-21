using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Concrete.TextEditors.Editors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Concrete.TextEditors {
    class FactoryEditor : IFactoryEditor {
        Dictionary<TypeEditor, ITextEditor> editors;

        IFactoryNames factoryNames;
        public FactoryEditor(IFactoryNames factoryNames) {
            this.factoryNames = factoryNames;

            editors = new Dictionary<TypeEditor,ITextEditor>();

            editors.Add(
                    TypeEditor.Css,
                    new CssEditor(factoryNames)
                );
            editors.Add(
                    TypeEditor.Html,
                    new HtmlEditor(factoryNames,this)
                );
            editors.Add(
                    TypeEditor.Js,
                    new JsEditor(factoryNames)
                );
        }
        public ITextEditor GetEditor(TypeEditor type) {
            if (editors.ContainsKey(type)) {
                return editors[type];
            }
            return null;
        }
    }
}
