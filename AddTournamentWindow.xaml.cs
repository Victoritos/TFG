using System;
using System.Collections.Generic;
using System.Windows;
namespace TFG.Clases
{
    public partial class AddTournamentWindow : Window
    {
        public List<Team> AllTeams { get; set; }
        public SqliteDbHelper SqliteDbHelper { get; set; }
        public AddTournamentWindow()
        {
            SqliteDbHelper = new SqliteDbHelper();
            InitializeComponent();
            AllTeams = new List<Team>();

            // Establece el DataContext de la ventana
            DataContext = this;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            // Obtener los valores ingresados por el usuario
            string name = NameTextBox.Text;
            string startDate = StartDatePicker.SelectedDate.ToString()  ;
            string endDate = EndDatePicker.SelectedDate.ToString() ;

            // Validar los valores (puedes agregar más validaciones según tus requisitos)
            if (string.IsNullOrWhiteSpace(name) )
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            List<Team> selectedParticipants = new List<Team>();
            foreach (Team participant in ParticipantsListBox.SelectedItems)
            {
                selectedParticipants.Add(participant);
            }


            // Crear una instancia de la clase Tournament y guardar los valores en la base de datos
            Tournament tournament = new Tournament(name, startDate, endDate,selectedParticipants);
            tournament.Participants = selectedParticipants;
            // Aquí debes implementar el código para guardar el torneo en la base de datos
            SqliteDbHelper.InsertTournament(tournament);
            // Mostrar un mensaje de éxito y cerrar la ventana
            MessageBox.Show("Torneo guardado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }
    }
}
