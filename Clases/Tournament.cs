using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TFG.Clases
{
    public class Tournament
    {
        private string dbFolderPath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD");
        //private string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2");
        private string dbFilePath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD", "MyDatabase.sqlite");
        //private string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2", "MyDatabase.sqlite");
        private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD", "MyDatabase.sqlite")};Version=3;";

        public int TournamentId { get; set; }
        public string? Name { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public List<Round> Rounds { get; set; }
        public List<Team> Participants { get; set; }
        public int numRondas { get; set; }

        public Tournament(int tournamentId, string? name, string startDate, string endDate)
        {
            TournamentId = tournamentId;
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Rounds = getRoundsTournaments();
            Participants = getAllTeams();
            numRondas= CalcularNumRondas();
        }
        public Tournament( string? name, string startDate, string endDate,List<Team> participants)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Rounds = getRoundsTournaments();
            Participants = participants;
            numRondas = CalcularNumRondas();
        }
        public Tournament()
        {
        }

        private int CalcularNumRondas()
        {
            return (int)Math.Sqrt(Participants.Count);
        }

        public List<Tournament> getAllournaments()
        {
            List<Tournament> tournaments = new List<Tournament>();

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Tournaments";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int tournamentId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string startDate = reader.GetString(2);
                        string endDate = reader.GetString(3);

                        Tournament torneo = new Tournament(tournamentId, name, startDate, endDate);
                        tournaments.Add(torneo);
                    }
                }

            }
            return tournaments;
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
                            string startDate = reader.GetString(2);
                            string endDate = reader.GetString(3);

                            Tournament torneo = new Tournament(tournamentId, name, startDate, endDate);
                            tournaments.Add(torneo);
                        }
                    }
                }
            }
            return tournaments;
        }

        public List<Round> getRoundsTournaments()
        {
            List<Round> rondas = new List<Round>();

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

                        Round ronda = new Round(roundId, name, tournamentId);
                        rondas.Add(ronda);
                    }
                }
            }
            return rondas;
        }
        public List<RoundMatches> getRoundsMatchesTournaments()
        {
            List<Round> rondas = getRoundsTournaments();
            List<RoundMatches> matches = new List<RoundMatches>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                for (int i = 0; i < rondas.Count; i++)
                {
                    Round ronda = rondas[i];

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
        public List<Team> getAllTeams()
        {
            List<Team> teams = new List<Team>();


            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM Teams where TeamId = " + TournamentId;
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int teamId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string email = reader.GetString(2);

                        Team team = new Team(teamId, name, email);
                        teams.Add(team);
                    }
                }
            }
            return teams;
        }
        public List<int> getParticipantsIds()
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
        public List<Team> getTournamentTeams()
        {
            List<Team> equipos = new List<Team>();
            List<int> ids = getParticipantsIds();
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
                            Team equipo = new Team(TeamId, name, email);
                            equipos.Add(equipo);
                        }
                    }
                }
            }
            return equipos;
        }
    }
}
