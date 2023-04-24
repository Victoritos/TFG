using System.Data.SQLite;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TFG.Clases;

namespace TFG
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private void iniciarBD()
        {

            string databaseFilePath = @"BBDD\MiBaseDeDatos.db";
            string connectionString = $"Data Source={databaseFilePath};Version=3;";


            // Consulta para verificar si la tabla existe
            string tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Rounds'";

            // Consulta para crear la tabla
            string createTableQuery = "CREATE TABLE Rounds (" +
                "\r\n   RoundId INTEGER PRIMARY KEY," +
                "\r\n   RoundName TEXT NOT NULL," +
                "\r\n   TournamentId INTEGER NOT NULL," +
                "\r\n   ParentRoundId INTEGER," +
                "\r\n   FOREIGN KEY (TournamentId) REFERENCES Tournaments(TournamentId)," +
                "\r\n   FOREIGN KEY (ParentRoundId) REFERENCES Rounds(RoundId)\r\n" +
                ");";
            string tableExistsQuery2 = "SELECT name FROM sqlite_master WHERE type='table' AND name='RoundMatches'";

            // Consulta para crear la tabla
            string createTableQuery2 = "CREATE TABLE RoundMatches (" +
                "\r\n   RoundMatchId INTEGER PRIMARY KEY," +
                "\r\n   RoundId INTEGER NOT NULL," +
                "\r\n   MatchOrder INTEGER NOT NULL," +
                "\r\n   HomeTeamId INTEGER," +
                "\r\n   AwayTeamId INTEGER," +
                "\r\n   HomeScore INTEGER," +
                "\r\n   AwayScore INTEGER," +
                "\r\n   FOREIGN KEY (RoundId) REFERENCES Rounds(RoundId)," +
                "\r\n   FOREIGN KEY (HomeTeamId) REFERENCES Teams(TeamId)," +
                "\r\n   FOREIGN KEY (AwayTeamId) REFERENCES Teams(TeamId)" +
                "\r\n);";
            // Crear la conexión a la base de datos
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                // Abrir la conexión
                connection.Open();

                // Verificar si la tabla Rounds existe
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

                // Verificar si la tabla RoundMatches existe
                using (SQLiteCommand command = new SQLiteCommand(tableExistsQuery2, connection))
                {
                    bool tableExists = command.ExecuteScalar() != null;

                    // Si la tabla no existe, crearla
                    if (!tableExists)
                    {
                        // Crear el comando para ejecutar la consulta
                        using (SQLiteCommand createTableCommand = new SQLiteCommand(createTableQuery2, connection))
                        {
                            // Ejecutar la consulta
                            createTableCommand.ExecuteNonQuery();
                        }
                    }
                }

                // Cerrar la conexión
                connection.Close();


            }
        }


        public MainWindow()
        {
            SqliteDbHelper bbdd= new SqliteDbHelper();
            bbdd.RunAll();
            //iniciarBD();
            //InitializeComponent();
        }

        private void verParticipantesClick(object sender, RoutedEventArgs e)
        {
            // Obtener la referencia a la pestaña deseada
            TabItem pestana2 = tabControl1.Items.Cast<TabItem>().FirstOrDefault(item => item.Header.Equals("Participantes"));

            // Seleccionar la pestaña deseada
            tabControl1.SelectedItem = pestana2;
        }
    }
}
