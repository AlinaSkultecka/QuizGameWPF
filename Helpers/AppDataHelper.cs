using QuizGameWPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;

namespace QuizGameWPF.Helpers
{
    public static class AppDataHelper
    {
        public static void EnsureAppDataFolder()
        {
            string baseAppData = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "QuizGameWPF");

            string defaultData = Path.Combine(baseAppData, "DefaultData");
            string jsonFolder = Path.Combine(defaultData, "DataJSON");
            string imageFolder = Path.Combine(defaultData, "Images");

            // Create folders if they don't exist
            Directory.CreateDirectory(jsonFolder);
            Directory.CreateDirectory(imageFolder);

            // Copy default quizzes from program folder to AppData
            string programJsonFolder = Path.Combine(AppContext.BaseDirectory, "DefaultData", "DataJSON");
            if (Directory.Exists(programJsonFolder))
            {
                foreach (var file in Directory.GetFiles(programJsonFolder, "*.json"))
                {
                    string destFile = Path.Combine(jsonFolder, Path.GetFileName(file));
                    if (!File.Exists(destFile))
                        File.Copy(file, destFile);
                }
            }

            // Copy default images from program folder to AppData
            string programImageFolder = Path.Combine(AppContext.BaseDirectory, "DefaultData", "Images");
            if (Directory.Exists(programImageFolder))
            {
                foreach (var file in Directory.GetFiles(programImageFolder))
                {
                    string destFile = Path.Combine(imageFolder, Path.GetFileName(file));
                    if (!File.Exists(destFile))
                        File.Copy(file, destFile);
                }
            }
        }

        public static bool HasUserQuizzes()
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "QuizGameWPF", "DefaultData", "DataJSON");
            string filePath = Path.Combine(folderPath, "QuizAddedByUser.json");

            if (!File.Exists(filePath))
                return false;

            string json = File.ReadAllText(filePath);
            if (string.IsNullOrWhiteSpace(json))
                return false;

            try
            {
                var quizzes = JsonSerializer.Deserialize<List<Quiz>>(json);
                return quizzes != null && quizzes.Count > 0;
            }
            catch
            {
                return false;
            }
        }
    }
}
