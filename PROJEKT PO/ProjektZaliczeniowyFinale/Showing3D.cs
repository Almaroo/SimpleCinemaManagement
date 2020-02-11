using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    public sealed class Showing3D : Showing
    {
        public Showing3D()
        {

        }
        public Showing3D(DateTime dateTime, Movie movie, ScreeningRoom screeningRoom, bool isPremiere) : base(dateTime, movie, screeningRoom, isPremiere)
        {
            _price = 15;
        }
    }
}
