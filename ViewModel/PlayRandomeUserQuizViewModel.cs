using QuizGameWPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace QuizGameWPF.ViewModel
{
    public class PlayRandomeUserQuizViewModel : INotifyPropertyChanged
    {
        public Quiz Quiz { get; set; }
        public string QuizTitle => Quiz?.QuizTitle ?? "";
        public string Category => $"{CurrentQuestion?.Category}";
        public Questions? CurrentQuestion { get; set; } // Made nullable
        public int SelectedAnswerIndex { get; set; }
        public int CorrectAnswers { get; set; }
        public int TotalAnswered { get; set; }
        public string ScoreText
        {
            get
            {
                int percent = 0;
                if (TotalAnswered > 0)
                {
                    percent = (int)((double)CorrectAnswers / TotalAnswered * 100);
                }
                return $"Correct answers: {CorrectAnswers}/{TotalAnswered} ({percent}%)";
            }
        }

        public PlayRandomeUserQuizViewModel()
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "QuizGameWPF", "DefaultData", "DataJSON");
            string filePath = Path.Combine(folderPath, "QuizAddedByUser.json");

            List<Quiz> quizzes = null;
            Quiz = null;
            CurrentQuestion = null;
            SelectedAnswerIndex = -1;

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    quizzes = string.IsNullOrWhiteSpace(json)
                        ? new List<Quiz>()
                        : JsonSerializer.Deserialize<List<Quiz>>(json) ?? new List<Quiz>();

                    if (quizzes.Count > 0)
                    {
                        // Pick a random quiz from the list
                        var random = new Random();
                        Quiz = quizzes[random.Next(quizzes.Count)];
                        // Pick a random question from the chosen quiz
                        CurrentQuestion = Quiz.GetRandomeQuestion();
                        OnPropertyChanged(nameof(QuizTitle));
                    }
                    // If empty, don't assign Quiz/CurrentQuestion
                }
                // If file doesn't exist, do nothing (treat as empty)
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quiz: {ex.Message}");
            }

            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(ScoreText));
        }

        public bool HasQuestions => CurrentQuestion != null;

        public event PropertyChangedEventHandler? PropertyChanged; // Made nullable
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public event EventHandler OnGameOver;
        public void NextQuestion(int selectedIndex)
        {
            TotalAnswered++;
            // Check if the selected answer is correct
            if (CurrentQuestion != null && CurrentQuestion.IsCorrect(selectedIndex))
            {
                CorrectAnswers++;
            }

            // Get the next question
            CurrentQuestion = Quiz.GetRandomeQuestion();

            // If no more questions, raise game over event
            if (CurrentQuestion == null)
            {
                // Game over!
                OnGameOver?.Invoke(this, EventArgs.Empty);
                return;
            }
            OnPropertyChanged("CurrentQuestion");
            OnPropertyChanged("ScoreText");
            OnPropertyChanged("Category");
        }
    }
}

