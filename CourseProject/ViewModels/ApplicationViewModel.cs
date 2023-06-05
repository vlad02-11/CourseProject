using CourseProject.Models;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CourseProject.ViewModels
{
    public class ApplicationViewModel : INotifyPropertyChanged
    {
        public WorkFolder WorkFolder
        {
            get => _workFolder;
            set
            {
                _workFolder = value;
                OnPropertyChanged();
            }
        }

        private WorkFolder _workFolder;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
