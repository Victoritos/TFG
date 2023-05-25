using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Documents;
using System.Xml.Linq;

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
        public void CreateTableTournaments()
        {
            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Tournaments'";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE Tournaments(" +
                "\r\n TournamentId INTEGER PRIMARY KEY," +
                "\r\n Name TEXT," +
                "\r\n StartDate DATETIME," +
                "\r\n EndDate DATETIME" +
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
                //"\r\n   FOREIGN KEY (ParentRoundId) REFERENCES Rounds(RoundId)\r\n" +
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

        public void CreateTableRoundMatches() {

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

        public void CreateTableParticipacion() { 

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
                "\r\nVALUES (1, 'Primera ronda'), (1, 'Segunda ronda'),(1, 'Final');" +
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

                        Console.WriteLine("este es el id "+teamId+" "+tournamentId+" ");
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
            CreateTableParticipacion();
            InsertData();
            SelectData();
        }
    }

}
