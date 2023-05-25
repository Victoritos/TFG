using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG.Clases
{
    public class Rounds
    {
        public readonly int RoundId;

        public readonly string? RoundName;

        public readonly int TournamentId;

        public Rounds()
        {
        }

        public Rounds(int roundId, string? roundName, int tournamentId)
        {
            RoundId = roundId;
            RoundName = roundName;
            TournamentId = tournamentId;
        }
    }
}
