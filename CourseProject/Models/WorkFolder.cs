using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

namespace CourseProject.Models
{
    public class WorkFolder : INotifyPropertyChanged
    {
        public FileInfo[] Files { get; private set; }
        public string SelectedFileContent
        {
            get
            {
                using var stream = new StreamReader(_selectedFile.OpenRead());
                return stream.ReadToEnd();
            }
        }

        public FileInfo SelectedFile
        {
            get => _selectedFile;
            set => SelectFile(value.Name);
        }

        private readonly DirectoryInfo _directory;
        private FileInfo _selectedFile;

        public WorkFolder(string path)
            : this(new DirectoryInfo(path))
        { }
        public WorkFolder(DirectoryInfo directory)
        {
            if (!directory.Exists) throw new ArgumentException(nameof(directory) + " not exist");


            Files = directory.GetFiles();

            TrySelectFirstFile();
            _directory = directory;
        }

        public void SelectFile(string fileName)
        {
            var chosenFile = Files.FirstOrDefault(f => f.Name == fileName);

            _selectedFile = chosenFile ?? throw new ArgumentException(nameof(fileName));
            OnPropertyChanged(nameof(SelectedFileContent));
            OnPropertyChanged(nameof(SelectedFile));
        }

        private void TrySelectFirstFile()
        {
            try
            {
                SelectedFile = Files.First();
            }
            catch (Exception) { }
        }

        public void Update()
        {
            if (!_directory.Exists) throw new InvalidOperationException();
            Files = _directory.GetFiles();
            TrySelectFirstFile();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
