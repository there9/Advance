using Prism.Mvvm;

namespace TimeTable.Modules.TotalGrid.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "TimeTable";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        public MainWindowViewModel()
        {

        }
    }
}
