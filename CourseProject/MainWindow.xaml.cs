using System.Drawing;
using CourseProject.Models;
using CourseProject.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using FileInfo = System.IO.FileInfo;
using MessageBox = System.Windows.MessageBox;

namespace CourseProject
{
    public partial class MainWindow : Window
    {
        private readonly ApplicationViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();

            _viewModel = new ApplicationViewModel();

            DataContext = _viewModel;
        }

        private void btnOpenFolder_Click(object sender, RoutedEventArgs e)
        {
            using var dialog = new FolderBrowserDialog();

            var result = dialog.ShowDialog();

            _viewModel.WorkFolder = new WorkFolder(dialog.SelectedPath);
        }

        private void CurrentFiles_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is FileInfo file)
                MessageBox.Show(file.FullName);
        }
    }
}
