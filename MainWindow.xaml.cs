﻿using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TFG.Clases;

namespace TFG
{
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            SqliteDbHelper bbdd = new SqliteDbHelper();
            //bbdd.RunAll();
            InitializeComponent();


        }

        private void VerParticipantes_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la referencia a la pestaña deseada
            TabItem pestana2 = tabControl1.Items.Cast<TabItem>().FirstOrDefault(item => item.Header.Equals("Participantes"));

            // Seleccionar la pestaña deseada
            tabControl1.SelectedItem = pestana2;
            Team participantes = new();
            teamsGrid.ItemsSource = participantes.getAllTeams();
        }

        private void verTorneosClick(object sender, RoutedEventArgs e)
        {
            // Obtener la referencia a la pestaña deseada
            TabItem pestana3 = tabControl1.Items.Cast<TabItem>().FirstOrDefault(item => item.Header.Equals("Torneos"));

            // Obtener el participante seleccionado desde el DataContext del botón
            var button = sender as Button;
            var participant = button.DataContext as Team;

            // Aquí puedes implementar la lógica para mostrar los torneos del participante en una pestaña aparte
            // Puedes utilizar el participante.Id para buscar los torneos correspondientes en la base de datos, por ejemplo.
            // Abre una nueva ventana o muestra los torneos en una pestaña aparte como prefieras.

            tabControl1.SelectedItem = pestana3;
            Tournament torneo = new Tournament();
            tournamentGrid.ItemsSource = torneo.getTeamTournaments(participant.getTournaments());

        }

        private void VerCuadrosClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var torneo = button.DataContext as Tournament;

            // Obtén los datos necesarios para dibujar el cuadro de torneo, como los equipos, las rondas y los partidos
            List<Team> teams = torneo.getTournamentTeams(); // Obtener los equipos desde la base de datos u otra fuente de datos
            List<Round> rounds = torneo.getRoundsTournaments(); // Obtener las rondas desde la base de datos u otra fuente de datos
            List<RoundMatches> roundMatches = torneo.getRoundsMatchesTournaments(); // Obtener los partidos desde la base de datos u otra fuente de datos

            // Crea una instancia de la ventana TournamentWindow
            TournamentWindow tournamentWindow = new TournamentWindow(torneo);

            // Dibuja el cuadro de torneo en la ventana TournamentWindow
            tournamentWindow.DrawCanva();

            // Muestra la ventana TournamentWindow
            tournamentWindow.Show();

        }

        private void VerTorneos_Click(object sender, RoutedEventArgs e)
        {
            // Obtener la referencia a la pestaña deseada
            TabItem pestana3 = tabControl1.Items.Cast<TabItem>().FirstOrDefault(item => item.Header.Equals("Torneos"));

            // Seleccionar la pestaña deseada
            tabControl1.SelectedItem = pestana3;
            Tournament torneo = new();
            tournamentGrid.ItemsSource = torneo.getAllournaments();

        }

        private void BorrarParticipante_Click(object sender, RoutedEventArgs e)
        {

        }

        private void AñadirParticipante_Click(object sender, RoutedEventArgs e)
        {
            AddTeamWindow addTeamWindow = new AddTeamWindow();
            addTeamWindow.ShowDialog();
        }

        private void AñadirTorneo_Click(object sender, RoutedEventArgs e)
        {
            Team aux = new Team();
            AddTournamentWindow addTorneo = new AddTournamentWindow();
            addTorneo.AllTeams = aux.getAllTeams(); // Obtén la lista de equipos desde tu lógica de base de datos

            addTorneo.ShowDialog();
        }
    }
}
