using System.Windows;
using System.Windows.Controls;

namespace QuizGameWPF
{
    public partial class MainMenuView : UserControl
    {
        public MainMenuView()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            // Find the MainWindow and switch to the quiz view
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.PlayChooseQuiz();
        }

        private void Add_Question_Click(object sender, RoutedEventArgs e)
        {
            // Find the MainWindow and switch to the add question view
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.AddTitle();
        }

        private void Edit_Question_Click(object sender, RoutedEventArgs e)
        {
            // Find the MainWindow and switch to edit question view
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.EditQuestion();
        }

    }
}
