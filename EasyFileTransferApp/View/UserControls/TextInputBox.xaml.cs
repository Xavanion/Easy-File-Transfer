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
    /// Interaction logic for TextInputBox.xaml
    /// </summary>
    public partial class TextInputBox : UserControl
    {
        public TextInputBox()
        {
            InitializeComponent();
        }

        public string Text
        {
            get { return txtInput.Text; }
            set { txtInput.Text = value; }
        }

        private string? placeholder;

        public string? Placeholder
        {
            get { return placeholder; }
            set {
                placeholder = value;
                txtInput.Text = placeholder;
            }
        }


        private void TxtInput_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtInput.Text == placeholder)
            {
                txtInput.Text = "";
                txtInput.Foreground = Brushes.Black;
            }
        }

        private void TxtInput_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtInput.Text))
            {
                txtInput.Text = placeholder;
                txtInput.Foreground = Brushes.DarkGray;
            }
        }

        private void TxtButton_Click(object sender, RoutedEventArgs e)
        {
            txtInput.Clear();
            txtInput.Focus();
        }
    }
}
