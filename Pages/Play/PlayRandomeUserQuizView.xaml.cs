using System.Windows;
using System.Windows.Controls;
using QuizGameWPF.ViewModel;

namespace QuizGameWPF.Pages.Play
{
    
    public partial class PlayRandomeUserQuizView : UserControl
    {
        public PlayRandomeUserQuizViewModel ViewModelRandome { get; set; }
        public PlayRandomeUserQuizView()
        {
            InitializeComponent();
            
            // Instantiate the ViewModel
            ViewModelRandome = new PlayRandomeUserQuizViewModel();
            
            // Set the DataContext for data binding
            DataContext = ViewModelRandome;

            // Subscribe to the event
            ViewModelRandome.OnGameOver += ViewModelRandome_OnGameOver;
        }

        private void ViewModelRandome_OnGameOver(object sender, EventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
            {
                // Switch to GameOverView with final score
                mainWindow.MainContent.Content = new GameOverView(ViewModelRandome.ScoreText);
            }
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is string tagString && int.TryParse(tagString, out int selectedIndex))
                {
                    ViewModelRandome.NextQuestion(selectedIndex);
                }
            }
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowMainMenu(); // This returns to MainMenuView
        }
    }
}
