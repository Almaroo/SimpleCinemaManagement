using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    public struct Address
    {
        private string _city;
        private string _street;
        private int _buildingNumber;

        public string City { get => _city; set => _city = value; }
        public string Street { get => _street; set => _street = value; }
        public int BuildingNumber { get => _buildingNumber; set => _buildingNumber = value; }

        public Address(string city, string street, int buildingNumber)
        {
            _city = city;
            _street = street;
            _buildingNumber = buildingNumber;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"{City} {Street} {BuildingNumber}");
            return stringBuilder.ToString();
        }

        public static Address CreateAdress(string city, string street, int buildingNumber)
        {
            return new Address(city, street, buildingNumber);
        }

    }

    public sealed class Cinema
    {
        private Address _adress;
        private string _name;
        private List<ScreeningRoom> _screeningRoomsTemplates;
        private List<Showing> _schedule;
        private string _cinemaId;
        private int _screeningRoomCount = 0;

        private static int _cinemaCount = 0;

        public string CinemaId { get => _cinemaId; set => _cinemaId = value; }
        public string Name { get => _name; set => _name = value; }
        public Address Adress { get => _adress; set => _adress = value; }
        public List<ScreeningRoom> ScreeningRoomsTemplates { get => _screeningRoomsTemplates; set => _screeningRoomsTemplates = value; }
        public List<Showing> Schedule { get => _schedule; set => _schedule = value; }
        public int ScreeningRoomCount { get => _screeningRoomCount; set => _screeningRoomCount = value; }
        public static int CinemaCount { get => _cinemaCount; set => _cinemaCount = value; }

        public Cinema()
        {

        }
        public Cinema(string name, Address adress)
        {
            _schedule = new List<Showing>();
            _screeningRoomsTemplates = new List<ScreeningRoom>();
            _cinemaId = GetCinemaId();
            _name = name;
            _adress = adress;
        }

        private string GetCinemaId()
        {
            return $"C{++_cinemaCount:000}";
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{this.GetType().Name} CinemaID: {_cinemaId} Name: {_name} Address: {_adress}");
            return stringBuilder.ToString();
        }

        public bool AddNewScreningRoomTemplate(object caller, params object[] parameters)
        {
            /*
             *  Summary:
             *      
             */

            if(caller is Admin)
            {
                _screeningRoomsTemplates.Add(new ScreeningRoom((int)parameters[0], (int[])parameters[1], _screeningRoomCount));
                _screeningRoomCount++;
                Console.WriteLine("Dodano salę kinową");
                return true;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }
        }

        public ScreeningRoom GetScreeningRoomTemplate(string screeningRoomId)
        {
            return _screeningRoomsTemplates.Find(x => x.ScreeningRoomId == screeningRoomId);
        }

        public bool AddNewShowing(object caller, params object[] parameters)
        {
            if (caller is Admin)
            {
                if ((bool)parameters[4])
                    _schedule.Add(new Showing3D((DateTime)parameters[0], (Movie)parameters[1], (ScreeningRoom)parameters[2], (bool)parameters[3]));
                else
                    _schedule.Add(new Showing((DateTime)parameters[0], (Movie)parameters[1], (ScreeningRoom)parameters[2], (bool)parameters[3]));
                Console.WriteLine("Dodano seans");
                return true;
            }
            else
            {
                Console.WriteLine("Nie masz wystarczających uprawnień");
                return false;
            }
        }

    }
}
