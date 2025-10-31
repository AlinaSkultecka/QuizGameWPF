using System;
using System.Collections.Generic;
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

