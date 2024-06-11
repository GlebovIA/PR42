using PR42.Context;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PR42.ViewModell
{
    public class VMCategories : INotifyPropertyChanged
    {
        public ObservableCollection<CategoriesContext> Categories { get; set; }
        public VMCategories() => Categories = CategoriesContext.AllCategories();
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
