using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ProjektZaliczeniowyFinale
{
    [Serializable]
    public sealed class Admin : User
    {
        [NonSerialized]
        private UsersDatabase _usersDatabase;
        [NonSerialized]
        private CinemasDatabase _cinemasDatabase;

        public Admin()
        {

        }
        public Admin(string name, string surname) : base(name, surname)
        {
            //ConnectDatabases();
        }
        
        public void ConnectDatabases()
        {
            _usersDatabase = UsersDatabase.GetInstance;
            _moviesDatabase = MoviesDatabase.GetInstance;
            _cinemasDatabase = CinemasDatabase.GetInstance;
        }

        public void LoadUpDatabases()
        {
            
            _cinemasDatabase.Deserialize();
            _moviesDatabase.Deserialize();
            if (_usersDatabase.Deserialize())
                _usersDatabase.Initialize();
        }

        
        //USERSDB MANAGEMENT
        public void AddUser(string name, string surname, string password, bool isAdmin)
        {
            _usersDatabase.Create(this, name, surname, password, isAdmin);
        }

        public void DeleteUsers(params string[] userIds)
        {
            _usersDatabase.Delete(this, (object)userIds);
        }

        public List<User> GetUsers(params string[] userIds)
        {
            return _usersDatabase.Read(this, (object)userIds);
        }

        //CINEMASDB MANAGEMENT
        public void AddCinema(string name, Address address)
        {
            _cinemasDatabase.Create(this, name, address);
        }

        public void DeleteCinema(params string[] cinemaId)
        {
            _cinemasDatabase.Delete(this, (object)cinemaId);
        }

        public List<Cinema> GetCinema(params string[] cinemaId)
        {
            return _cinemasDatabase.Read(this, (object)cinemaId);
        }

        //MOVIESDB MANAGEMENT
        public void AddMovie(string name, int duration)
        {
            _moviesDatabase.Create(this, name, duration);
        }

        public bool DeleteMovie(params string[] movieId)
        {
            return _moviesDatabase.Delete(this, (object)movieId);
        }

        //CINEMA MANAGEMENT
        public void AddScreeningRoomToCinema(string cinemaId, int[] rows)
        {
            GetCinema(cinemaId).First().AddNewScreningRoomTemplate(this, rows.Length, rows);
        }

        public void AddShowingToCinemaSchedule(string cinemaId, string screeningRoomId, string movieId, DateTime showingDate, bool isPremiere, bool is3D)
        {
            var cinema = _cinemasDatabase.Read(this, (object)new string[] { cinemaId }).First();//XDDD
            var movie = _moviesDatabase.Read(this, (object)new string[] {movieId}).First(); //XDDD
            var screeningRoom = cinema.GetScreeningRoomTemplate(screeningRoomId).Clone();

            cinema.AddNewShowing(this, showingDate, movie, screeningRoom, isPremiere, is3D);
        }


        
        //USER MANAGEMENT
        public void AddCinemasToUser(string userId, params object[] cinemaIds)
        {
            User user = _usersDatabase.Read(this, userId).First();
            foreach(Cinema cinema in _cinemasDatabase.Read(this, cinemaIds))
            {
                user.ManagedCinemas.Add(cinema);
            }

        }
    }
}
