using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QuizGameWPF.Models;

namespace QuizGameWPF.Helpers
{
    public static class QuizSaver
    {
        // Define folder and file paths (AppData\Local\QuizGameWPF\DataJSON\QuizAddedByUser.json)
        private static readonly string folderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "QuizGameWPF", "DataJSON");
        private static readonly string filePath = Path.Combine(folderPath, "QuizAddedByUser.json");
        
        public static void SaveQuiz(Quiz quiz)
        {
            // Creates the folder if it doesn't exist 
            Directory.CreateDirectory(folderPath);

            // Load existing quizzes
            List<Quiz> allQuizzes;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                // Handle empty file case
                if (string.IsNullOrWhiteSpace(json))
                    allQuizzes = new List<Quiz>();
                else
                    allQuizzes = JsonSerializer.Deserialize<List<Quiz>>(json) ?? new List<Quiz>();
            }
            else
            {
                allQuizzes = new List<Quiz>();
            }

            // SET DEFAULT IMAGE FOR ALL QUESTIONS HERE
            string defaultImagePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "QuizGameWPF", "Images", "00.png");

            foreach (var question in quiz.Questions)
            {
                question.ImagePath = defaultImagePath;
            }

            // Update or add the quiz
            allQuizzes.RemoveAll(q => q.QuizTitle == quiz.QuizTitle);
            allQuizzes.Add(quiz);

            // Save back to file
            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(allQuizzes, options);
            File.WriteAllText(filePath, updatedJson);
        }
    }
}
