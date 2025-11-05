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

        [JsonIgnore]  // Exclude from JSON serialization (do not see this property in JSON file)
        private List<Questions>? _remainingQuestions;  //private internal field to store data inside the class

        [JsonIgnore]
        public Random Randomizer { get; set; }


        public Quiz(string quizTitle = "")
        {
            QuizTitle = quizTitle;
            Questions = new List<Questions>();
            Randomizer = new Random();
            _remainingQuestions = null;
        }

        
        public Questions? GetRandomeQuestion()
        {
            // Only refill ONCE, at the start
            if (_remainingQuestions == null)
            {
                _remainingQuestions = new List<Questions>(Questions) // Returns one random question that hasn't been used yet.
                    .OrderBy(q => Randomizer.Next())
                    .ToList();
            }

            if (_remainingQuestions.Count == 0)  // When all are used, returns null.
                return null;

            // Take the first remaining question
            var next = _remainingQuestions[0];
            _remainingQuestions.RemoveAt(0);
            return next;
        }

        public void AddQuestion(string questionText, int correctAnswerIndex, List<string> answers)
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
    }
}
