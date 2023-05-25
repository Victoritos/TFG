using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG.Clases
{
    public class RoundMatches
    {
        private string dbFolderPath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD");
        //private string dbFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2");
        private string dbFilePath = Path.Combine("C:\\Users\\viccl\\source\\repos\\TFG", "BBDD", "MyDatabase.sqlite");
        //private string dbFilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD2", "MyDatabase.sqlite");
        private string connectionString = $"Data Source={Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "BBDD", "MyDatabase.sqlite")};Version=3;";

        public int RoundMatchId { get; set; }
        public int RoundId { get; set; }
        public int? HomeTeamId { get; set; }
        public int? AwayTeamId { get; set; }
        public int? HomeTeamScore { get; set; }
        public int? AwayTeamScore { get; set; }

        public RoundMatches(int roundMatchId, int roundId, int? homeTeamId, int? awayTeamId, int? homeTeamScore, int? awayTeamScore)
        {
            RoundMatchId = roundMatchId;
            RoundId = roundId;
            HomeTeamId = homeTeamId;
            AwayTeamId = awayTeamId;
            HomeTeamScore = homeTeamScore;
            AwayTeamScore = awayTeamScore;
        }

        
    }
}
