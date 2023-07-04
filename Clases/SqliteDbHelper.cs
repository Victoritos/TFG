using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TFG.Clases
{


    public class SqliteDbHelper
    {
        private string dbFolderPath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD");
        //private string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2");
        private string dbFilePath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD", "MyDatabase.sqlite");
        //private string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2", "MyDatabase.sqlite");
        private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD", "MyDatabase.sqlite")};Version=3;";

        public void CreateDatabase()
        {
            if (!Directory.Exists(dbFolderPath))
            {
                Directory.CreateDirectory(dbFolderPath);
            }
            SQLiteConnection.CreateFile(dbFilePath);
        }

        public void CreateTableTeams()
        {
            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Teams'";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE Teams(" +
                "\r\n TeamId INTEGER PRIMARY KEY," +
                "\r\n Name TEXT," +
                "\r\n Email TEXT" +
                ");";


            using var connection = new SQLiteConnection(connectionString);
            connection.Open();

            using SQLiteCommand command = new SQLiteCommand(tableExistsQuery, connection);

            bool tableExists = command.ExecuteScalar() != null;

            // Si la tabla no existe, crearla
            if (!tableExists)
            {
                // Crear el comando para ejecutar la consulta
                using SQLiteCommand createTableCommand = new(createTableQuery, connection);
                // Ejecutar la consulta
                createTableCommand.ExecuteNonQuery();
            }
        }
        public void CreateTableTournaments()
        {
            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Tournaments'";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE Tournaments(" +
                "\r\n TournamentId INTEGER PRIMARY KEY," +
                "\r\n Name TEXT," +
                "\r\n StartDate TEXT," +
                "\r\n EndDate TEXT" +
                ");";



            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(tableExistsQuery, connection))
                {
                    bool tableExists = command.ExecuteScalar() != null;

                    // Si la tabla no existe, crearla
                    if (!tableExists)
                    {
                        // Crear el comando para ejecutar la consulta
                        using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            // Ejecutar la consulta
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public void CreateTableRounds()
        {
            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Rounds'";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE Rounds (" +
                "\r\n   RoundId INTEGER PRIMARY KEY," +
                "\r\n   RoundName TEXT NOT NULL," +
                "\r\n   TournamentId INTEGER NOT NULL," +
                "\r\n   FOREIGN KEY (TournamentId) REFERENCES Tournaments(TournamentId)" +
                ");";


            SQLiteConnection connection = ConnectToDatabase();


            using SQLiteCommand command = new(tableExistsQuery, connection);
            bool tableExists = command.ExecuteScalar() != null;

            // Si la tabla no existe, crearla
            if (!tableExists)
            {
                // Crear el comando para ejecutar la consulta
                using SQLiteCommand createTableCommand = new(createTableQuery, connection);
                // Ejecutar la consulta
                createTableCommand.ExecuteNonQuery();
            }
        }

        private SQLiteConnection ConnectToDatabase()
        {
            var connection = new SQLiteConnection(connectionString);
            connection.Open();
            return connection;
        }

        public void CreateTableRoundMatches()
        {

            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='RoundMatches'";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE RoundMatches (" +
                "\r\n   RoundMatchId INTEGER PRIMARY KEY," +
                "\r\n   RoundId INTEGER NOT NULL," +
                "\r\n   HomeTeamId INTEGER," +
                "\r\n   AwayTeamId INTEGER," +
                "\r\n   HomeTeamScor INTEGER," +
                "\r\n   AwayTeamScore INTEGER," +
                "\r\n   FOREIGN KEY (HomeTeamId) REFERENCES Teams(TeamId)," +
                "\r\n   FOREIGN KEY (AwayTeamId) REFERENCES Teams(TeamId)," +
                "\r\n   FOREIGN KEY (RoundId) REFERENCES Rounds(RoundId)" +
                "\r\n);";

            using var connection = new SQLiteConnection(connectionString);
            connection.Open();
            using SQLiteCommand command = new(tableExistsQuery, connection);
            bool tableExists = command.ExecuteScalar() != null;

            // Si la tabla no existe, crearla
            if (!tableExists)
            {
                // Crear el comando para ejecutar la consulta
                using SQLiteCommand createTableCommand = new(createTableQuery, connection);
                // Ejecutar la consulta
                createTableCommand.ExecuteNonQuery();
            }
        }

        public void CreateTableParticipantTournament()
        {

            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='ParticipantTournament '";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE ParticipantTournament  (" +
                "\r\n   ParticipantId INTEGER," +
                "\r\n   TournamentId INTEGER," +
                "\r\n   PRIMARY KEY (ParticipantId, TournamentId)," +
                "\r\n   FOREIGN KEY (ParticipantId) REFERENCES Participants(ParticipantId)," +
                "\r\n   FOREIGN KEY (TournamentId) REFERENCES Tournaments(TournamentId)" +
                "\r\n);";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(tableExistsQuery, connection))
                {
                    bool tableExists = command.ExecuteScalar() != null;

                    // Si la tabla no existe, crearla
                    if (!tableExists)
                    {
                        // Crear el comando para ejecutar la consulta
                        using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery, connection))
                        {
                            // Ejecutar la consulta
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        public void Dropdata()
        {
            string query = "-- Eliminar la tabla Matches" +
                "\r\nDROP TABLE IF EXISTS RoundMatches;" +
                "\r\n" +
                "\r\n-- Eliminar la tabla Teams" +
                "\r\nDROP TABLE IF EXISTS Teams;" +
                "\r\n" +
                "\r\n-- Eliminar la tabla Rounds" +
                "\r\nDROP TABLE IF EXISTS Rounds;" +
                "\r\n" +
                "\r\n-- Eliminar la tabla ParticipantTournament " +
                "\r\nDROP TABLE IF EXISTS ParticipantTournament ;" +
                "\r\n" +
                "\r\n-- Eliminar la tabla Tournaments" +
                "\r\nDROP TABLE IF EXISTS Tournaments;" +
                "\r\n";

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public void InsertData()
        {
            string query = "-- Insertar datos en la tabla Tournaments" +
                "\r\nINSERT INTO Tournaments (Name, StartDate, EndDate)" +
                "\r\nVALUES ('Torneo de Fútbol 2023', '2023-05-01 00:00:00', '2023-06-30 00:00:00');" +
                "\r\n" +
                "\r\n-- Insertar datos en la tabla Rounds" +
                "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
                "\r\nVALUES" +
                "\r\n(1, 'Primera ronda')," +
                "\r\n(1, 'Segunda ronda')," +
                "\r\n(1, 'Final');" +
                "\r\n" +
                "\r\n-- Insertar datos en la tabla Teams" +
                "\r\nINSERT INTO Teams (Name, Email)" +
                "\r\nVALUES" +
                "\r\n('Equipo A', 'equipoa@example.com')," +
                "\r\n('Equipo B', 'equipob@example.com')," +
                "\r\n('Equipo C', 'equipoc@example.com')," +
                "\r\n('Equipo D', 'equipod@example.com')," +
                "\r\n('Equipo E', 'equipoe@example.com'), " +
                "\r\n('Equipo F', 'equipof@example.com')," +
                "\r\n('Equipo G', 'equipog@example.com'), " +
                "\r\n('Equipo H', 'equipoh@example.com'); " +
                "\r\n" +
                "\r\n-- Insertar datos en la tabla Matches" +
                "\r\nINSERT INTO RoundMatches (RoundId, HomeTeamId, AwayTeamId, HomeTeamScor, AwayTeamScore)" +
                "\r\nVALUES" +
                "\r\n-- Primera ronda" +
                "\r\n(1, 1, 2, 3, 2)," +
                "\r\n(1, 3, 4, 3, 0)," +
                "\r\n(1, 5, 6, 3, 1)," +
                "\r\n(1, 7, 8, 3, 0)," +
                "\r\n" +
                "\r\n-- Segunda Ronda" +
                "\r\n(2, 1, 3, 3, 0)," +
                "\r\n(2, 5, 8, 3, 1)," +
                "\r\n" +
                "\r\n-- Final" +
                "\r\n(3, 1, 5, null, null);" +
                "\r\nINSERT INTO ParticipantTournament (ParticipantId, TournamentId)" +
                "\r\nVALUES" +
                "\r\n(1, 1)," +
                "\r\n(2, 1)," +
                "\r\n(3, 1)," +
                "\r\n(4, 1)," +
                "\r\n(5, 1)," +
                "\r\n(6, 1)," +
                "\r\n(7, 1)," +
                "\r\n(8, 1);" +
                "\r\n ";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
            }
        }
        public void InsertTournament(Tournament tournament)
        {
            int torneoId = 0; ;
            string torneoQ = "\r\nINSERT INTO Tournaments (Name, StartDate, EndDate)" +
                "\r\nVALUES ('" + tournament.Name + "', '" + tournament.StartDate + "', '" + tournament.EndDate + "');" +
                "\r\n";
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(torneoQ, connection);
                command.ExecuteNonQuery();
                //Sacar el id del torneo
                string sql = "SELECT TournamentId FROM Tournaments";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        torneoId = reader.GetInt32(0);
                    }
                }
            }
            string rondasQ;
            if (tournament.numRondas == 1)
            {
                rondasQ = "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
               "\r\nVALUES" +
               "\r\n(" + torneoId + ", 'Final');";
            }
            else if (tournament.numRondas == 2)
            {
                rondasQ = "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
               "\r\nVALUES" +
               "\r\n(" + torneoId + ", 'Primera Ronda')," +
               "\r\n(" + torneoId + ", 'Final');";
            }
            else if (tournament.numRondas == 3)
            {
                rondasQ = "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
               "\r\nVALUES" +
               "\r\n(" + torneoId + ", 'Primera Ronda')," +
               "\r\n(" + torneoId + ", 'Segunda Ronda')," +
               "\r\n(" + torneoId + ", 'Final');";
            }
            else if (tournament.numRondas == 4)
            {
                rondasQ = "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
               "\r\nVALUES" +
               "\r\n(" + torneoId + ", 'Primera Ronda')," +
               "\r\n(" + torneoId + ", 'Segunda Ronda')," +
               "\r\n(" + torneoId + ", 'Tercera Ronda')," +
               "\r\n(" + torneoId + ", 'Final');";
            }
            else if (tournament.numRondas == 5)
            {
                rondasQ = "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
               "\r\nVALUES" +
               "\r\n(" + torneoId + ", 'Primera Ronda')," +
               "\r\n(" + torneoId + ", 'Segunda Ronda')," +
               "\r\n(" + torneoId + ", 'Tercera Ronda')," +
               "\r\n(" + torneoId + ", 'Cuarta Ronda')," +
               "\r\n(" + torneoId + ", 'Final');";
            }
            else
            {
                rondasQ = "\r\nINSERT INTO Rounds (TournamentId, RoundName)" +
                "\r\nVALUES" +
                "\r\n(" + torneoId + ", 'Torneo Vacio');";
            }
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                var command = new SQLiteCommand(rondasQ, connection);
                command.ExecuteNonQuery();
            }
            InsertMatches(torneoId, tournament.numRondas, tournament.Participants);
        }

        private void InsertMatches(int torneoid, int numRondas, List<Team> participantes)
        {
            List<int> matches = new List<int>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                //Sacar el id de las rondas
                string sql = "SELECT RoundId FROM Rounds where TournamentId = '" + torneoid + "';";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);
                connection.Open();
                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        matches.Add(reader.GetInt32(0));
                    }
                }
            }
            for (int i = 0; i < matches.Count; i++)
            {
                int numPartidos = (int)Math.Pow(2, matches.Count - i - 1);
                for (int j = 0; j < numPartidos; j++)
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        string MatchQ = "\r\nINSERT INTO RoundMatches (RoundId, HomeTeamId, AwayTeamId, HomeTeamScor, AwayTeamScore)" +
                                        "\r\nVALUES" +
                                        "\r\n(" + matches[i] + ", 0, 0, 0, 0);";
                        connection.Open();
                        var command = new SQLiteCommand(MatchQ, connection);
                        command.ExecuteNonQuery();
                    }
                }
            }
            for (int i = 0; i < participantes.Count; i++)
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    string MatchQ = "\r\nINSERT INTO ParticipantTournament (ParticipantId, TournamentId)" +
                                    "\r\nVALUES" +
                                    "\r\n(" + participantes[i].TeamId + ", " + torneoid + ");" +
                                    "\r\n ";
                    connection.Open();
                    var command = new SQLiteCommand(MatchQ, connection);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void UpdateMatch(RoundMatches match)
        {
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "UPDATE RoundMatches SET HomeTeamId = @HomeTeamId, AwayTeamId = @AwayTeamId, HomeTeamScor = @HomeTeamScore, AwayTeamScore = @AwayTeamScore WHERE RoundMatchId = @MatchId";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@HomeTeamId", match.HomeTeamId);
                        command.Parameters.AddWithValue("@AwayTeamId", match.AwayTeamId);
                        command.Parameters.AddWithValue("@HomeTeamScore", match.HomeTeamScore);
                        command.Parameters.AddWithValue("@AwayTeamScore", match.AwayTeamScore);
                        command.Parameters.AddWithValue("@MatchId", match.RoundMatchId);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public int? GetTeamIdByName(string teamName)
        {
            int? teamId = null;

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT TeamId FROM Teams WHERE Name = @TeamName";
                using (SQLiteCommand command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@TeamName", teamName);

                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            teamId = reader.IsDBNull(0) ? null : (reader.GetInt32(0));
                        }
                    }
                }

                connection.Close();
            }

            return teamId;
        }

        public RoundMatches GetMatchById(int matchId)
        {

            var match = new RoundMatches();
            try
            {
                using (SQLiteConnection connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT * FROM RoundMatches WHERE RoundMatchId = @MatchId";
                    using (SQLiteCommand command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@MatchId", matchId);

                        using (SQLiteDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int roundMatchId = reader.GetInt32(0);
                                int roundId = reader.GetInt32(1);
                                int? homeTeamId = reader.IsDBNull(2) ? null : reader.GetInt32(2);
                                int? awayTeamId = reader.IsDBNull(3) ? null : reader.GetInt32(3);
                                int? homeTeamScore = reader.IsDBNull(4) ? null : reader.GetInt32(4);
                                int? awayTeamScore = reader.IsDBNull(5) ? null : reader.GetInt32(5);

                                match = new RoundMatches(roundMatchId, roundId, homeTeamId, awayTeamId, homeTeamScore, awayTeamScore);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return match ?? null;
        }
        public void SelectData()
        {

            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                string sql = "SELECT * FROM ParticipantTournament";
                SQLiteCommand cmd = new SQLiteCommand(sql, connection);

                using (SQLiteDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int teamId = reader.GetInt32(0);
                        int tournamentId = reader.GetInt32(1);

                        Console.WriteLine("este es el id " + teamId + " " + tournamentId + " ");
                    }
                }
            }
        }

        public void RunAll()
        {
            Dropdata();
            CreateDatabase();
            CreateTableTournaments();
            CreateTableTeams();
            CreateTableRounds();
            CreateTableRoundMatches();
            CreateTableParticipantTournament();
            InsertData();
            SelectData();
        }

        public string getmyfuckingquery(string tournamentId , string roundName)
        {
            return $"INSERT INTO Rounds({tournamentId}, {roundName})";
        }
    }
}
