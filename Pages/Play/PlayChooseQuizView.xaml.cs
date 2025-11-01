using QuizGameWPF.ViewModel;
using System.Windows;
using System.Windows.Controls;

namespace QuizGameWPF.Pages.Play
{
    /// <summary>
    /// Interaction logic for PlayChooseQuiz.xaml
    /// </summary>
    public partial class PlayChooseQuizView : UserControl
    {
        public PlayQuizGBGViewModel ViewModelGBG { get; set; }
        public PlayRandomeUserQuizViewModel ViewModelRandome { get; set; }

        public PlayChooseQuizView()
        {
            InitializeComponent();
            ViewModelGBG = new PlayQuizGBGViewModel();
            DataContext = ViewModelGBG;

            ViewModelRandome = new PlayRandomeUserQuizViewModel();
            DataContext = ViewModelRandome;

            // Disable play button if no user-created quizzes
            PlayRandomUserQuizButton.IsEnabled = QuizGameWPF.Helpers.AppDataHelper.HasUserQuizzes();

            PlayRandomUserQuizButton.Visibility = QuizGameWPF.Helpers.AppDataHelper.HasUserQuizzes()
                ? Visibility.Visible
                : Visibility.Collapsed;
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowMainMenu(); // This returns to MainMenuView
        }

        private void PlayGBGQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
                mainWindow.MainContent.Content = new PlayGBGQuizView();
        }
        private void PlayRandomeUserQuiz_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            if (mainWindow != null)
                mainWindow.MainContent.Content = new PlayRandomeUserQuizView();
        }
    }
}
