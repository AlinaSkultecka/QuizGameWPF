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

namespace QuizGameWPF.Pages.Play
{
    /// <summary>
    /// Interaction logic for GameOverView.xaml
    /// </summary>
    public partial class GameOverView : UserControl
    {
        public PlayQuizGBGViewModel ViewModelGBG { get; set; }
        public PlayRandomeUserQuizViewModel ViewModelRandome { get; set; }

        public string FinalScore
        {
            get { return (string)GetValue(FinalScoreProperty); }
            set { SetValue(FinalScoreProperty, value); }
        }

        public static readonly DependencyProperty FinalScoreProperty =
            DependencyProperty.Register("FinalScore", typeof(string), typeof(GameOverView), new PropertyMetadata(""));

        public GameOverView()
        {
            InitializeComponent();            
            ViewModelGBG = new PlayQuizGBGViewModel();
            DataContext = ViewModelGBG;

            ViewModelRandome = new PlayRandomeUserQuizViewModel();
            DataContext = ViewModelRandome;
        }
        public GameOverView(string scoreText)
        {
            InitializeComponent();
            FinalScore = scoreText;
            DataContext = this; // Bind FinalScore to XAML
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
