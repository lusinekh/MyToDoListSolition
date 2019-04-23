using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoListClasseLibreary;

namespace ConsoleApplicationToDoList
{
    class Program
    {
        static void Main(string[] args)
        {
            string connetionString = @"Data Source=DESKTOP-I87ABNR\SQLEXPRESS;Initial Catalog=ToDoList;Integrated Security=True;Pooling=False";
            ToDoList itm = new ToDoList();
            using (SqlConnection t = itm.Connetion(connetionString))
            {
                itm.AddToDoListItm(t, "Read", true);
                itm.ShowToDoList(t);
                itm.IsCompleted(t, 22);
                itm.ShowToDoList(t);
                itm.IsActive(t, 22);
                itm.DelateDoListItm(t, 22);
                itm.GetAllItms(t);
                itm.GetActiveItems(t);
                var e = itm.GetAllItms(t);
                Console.WriteLine("GetAllItms");
             
                foreach (var ef in e)
                {

                    Console.WriteLine(ef);
                }
            }
        }
    }
}
