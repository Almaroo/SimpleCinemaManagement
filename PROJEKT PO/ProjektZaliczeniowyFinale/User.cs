using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    [Serializable]
    public class User
    {
        private string _name;
        private string _surname;
        private string _userId;
        [NonSerialized]
        protected List<Cinema> _managedCinemas;
        [NonSerialized]
        protected MoviesDatabase _moviesDatabase;


        private static int _userCount = 0;

        public string Name { get => _name; set => _name = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public string UserId { get => _userId; set => _userId = value; }
        public List<Cinema> ManagedCinemas { get => _managedCinemas; set => _managedCinemas = value; }
        public static int UserCount { get => _userCount; set => _userCount = value; }

        public User()
        {

        }
        public User(string name, string surname)
        {
            _moviesDatabase = MoviesDatabase.GetInstance;
            _managedCinemas = new List<Cinema>();
            _userCount++;
            _name = name;
            _surname = surname;
            _userId = GetUserId();
        }

        private string GetUserId()
        {
            return $"{Name[0].ToString()}{_surname[0].ToString()}{_userCount:000}".ToUpper();
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.Append($"{this.GetType().Name} {_userId} {Name} {_surname}");

            return stringBuilder.ToString();
        }

        public List<Movie> GetMovies(params string[] movieIds)
        {
            return _moviesDatabase.Read(this, (object)movieIds);
        }

        public void ShowManagedCinemas()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Cinemas managed by {this.ToString()}");
            stringBuilder.AppendLine();
            foreach(Cinema cinema in _managedCinemas)
            {
                stringBuilder.AppendLine($"{cinema.ToString()}");
            }

            Console.WriteLine(stringBuilder.ToString());
        }

        public Cinema GetManagedCinema(string cinemaId)
        {
            return _managedCinemas.Find(x => x.CinemaId == cinemaId);
        }
    }
}
