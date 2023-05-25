using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TFG.Clases
{
    internal class Tournament
    {
        private string dbFolderPath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD");
        //private string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2");
        private string dbFilePath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD", "MyDatabase.sqlite");
        //private string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2", "MyDatabase.sqlite");
        private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD", "MyDatabase.sqlite")};Version=3;";

        public int TournamentId { get; set; }
        public string? Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public Tournament(int tournamentId, string? name, DateTime startDate, DateTime endDate)
        {
            TournamentId = tournamentId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
        }

        public Tournament()
        {
        }

        public List<Tournament> getTeamTournaments(List<int> ids)
        {
            List<Tournament> tournaments = new List<Tournament>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                for (int i = 0; i < ids.Count; i++)
                {
                    connection.Open();
                    string sql = "SELECT * FROM Tournaments where TournamentId = " + ids[i];
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int tournamentId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            DateTime startDate = reader.GetDateTime(2);
                            DateTime endDate = reader.GetDateTime(3);

                            Tournament torneo = new Tournament(tournamentId, name, startDate, endDate);
                            tournaments.Add(torneo);
                        }
                    }
                }
            }
            return tournaments;
        }

        public List<Rounds> getRoundsTournaments()
        {
            List<Rounds> rondas = new List<Rounds>();

            using (var connection = new SQLiteConnection(connectionString))
            {

                connection.Open();
                string sql = "SELECT * FROM Rounds where TournamentId = " + TournamentId;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int roundId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int tournamentId = reader.GetInt32(2);

                        Rounds ronda = new Rounds(roundId, name, tournamentId);
                        rondas.Add(ronda);
                    }
                }
            }
            return rondas;
        }
        public List<RoundMatches> getRoundsMatchesTournaments()
        {
            List<Rounds> rondas = getRoundsTournaments();
            List<RoundMatches> matches = new List<RoundMatches>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                for (int i = 0; i < rondas.Count; i++)
                {
                    Rounds ronda = rondas[i];

                    string sql = "SELECT * FROM RoundMatches where RoundId = " + ronda.RoundId;
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int? HomeTeamId = null;
                            int? AwayTeamId = null;
                            int? homeTeamScore = null;
                            int? AwayTeamScore = null;

                            int roundMatchId = reader.GetInt32(0);
                            int roundId = reader.GetInt32(1);
                            if (!reader.IsDBNull(2))
                                HomeTeamId = reader.GetInt32(2);
                            if (!reader.IsDBNull(3))
                                AwayTeamId = reader.GetInt32(3);
                            if (!reader.IsDBNull(4))
                                homeTeamScore = reader.GetInt32(4);
                            if (!reader.IsDBNull(5))
                                AwayTeamScore = reader.GetInt32(5);


                            RoundMatches match = new RoundMatches(roundMatchId, roundId, HomeTeamId, AwayTeamId, homeTeamScore, AwayTeamScore);
                            matches.Add(match);
                        }
                    }

                }

            }
            return matches;
        }
        public List<int> getParticipants()
        {
            List<int> ids = new List<int>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT ParticipantId FROM ParticipantTournament where TournamentId = " + TournamentId;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        ids.Add(id);
                    }
                }
            }
            return ids;

        }
        public List<Teams> getTournamentTeams()
        {
            List<Teams> equipos = new List<Teams>();
            List<int> ids = getParticipants();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                for (int i = 0; i < ids.Count; i++)
                {
                    string sql = "SELECT * FROM Teams where TeamId = " + ids[i];
                    SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                    using (SQLiteDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int TeamId = reader.GetInt32(0);
                            string name = reader.GetString(1);
                            string email = reader.GetString(2);
                            Teams equipo = new Teams(TeamId, name, email);
                            equipos.Add(equipo);
                        }
                    }
                }
            }
            return equipos;
        }
    }
}
