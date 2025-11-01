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
            "QuizGameWPF", "DefaultData", "DataJSON");
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

            // Set default image only if not already set
            foreach (var question in quiz.Questions)
            {
                if (string.IsNullOrWhiteSpace(question.ImagePath))
                    question.ImagePath = "00.png"; // Only store the file name, not the full path
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
