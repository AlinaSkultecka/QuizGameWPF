using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGameWPF.Models
{
    public class Questions
    {
        public int ID { get; set; }
        public string CreatedDate { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        public string EditedDate { get; set; } = "";
        public string Category { get; set; } = string.Empty;
        public string QuestionText { get; set; } = string.Empty;
        public string[] Answers { get; set; } = Array.Empty<string>();
        public int CorrectAnswerIndex { get; set; }
        public string? ImagePath { get; set; }

        public string AbsoluteImagePath
        {
            get
            {
                // Handle missing or empty ImagePath
                string fileName = ImagePath ?? "";

                // If your JSON is "DefaultData/Images/23.png", strip the prefix:
                if (fileName.StartsWith("DefaultData/Images/", StringComparison.OrdinalIgnoreCase))
                    fileName = fileName.Substring("DefaultData/Images/".Length);

                // Use "00.png" fallback if missing
                if (string.IsNullOrWhiteSpace(fileName))
                    fileName = "00.png";

                // Build the absolute path
                return Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                    "QuizGameWPF", "DefaultData", "Images", fileName
                );
            }
        }

        public Questions() { }

        public Questions(
            int id,
            string category,
            string questionText,
            int correctAnswerIndex,
            string[] answers,
            string createdDate,
            string editedDate,
            string? imagePath = null
        )
        {
            ID = id;
            Category = category;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
            CreatedDate = createdDate ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            EditedDate = editedDate;
            ImagePath = imagePath;
        }

        public bool IsCorrect(int selectedIndex)
        {
            return selectedIndex == CorrectAnswerIndex;
        }

        public override string ToString()
        {
            return $"{QuestionText} (Correct: {Answers[CorrectAnswerIndex]})";
        }
    }
}

