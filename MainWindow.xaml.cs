using QuizGameWPF.Pages.Add;
using QuizGameWPF.Pages.Play;
using System.Windows;

namespace QuizGameWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            // Checking if the default data folder exists on User`s computer (AppData\Local) before showing the Main Menu
            QuizGameWPF.Helpers.AppDataHelper.EnsureAppDataFolder();

            InitializeComponent();
            MainContent.Content = new QuizGameWPF.Pages.UserEntrance.UserEntranceView();
        }

        public void ShowMainMenu()
        {
            MainContent.Content = new MainMenuView();
        }

        // Navigate to Play Quiz
        public void PlayChooseQuiz()
        {
            MainContent.Content = new PlayChooseQuizView();
        }

        // Navigate to Add Quiz
        public void AddTitle()
        {
            MainContent.Content = new AddTitle();
        }

        // Navigate to Edit Quiz
        public void EditQuestion()
        {
            MainContent.Content = new EditQuiz();
        }
    }
}