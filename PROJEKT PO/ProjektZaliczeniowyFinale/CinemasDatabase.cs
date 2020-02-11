using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ProjektZaliczeniowyFinale
{
    public sealed class CinemasDatabase : Database<Cinema>
    {

        private static CinemasDatabase _instance;
        private static object _instanceLock = new object();

        public List<Cinema> Cinemas { get => _content; set => _content = value; }

        public CinemasDatabase() : base()
        {
        }

        public static CinemasDatabase GetInstance
        {
            get
            {
                if (_instance == null)
                   _instance = new CinemasDatabase();
                //_instance.Deserialize();
                return _instance;
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{this.GetType().Name}");
            foreach(Cinema cinema in _content)
            {
                stringBuilder.AppendLine(cinema.ToString());
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
             *      Creates new cinema in database
             *  
             *  Parameters:
             *      caller: object that calls method
             *      parameters: array of exactly 2 objects where: [name, address]
             *      
             *  Returns;
             *      true: if new cinema was created
             *      false: if not allowed to add
            */
            if (caller is Admin && parameters.Length == 2)
            {
                Cinema tmp = new Cinema((string)parameters[0], (Address)parameters[1]);
                Cinemas.Add(tmp);
                Console.WriteLine("Dodano kino");
                return true;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }
        }

        public override List<Cinema> Read(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      Gets cinemas that match given userIds
             *      
             *  Parameters:
             *      caller: object that calls function
             *      parameters: array of n objects where: [cinemaId0, cinemaId1, ... , cinemaIdn]
             *      
             *  Returns:
             *      List<User>: always
             *   
             */
            int count = 0;
            List<Cinema> retVal = new List<Cinema>();
            foreach (string s in (string[])parameters[0])
            {
                foreach (Cinema cinema in _content)
                {
                    if(s != "all")
                    {
                        if (cinema.CinemaId == s)
                        {
                            retVal.Add(cinema);
                            count++;
                        }
                    }
                    else
                    {
                        retVal.Add(cinema);
                        count++;
                    }
                }
            }
            Console.WriteLine($"Zwrócono listę {count} kin");
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
             *      Deletes cinemas with given movieIds
             *  
             *  Parameters:
             *      caller: object that calls function
             *      parameters: array of objects where: [cinemaId0, cinemaId1, ... , cinemaIdn]
             *      
             *  Returns:
             *      true: if successfully deleted at least one cinema
             *      false: if not allowed to delete or no cinema found
             */
            if (caller is Admin)
            {
                int count = 0;
                foreach (Cinema cinema in Read(caller, parameters))
                {
                    _content.Remove(cinema);
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
                Cinema.CinemaCount = _content.Count;
            }
            catch(Exception e)
            {
                success = false;
            }

            return success;
        }
    }
}
