using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListClasseLibreary
{
    public class ToDoItem
    {
        #region propertis        
        public int Id { set; get; }
        public string Discript { set; get; }
        public bool IsDone { set; get; }
        #endregion

        #region Constructor
        public ToDoItem()
        {

        }

        #endregion

        #region Function
        public override string ToString()
        {
            return $"Id={Id}\t Discript={Discript}\t IsDone={IsDone} ";
        }
        #endregion
    }
}
