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
using QuizGameWPF.ViewModel;

namespace QuizGameWPF
{
    /// <summary>
    /// Interaction logic for EditQuestion.xaml
    /// </summary>
    public partial class EditQuestion : UserControl
    {
        public PlayQuizGBGViewModel ViewModel { get; set; }
        public EditQuestion()
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
    }
}
