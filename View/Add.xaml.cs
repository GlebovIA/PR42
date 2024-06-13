using PR42.ViewModell;
using System.Windows.Controls;

namespace PR42.View
{
    /// <summary>
    /// Логика взаимодействия для Add.xaml
    /// </summary>
    public partial class Add : Page
    {
        public Add(object Context)
        {
            InitializeComponent();
            DataContext = new
            {
                item = Context,
                categories = new VMCategories()
            };
        }
    }
}
