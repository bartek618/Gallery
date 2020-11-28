using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Gallery
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Item> List { get; set; } = new ObservableCollection<Item>();
        public MainWindow()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            MainImage.Zoom(1.2);
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            MainImage.Zoom(0.8);
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                List<string> _list = Directory.GetFiles(folderBrowserDialog.SelectedPath).ToList();

                List.Clear();
                foreach (string item in _list)
                {
                    List.Add(new Item { Value = item });
                }
            }
        }
        private void Images_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MainImage.LoadImage((e.AddedItems[0] as Item).Value);
        }
    }
    public class Item
    {
        public string Value { get; set; }
    }

}
