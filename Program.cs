﻿using Antlr4.Runtime;
using CourseWork.Abstract;
using CourseWork.Abstract.TextEditors;
using CourseWork.Concrete;
using CourseWork.Concrete.TextEditors;
using CourseWork.Grammar.Css;
using CourseWork.Grammar.Css.EditToken;
using CourseWork.Grammar.Html;
using CourseWork.Grammar.Html.EditToken;
using CourseWork.Grammar.Js;
using CourseWork.Grammar.Js.EditToken;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CourseWork {
    class Program {
        [STAThread]
        static void Main(string[] args) {
            if (args.Length < 1) {
                Console.WriteLine("Specify the folder");
                return;
            }
            Manager m = new Manager();
            m.DoRefactor(args[0]);/**/
            Console.WriteLine("---End---");
           // Console.Read();
        }
    }
}
