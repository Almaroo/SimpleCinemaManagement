using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    public static class Ticket
    {
        public static void GenerateTicket(Showing showing, bool isDiscounted, int row, int seat)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("=== TICKET ===");
            stringBuilder.AppendLine($"Type: {showing.GetType().Name} {(showing.IsPremiere ? "Premiere" : null)}");
            stringBuilder.AppendLine($@"Date: {showing.ShowingDate.ToString("dddd, dd MMMM yyyy HH:mm:ss")}");
            stringBuilder.AppendLine($"Row: {row}, Seat: {seat}");
            stringBuilder.AppendLine($"Price: {(isDiscounted ? showing.Price/2 : showing.Price):C}");
            stringBuilder.AppendLine($"Discount: {(isDiscounted ? "YES" : "NO")}");

            Console.WriteLine(stringBuilder.ToString());

        }
    }
}
