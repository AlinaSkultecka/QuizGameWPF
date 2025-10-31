using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using QuizGameWPF.Models;

namespace QuizGameWPF.Helpers
{
    public static class QuizSaver
    {
        private static readonly string filePath = @"C:\Users\grigo\Desktop\C#\3_Labbs\QuizGameWPF\AppData\Local\QuizAddedByUser.json";

        public static void SaveQuiz(Quiz quiz)
        {
            List<Quiz> allQuizzes;

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);

                // --- THIS CHECK FIXES YOUR BUG ---
                if (string.IsNullOrWhiteSpace(json))
                    allQuizzes = new List<Quiz>();
                else
                    allQuizzes = JsonSerializer.Deserialize<List<Quiz>>(json) ?? new List<Quiz>();
            }
            else
            {
                allQuizzes = new List<Quiz>();
            }

            // (Add or replace quiz and write as before)
            allQuizzes.RemoveAll(q => q.QuizTitle == quiz.QuizTitle);
            allQuizzes.Add(quiz);

            var options = new JsonSerializerOptions { WriteIndented = true };
            string updatedJson = JsonSerializer.Serialize(allQuizzes, options);
            File.WriteAllText(filePath, updatedJson);
        }
    }
}
