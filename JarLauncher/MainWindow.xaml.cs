using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace JarLauncher
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static readonly DependencyProperty ExecutablesFoundProperty = DependencyProperty.Register("ExecutablesFound", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));
        public static readonly DependencyProperty FileSelectedProperty = DependencyProperty.Register("FileSelected", typeof(bool), typeof(MainWindow), new UIPropertyMetadata(false));

        public bool FileSelected
        {
            get
            {
                return (bool)GetValue(FileSelectedProperty);
            }
            set
            {
                SetValue(FileSelectedProperty, value);
            }
        }

        public bool ExecutablesFound
        {
            get
            {
                return (bool)GetValue(ExecutablesFoundProperty);
            }
            set
            {
                SetValue(ExecutablesFoundProperty, value);
            }
        }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void DropButtonDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effects = DragDropEffects.Link;
                if (paths.Length > 0) Console.WriteLine(paths[0]);
            }
        }

        private void DropButtonDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Link;
            } else
            {
                e.Effects = DragDropEffects.None;
            }
        }

        private void StartButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void DropButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select jar file to open";
            ofd.Filter = "Jar files (*.jar)|*.jar|All files (*.*)|*.*";
            var result = ofd.ShowDialog(this);
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
