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
using System.IO;
using System.Text.Json;
using QuizGameWPF.Helpers;
using QuizGameWPF.Models;
using QuizGameWPF.ViewModel;


namespace QuizGameWPF
{
    /// <summary>
    /// Interaction logic for AddNewQuestion.xaml
    /// </summary>
    public partial class AddNewQuestion : UserControl
    {
        private Quiz currentQuiz;
        public AddNewQuestion(string title)
        {
            InitializeComponent();
            currentQuiz = new Quiz(title);
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowMainMenu(); // This returns to MainMenuView
        }


        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            // Collect data from TextBoxes
            string questionText = QuestionTextBox.Text;
            string answer1 = Answer1Box.Text;
            string answer2 = Answer2Box.Text;
            string answer3 = Answer3Box.Text;
            int correctIndex = int.Parse(CorrectIndexBox.Text);

            // Create question
            var newQuestion = new Questions
            {
                ID = currentQuiz.Questions.Count + 1,
                QuestionText = questionText,
                Answers = new List<string> { answer1, answer2, answer3 },
                CorrectAnswerIndex = correctIndex - 1,
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Category = "User's question"
            };

            if (string.IsNullOrWhiteSpace(questionText) ||
                string.IsNullOrWhiteSpace(answer1) ||
                string.IsNullOrWhiteSpace(answer2) ||
                string.IsNullOrWhiteSpace(answer3) ||
                !(correctIndex >= 1 && correctIndex <= 3))
            {
                MessageBox.Show("Please fill out all fields and enter a correct answer index (1-3).");
                return;
            }

            currentQuiz.Questions.Add(newQuestion);

            // Optionally, inform the user
            MessageBox.Show("Question added!");

            // Clear for next
            QuestionTextBox.Clear();
            Answer1Box.Clear();
            Answer2Box.Clear();
            Answer3Box.Clear();
            CorrectIndexBox.Clear();
            QuestionTextBox.Focus();
        }

        private void FinishAddingQuiz_Click(object sender, RoutedEventArgs e)
        {
            // Collect data from TextBoxes
            string questionText = QuestionTextBox.Text;
            string answer1 = Answer1Box.Text;
            string answer2 = Answer2Box.Text;
            string answer3 = Answer3Box.Text;

            // Try to parse correct index, handle if not an integer
            if (!int.TryParse(CorrectIndexBox.Text, out int correctIndex) ||
                string.IsNullOrWhiteSpace(questionText) ||
                string.IsNullOrWhiteSpace(answer1) ||
                string.IsNullOrWhiteSpace(answer2) ||
                string.IsNullOrWhiteSpace(answer3) ||
                !(correctIndex >= 1 && correctIndex <= 3))
            {
                MessageBox.Show("Please fill out all fields and enter a correct answer index (1-3).");
                return;
            }

            // Only create the question if validation passed!
            var newQuestion = new Questions
            {
                ID = currentQuiz.Questions.Count + 1,
                QuestionText = questionText,
                Answers = new List<string> { answer1, answer2, answer3 },
                CorrectAnswerIndex = correctIndex - 1,
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                Category = "User's question"
            };

            currentQuiz.Questions.Add(newQuestion);

            QuizGameWPF.Helpers.QuizSaver.SaveQuiz(currentQuiz);

            MessageBox.Show("Your quiz has been saved!");

            // Optionally navigate back to main menu
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowMainMenu();
        }
    }
}
