# part3
 CyberBot – Cybersecurity Awareness Chatbot
 Project Overview

CyberBot is a WPF-based desktop chatbot application designed to educate users about cybersecurity in an interactive way. It uses a chat interface, SQL database integration, a quiz system, and simple natural language processing (NLP) to simulate a smart cybersecurity assistant.

The system was developed as part of a programming project to demonstrate:

GUI development (WPF)
Database integration (SQL Server)
NLP simulation
Quiz/game logic
Task management system
🚀 Features
💬 Chatbot Interface
Interactive chat-based UI
Friendly responses with cyber awareness tips
Pink and blue themed design
🧠 Natural Language Processing (NLP)
Detects phrases like:
“my name is…”
“remind me to…”
“add task”
Responds dynamically based on user input
📋 Task Manager (SQL Database)
Add tasks through chatbot commands
Store tasks in SQL Server database
View all saved tasks
Includes:
Title
Description
Reminder date
Completion status
🎮 Cybersecurity Quiz
10+ questions (MCQ + True/False)
Instant feedback after each answer
Final score displayed at the end
Educational cybersecurity explanations included
📊 Activity Log
Tracks all user actions
Stores:
Messages sent
Tasks added
Quiz activity
SQL tests
Displays last 10 actions with timestamps
🔌 SQL Connection Test
Built-in command:
test sql
Confirms database connectivity
Displays number of tasks stored
🛠️ Technologies Used
C# (.NET 8.0)
WPF (Windows Presentation Foundation)
SQL Server Express
Microsoft.Data.SqlClient
🗄️ Database Setup
Create Database:
CREATE DATABASE CyberBotDB;
Create Table:
USE CyberBotDB;

CREATE TABLE Tasks (
    TaskId INT IDENTITY(1,1) PRIMARY KEY,
    Title VARCHAR(255),
    Description VARCHAR(500),
    ReminderDate DATETIME NULL,
    IsCompleted BIT DEFAULT 0
);
⚙️ How to Run the Project
Open project in Visual Studio
Ensure SQL Server Express is installed and running
Create the CyberBotDB database using the script above
Update connection string if needed:
Server=.\SQLEXPRESS;Database=CyberBotDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;
Build and Run the project
💬 How to Use

Type commands in the chatbot:

menu → Show options
quiz → Start cybersecurity quiz
add task → Add a task
tasks → View all tasks
test sql → Test database connection
log → View activity history
my name is ___ → Store user name
📈 Learning Outcomes

This project demonstrates:

Event-driven programming
Database CRUD operations
UI design with WPF
Basic AI/NLP simulation
Game/quiz logic implementation
👨‍💻 Author

CyberBot Project – Student POE Submission

📌 Notes
Ensure SQL Server Express is running before launching the app
Database must be created manually before first run
Application uses local database connection
