using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace QuizGameWPF.Models
{
    public class Quiz
    {
        public string QuizTitle { get; set; }
        public List<Questions> Questions { get; set; }

        [JsonIgnore]
        private List<Questions> _remainingQuestions;

        [JsonIgnore]
        public Random Randomizer { get; set; }


        public Quiz(string quizTitle = "")
        {
            QuizTitle = quizTitle;
            Questions = new List<Questions>();
            Randomizer = new Random();
            _remainingQuestions = new List<Questions>();
        }

        /// <summary>
        /// Returns one random question that hasn't been used yet.
        /// When all are used, returns null.
        /// </summary>
        public Questions? GetRandomeQuestion()
        {
            // If this is the first call or all used up, reset
            if (_remainingQuestions == null || _remainingQuestions.Count == 0)
            {
                _remainingQuestions = new List<Questions>(Questions);
                // Optional: shuffle the list for better randomness
                _remainingQuestions = _remainingQuestions.OrderBy(q => Randomizer.Next()).ToList();
            }

            if (_remainingQuestions.Count == 0)
                return null;

            // Take the first remaining question
            var next = _remainingQuestions[0];
            _remainingQuestions.RemoveAt(0);
            return next;
        }

        public void AddQuestion(string questionText, int correctAnswerIndex, params string[] answers)
        {
            var q = new Questions
            {
                ID = Questions.Count + 1,
                QuestionText = questionText,
                Answers = answers,
                CorrectAnswerIndex = correctAnswerIndex,
                Category = "General",
                CreatedDate = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
                EditedDate = ""
            };

            Questions.Add(q);
        }

        //public void RemoveQuestion(int index)
        //{

        //}

    }
}
