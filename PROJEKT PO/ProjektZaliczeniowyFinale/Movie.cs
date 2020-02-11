using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    public class Movie
    {
        private string _title;
        private int _duration;
        private string _movieId;

        private static int _movieCount = 0;
        public string Title { get => _title; set => _title = value; }
        public int Duration { get => _duration; set => _duration = value; }
        public string MovieId { get => _movieId; set => _movieId = value; }
        public static int MovieCount { get => _movieCount; set => _movieCount = value; }

        public Movie()
        {

        }
        public Movie(string title, int duration)
        {
            _title = title;
            _duration = duration;
            _movieId = GetMovieId();
        }

        private string GetMovieId()
        {
            return $"M{++_movieCount:000}";
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{this.GetType().Name}");
            stringBuilder.AppendLine($"Id: {_movieId}");
            stringBuilder.AppendLine($"Title: {_title}");
            stringBuilder.AppendLine($"Duration: {_duration / 60}:{_duration % 60}h");

            return stringBuilder.ToString();

        }

    }
}
