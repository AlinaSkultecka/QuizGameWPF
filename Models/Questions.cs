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
        public string CreatedDate { get; set; }
        public string EditedDate { get; set; }
        public string Category { get; set; }
        public string QuestionText { get; set; }
        public string[] Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }

        public Questions() { }

        public Questions(
            int id,
            string category,
            string questionText,
            int correctAnswerIndex,
            string[] answers,
            string createdDate = null,
            string editedDate = ""
        )
        {
            ID = id;
            Category = category;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
            CreatedDate = createdDate ?? DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            EditedDate = editedDate;
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

