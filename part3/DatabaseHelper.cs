using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace PART2
{
    public class DatabaseHelper
    {
        private string connectionString =
            @"Server=.\SQLEXPRESS;Database=CyberBotDB;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;";

        public void AddTask(string title, string description, DateTime reminder)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = @"INSERT INTO Tasks (Title, Description, ReminderDate, IsCompleted)
                                 VALUES (@t, @d, @r, 0)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@t", title);
                    cmd.Parameters.AddWithValue("@d", description);
                    cmd.Parameters.AddWithValue("@r", reminder);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<string> GetTasks()
        {
            List<string> tasks = new List<string>();

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();

                string query = "SELECT TaskId, Title, Description, ReminderDate, IsCompleted FROM Tasks";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        tasks.Add($"ID:{reader["TaskId"]} | {reader["Title"]} | {reader["Description"]} | Done:{reader["IsCompleted"]}");
                    }
                }
            }

            return tasks;
        }
    }
}