using CourseWork.Abstract.TextEditors;
using CourseWork.Concrete;
using CourseWork.Concrete.TextEditors;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork {
    internal sealed class Manager {
        IFactoryEditor factoryEditor;
        List<Action> queue;
        public Manager() {
            factoryEditor = new FactoryEditor(new FactoryNames(new NameGenerator()));
            queue = new List<Action>();
        }
        public void DoRefactor(string pathFolder) {
            DirectoryInfo root = Directory.CreateDirectory(pathFolder);
            if (root.Parent == null) return;

            string pathParentOfRoot = root.Parent.FullName;
            string pathMirorFolder = string.Concat(pathParentOfRoot, "\\", root.Name, "_min");

            DirectoryInfo mirror = new DirectoryInfo(pathMirorFolder);
            mirror.Create();
            DoRefactor(root, mirror);
            //queue.ForEach(h=>h());
            foreach (var item in queue) {
                item();
            }
            queue.Clear();
        }

        private void DoRefactor(DirectoryInfo rootFolder, DirectoryInfo mirrorFolder) {
            foreach (var dir in rootFolder.GetDirectories()) {
                string pathNextMirror = mirrorFolder.FullName + "\\" + dir.Name;
                DirectoryInfo nextMirror = new DirectoryInfo(pathNextMirror);
                nextMirror.Create();
                DoRefactor(dir, nextMirror);
            }
            foreach (var file in rootFolder.GetFiles()) {
                TypeEditor type = GetTypeEditor(file.Name);
                var editor = factoryEditor.GetEditor(type);
                string pathMirrorFile = mirrorFolder.FullName + "\\" + file.Name;

                if (editor == null) {
                    file.CopyTo(pathMirrorFile);
                    continue;
                }

                Action write = () => {
                    Console.WriteLine(file.FullName);
                    string inputText = File.ReadAllText(file.FullName);
                    string resultText = editor.EditText(inputText);
                    File.WriteAllText(pathMirrorFile, resultText);
                };

                if (type == TypeEditor.Js) {
                    queue.Add(write);
                } else {
                    write();
                }
            }
        }

        static public TypeEditor GetTypeEditor(string pathFile) {
            int indexPoint = pathFile.LastIndexOf('.');
            if (indexPoint == -1) {
                return TypeEditor.None;
            }

            string typeS = pathFile.Substring(indexPoint + 1);
            switch (typeS.ToUpper()) {
                case "CSS":
                    return TypeEditor.Css;
                case "HTML":
                    return TypeEditor.Html;
                case "JS":
                    return TypeEditor.Js;
                default:
                    return TypeEditor.None;
            }
        }
    }
}
