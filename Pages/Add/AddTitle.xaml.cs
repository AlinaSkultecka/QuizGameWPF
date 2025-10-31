using QuizGameWPF.Helpers;
using QuizGameWPF.Models;
using QuizGameWPF.ViewModel;
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


namespace QuizGameWPF.Pages.Add
{
    /// <summary>
    /// Interaction logic for AddTitle.xaml
    /// </summary>
    public partial class AddTitle : UserControl
    {
        public PlayQuizGBGViewModel ViewModel { get; set; }
        public AddTitle()
        {
            InitializeComponent();
            ViewModel = new PlayQuizGBGViewModel();
            DataContext = ViewModel;
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowMainMenu(); // This returns to MainMenuView
        }


        private void SaveQuizTitle_Click(object sender, RoutedEventArgs e)
        {
            string title = TitleTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(title))
            {
                MessageBox.Show("Please enter a quiz title!");
                return;
            }

            // Pass this title to the next window (where you add questions)
            var addQuestionsPage = new AddNewQuestion(title);

            var mainWindow = Application.Current.MainWindow as MainWindow;
            // direct assignment (if MainContent is named as such and is public/internal):
            mainWindow.MainContent.Content = addQuestionsPage;
        }
    }
}
