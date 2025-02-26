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

namespace EasyFileTransferApp.View.Pages
{
    /// <summary>
    /// Interaction logic for SharePage.xaml
    /// </summary>
    public partial class SharePage : Page
    {
        public SharePage()
        {
            InitializeComponent();
        }

        private void LocalScan_Click(object sender, RoutedEventArgs e)
        {
            // Start local scan of clients with app open
        }

        private void CreateGroup_Click(object sender, RoutedEventArgs e)
        {
            // Generate Code and open room
        }

        private void JoinGroup_Click(object sender, RoutedEventArgs e)
        {
            string groupCode = GroupCodeInput.Text;
            if (!string.IsNullOrWhiteSpace(groupCode))
            {
                // Handle Joining group with provided groupCode
            }
        }
    }
}
