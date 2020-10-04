using ChatClient.ViewModels;
using ChatClient.Views;
using MaterialDesignThemes.Wpf;
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
        public MainViewModel MainViewModel { get; set; }

        public MainWindow()
        {
            MainViewModel = new MainViewModel();
            DataContext = MainViewModel;
            MainViewModel.CloseWindowRequest_Event += ClosingWindow_Request;
            MainViewModel.ConnexionFailed_Event += ConnexionFailed_EventHandler;
            InitializeComponent();
        }

        private void TBXUsername_TextChanged(object sender, TextChangedEventArgs e)
        {
            BTNLogin.IsEnabled = string.IsNullOrWhiteSpace(TBXUsername.Text) ? false : true;
        }

        private void BTNLogin_Click(object sender, RoutedEventArgs e)
        {
            MainViewModel.Login();
        }

        private void ConnexionFailed_EventHandler()
        {
            var messageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(2000));
            ConnexionSnackBar.MessageQueue = messageQueue;
            ConnexionSnackBar.MessageQueue.Enqueue("Connexion to hub failed");
        }

        private void ClosingWindow_Request()
        {
            this.Close();
        }
    }
}
