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
            ViewModelRandome = new PlayRandomeUserQuizViewModel();
            InitializeComponent();
            DataContext = ViewModelRandome;
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
