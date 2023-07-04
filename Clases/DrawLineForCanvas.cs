using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TFG.Clases
{
    class DrawLineForCanvas
    {
        IEnumerable<RoundMatches> matches;

        public DrawLineForCanvas(IEnumerable<RoundMatches> matches)
        {
            if(matches.Count() < 2)
            {
                throw new ArgumentException("fuck my life");
            }
            this.matches = matches;
        }
    }
}
