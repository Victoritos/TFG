using System;
using System.Collections.Generic;
using System.Windows;
using TFG.Clases;

namespace TFG
{
    public partial class UpdateWindow : Window
    {
        private List<Team> teams;
        private int roundMatchId;
        SqliteDbHelper dbHelper;
        public event EventHandler MatchUpdated;
        public RoundMatches UpdatedMatch { get; private set; }
        public UpdateWindow(int matchId, List<Team> tournamentTeams)
        {
            dbHelper = new SqliteDbHelper();
            InitializeComponent();
            roundMatchId = matchId;
            teams = tournamentTeams;
            PopulateComboBoxes();
            LoadMatchData();
        }

        private void PopulateComboBoxes()
        {
            cmbHomeTeam.ItemsSource = teams;
            cmbHomeTeam.DisplayMemberPath = "Name";
            cmbHomeTeam.SelectedValuePath = "TeamId";

            cmbAwayTeam.ItemsSource = teams;
            cmbAwayTeam.DisplayMemberPath = "Name";
            cmbAwayTeam.SelectedValuePath = "TeamId";
        }

        private void LoadMatchData()
        {
            RoundMatches match = dbHelper.GetMatchById(roundMatchId);

            if (match != null)
            {
                cmbHomeTeam.SelectedValue = match.HomeTeamId;
                cmbAwayTeam.SelectedValue = match.AwayTeamId;
                txtHomeTeamScore.Text = match.HomeTeamScore?.ToString();
                txtAwayTeamScore.Text = match.AwayTeamScore?.ToString();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            int? homeTeamScore = null;
            int? awayTeamScore = null;

            if (!string.IsNullOrEmpty(txtHomeTeamScore.Text))
                homeTeamScore = int.Parse(txtHomeTeamScore.Text);

            if (!string.IsNullOrEmpty(txtAwayTeamScore.Text))
                awayTeamScore = int.Parse(txtAwayTeamScore.Text);

            int? homeTeamId = cmbHomeTeam.SelectedValue as int?;
            int? awayTeamId = cmbAwayTeam.SelectedValue as int?;
            RoundMatches match = new RoundMatches(roundMatchId, homeTeamScore, awayTeamScore, homeTeamId, awayTeamId);
            dbHelper.UpdateMatch(match);
            MessageBox.Show("Partido actualizado Satisfactoriamente", "Update Match", MessageBoxButton.OK, MessageBoxImage.Information);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

    }
}
