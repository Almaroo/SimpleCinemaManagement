using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IronPdf;

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

            var Renderer = new HtmlToPdf();

            Renderer.PrintOptions.MarginBottom = 0;
            Renderer.PrintOptions.MarginLeft = 0;
            Renderer.PrintOptions.MarginRight = 0;
            Renderer.PrintOptions.MarginTop = 0;
            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            Renderer.PrintOptions.Title = "Ticket";
            Renderer.PrintOptions.CustomCssUrl = $@"{Directory.GetCurrentDirectory()}\..\..\Resources\style.css";

            string customHTML = $"<html><body>" +
                $@"<img class='icon' src='../../image/camera.svg'>" +
                $@"<h1 class='main-text'>Ticket</h1>" +
                $@"<h3 class='text'>Type: {showing.GetType().Name} {(showing.IsPremiere ? "Premiere" : null)}</h3>" +
                $@"<h3 class='text'>Movie: {showing.Movie.Title}</h3>" +
                $@"<h3 class='text'>Screening Room: {showing.ScreeningRoom.ScreeningRoomId}</h3>" +
                $@"<h3 class='text'>Date: {showing.ShowingDate.ToString("dddd, dd MMMM")}</h3>" +
                $@"<h3 class='text'>Row: {row}, Seat: {seat}</h3>" +
                $@"<h3 class='text'>Price: {(isDiscounted ? showing.Price / 2 : showing.Price):C}</h3>" +
                $@"<h3 class='text'>Discount: {(isDiscounted ? "YES" : "NO")}</h3>" +
                $@"</body></html>";

            var PDF = Renderer.RenderHtmlAsPdf(customHTML);
            PDF.SaveAs("ticket.pdf");

            Console.WriteLine(stringBuilder.ToString());

        }
    }
}
