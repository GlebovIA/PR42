using PR42.ViewModell;
using System.Windows.Controls;

namespace PR42.View
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        public Main()
        {
            InitializeComponent();
            DataContext = new VMItems();
        }
    }
}
