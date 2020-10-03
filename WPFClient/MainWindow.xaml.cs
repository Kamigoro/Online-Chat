using ChatClient.Views;
using Microsoft.AspNetCore.SignalR.Client;
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

namespace WPFChatClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TBXUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            BTNLogin.IsEnabled = string.IsNullOrEmpty(TBXUsername.Text) ? false : true;
        }

        private void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            new ChatWindow(TBXUsername.Text).Show();
            this.Close();
        }
    }
}
