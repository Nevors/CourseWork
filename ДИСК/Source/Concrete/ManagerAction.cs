using CourseWork.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseWork.Concrete {
    class ManagerAction : IManagerAction<ActionOrder> {
        ActionList list = new ActionList();

        public void AddAction(ActionOrder action) {
            list.Add(action);
        }

        public void DoAction() {
            list.DoAction();
        }

        protected class ActionList {
            IList<ActionOrder> list;

            public ActionList() {
                list = new List<ActionOrder>();
            }
            public void Add(ActionOrder token) {
                list.Add(token);
            }
            
            public void DoAction() {
                var actions = list.OrderByDescending(a => a.Order).ToList();
                foreach(var item in actions) {
                    item.Action();
                }
            }
        }
    }

    public class ActionOrder {
        public int Order { get; set; }
        public Action Action { get; set; }
    }
}
