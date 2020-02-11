using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    public class Showing
    {
        protected DateTime _showingDate;
        protected TimeSpan _durationInSchedule;
        protected Movie _movie;
        protected ScreeningRoom _screeningRoom;
        protected int _price;
        protected bool _isPremiere;
        private string _showingId;

        public Movie Movie { get => _movie; set => _movie = value; }
        public ScreeningRoom ScreeningRoom { get => _screeningRoom; set => _screeningRoom = value; }
        public int Price { get => _price; set => _price = value; }
        public bool IsPremiere { get => _isPremiere; set => _isPremiere = value; }
        public DateTime ShowingDate { get => _showingDate; set => _showingDate = value; }
        public string ShowingId { get => _showingId; set => _showingId = value; }

        public Showing()
        {

        }
        public Showing(DateTime showingDate, Movie movie, ScreeningRoom screeningRoom, bool isPremiere)
        {
            _showingDate = showingDate;
            _movie = movie;
            _screeningRoom = screeningRoom;
            _isPremiere = isPremiere;
            if (_isPremiere)
                _price = 15;
            else
                _price = 10;
        }

        private void LockSeat(int row, int seat)
        {
            _screeningRoom.Seats[row - 1][seat - 1] = ScreeningRoom.Seat.Zajete;
        }

        public bool SellTicket(int row, int seat, bool isDiscounted)
        {
            if(_screeningRoom.isSeatFree(row, seat))
            {
                LockSeat(row, seat);
                Ticket.GenerateTicket(this, isDiscounted, row, seat);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{this.GetType().Name}: {_showingId} {Movie.Title} {Movie.Duration / 60}:{Movie.Duration % 60} {ScreeningRoom.ScreeningRoomId} {ShowingDate.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
            stringBuilder.Append(ScreeningRoom.ToString());
            return stringBuilder.ToString();
        }
    }
}
