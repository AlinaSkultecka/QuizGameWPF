# Quiz Game Gothenburg

A graphical **Quiz Game** built with **C#**, **.NET**, and **WPF (XAML)**.  
Players can **play**, **edit**, or **create** quizzes — each in separate views using `UserControls`.

## Features

###  🎮  **Play Mode**
Play quizzes with multiple-choice questions (three options each).  
There are **two types of quizzes**:
1. **Built-in “Gothenburg” Quiz**   
       - Comes preloaded with questions about Gothenburg.  
       - Cannot be edited or deleted by the user.  
    
2. **User-Created Quizzes**   
       - Created and saved by the user inside the app.  
       - Stored as JSON files in AppData.  
       - Initially, this section is **empty**, and the *“Play Random User Quiz”* button is **disabled** until the user creates a quiz.

While playing, questions appear in **random order**, and after each answer you’ll see:
- ✅ Total correct answers  
- 📊 Percentage of correct answers

###  ✏️ **Edit Mode:**  
- Choose the quize and question in the menu to edit it.  
- Allows modifying the quiz title and question text or any of its answers.  
- Supports deleting individual questions.  
- If all questions are deleted, the app asks whether to:
  - ❌ Delete the quiz file, or  
  - 📂 Keep the empty quiz.

####  🧠 **Create Mode:**  
- Lets you name a new quiz and add as many questions as you want.  
- Each question includes **three answer choices** and one **correct answer**.  
- Quizzes are saved asynchronously in **JSON format**.


###  💾 **JSON Data:**
All quizzes and questions are saved and loaded from JSON files in your local AppData.
(C:\Users\<YourName>\AppData\Local\QuizGameWPF\DefaultData\)


### 🔮 Future Improvements
- **Translate** the app and Gothenburg quizzes into **Swedish**, and make the app available in **two languages**.  
- **Add category filters** to play Gothenburg quizzes by topic.  
- **Add user-related features**, such as saving each user's score and displaying a **top players leaderboard**.  
- **Enable import and export** of quizzes to share them with other users.  
- **Improve the WPF UI** with animations and adaptive layouts.  
- **Build a cross-platform version** using **.NET MAUI** (the app currently runs only on Windows).


### Sources
The questions about Gothenburg are taken from Höglund, B. (2023). GöteborgQuiz 2 : Har du koll på Göteborg? Ordalaget Bokförlag.


### Wireframe of the program
The overall layout and navigation flow of the **Quiz Game** application.  
It shows how users move between the main sections of the program and what each view contains.

<img width="613" height="1125" alt="Wireframe" src="https://github.com/user-attachments/assets/137b13b1-96f0-41c3-9b54-199704fbe4c7" />
