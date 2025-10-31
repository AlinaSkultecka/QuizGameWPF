using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Windows;
using QuizGameWPF.Models;


namespace QuizGameWPF.ViewModel
{
    public class PlayQuizGBGViewModel : INotifyPropertyChanged
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

        public PlayQuizGBGViewModel()
        {
            string filePath = @"C:\Users\grigo\Desktop\C#\3_Labbs\QuizGameWPF\QuizGBGEnglish.json";

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var quiz = JsonSerializer.Deserialize<Quiz>(json);

                    if (quiz != null && quiz.Questions != null && quiz.Questions.Count > 0)
                    {
                        Quiz = quiz;  // <-- Use loaded quiz directly!
                        CurrentQuestion = Quiz.GetRandomeQuestion();
                        OnPropertyChanged(nameof(QuizTitle));
                    }
                    else
                    {
                        MessageBox.Show("No questions found in JSON file.");
                        Quiz = new Quiz("Empty Quiz");
                        CurrentQuestion = null;
                    }
                }
                else
                {
                    MessageBox.Show("Question file not found.");
                    Quiz = new Quiz("Test Quiz (Default)");
                    Quiz.AddQuestion("Fallback: What is 2+2?", 1, "3", "4", "5");
                    CurrentQuestion = Quiz.GetRandomeQuestion();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading quiz: {ex.Message}");
                Quiz = new Quiz("Error Quiz");
                CurrentQuestion = null;
            }

            SelectedAnswerIndex = -1;
            OnPropertyChanged(nameof(CurrentQuestion));
            OnPropertyChanged(nameof(ScoreText));

        }

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
