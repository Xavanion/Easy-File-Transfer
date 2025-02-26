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

namespace EasyFileTransferApp.View.UserControls
{
    /// <summary>
    /// Interaction logic for DropAddFile.xaml
    /// </summary>
    public partial class DropAddFile : UserControl
    {
        public DropAddFile()
        {
            InitializeComponent();
        }

        // Responds to when the user releases the file
        private void File_Drop(object sender, DragEventArgs e)
        {
            BorderRec.Stroke = Brushes.White;

            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                if (files.Length > 0)
                {
                    foreach(string file in files)
                    {
                        // Pass each file to uploader
                    }
                }
            }
            else
            {
                Console.WriteLine("The Dropped data was not a file");
            }
        }

        // Responds to when the user clicks on the border to upload a file
        private void File_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            OpenFileDialog fileDialog = new()
            {
                Title = "Upload any file(s)",
                Filter = "All Files | *.*",
                Multiselect = true
            };
            bool? success = fileDialog.ShowDialog();
            if (success == true)
            {
                string[] filePaths = fileDialog.FileNames;

                if (filePaths.Length > 0)
                {
                    foreach(string file in filePaths)
                    {
                        // Pass each file to uploader
                    }
                }
            }
            else
            {
                Console.WriteLine("No File Selected");
            }
        }

        // 
        private void File_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                BorderRec.Stroke = Brushes.LightBlue;
            }
            else
            {
                BorderRec.Stroke = Brushes.MediumVioletRed;
            }
        }

        private void File_DragLeave(object sender, DragEventArgs e)
        {
            BorderRec.Stroke = Brushes.White;
        }

    }
}
