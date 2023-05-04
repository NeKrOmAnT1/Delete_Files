using ModernWpf;
using System;
using System.Collections.Generic;
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
using Path = System.IO.Path;

namespace WpfApp6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ThemeManager.Current.ApplicationTheme = ApplicationTheme.Dark;
            this.Loaded += MainWindow_Loaded;
        }
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            DriveInfo[] di = DriveInfo.GetDrives();
            foreach (DriveInfo d in di)
                lbx.Items.Add(d.Name);
        }

        private void lbx_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lbDirectory.Items.Clear();
            lbFile.Items.Clear();
            var select = lbx.SelectedItem as string;
            var directories = Directory.GetDirectories(select);
            foreach (var d in directories)
                lbDirectory.Items.Add(d);
            foreach (var d in Directory.GetFiles(select))
                lbFile.Items.Add(d);


        }

        private void lbDirectory_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                var select = lbDirectory.SelectedItem as string;
                if (select != null)
                {
                    RunContentDir(select);
                    RunContentFile(select);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RunContentDir(string select)
        {
            lbDirectory.Items.Clear();
            var directories = Directory.GetDirectories(select);
            foreach (var d in directories)
                lbDirectory.Items.Add(d);
        }
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            var select = lbFile.SelectedItem as string;
            var directory = Path.GetDirectoryName(select);

            var r = MessageBox.Show($"вы уверены, что хотите удалить файл {select}?", "Вопрос", MessageBoxButton.YesNo);
            if (r == MessageBoxResult.Yes)
            {
                File.Delete(select);
                MessageBox.Show("Файл удален");
                RunContentFile(directory);
            }
        }

        private void RunContentFile(string directory)
        {
            try
            {
                lbFile.Items.Clear();
                foreach (var d in Directory.GetFiles(directory))
                    lbFile.Items.Add(d);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
