using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGameWPF.Models
{
    public class User
    {
        public string UserName { get; set; }
        public int HighScore { get; set; } 
    }

    public static class UserSession
    {
        public static User CurrentUser { get; set; }
    }
}
