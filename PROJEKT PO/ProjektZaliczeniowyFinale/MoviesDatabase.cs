using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{ 

    public sealed class MoviesDatabase : Database<Movie>
    {
        private static MoviesDatabase _instance;
        private static object _instanceLock = new object();

        public List<Movie> Movies { get => _content; set => _content = value; }

        public MoviesDatabase() : base()
        {

        }

        public static MoviesDatabase GetInstance
        {
            get
            {
                if (_instance == null)
                    _instance = new MoviesDatabase();
                //_instance.Deserialize();
                return _instance;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{this.GetType().Name}");
            foreach (Movie m in _content)
            {
                stringBuilder.AppendLine($"{m.ToString()}");
            }

            return stringBuilder.ToString();
        }

        public static void ShowDB()
        {
            Console.WriteLine(_instance.ToString());
        }

        public override bool Create(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Creates new movie in database
             *  
             *  Parameters:
             *      caller: object that calls method
             *      parameters: array of exactly 2 objects where: [name, duration]
             *      
             *  Returns;
             *      true: if new movie was created
             *      false: if not allowed to add
            */
            if (caller is Admin && parameters.Length == 2)
            {
                Movie tmp = new Movie((string)parameters[0], (int)parameters[1]);
                Movies.Add(tmp);
                Console.WriteLine("Dodano film");
                return true;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }
        }

        public override List<Movie> Read(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Gets movies that match given userIds
             *      
             *  Parameters:
             *      caller: object that calls function
             *      parameters: array of n objects where: [movieId0, movieId1, ... , movieIdn]
             *      
             *  Returns:
             *      List<User>: always
             * 
             *   
             */

            int count = 0;
            List<Movie> retVal = new List<Movie>();
            foreach(string s in (string[])parameters[0])
            {
                foreach (Movie movie in _content)
                {
                    if((s != "all"))
                    {
                        if (movie.MovieId == s)
                        {
                            retVal.Add(movie);
                            count++;
                        }
                    }
                    else
                    {
                        retVal.Add(movie);
                        count++;
                    }
                }
            }
            Console.WriteLine($"Zwrócono listę {count} filmów");
            return retVal;
        }

        public override bool Update(object caller, params object[] parameters)
        {
            throw new NotImplementedException();
        }

        public override bool Delete(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Deletes movies with given movieIds
             *  
             *  Parameters:
             *      caller: object that calls function
             *      parameters: array of objects where: [movieId0, movieId1, ... , movieIdn]
             *      
             *  Returns:
             *      true: if successfully deleted at least one movie
             *      false: if not allowed to delete or no movie found
             */
            if (caller is Admin)
            {
                int count = 0;
                foreach (Movie movie in Read(caller, parameters))
                {
                    _content.Remove(movie);
                    count++;
                }

                if (count > 0)
                    return true;
                else
                    return false;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }
        }

        public override bool Deserialize()
        {
            bool success = true;
            try
            {
                base.Deserialize();
                Movie.MovieCount = _content.Count;
            }
            catch(Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}