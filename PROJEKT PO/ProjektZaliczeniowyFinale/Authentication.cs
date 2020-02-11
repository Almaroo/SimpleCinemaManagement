using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ProjektZaliczeniowyFinale
{
    public static class Authentication
    {
        public static byte[] GetPasswordHash(string password)
        {
            using (SHA256 sHA256 = SHA256.Create())
            {
                return sHA256.ComputeHash(Encoding.ASCII.GetBytes(password));
            }
        }

        //prawdopodobnie bardzo mało efektywne ale C# nie ma memcmp
        public static bool CompareHashes(byte[] b1, byte[] b2)
        {
            bool equal = true;

            if (b1.Length == b2.Length)
            {
                for (int i = 0; i < b1.Length; i++)
                {
                    if (b1[i] != b2[i])
                    {
                        equal = false;
                        break;
                    }
                }
            }
            else
                equal = false;

            return equal;
        }
    }
}
