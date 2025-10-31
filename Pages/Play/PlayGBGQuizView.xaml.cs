using System.Windows;
using System.Windows.Controls;
using QuizGameWPF.ViewModel;

namespace QuizGameWPF
{
    public partial class PlayGBGQuizView : UserControl
    {
        public PlayQuizGBGViewModel ViewModelGBG { get; set; }
        public PlayGBGQuizView()
        {
            InitializeComponent();
            // Instantiate the ViewModel
            ViewModelGBG = new PlayQuizGBGViewModel();
            // Set the DataContext for data binding
            DataContext = ViewModelGBG;
        }

        private void AnswerButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button button)
            {
                if (button.Tag is string tagString && int.TryParse(tagString, out int selectedIndex))
                {
                    ViewModelGBG.NextQuestion(selectedIndex);
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
