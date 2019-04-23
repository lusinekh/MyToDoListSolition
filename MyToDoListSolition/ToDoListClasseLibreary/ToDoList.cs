using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoListClasseLibreary
{
    public class ToDoList:IDisposable
    {
        #region Varibels
        private SqlConnection cnn;
        private List<ToDoItem> items;
        #endregion


        public ToDoList()
        {

        }

        #region Propertis  
        public List<ToDoItem> Items
        {
            get
            {
                return items;
            }
        }
        #endregion

        #region Functions
        public SqlConnection Connetion(string ConnetionString)
        {
            cnn = new SqlConnection(ConnetionString);
            
                try
                {
                    cnn.Open();
                    Console.WriteLine(cnn.ToString());
                    Console.WriteLine("Connetion..........");
                    return cnn;
                }

                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    throw;
                }            
        }
        public void AddToDoListItm(SqlConnection CnnAdd, string Discript, bool Isdone)
        {
            int IsDoneValue = Isdone == true ? 1 : 0;

            string query = $"INSERT INTO ToDoList (Discript,IsDone)VALUES('{Discript}',{IsDoneValue}); ";
            SqlCommand cmd = new SqlCommand(query, CnnAdd);

            try
            {
                cmd.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UpdatToDoListItm(SqlConnection CnnAdd, bool Isdone,int   id)
        {
            int IsDoneValue = Isdone == true ? 1 : 0;

            string query = $"UPDATE ToDoList SET IsDone = {IsDoneValue} WHERE Id = {id}; ";        
            SqlCommand cmd = new SqlCommand(query, CnnAdd);
            try
            {
                cmd.ExecuteNonQuery();
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public List<ToDoItem> GetAllItms(SqlConnection CnnShow)
        {
            items = new List<ToDoItem>();
            SqlCommand commandShow = new SqlCommand("SELECT * FROM ToDoList;", CnnShow);
            try
            {
                SqlDataReader reader = commandShow.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var item = new ToDoItem
                        {
                            Id = (int)reader["Id"],
                            Discript = reader["Discript"].ToString(),
                            IsDone = (bool)reader["IsDone"],
                        };
                        items.Add(item);
                    }
                }
                reader.Close();
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }

            return items;
        }
        public void ShowToDoList(SqlConnection CnnShow)
        {

            SqlCommand commandShow = new SqlCommand("SELECT * FROM ToDoList;", CnnShow);          
            try
            {
                SqlDataReader reader = commandShow.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Console.WriteLine("{0}\t{1}\t{2}", reader.GetInt32(0),
                        reader.GetString(1), reader.GetBoolean(2));
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void IsCompleted(SqlConnection CnnApdate, int Id)
        {
            string queryAbdate = $" UPDATE ToDoList SET IsDone = 1 WHERE Id = {Id}; ";
            SqlCommand Apdate = new SqlCommand(queryAbdate, CnnApdate);
            try
            {
                Apdate.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void IsActive(SqlConnection CnnApdate, int Id)
        {
            string queryAbdate = $" UPDATE ToDoList SET IsDone = 0 WHERE Id = {Id}; ";
            SqlCommand Apdate = new SqlCommand(queryAbdate, CnnApdate);
            try
            {
                Apdate.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void DelateDoListItm(SqlConnection CnnDelate, int Id)
        {
            string DelateItms = $"DELETE FROM ToDoList WHERE Id = {Id};";
            SqlCommand DeleteItms = new SqlCommand(DelateItms, CnnDelate);
            try
            {
                DeleteItms.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public void DelateAllItms(SqlConnection Cnn)
        {

            string DeleteAll = "  DELETE FROM ToDoList; ";
            SqlCommand DeleteAllItms = new SqlCommand(DeleteAll, Cnn);
            try
            {
                DeleteAllItms.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

        public void DelateCompletedItmS(SqlConnection Cnn,int Id)
        {

            string DelateCompletedItms = $"DELETE FROM ToDoList WHERE IsDone = 1;";
            SqlCommand DeleteAllCompletedItms = new SqlCommand(DelateCompletedItms, Cnn);
            try
            {
                DeleteAllCompletedItms.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public List<ToDoItem> GetActiveItems(SqlConnection Cnn)
        {
            GetAllItms(cnn);
            List<ToDoItem> ActiveItems;
            if (items != null)
            {
                ActiveItems = new List<ToDoItem>();
                var Result = from s in items
                             where s.IsDone == false
                             select s;

                foreach (var i in Result)
                {
                    var item = new ToDoItem
                    {
                        Id = i.Id,
                        Discript = i.Discript,
                        IsDone = i.IsDone
                    };

                    ActiveItems.Add(item);
                }
            }
            else
            {
                throw new NullReferenceException("The Item is null ");
            }

            return ActiveItems;
        }
        public List<ToDoItem> GetCompletedItems(SqlConnection Cnn)
        {
            GetAllItms(cnn);
            List<ToDoItem> CompletedItems;
            if (items != null)
            {
                CompletedItems = new List<ToDoItem>();
                var Result = from s in items
                             where s.IsDone == false
                             select s;

                foreach (var i in Result)
                {
                    var item = new ToDoItem
                    {
                        Id = i.Id,
                        Discript = i.Discript,
                        IsDone = i.IsDone
                    };

                    CompletedItems.Add(item);
                }                
            }
            else
            {
                throw new NullReferenceException("The Item is null ");
            }

            return CompletedItems;
        }
        public void Dispose()
        {
            cnn.Close();
        }
        #endregion
    }
}
