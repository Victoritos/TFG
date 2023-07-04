using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TFG.Clases
{

    public class Team
    {
        private string dbFolderPath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD");
        //private string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2");
        private string dbFilePath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD", "MyDatabase.sqlite");
        //private string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2", "MyDatabase.sqlite");
        private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD", "MyDatabase.sqlite")};Version=3;";


        public int TeamId { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }

        public Team(int teamId, string? name, string? email)
        {
            TeamId = teamId;
            Name = name;
            Email = email;
        }

        public Team()
        {
        }

        public List<Team> getAllTeams()
        {
            List<Team> teams = new List<Team>();


            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Teams";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int teamId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string email = reader.GetString(2);

                        Team team = new Team ( teamId, name, email );
                        teams.Add(team);
                    }
                }
            }
            return teams;
        }
        public List<int> getTournaments()
        {
            List<int> torneos = new List<int>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT TournamentId FROM ParticipantTournament where ParticipantId = " + TeamId;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        torneos.Add(id);
                    }
                }
            }
            return torneos;

        }


    }
}
