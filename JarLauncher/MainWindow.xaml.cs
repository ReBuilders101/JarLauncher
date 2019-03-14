using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

using Microsoft.WindowsAPICodePack.Dialogs;

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
            UpdateExecutables();
        }

        public static string ExePath()
        {
            return new Uri(System.Reflection.Assembly.GetExecutingAssembly().CodeBase).LocalPath;
        }

        private string SelectedFilePath = "";
        private string JavaExePath = "";
        private string JavawExePath = "";


        private void DropButtonDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                var paths = (string[])e.Data.GetData(DataFormats.FileDrop);
                e.Effects = DragDropEffects.Link;
                if (paths.Length > 0)
                {
                    var path = paths[0];
                    if(File.Exists(path))
                    {
                        SelectedFilePath = path;
                        FileSelected = true;
                    }
                }
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

        private bool UpdateExecutables(string jreFolder)
        {
            if (Directory.Exists(jreFolder))
            {
                var javaPath  = jreFolder + @"bin\java.exe";
                var javawPath = jreFolder + @"bin\javaw.exe";
                if (File.Exists(javaPath) && File.Exists(javawPath))
                {
                    JavaExePath = javaPath;
                    JavawExePath = javawPath;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        private void UpdateExecutables()
        {
            var folderPath = ExePath();
            var lastBS = folderPath.LastIndexOf('\\');
            folderPath = folderPath.Substring(0, lastBS);
            folderPath = folderPath + @"\jre\";
            ExecutablesFound = UpdateExecutables(folderPath);
        }

        private void DropButtonClick(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Title = "Select jar file to open";
            ofd.Filter = "Jar files (*.jar)|*.jar|All files (*.*)|*.*";
            var result = ofd.ShowDialog(this);
            if(result == true) //can't just use if(result), because result is of type bool? -> If it looks stupid but it works, it's not stupid
            {
                var path = ofd.FileName;
                if (File.Exists(path))
                {
                    SelectedFilePath = path;
                    FileSelected = true;
                }
            }
        }

        private void QuitButtonClick(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string link = e.Uri.ToString();
            Process.Start(link);
        }

        private void JreButtonClick(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog cofd;
        }
    }
}
