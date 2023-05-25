using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using TFG.Clases;

namespace TFG
{
    /// <summary>
    /// Lógica de interacción para TournamentWindow.xaml
    /// </summary>
    public partial class TournamentWindow : Window
    {
        private List<Teams> teams;
        private List<Rounds> rounds;
        private List<RoundMatches> roundMatches;

        public TournamentWindow(List<Teams> tournamentTeams, List<Rounds> tournamentRounds, List<RoundMatches> tournamentRoundMatches)
        {
            InitializeComponent();

            teams = tournamentTeams;
            rounds = tournamentRounds;
            roundMatches = tournamentRoundMatches;

            DrawTournamentBracket();
        }
        //public void DrawTournamentBracket()
        //{
        //    // Limpiar el contenido actual del Canvas
        //    tournamentCanvas.Children.Clear();

        //    // Calcular el número total de rondas y partidos
        //    int totalRounds = rounds.Count;
        //    int totalMatches = roundMatches.Count;

        //    // Calcular la altura y el ancho del Canvas
        //    double canvasHeight = totalRounds * 100 + 50; // 100 unidades de altura por cada ronda + 50 unidades para los nombres de las rondas
        //    double canvasWidth = totalMatches * 200 + 100; // 200 unidades de ancho por cada partido + 100 unidades para los nombres de los equipos

        //    // Establecer el tamaño del Canvas
        //    tournamentCanvas.Width = canvasWidth;
        //    tournamentCanvas.Height = canvasHeight;

        //    // Dibujar los nombres de las rondas
        //    for (int i = 0; i < totalRounds; i++)
        //    {
        //        TextBlock roundNameTextBlock = new TextBlock();
        //        roundNameTextBlock.Text = rounds[i].RoundName;
        //        roundNameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
        //        roundNameTextBlock.VerticalAlignment = VerticalAlignment.Center;
        //        roundNameTextBlock.Margin = new Thickness(10, i * 100 + 10, 0, 0);

        //        tournamentCanvas.Children.Add(roundNameTextBlock);
        //    }

        //    // Dibujar los nombres de los equipos
        //    for (int i = 0; i < totalMatches; i++)
        //    {
        //        TextBlock teamNameTextBlock = new TextBlock();
        //        teamNameTextBlock.Text = teams[i].Name;
        //        teamNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
        //        teamNameTextBlock.VerticalAlignment = VerticalAlignment.Top;
        //        teamNameTextBlock.Margin = new Thickness(i * 200 + 100, 10, 0, 0);

        //        tournamentCanvas.Children.Add(teamNameTextBlock);
        //    }

        //    // Dibujar los resultados de los partidos
        //    for (int i = 0; i < totalMatches; i++)
        //    {
        //        int roundIndex = roundMatches[i].RoundId;
        //        int matchY = roundIndex * 100 + 50;
        //        int matchX = i * 200 + 100;

        //        Button matchButton = new Button();
        //        matchButton.Content = $"{roundMatches[i].HomeTeamId} vs {roundMatches[i].AwayTeamId}";
        //        matchButton.Tag = roundMatches[i].RoundMatchId;
        //        matchButton.Width = 150;
        //        matchButton.Height = 50;
        //        matchButton.Margin = new Thickness(matchX, matchY, 0, 0);
        //        matchButton.Click += MatchButton_Click;

        //        tournamentCanvas.Children.Add(matchButton);
        //    }
        //}
        public void DrawTournamentBracket()
        {
            // Limpiar el contenido actual del Canvas
            tournamentCanvas.Children.Clear();

            // Calcular el número total de rondas y partidos
            int totalRounds = rounds.Count;
            int totalMatches = roundMatches.Count;

            // Calcular la altura y el ancho del Canvas
            double canvasHeight = totalRounds * 100 + 50; // 100 unidades de altura por cada ronda + 50 unidades para los nombres de las rondas
            double canvasWidth = totalMatches * 200 + 100; // 200 unidades de ancho por cada partido + 100 unidades para los nombres de los equipos

            // Establecer el tamaño del Canvas
            tournamentCanvas.Width = canvasWidth;
            tournamentCanvas.Height = canvasHeight;

            // Dibujar los nombres de las rondas
            for (int i = 0; i < totalRounds; i++)
            {
                TextBlock roundNameTextBlock = new TextBlock();
                roundNameTextBlock.Text = rounds[i].RoundName;
                roundNameTextBlock.HorizontalAlignment = HorizontalAlignment.Left;
                roundNameTextBlock.VerticalAlignment = VerticalAlignment.Center;
                roundNameTextBlock.Margin = new Thickness(10, i * 100 + 10, 0, 0);

                tournamentCanvas.Children.Add(roundNameTextBlock);
            }

            // Dibujar los nombres de los equipos y los resultados de los partidos
            int maxMatchesInRound = (int)Math.Pow(2, totalRounds - 1); // Máximo número de partidos en una ronda
            double initialX = (canvasWidth - maxMatchesInRound * 200) / 2; // Posición inicial en X para centrar la primera ronda
            int indiceActual = 0;
            double matchX = initialX - 200;
            int j = 0;
            for (int i = 0; i < totalMatches; i++)
            {
                int roundIndex = roundMatches[i].RoundId;
                if (indiceActual == 0)
                    indiceActual = roundIndex;
                if (indiceActual < roundIndex)
                {
                    j++;
                    indiceActual = roundIndex;
                    matchX = initialX + 100 + j * 100;
                }
                else
                {
                    matchX += 200;
                }
                int matchY = roundIndex * 100 + 50;


                //TextBlock teamNameTextBlock = new TextBlock();
                //teamNameTextBlock.Text = teams[i].Name;
                //teamNameTextBlock.HorizontalAlignment = HorizontalAlignment.Center;
                //teamNameTextBlock.VerticalAlignment = VerticalAlignment.Top;
                //teamNameTextBlock.Margin = new Thickness(matchX, 10, 0, 0);

                //tournamentCanvas.Children.Add(teamNameTextBlock);

                Button matchButton = new Button();
                matchButton.Content = $"{roundMatches[i].HomeTeamId} vs {roundMatches[i].AwayTeamId}";
                matchButton.Tag = roundMatches[i].RoundMatchId;
                matchButton.Width = 150;
                matchButton.Height = 50;
                matchButton.Margin = new Thickness(matchX, matchY, 0, 0);
                matchButton.Click += MatchButton_Click;

                tournamentCanvas.Children.Add(matchButton);
            }
        }







        private void MatchButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int matchId = (int)clickedButton.Tag;

            // Aquí puedes realizar las acciones necesarias cuando se hace clic en un partido, como actualizar el resultado o mostrar más detalles.
        }

    }
}