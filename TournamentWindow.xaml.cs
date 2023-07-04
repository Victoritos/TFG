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
        private List<Team> teams;
        private List<Round> rounds;
        private List<RoundMatches> roundMatches;
        double canvasHeight;
        double canvasWidth;
        Tournament Torneo;
        public TournamentWindow(Tournament torneo)
        {
            Torneo = torneo;
            teams = torneo.getTournamentTeams();
            rounds = torneo.getRoundsTournaments();
            roundMatches = torneo.getRoundsMatchesTournaments();
            canvasHeight = rounds.Count * 100 + 50; ;
            canvasWidth = roundMatches.Count * 100 + 50;
            InitializeComponent();

            DrawCanva();
        }
        private void setTournament() 
        {
            teams = Torneo.getTournamentTeams();
            rounds = Torneo.getRoundsTournaments();
            roundMatches = Torneo.getRoundsMatchesTournaments();
        }

        public void DrawCanva()
        {
            tournamentCanvas.Children.Clear();
            PrintRoundsNames(rounds.Count);
            DrawMatchesSchemaContent();
        }

        private void DrawMatchesSchemaContent()
        {
            // Calcular el número total de rondas y partidos
            int totalRounds = rounds.Count;
            int totalMatches = roundMatches.Count;
            

            //Search way to avoid casting to int
            int maxMatchesInRound = (int)Math.Pow(2, totalRounds - 1); 

            // Calcular la altura y el ancho del Canvas
            double canvasHeight = totalRounds * 100 + 50; // 100 unidades de altura por cada ronda + 50 unidades para los nombres de las rondas
            double canvasWidth = totalMatches * 250 + 100; // 200 unidades de ancho por cada partido + 100 unidades para los nombres de los equipos

            // Establecer el tamaño del Canvas
            tournamentCanvas.Width = canvasWidth;
            tournamentCanvas.Height = canvasHeight;

         
            double initialX = (canvasWidth - maxMatchesInRound * 200) / 2; // Posición inicial en X para centrar la primera ronda
            int indiceActual = 0;
            double matchX = initialX - 200;
            int j = 0;
            for (int i = 0; i < totalMatches; i++)
            {
                int roundIndex = roundMatches[i].RoundId - (roundMatches[0].RoundId - 1);

                if (indiceActual == 0)
                {
                    indiceActual = roundIndex;
                }

                if (indiceActual < roundIndex)
                {
                    j++;
                    indiceActual = roundIndex;
                    if (totalRounds == 2)
                        matchX = initialX + j * 100;
                    else
                        matchX = initialX + 100 + j * 100;
                }
                else
                {
                    matchX += 200;
                }
                int matchY = (roundIndex - 1) * 100 + 10;

                Button matchButton = newButton(matchX, i, matchY);

                tournamentCanvas.Children.Add(matchButton);
            }
        }

        private Button newButton(double matchX, int i, int matchY)
        {
            Button matchButton = new();
            matchButton.Content = $"  {roundMatches[i].HomeTeamId} vs {roundMatches[i].AwayTeamId}  " +
                $" \n Resultado {roundMatches[i].HomeTeamScore} - {roundMatches[i].AwayTeamScore}";
            matchButton.Tag = roundMatches[i].RoundMatchId;
            matchButton.Width = 150;
            matchButton.Height = 50;
            matchButton.Margin = new Thickness(matchX, matchY, 0, 0);
            matchButton.Click += MatchButton_Click;
            return matchButton;
        }

        private void PrintRoundsNames(int totalRounds)
        {
            for (int i = 0; i < totalRounds; i++)
            {
                TextBlock roundNameTextBlock = new()
                {
                    Text = rounds[i].RoundName,
                    HorizontalAlignment = HorizontalAlignment.Left,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(10, i * 100 + 10, 0, 0)
                };
                tournamentCanvas.Children.Add(roundNameTextBlock);
            }
        }

        private void MatchButton_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = (Button)sender;
            int matchId = (int)clickedButton.Tag;

            UpdateWindow updateWindow = new UpdateWindow(matchId, teams);
            updateWindow.ShowDialog();
            setTournament();
            DrawCanva();
            
        }




    }
}