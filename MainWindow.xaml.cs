using QuizGameWPF.Pages.Add;
using QuizGameWPF.Pages.Play;
using System.Windows;

namespace QuizGameWPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContent.Content = new QuizGameWPF.Pages.UserEntrance.UserEntranceView();

        }

        public void ShowMainMenu()
        {
            MainContent.Content = new MainMenuView();
        }

        public void PlayChooseQuiz()
        {
            MainContent.Content = new PlayChooseQuizView();
        }

        public void AddTitle()
        {
            MainContent.Content = new AddTitle();
        }

        public void EditQuestion()
        {
            MainContent.Content = new EditQuestion();
        }
    }
}