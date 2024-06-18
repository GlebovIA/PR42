using PR42.Classes;
using PR42.Modell;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;

namespace PR42.Context
{
    public class ItemsContext : Items
    {
        private static bool _isNew = true;
        public ItemsContext(bool save = false)
        {
            if (save) save = true;
            Category = new Categories();
        }
        public static ObservableCollection<ItemsContext> AllItems()
        {
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            ObservableCollection<CategoriesContext> allCategories = CategoriesContext.AllCategories();
            SqlConnection connection;
            SqlDataReader dataItems = Connection.Query("Select * from [dbo].[Items]", out connection);
            while (dataItems.Read())
            {
                allItems.Add(new ItemsContext()
                {
                    Id = dataItems.GetInt32(0),
                    Name = dataItems.GetString(1),
                    Price = dataItems.GetDouble(2),
                    Description = dataItems.GetString(3),
                    Category = dataItems.IsDBNull(4) ? null : allCategories.Where(x => x.Id == dataItems.GetInt32(4)).First()
                });
            }
            Connection.CloseConnection(connection);
            return allItems;
        }
        public void Save()
        {
            SqlConnection connection;
            if (_isNew)
            {
                SqlDataReader dataItems = Connection.Query("Insert into " +
                    "[dbo].[Items] (" +
                    "Name, " +
                    "Price, " +
                    "Description, " +
                    "Category) " +
                    "OUTPUT Inserted.Id " +
                    "Values (" +
                    $"N'{Name}', " +
                    $"{Price}, " +
                    $"N'{Description}', " +
                    $"{Category.Id})", out connection);
                dataItems.Read();
                Id = dataItems.GetInt32(0);
            }
            else
            {
                Connection.Query("Update [dbo].[Items] " +
                    "Set " +
                    $"Name = N'{Name}', " +
                    $"Price = {Price}, " +
                    $"Description = N'{Description}', " +
                    $"Category = {Category.Id} " +
                    "Where " +
                    $"Id = {Id}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.Main);
        }
        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("Delete from [dbo].[Items] " +
                "Where " +
                $"Id = {Id}", out connection);
            Connection.CloseConnection(connection);
        }
        public RelayCommand OnEdit
        {
            get
            {
                _isNew = false;
                return new RelayCommand(obj => MainWindow.init.frame.Navigate(new View.Add(this)));
            }
        }
        public RelayCommand OnSave
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Category = CategoriesContext.AllCategories().Where(x => x.Id == Category.Id).First();
                    _isNew = _isNew != true ? _isNew : true;
                    Save();
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(obj =>
                {
                    Delete();
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
