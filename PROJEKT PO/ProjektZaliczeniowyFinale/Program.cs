using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    class Program
    {
        static void Main(string[] args)
        {
            User currentUser = UsersDatabase.LogIn("MB001", "1234");
            if (currentUser is Admin a)
            {
                a.AddUser("Jan", "Nowak", "4321", false);
                a.AddMovie("Harry Potter i Komnata Tajemnic", 156);
                a.AddCinema("Multikino", Address.CreateAdress("Kraków", "Pilotów", 2));
                a.AddCinemasToUser("JN002", "C001");
                a.AddScreeningRoomToCinema("C001", new int[] { 5, 6, 10, 4 });
                a.AddShowingToCinemaSchedule("C001", "SR001", "M001", DateTime.Now, false, false);
            }        

            currentUser = (User)UsersDatabase.LogIn("JN002", "4321");
            currentUser.ShowManagedCinemas();
            Console.WriteLine(currentUser.GetManagedCinema("C001").Schedule.First().ScreeningRoom.ToString());


            Console.ReadKey();
        }
    }
}
