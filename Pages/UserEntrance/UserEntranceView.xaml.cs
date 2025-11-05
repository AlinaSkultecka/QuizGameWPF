using QuizGameWPF.Models;
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

namespace QuizGameWPF.Pages.UserEntrance
{
    public partial class UserEntranceView : UserControl
    {
        public UserEntranceView()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            string userName = NameInput.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                GreetingText.Text = "Please enter your name.";
                return;
            }

            UserSession.CurrentUser = new User
            {
                UserName = userName,
                HighScore = 0 // Or load from file/database if you have persistence
            };

            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                mainWindow.MainContent.Content = new MainMenuView();
            }
        }

        private void NameInput_TextChanged(object sender, TextChangedEventArgs e)
        {
            PlaceholderText.Visibility = string.IsNullOrWhiteSpace(NameInput.Text)
                ? Visibility.Visible
                : Visibility.Collapsed;
        }
    }
}
