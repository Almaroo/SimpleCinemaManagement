using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ProjektZaliczeniowyFinale
{
    [Serializable]
    public sealed class ScreeningRoom : ICloneable
    {
        public enum Seat : byte
        {
            Zajete = 0x00,
            Wolne = 0x01
        };

        private Seat[][] seats;
        private string _screeningRoomId;

        public string ScreeningRoomId { get => _screeningRoomId; set => _screeningRoomId = value; }
        public Seat[][] Seats { get => seats; set => seats = value; }

        public ScreeningRoom()
        {

        }
        public ScreeningRoom(int rowsNumber, int[] rowsLengths, int screeningRoomCount)
        {
            InitializeArray(rowsNumber, rowsLengths);
            _screeningRoomId = getScreeningRoomId(screeningRoomCount);
        }

        private string getScreeningRoomId(int screeningRoomCount)
        {
            return $"SR{++screeningRoomCount:000}";
        }

        private void InitializeArray(int rowsNumber, int[] rowsLengths)
        {
            seats = new Seat[rowsNumber][];
            for (int i = 0; i < rowsNumber; i++)
            {
                seats[i] = new Seat[rowsLengths[i]];
            }

            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[i].Length; j++)
                {
                    seats[i][j] = Seat.Wolne;
                }
            }
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"---{_screeningRoomId}---");
            stringBuilder.AppendLine("===   ===");
            for (int i = 0; i < seats.Length; i++)
            {
                for (int j = 0; j < seats[i].Length; j++)
                {
                    if(seats[i][j] == Seat.Wolne)
                    {
                        stringBuilder.Append($" O ");
                    }   
                    else
                    {
                        stringBuilder.Append($" X ");
                    }
                }
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public bool isSeatFree(int row, int seat)
        {

            bool retVal = true;
            try
            {
                if (seats[row - 1][seat - 1] == Seat.Wolne)
                    retVal = true;
                else
                    retVal = false;
            }
            catch(IndexOutOfRangeException e)
            {
                Console.WriteLine($"W rzędzie {row} nie ma miejsca o numerze {seat}");
                retVal = false;
            }

            return retVal;
        }

        public object Clone()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, this);
                memoryStream.Position = 0;
                return binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}
