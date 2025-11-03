using QuizGameWPF.Models;
using QuizGameWPF.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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
using QuizGameWPF.Helpers;

namespace QuizGameWPF
{
    public partial class EditQuiz : UserControl
    {
        public PlayRandomeUserQuizViewModel ViewModel { get; set; }
        
        private List<Quiz> loadedQuizzes;
        private Quiz currentQuiz;
        private Questions currentQuestion;

        public EditQuiz()
        {
            InitializeComponent();
            ViewModel = new PlayRandomeUserQuizViewModel();
            DataContext = ViewModel;
            LoadQuizzes();
        }

        private void ReturnToMenu_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = Application.Current.MainWindow as MainWindow;
            mainWindow?.ShowMainMenu(); // This returns to MainMenuView
        }

        private void LoadQuizzes()
        {
            string folderPath = System.IO.Path.Combine(
               Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               "QuizGameWPF", "DefaultData", "DataJSON");
            string filePath = System.IO.Path.Combine(folderPath, "QuizAddedByUser.json");
            string json = File.ReadAllText(filePath);

            loadedQuizzes = JsonSerializer.Deserialize<List<Quiz>>(json);

            // Fill quiz ComboBox with titles
            QuizComboBox.ItemsSource = loadedQuizzes.Select(q => q.QuizTitle).ToList();
        }

        private void QuizComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuizComboBox.SelectedItem == null)
                return;

            string selectedTitle = QuizComboBox.SelectedItem.ToString();
            currentQuiz = loadedQuizzes.FirstOrDefault(q => q.QuizTitle == selectedTitle);

            if (currentQuiz != null)
            {
                // Set the title in the textbox
                QuizTitleTextBox.Text = currentQuiz.QuizTitle;

                // Fill question ComboBox with question texts
                QuestionComboBox.ItemsSource = currentQuiz.Questions.Select(q => q.QuestionText).ToList();

                // Optionally clear question fields if you want
                QuestionTextBox.Clear();
                Answer1Box.Clear();
                Answer2Box.Clear();
                Answer3Box.Clear();
                CorrectIndexBox.Clear();
            }
        }

        private void QuestionComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (QuestionComboBox.SelectedItem == null || currentQuiz == null)
                return;

            string questionText = QuestionComboBox.SelectedItem.ToString();
            var question = currentQuiz.Questions.FirstOrDefault(q => q.QuestionText == questionText);

            if (question != null)
            {
                currentQuestion = question;

                QuestionTextBox.Text = question.QuestionText;
                Answer1Box.Text = question.Answers?.ElementAtOrDefault(0) ?? "";
                Answer2Box.Text = question.Answers?.ElementAtOrDefault(1) ?? "";
                Answer3Box.Text = question.Answers?.ElementAtOrDefault(2) ?? "";
                CorrectIndexBox.Text = (question.CorrectAnswerIndex + 1).ToString(); // user sees 1-based index
            }
        }

        private void SaveQuestion_Click(object sender, RoutedEventArgs e)
        {
            // Collect data from TextBoxes
            string questionText = QuestionTextBox.Text;
            string answer1 = Answer1Box.Text;
            string answer2 = Answer2Box.Text;
            string answer3 = Answer3Box.Text;
            
            // Safely parse the correct answer index
            if (!int.TryParse(CorrectIndexBox.Text, out int correctIndex) || correctIndex < 1 || correctIndex > 3)
            {
                MessageBox.Show("Please enter a valid correct answer index (1, 2, or 3).");
                return;
            }

            // If editing, update the existing question
            if (currentQuestion != null)
            {
                currentQuestion.QuestionText = questionText;
                currentQuestion.Answers = new List<string> { answer1, answer2, answer3 };
                currentQuestion.CorrectAnswerIndex = correctIndex - 1; // 0-based
                currentQuestion.EditedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                // Optionally update other fields, like ImagePath or Category if your UI supports it
            }
            else
            {
                MessageBox.Show("There are no questions in this quiz to edit. Please select a question or add a new one.");
                return;
            }

            SaveQuizzesToFile();

            // Notify user
            MessageBox.Show("Question saved!");

            // Clear fields for next time
            QuestionTextBox.Clear();
            Answer1Box.Clear();
            Answer2Box.Clear();
            Answer3Box.Clear();
            CorrectIndexBox.Clear();
            QuestionTextBox.Focus();
            currentQuestion = null;
        }

        // Save JSON back to file
        private void SaveQuizzesToFile()
        {
            string folderPath = System.IO.Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "QuizGameWPF", "DefaultData", "DataJSON");
            string filePath = System.IO.Path.Combine(folderPath, "QuizAddedByUser.json");

            string json = JsonSerializer.Serialize(loadedQuizzes, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
        }

        private void DeleteQuestion_Click(object sender, RoutedEventArgs e)
        {
            if (currentQuiz == null || currentQuestion == null)
            {
                MessageBox.Show("No question selected to delete.");
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this question?",
                "Delete Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                currentQuiz.Questions.Remove(currentQuestion);
                SaveQuizzesToFile();

                // Refresh question list
                QuestionComboBox.ItemsSource = currentQuiz.Questions.Select(q => q.QuestionText).ToList();

                // Clear fields
                QuestionTextBox.Clear();
                Answer1Box.Clear();
                Answer2Box.Clear();
                Answer3Box.Clear();
                CorrectIndexBox.Clear();
                currentQuestion = null;

                MessageBox.Show("Question deleted!");

                // New logic: If no questions are left, ask about deleting the quiz
                if (currentQuiz.Questions.Count == 0)
                {
                    var quizDeleteResult = MessageBox.Show(
                        $"There are no questions left in \"{currentQuiz.QuizTitle}\".\n" +
                        "Do you want to delete the whole quiz?",
                        "Delete Empty Quiz?",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    if (quizDeleteResult == MessageBoxResult.Yes)
                    {
                        loadedQuizzes.Remove(currentQuiz);
                        SaveQuizzesToFile();

                        // Refresh quiz list and UI
                        QuizComboBox.ItemsSource = loadedQuizzes.Select(q => q.QuizTitle).ToList();
                        QuizTitleTextBox.Clear();
                        QuestionComboBox.ItemsSource = null;
                        MessageBox.Show("Quiz deleted!");
                        currentQuiz = null;
                    }
                    else
                    {
                        MessageBox.Show("Quiz kept, but is now empty.");
                        // Optionally clear question selection/UI here as well
                    }
                }
            }
        }
    }
}
