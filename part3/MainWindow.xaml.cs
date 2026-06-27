using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PART2
{
    public partial class MainWindow : Window
    {
        private string userName = "";
        private List<string> activityLog = new List<string>();

        private int score = 0;
        private int qIndex = 0;
        private bool quizActive = false;

        private DatabaseHelper db = new DatabaseHelper();

        private List<(string q, string[] options, int ans, string explanation)> quiz =
            new List<(string, string[], int, string)>
        {
            ("What is phishing?", new[]{"Fake emails","Firewall","VPN","Antivirus"},0,"Fake emails used to steal data."),
            ("VPN is used for security", new[]{"True","False"},0,"Encrypts internet traffic."),
            ("Strong passwords are important", new[]{"True","False"},0,"Prevents hacking."),
            ("Public WiFi is safe", new[]{"True","False"},1,"It is unsafe."),
            ("2FA adds security", new[]{"True","False"},0,"Extra login layer."),
            ("Antivirus removes malware", new[]{"True","False"},0,"Detects threats."),
            ("Phishing is safe", new[]{"True","False"},1,"It is dangerous."),
            ("Updating passwords is good", new[]{"True","False"},0,"Improves safety."),
            ("VPN protects data", new[]{"True","False"},0,"Encrypts traffic."),
            ("Clicking unknown links is safe", new[]{"True","False"},1,"Risky behaviour.")
        };

        public MainWindow()
        {
            InitializeComponent();
            Loaded += MainWindow_Loaded;
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            AddBot("CyberBot Online 💖");
            AddBot("Type 'menu' to begin.");
        }

        // ================= INPUT =================
        private void SendButton_Click(object sender, RoutedEventArgs e)
        {
            string input = InputTextBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                AddBot("Please type something.");
                return;
            }

            AddUser(input);
            HandleInput(input.ToLower());
            InputTextBox.Clear();
        }

        // ================= MAIN LOGIC =================
        private void HandleInput(string input)
        {
            Log(input);

            if (input.Contains("menu"))
            {
                AddBot("MENU 💖:\nquiz | add task | tasks | test sql | log");
            }

            else if (input.Contains("my name is"))
            {
                userName = input.Replace("my name is", "").Trim();
                AddBot($"Nice to meet you {userName} 💖");
            }

            else if (input.Contains("quiz"))
            {
                StartQuiz();
            }

            else if (input.Contains("add task"))
            {
                db.AddTask("Manual Task", "Added via chatbot", DateTime.Now.AddDays(1));
                AddBot("Task added 💖");
            }

            else if (input.Contains("tasks"))
            {
                ShowTasks();
            }

            else if (input.Contains("test sql"))
            {
                TestSql();
            }

            else if (input.Contains("log"))
            {
                ShowLog();
            }

            else if (quizActive && int.TryParse(input, out int ans))
            {
                CheckAnswer(ans - 1);
            }

            else
            {
                AddBot("Type 'menu' 💖");
            }
        }

        // ================= SQL TEST =================
        private void TestSql()
        {
            try
            {
                var tasks = db.GetTasks();
                AddBot("SQL CONNECTION SUCCESS 💖");
                AddBot($"Tasks in DB: {tasks.Count}");
            }
            catch (Exception ex)
            {
                AddBot("SQL FAILED ❌");
                AddBot(ex.Message);
            }
        }

        // ================= QUIZ =================
        private void StartQuiz()
        {
            quizActive = true;
            score = 0;
            qIndex = 0;

            AddBot("Quiz started 💖");
            AskQuestion();
        }

        private void AskQuestion()
        {
            if (qIndex >= quiz.Count)
            {
                quizActive = false;
                AddBot($"Final Score: {score}/{quiz.Count}");
                AddBot("Great job 💖");
                return;
            }

            var q = quiz[qIndex];

            string msg = q.q + "\n";
            for (int i = 0; i < q.options.Length; i++)
                msg += $"{i + 1}. {q.options[i]}\n";

            AddBot(msg);
        }

        private void CheckAnswer(int answer)
        {
            var q = quiz[qIndex];

            if (answer == q.ans)
            {
                score++;
                AddBot("Correct 💖");
            }
            else
            {
                AddBot("Wrong ❌ " + q.explanation);
            }

            qIndex++;
            AskQuestion();
        }

        // ================= TASKS =================
        private void ShowTasks()
        {
            var tasks = db.GetTasks();

            AddBot("TASKS 💖");
            foreach (var t in tasks)
                AddBot(t);
        }

        // ================= LOG =================
        private void Log(string action)
        {
            activityLog.Add($"{DateTime.Now:HH:mm:ss} - {action}");

            if (activityLog.Count > 10)
                activityLog.RemoveAt(0);
        }

        private void ShowLog()
        {
            AddBot("ACTIVITY LOG 💖");
            foreach (var l in activityLog)
                AddBot(l);
        }

        // ================= UI =================
        private void AddUser(string msg)
        {
            ChatPanel.Children.Add(new TextBlock
            {
                Text = "🧑 " + msg,
                Foreground = Brushes.White
            });

            ScrollViewer.ScrollToEnd();
        }

        private void AddBot(string msg)
        {
            ChatPanel.Children.Add(new TextBlock
            {
                Text = "🤖 " + msg,
                Foreground = Brushes.White
            });

            ScrollViewer.ScrollToEnd();
        }
    }
}